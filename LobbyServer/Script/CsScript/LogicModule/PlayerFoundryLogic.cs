using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Common.Log;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerFoundryLogic
    {
        private int m_UserId;
        private PlayerFoundry m_Foundry;

        public static readonly TimeSpan CoolDownTime = new TimeSpan(GameConfigs.GetInt("Foundry_Cool_Down_Seconds",600) * GameConsts.TicksPerSecond);
        public PlayerFoundryLogic()
        {

        }

        public PlayerFoundry MyFoundry
        {
            get { return m_Foundry; }
        }

        public void SetUser(int userId)
        {
            m_UserId = userId;
            m_Foundry = CacheSet.PlayerFoundryCache.FindKey(userId.ToString(), userId);
            if (m_Foundry == null)
            {
                m_Foundry = new PlayerFoundry();
                m_Foundry.UserId = userId;
                m_Foundry.CurrentRoomId = -1;
                m_Foundry.FoundryCount = 0;
                m_Foundry.NextFoundryTime = DateTime.UtcNow.Ticks;
                m_Foundry.CanReceiveRewards.AddRange(new bool[] { false, false, false });
                m_Foundry.HasReceivedRewards.AddRange(new bool[] { false, false, false });
                CacheSet.PlayerFoundryCache.Add(m_Foundry);
            }
        }

        public void ResetUser()
        {
            m_Foundry.FoundryCount = 0;
            m_Foundry.NextFoundryTime = DateTime.UtcNow.Ticks;
            m_Foundry.CanReceiveRewards.Clear();
            m_Foundry.CanReceiveRewards.AddRange(new bool[] { false, false, false });
            m_Foundry.HasReceivedRewards.Clear();
            m_Foundry.HasReceivedRewards.AddRange(new bool[] { false, false, false });
        }

        public FoundryRoom GetRoomInfo()
        {
            int roomId = m_Foundry.CurrentRoomId;
            if (roomId == -1)
            {
                return null;
            }
            var roomInfo = CacheSet.FoundryRoomCache.FindKey(roomId);
            if (roomInfo == null)
            {
                m_Foundry.CurrentRoomId = -1;
            }
            return roomInfo;
        }

        public PlayerFoundry GetPlayerFoundryInfo()
        {
            return m_Foundry;
        }

        public PBGearFoundryInfo GetAllFoundryData()
        {
            PBGearFoundryInfo retData = new PBGearFoundryInfo()
            {
                TeamId = m_Foundry.CurrentRoomId,
                NextFoundryTimeInTicks = m_Foundry.NextFoundryTime,
            };
            for (int i = 0; i < GameConfigs.GetInt("Gear_Foundry_Level_Count", 3); i++)
            {
                retData.RewardFlags.Add(m_Foundry.CanReceiveRewards[i]);
            }
            var roomInfo = GetRoomInfo();
            if (roomInfo != null)
            {
                PlayerLogic p = new PlayerLogic();
                foreach (int playerId in roomInfo.Players)
                {
                    p.SetUser(playerId);
                    SetUser(playerId);
                    retData.Players.Add(new PBGearFoundryPlayerInfo()
                    {
                        Player = new PBPlayerInfo()
                        {
                            Id = playerId,
                            Name = p.MyPlayer.Name,
                            PortraitType = p.MyPlayer.PortraitType
                        },
                        FoundryCount = m_Foundry.FoundryCount,
                    });
                }
                retData.Progress = new PBGearFoundryProgressInfo()
                {
                    CurrentLevel = roomInfo.Level,
                    CurrentProgress = roomInfo.Progress,
                };
            }
            return retData;
        }

        public PBGearFoundryInfo MatchRoom(int level)
        {
            TraceLog.WriteInfo("Room count when match:{0}", CacheSet.FoundryRoomCache.Count);
            List<FoundryRoom> roomList;
            if (level < 0)
            {
                roomList = CacheSet.FoundryRoomCache.FindAll(delegate (FoundryRoom r) {
                    return r != null && r.Players.Count < GameConsts.Foundry.MaxFoundryPlayerCount && (r.Level != GameConsts.Foundry.MaxFoundryLevel && r.Progress != GameConfigs.GetInt("Gear_Foundry_Progress_Count_" + GameConsts.Foundry.MaxFoundryLevel, 5));
                });
            }
            else
            {
                roomList = CacheSet.FoundryRoomCache.FindAll(delegate (FoundryRoom r) {
                    return r != null && r.Players.Count < GameConsts.Foundry.MaxFoundryPlayerCount && r.Level == level && (r.Level != GameConsts.Foundry.MaxFoundryLevel && r.Progress != GameConfigs.GetInt("Gear_Foundry_Progress_Count_" + GameConsts.Foundry.MaxFoundryLevel, 5));
                });
            }
            if (roomList == null || roomList.Count == 0)
            {
                return null;
            }
            roomList.Sort(delegate(FoundryRoom a, FoundryRoom b) { return b.Players.Count.CompareTo(a.Players.Count) * 1000 + b.Progress.CompareTo(a.Progress); });
            foreach (var room in roomList)
            {
                if (JoinRoom(room.Id))
                {
                    return GetAllFoundryData();
                }
            }
            return null;
        }

        public bool JoinRoom(int roomId)
        {
            var roomInfo = CacheSet.FoundryRoomCache.FindKey(roomId);
            roomInfo.ModifyLocked(() =>
            {
                roomInfo.Players.Add(m_UserId);
                if (roomInfo.Players.Count > GameConsts.Foundry.MaxFoundryPlayerCount)
                {
                    roomInfo.Players.Remove(m_UserId);
                }
            });
            if (!roomInfo.Players.Contains(m_UserId))
            {
                return false;
            }
            m_Foundry.CurrentRoomId = roomId;
            return true;
        }

        public bool QuitRoom()
        {
            int roomId = m_Foundry.CurrentRoomId;
            var roomInfo = CacheSet.FoundryRoomCache.FindKey(roomId);
            if (roomInfo == null)
            {
                return false;
            }
            roomInfo.ModifyLocked(() => { roomInfo.Players.Remove(m_UserId); });
            if(roomInfo.Players.Count <= 0)
            {
                CacheSet.FoundryRoomCache.Delete(roomInfo);
                TraceLog.WriteInfo("Room count when quit:{0}", CacheSet.FoundryRoomCache.Count);
            }
            m_Foundry.CurrentRoomId = -1;
            m_Foundry.FoundryCount = 0;
            return true;
        }

        public bool Foundry()
        {
            int userId = m_UserId;
            if (m_Foundry.NextFoundryTime > DateTime.UtcNow.Ticks)
            {
                return false;
            }
            int roomId = m_Foundry.CurrentRoomId;
            var roomInfo = CacheSet.FoundryRoomCache.FindKey(roomId);
            if (roomInfo == null)
            {
                return false;
            }
            if (roomInfo.Level >= GameConfigs.GetInt("Gear_Foundry_Level_Count", 3) && roomInfo.Progress >= GameConfigs.GetInt("Gear_Foundry_Progress_Count_2", 5))
            {
                return false;
            }
            m_Foundry.FoundryCount += 1;
            m_Foundry.NextFoundryTime = DateTime.UtcNow.Add(CoolDownTime).Ticks;
            roomInfo.ModifyLocked(() =>
            {
                roomInfo.Progress += 1;
                if (roomInfo.Progress >= GameConfigs.GetInt("Gear_Foundry_Progress_Count_" + roomInfo.Level, 5) && roomInfo.Level < GameConfigs.GetInt("Gear_Foundry_Level_Count", 3))
                {
                    foreach (int playerId in roomInfo.Players)
                    {
                        SetUser(playerId);
                        if (!m_Foundry.HasReceivedRewards[roomInfo.Level])
                        {
                            m_Foundry.CanReceiveRewards[roomInfo.Level] = true;
                        }
                    }
                    roomInfo.Level += 1;
                    roomInfo.Progress = 0;
                }
            });
            SetUser(userId);
            return true;
        }

        public bool GetReward(int level)
        {
            if (!m_Foundry.CanReceiveRewards[level] || m_Foundry.HasReceivedRewards[level])
            {
                return false;
            }
            m_Foundry.HasReceivedRewards[level] = true;
            m_Foundry.CanReceiveRewards[level] = false;
            PlayerDailyQuestLogic.GetInstance(m_UserId).UpdateDailyQuest(DailyQuestType.ClaimGearFoundryReward, 1);
            return true;
        }

        public bool CreateRoom()
        {
            if (m_Foundry.CurrentRoomId >= 0)
            {
                return false;
            }
            FoundryRoom room = new FoundryRoom();
            room.Id = (int)CacheSet.FoundryRoomCache.GetNextNo();
            room.Level = 0;
            room.Progress = 0;
            room.Players.Add(m_UserId);
            CacheSet.FoundryRoomCache.Add(room);
            m_Foundry.CurrentRoomId = room.Id;
            return true;
        }

        public void PushRoomPlayerChangedNotify(int roomId = 0, bool needPushToMe = true)
        {
            FoundryRoom roomInfo;
            if (roomId == 0)
            {
                roomInfo = CacheSet.FoundryRoomCache.FindKey(m_Foundry.CurrentRoomId);
            }
            else
            {
                roomInfo = CacheSet.FoundryRoomCache.FindKey(roomId);
            }
            if (roomInfo == null)
            {
                return;
            }
            LCPlayerListInGearFoundryTeam package = new LCPlayerListInGearFoundryTeam();
            List<GameSession> targets = new List<GameSession>();
            PlayerLogic p = new PlayerLogic();
            int userId = m_UserId;
            foreach (int playerId in roomInfo.Players)
            {
                var target = GameSession.Get(playerId);
                if (target != null)
                {
                    targets.Add(target);
                }
                var gearFoundryPlayerInfo = new PBGearFoundryPlayerInfo();
                p.SetUser(playerId);
                gearFoundryPlayerInfo.Player = new PBPlayerInfo()
                {
                    Id = playerId,
                    Name = p.MyPlayer.Name,
                    PortraitType = p.MyPlayer.PortraitType,
                    Level = p.MyPlayer.Level
                };
                SetUser(playerId);
                gearFoundryPlayerInfo.FoundryCount = m_Foundry.FoundryCount;
                package.Players.Add(gearFoundryPlayerInfo);
            }
            SetUser(userId);
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream(2105, ProtoBufUtils.Serialize(package));
            foreach (var target in targets)
            {
                if (!needPushToMe && target.UserId == m_UserId)
                {
                    continue;
                }
                target.SendAsync(buffer, 0, buffer.Length);
            }
        }

        public void PushRoomProgressChangedNotify()
        {
            var roomInfo = CacheSet.FoundryRoomCache.FindKey(m_Foundry.CurrentRoomId);
            LCPerformFoundry package = new LCPerformFoundry();
            int userId = m_UserId;
            foreach (int playerId in roomInfo.Players)
            {
                var target = GameSession.Get(playerId);
                if (target != null && playerId != userId)
                {
                    SetUser(playerId);
                    package.RewardFlags.AddRange(m_Foundry.CanReceiveRewards);
                    package.PerformerPlayerId = userId;
                    var room = CacheSet.FoundryRoomCache.FindKey(m_Foundry.CurrentRoomId);
                    package.Progress = new PBGearFoundryProgressInfo()
                    {
                        CurrentLevel = room.Level,
                        CurrentProgress = room.Progress
                    };
                    byte[] buffer = CustomActionDispatcher.GeneratePackageStream(2108, ProtoBufUtils.Serialize(package));
                    target.SendAsync(buffer, 0, buffer.Length);
                }
            }
            SetUser(userId);
        }

        public int GetProgress()
        {
            foreach(bool has in m_Foundry.HasReceivedRewards)
            {
                if (!has)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
