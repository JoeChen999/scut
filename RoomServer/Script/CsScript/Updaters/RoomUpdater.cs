using System;
using System.Collections.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Common.Timing;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public class RoomUpdater
    {
        private SyncTimer m_SyncTimer;
        private Room m_Room;
        private long m_LastUpdateTime;
        private Dictionary<ActionType, BaseActionProcessor> m_ActionProcessors;

        public List<Message> MessageQueue
        {
            get;
            set;
        }

        public RoomUpdater(Room roomData)
        {
            m_SyncTimer = new SyncTimer(OnUpdate, 0, 100);
            m_Room = roomData;
            MessageQueue = new List<Message>();
            m_ActionProcessors = new Dictionary<ActionType, BaseActionProcessor>()
            {
                {ActionType.GetRoomInfo, new GetRoomInfoProcessor(roomData)},
                {ActionType.CREntityMove, new EntityMoveProcessor(roomData)},
                {ActionType.CREntityPerformSkillStart, new EntityPerformSkillStartProcessor(roomData)},
                {ActionType.CREntityPerformSkillEnd, new EntityPerformSkillEndProcessor(roomData)},
                {ActionType.CREntityImpact, new EntityPerformSkillImpactProcessor(roomData)},
                {ActionType.GiveUpBattle, new PlayerGiveUpProcessor(roomData)},
                {ActionType.EntitySwitchHero, new EntitySwitchHeroProcessor(roomData)},
                {ActionType.CREntityPerformSkillFF, new EntityPerformSkillFFProcessor(roomData)},
                {ActionType.CREntitySKillRushing, new EntitySkillRushingProcessor(roomData) },
                {ActionType.CREntityAddBuff, new EntityAddBuffProcessor(roomData)},
                {ActionType.CREntityRemoveBuff, new EntityRemoveBuffProcessor(roomData)},
                {ActionType.CRRoomReady, new RoomReadyProcessor(roomData)},
            };
        }

        public void Start()
        {
            m_SyncTimer.Start();
            m_LastUpdateTime = DateTime.UtcNow.Ticks;
        }

        public void Stop()
        {
            m_SyncTimer.Stop();
        }

        private void OnUpdate(object state)
        {
            switch (m_Room.State)
            {
                case RoomState.WaitingConnect:
                    ProcessMessages();
                    UpdateWaiting();
                    break;
                case RoomState.Running:
                    ProcessMessages();
                    UpdateRunning();
                    break;
                case RoomState.Finish:
                    UpdateFinish();
                    break;
                default:
                    break;
            }
        }

        public void UpdateWaiting()
        {
            List<int> gaveUpPlayers = new List<int>();
            if (DateTime.UtcNow.Ticks - m_Room.CreateTime > Constants.WaitingConnectTimeLimit * TimeSpan.TicksPerSecond)
            {
                foreach (var player in m_Room.Players)
                {
                    if (player.Value.State != RoomPlayerState.Playing)
                    {
                        gaveUpPlayers.Add(player.Key);
                    }
                }
                if (gaveUpPlayers.Count == m_Room.Players.Count)
                {
                    RoomManager.GetInstance(m_Room.Id).CloseRoom();
                    return;
                }
                else if(gaveUpPlayers.Count > 0)
                {
                    m_Room.StartTime = DateTime.UtcNow.Ticks;
                    foreach (var rp in m_Room.Players)
                    {
                        RCPushRoomReady packet = new RCPushRoomReady()
                        {
                            StartTime = m_Room.StartTime
                        };
                        byte[] buffer = CustomActionDispatcher.GeneratePackageStream(4002, ProtoBufUtils.Serialize(packet));
                        GameSession.Get(rp.Value.PlayerId).SendAsync(buffer, 0, buffer.Length);
                    }
                    RoomManager.GetInstance(m_Room.Id).PlayerGaveUp(gaveUpPlayers[0]);
                    return;
                }
            }

            bool allReady = true;
            foreach (var player in m_Room.Players)
            {
                if (player.Value.State != RoomPlayerState.Playing)
                {
                    allReady = false;
                }
            }
            if (allReady)
            {
                m_Room.StartTime = DateTime.UtcNow.Ticks;
                m_Room.State = RoomState.Running;
                foreach (var rp in m_Room.Players)
                {
                    RCPushRoomReady packet = new RCPushRoomReady()
                    {
                        StartTime = m_Room.StartTime
                    };
                    byte[] buffer = CustomActionDispatcher.GeneratePackageStream(4002, ProtoBufUtils.Serialize(packet));
                    GameSession.Get(rp.Value.PlayerId).SendAsync(buffer, 0, buffer.Length);
                }
            }
        }

        public void UpdateRunning()
        {
            if (DateTime.UtcNow.Ticks - m_Room.StartTime > m_Room.RemainingTime * TimeSpan.TicksPerSecond)
            {
                SinglePvpTimeOut();
            }

            foreach (var player in m_Room.Players)
            {
                bool isDead = true;
                foreach (var Hero in player.Value.Heros)
                {
                    if (Hero.HP > 0)
                    {
                        isDead = false;
                    }
                }
                if (isDead)
                {
                    player.Value.State = RoomPlayerState.Failed;
                    foreach (var rp in m_Room.Players)
                    {
                        if (rp.Value.Camp != player.Value.Camp)
                        {
                            rp.Value.State = RoomPlayerState.Winned;
                        }
                    }
                    m_Room.EndReason = InstanceSuccessReason.HasBeatenOpponent;
                    m_Room.State = RoomState.Finish;
                }
            }
        }

        public void UpdateFinish()
        {
            if (DateTime.UtcNow.Ticks - m_Room.StartTime > Constants.RoomExpireTime * TimeSpan.TicksPerSecond)
            {
                RWPvpResult result = new RWPvpResult();
                foreach (var player in m_Room.Players)
                {
                    result.PlayerIds.Add(player.Value.PlayerId);
                    result.ServerId.Add(player.Value.ServerId);
                    result.Result.Add((int)player.Value.State);
                    var session = GameSession.Get(player.Key);
                    if (session != null)
                    {
                        session.Close();
                    }
                }
                if (result.PlayerIds.Count > 0)
                {
                    LobbyServerSender.Send("RWPvpResultHandler", result, callback => { });
                }
                RoomManager.GetInstance(m_Room.Id).CloseRoom();
                return;
            }
            RWPvpResult resultPacket = new RWPvpResult();
            foreach (var player in m_Room.Players)
            {
                if (player.Value.State == RoomPlayerState.Winned || player.Value.State == RoomPlayerState.Failed || player.Value.State == RoomPlayerState.Draw)
                {
                    resultPacket.PlayerIds.Add(player.Value.PlayerId);
                    resultPacket.ServerId.Add(player.Value.ServerId);
                    resultPacket.Result.Add((int)player.Value.State);
                    var session = GameSession.Get(player.Value.PlayerId);
                    if (session != null)
                    {
                        RCPushBattleResult packet = new RCPushBattleResult()
                        {
                            Result = (int)player.Value.State,
                            Reason = (int)m_Room.EndReason,
                        };
                        byte[] buffer = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCPushBattleResult, ProtoBufUtils.Serialize(packet));
                        session.SendAsync(buffer, 0, buffer.Length);
                    }
                    player.Value.State = RoomPlayerState.Exited;
                }
                else
                {
                    continue;
                }
            }
            if (resultPacket.PlayerIds.Count > 1)
            {
                TraceLog.WriteInfo("Player1:{0},win=>{1}\nPlayer2：{2},win=>{3}", resultPacket.PlayerIds[0], resultPacket.Result[0], resultPacket.PlayerIds[1], resultPacket.Result[1]);
                LobbyServerSender.Send("RWPvpResultHandler", resultPacket, callback => { });
            }

            bool allExited = true;
            foreach (var player in m_Room.Players)
            {
                if (player.Value.State != RoomPlayerState.Exited)
                {
                    allExited = false;
                }
            }
            if (allExited)
            {
                RoomManager.GetInstance(m_Room.Id).CloseRoom();
                return;
            }
        }

        private void ProcessMessages()
        {
            int i = 0;
            for (; i < Constants.MaxProcessCountPerUpdate; i++)
            {
                if (i >= MessageQueue.Count)
                {
                    break;
                }
                ProcessMessage(MessageQueue[i]);
            }
            MessageQueue.RemoveRange(0, i);
        }

        private void ProcessMessage(Message m)
        {
            m_ActionProcessors[m.Type].GetData(m);
            if (m_ActionProcessors[m.Type].Verify(m))
            {
                m_ActionProcessors[m.Type].Process();
            }
            m_ActionProcessors[m.Type].PushResult();
        }

        private void SinglePvpTimeOut()
        {
            int camp1RemainingHeroCount = 0;
            int camp2RemainingHeroCount = 0;
            foreach (var player in m_Room.Players)
            {
                if (player.Value.Camp == (int)CampType.Player)
                {
                    foreach (var hero in player.Value.Heros)
                    {
                        camp1RemainingHeroCount += hero.HP > 0 ? 1 : 0;
                    }
                }
                if (player.Value.Camp == (int)CampType.Player2)
                {
                    foreach (var hero in player.Value.Heros)
                    {
                        camp2RemainingHeroCount += hero.HP > 0 ? 1 : 0;
                    }
                }
            }
            if (camp1RemainingHeroCount != camp2RemainingHeroCount)
            {
                foreach (var player in m_Room.Players)
                {
                    if (player.Value.Camp == (int)CampType.Player)
                    {
                        player.Value.State = camp1RemainingHeroCount > camp2RemainingHeroCount ? RoomPlayerState.Winned : RoomPlayerState.Failed;
                    }
                    else
                    {
                        player.Value.State = camp1RemainingHeroCount > camp2RemainingHeroCount ? RoomPlayerState.Failed : RoomPlayerState.Winned;
                    }
                }
            }
            else
            {
                foreach (var player in m_Room.Players)
                {
                    player.Value.State = RoomPlayerState.Draw;
                }
            }
            m_Room.EndReason = InstanceSuccessReason.TimeOut;
            m_Room.State = RoomState.Finish;
        }
    }
}
