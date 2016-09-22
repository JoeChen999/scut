using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private static Dictionary<int, PlayerAchievementLogic> m_Instances = new Dictionary<int, PlayerAchievementLogic>();

        private int m_UserId;
        private PlayerAchievement m_AchievementInfo;
        private Dictionary<int, Action<TrackingAchievement>> m_AchievementIniters; 
        private Dictionary<int, Action<object[]>> m_AchievementUpdaters;

        private PlayerAchievementLogic(int userId)
        {
            m_UserId = userId;
            m_AchievementIniters = new Dictionary<int, Action<TrackingAchievement>>()
            {
                {(int)AchievementType.PlayerLevel, InitPlayerLevelAchievementProgress},
                {(int)AchievementType.HeroLevel, InitHeroLevelAchievementProgress},
                {(int)AchievementType.HeroCount, InitHeroCountProgress},
                {(int)AchievementType.HeroStarLevel, InitHeroStarLevelProgress},
                {(int)AchievementType.HeroMight, InitHeroMightAchievementProgress},
                {(int)AchievementType.HeroElevationLevel, InitHeroElevationLevelAchievementProgress},
                {(int)AchievementType.HeroConsiousnessLevel, InitHeroConsciousnessLevelAchievementProgress},
                {(int)AchievementType.GearQuality, InitGearQualityAchievementProgress},
                {(int)AchievementType.GearStrengthenLevel, InitGearStrengthenLevelAchievementProgress},
                {(int)AchievementType.GearLevel, InitGearLevelAchievementProgress},
                {(int)AchievementType.FriendCount, InitFriendCountAchievementProgress},
                {(int)AchievementType.PvpWinCount, InitPvpWinCountAchievementProgress},
                {(int)AchievementType.InstanceCompletedCount, InitInstanceCompletedCountAchievementProgress},
                {(int)AchievementType.OpenedMoneyChanceCount, InitOpenedMoneyChanceCountAchievementProgress},
                {(int)AchievementType.CostedCoin, InitCostedCoinAchievementProgress},
                {(int)AchievementType.CostedMoney, InitCostedMoneyAchievementProgress},
            };
            m_AchievementUpdaters = new Dictionary<int, Action<object[]>>()
            {
                {(int)AchievementType.PlayerLevel, UpdatePlayerLevelAchievement},
                {(int)AchievementType.HeroLevel, UpdateHeroLevelAchievement},
                {(int)AchievementType.HeroCount, UpdateHeroCountAchievement},
                {(int)AchievementType.HeroStarLevel, UpdateHeroStarLevelAchievement},
                {(int)AchievementType.HeroMight, UpdateHeroMightAchievement},
                {(int)AchievementType.HeroElevationLevel, UpdateHeroElevationLevelAchievement},
                {(int)AchievementType.HeroConsiousnessLevel, UpdateHeroConsciousnessLevelAchievement},
                {(int)AchievementType.GearQuality, UpdateGearQualityAchievement},
                {(int)AchievementType.GearStrengthenLevel, UpdateGearStrengthenLevelAchievement},
                {(int)AchievementType.GearLevel, UpdateGearLevelAchievement },
                {(int)AchievementType.FriendCount, UpdateFriendCountAchievement},
                {(int)AchievementType.PvpWinCount, UpdatePvpWinCountAchievement},
                {(int)AchievementType.InstanceCompletedCount, UpdateInstanceCompletedCountAchievement},
                {(int)AchievementType.OpenedMoneyChanceCount, UpdateOpenedMoneyChanceCountAchievement},
                {(int)AchievementType.CostedCoin, UpdateCostedCoinAchievement},
                {(int)AchievementType.CostedMoney, UpdateCostedMoneyAchievement},
            };
            m_AchievementInfo = CacheSet.PlayerAchievementCache.FindKey(userId.ToString());
            if (m_AchievementInfo == null)
            {
                m_AchievementInfo = new PlayerAchievement();
                m_AchievementInfo.UserId = userId;
                InitAchievementInfo();
                CacheSet.PlayerAchievementCache.Add(m_AchievementInfo);
            }
        }

        public static PlayerAchievementLogic GetInstance(int userId)
        {
            if(m_Instances.ContainsKey(userId) && m_Instances[userId] != null)
            {
                return m_Instances[userId];
            }
            PlayerAchievementLogic pa = new PlayerAchievementLogic(userId);
            m_Instances.Add(userId, pa);
            return pa;
        }

        public CacheDictionary<int, TrackingAchievement> TrackingAchievements
        {
            get
            {
                return m_AchievementInfo.TrackingAchievements;
            }
        }

        public CacheList<int> CompletedAchievements
        {
            get
            {
                return m_AchievementInfo.CompletedAchievements;
            }
        }

        public CacheDictionary<int, int> CompleteAchievement(int id, out TrackingAchievement newAchievement)
        {
            newAchievement = null;
            var aData = CacheSet.AchievementTable.GetData(id);
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            if (!pp.CheckPackageSlot(aData.RewardItems))
            {
                return null;
            }
            if (aData == null)
            {
                TraceLog.WriteError("DataTable Error: can not find Id:"+id.ToString());
                return null;
            }
            if (!m_AchievementInfo.TrackingAchievements.ContainsKey(aData.AchievementType))
            {
                TraceLog.WriteError("You don not have this achievement, Id:" + id.ToString());
                return null;
            }
            var completedAchievement = m_AchievementInfo.TrackingAchievements[aData.AchievementType];
            if(completedAchievement.Id != id)
            {
                TraceLog.WriteError("You don not have this achievement, Id:" + id.ToString());
                return null;
            }
            if(completedAchievement.Progress < completedAchievement.RequiredProgress)
            {
                TraceLog.WriteError("You have not completed this achievement, Id:" + id.ToString());
                return null;
            }
            m_AchievementInfo.CompletedAchievements.Add(id);
            var nextAchievementData = CacheSet.AchievementTable.GetData(a => a.PreAchievementId == id);
            m_AchievementInfo.TrackingAchievements.Remove(aData.AchievementType);
            if(nextAchievementData != null)
            {
                TrackingAchievement ta = new TrackingAchievement()
                {
                    Id = nextAchievementData.Id,
                    RequiredProgress = nextAchievementData.TargetProgressCount,
                };
                ta.Params.AddRange(nextAchievementData.Params);
                m_AchievementIniters[nextAchievementData.AchievementType](ta);
                newAchievement = ta;
                m_AchievementInfo.TrackingAchievements.Add(nextAchievementData.AchievementType, ta);
            }
            return aData.RewardItems;
        }

        public void OpenNewAchievement(int newLevel)
        {
            var initialAchievements = CacheSet.AchievementTable.GetAllData(a => a.PrePlayerLevel <= newLevel);
            var addedAchievement = new List<TrackingAchievement>();
            foreach(var achievement in initialAchievements)
            {
                if (m_AchievementInfo.TrackingAchievements.ContainsKey(achievement.AchievementType) || m_AchievementInfo.CompletedAchievements.Contains(achievement.Id))
                {
                    continue;
                }
                TrackingAchievement ta = new TrackingAchievement()
                {
                    Id = achievement.Id,
                    RequiredProgress = achievement.TargetProgressCount,
                };
                ta.Params.AddRange(achievement.Params);
                m_AchievementIniters[achievement.AchievementType](ta);
                m_AchievementInfo.TrackingAchievements.Add(achievement.AchievementType, ta);
                addedAchievement.Add(ta);
            }
            PushProgressModified(addedAchievement);
        }

        public void UpdateAchievement(AchievementType type, params object[] param)
        {
            if (m_AchievementInfo.TrackingAchievements.ContainsKey((int)type) && m_AchievementInfo.TrackingAchievements[(int)type] != null)
            {
                m_AchievementUpdaters[(int)type](param);
            }
        }

        private void InitAchievementInfo()
        {
            PlayerLogic player = new PlayerLogic();
            player.SetUser(m_UserId);

            var initialAchievements = CacheSet.AchievementTable.GetAllData(a => a.PreAchievementId == -1 && a.PrePlayerLevel <= player.MyPlayer.Level);
            foreach (var achievement in initialAchievements)
            {
                TrackingAchievement ta = new TrackingAchievement()
                {
                    Id = achievement.Id,
                    RequiredProgress = achievement.TargetProgressCount,
                };
                ta.Params.AddRange(achievement.Params);
                m_AchievementIniters[achievement.AchievementType](ta);
                m_AchievementInfo.TrackingAchievements.Add(achievement.AchievementType, ta);
            }
        }

        private void PushProgressModified(List<TrackingAchievement> achievements)
        {
            LCPushAchievementProgress packet = new LCPushAchievementProgress();
            foreach(var achievement in achievements)
            {
                packet.TrackingAchievements.Add(new PBTrackingAchievement()
                {
                    AchievementId = achievement.Id,
                    ProgressCount = achievement.Progress
                });
            }
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream(packet.PacketActionId, ProtoBufUtils.Serialize(packet));
            var session = GameSession.Get(m_UserId);
            if(session != null)
            {
                session.SendAsync(buffer, 0, buffer.Length);
            }
        }
    }
}
