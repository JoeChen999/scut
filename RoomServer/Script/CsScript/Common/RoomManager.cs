using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Common.Timing;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public class RoomManager
    {
        private static Dictionary<int, RoomUpdater> s_RoomUpdaters = new Dictionary<int, RoomUpdater>();
        private static Dictionary<int, RoomManager> s_RoomManagers = new Dictionary<int, RoomManager>();
        private static object s_CreateRoomLock = new object();
        private static int s_CurrentRoomId = 1;

        private RoomUpdater m_RoomUpdater;
        private Room m_Room;

        public Room RoomInfo
        {
            get { return m_Room; }
        }

        private RoomManager()
        {
            
        }

        public static RoomManager GetInstance(LRCreateNewRoom roomData)
        {
            RoomManager rm = new RoomManager();
            var room = rm.CreateRoom(roomData);
            s_RoomManagers.Add(room.Id, rm);
            return rm;
        }

        public static RoomManager GetInstance(int roomId)
        {
            if (s_RoomManagers.ContainsKey(roomId))
            {
                return s_RoomManagers[roomId];
            }
            return null;
        }

        private Room CreateRoom(LRCreateNewRoom roomData)
        {
            lock (s_CreateRoomLock)
            {
                m_Room = new Room();
                m_Room.Id = s_CurrentRoomId;
                CacheSet.RoomCache.TryAdd(s_CurrentRoomId.ToString(), m_Room);
                s_CurrentRoomId++;
            }
            InitRoom(roomData);
            m_RoomUpdater = new RoomUpdater(m_Room);
            s_RoomUpdaters.Add(m_Room.Id, m_RoomUpdater);
            m_RoomUpdater.Start();
            return m_Room;
        }

        public void CloseRoom()
        {
            s_RoomUpdaters.Remove(m_Room.Id);
            s_RoomManagers.Remove(m_Room.Id);
            CacheSet.RoomCache.TryRemove(m_Room.Id.ToString());
            m_RoomUpdater.Stop();
        }

        public void PushIntoMessageQueue(int actionId, GameSession session, PacketBase packet)
        {
            Message m = new Message()
            {
                Type = (ActionType)actionId,
                Session = session,
                Packet = packet
            };
            m_RoomUpdater.MessageQueue.Add(m);
        }

        public void PlayerGaveUp(int userId)
        {
//             if(m_Room.State != RoomState.Running)
//             {
//                 return;
//             }
            int camp = m_Room.Players[userId].Camp;
            foreach (var player in m_Room.Players)
            {
                player.Value.State = player.Value.Camp == camp ? RoomPlayerState.Failed : RoomPlayerState.Winned;
            }
            m_Room.EndReason = InstanceSuccessReason.AbandonedByOpponent;
            m_Room.State = RoomState.Finish;
        }

        private void InitRoom(LRCreateNewRoom roomData)
        {
            m_Room.State = RoomState.WaitingConnect;
            m_Room.EndReason = InstanceSuccessReason.Unknown;
            m_Room.CreateTime = DateTime.UtcNow.Ticks;
            m_Room.Token = roomData.Token;
            var allInstance = CacheSet.SinglePvpInstanceTable.GetAllData();
            m_Room.InstanceId = new Random().Next(1, allInstance.Count);
            DTSinglePvpInstance instanceData = CacheSet.SinglePvpInstanceTable.GetData(m_Room.InstanceId);
            m_Room.RemainingTime = instanceData.TimerDuration + 5;
            int curEntityId = 1;
            int playerIndex = 0;
            foreach (var playerData in roomData.RoomPlayerInfo)
            {
                RoomPlayer player = new RoomPlayer()
                {
                    PlayerId = playerData.PlayerInfo.Id,
                    Name = playerData.PlayerInfo.Name,
                    Level = playerData.PlayerInfo.Level,
                    VipLevel = playerData.PlayerInfo.VipLevel,
                    PortraitType = playerData.PlayerInfo.PortraitType,
                    State = RoomPlayerState.WaitingConnect,
                    PositionX = playerIndex < roomData.RoomPlayerInfo.Count / 2 ? instanceData.SpawnPointX : instanceData.SpawnPointX2,
                    PositionY = playerIndex < roomData.RoomPlayerInfo.Count / 2 ? instanceData.SpawnPointY : instanceData.SpawnPointY2,
                    Rotation = playerIndex < roomData.RoomPlayerInfo.Count / 2 ? instanceData.SpawnAngle : instanceData.SpawnAngle2,
                    InBattleEntity = curEntityId,
                    Camp = playerIndex < roomData.RoomPlayerInfo.Count / 2 ? (int)CampType.Player : (int)CampType.Player2,
                    ServerId = playerData.LobbyServerId,
                };
                foreach (var heroData in playerData.RoomHeroInfo)
                {
                    RoomHero hero = new RoomHero()
                    {
                        EntityId = curEntityId++,
                        HeroType = heroData.LobbyHeroInfo.Type,
                        HeroLv = heroData.LobbyHeroInfo.Level,
                        HeroStarLevel = heroData.LobbyHeroInfo.StarLevel,
                        ElevationLevel = heroData.LobbyHeroInfo.ElevationLevel,
                        ConsciousnessLevel = heroData.LobbyHeroInfo.ConsciousnessLevel,
                    };
                    hero.Skills.AddRange(heroData.LobbyHeroInfo.SkillLevels);
                    foreach (var gear in heroData.LobbyHeroInfo.GearInfo)
                    {
                        GearData heroGear = new GearData()
                        {
                            Id = gear.Id,
                            Type = gear.Type,
                            Level = gear.Level,
                            StrengthenLevel = gear.StrengthenLevel
                        };
                        hero.Gears.Add(heroGear);
                    }
                    foreach (var soul in heroData.LobbyHeroInfo.SoulInfo)
                    {
                        hero.Souls.Add(new SoulData()
                        {
                            Id = soul.Id,
                            TypeId = soul.Type,
                        });
                    }
                    hero.HP = hero.MaxHP;
                    hero.SteadyBarValue = hero.SteadyBarValue;
                    hero.HasSteadyBar = true;
                    hero.LastBreakSteadyTime = DateTime.UtcNow.Ticks;
                    player.Heros.Add(hero);
                }
                m_Room.Players.Add(player.PlayerId, player);
                playerIndex++;
            }
        }
    }
}
