using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerDailyQuestLogic
    {
        private static Dictionary<int, PlayerDailyQuestLogic> m_Instances = new Dictionary<int, PlayerDailyQuestLogic>();

        private int m_UserId;
        private PlayerDailyQuest m_DailyQuestInfo;
        private Dictionary<int, Action<TrackingDailyQuest, object[]>> m_DailyQuestUpdaters;

        private PlayerDailyQuestLogic(int userId)
        {
            m_UserId = userId;
            m_DailyQuestUpdaters = new Dictionary<int, Action<TrackingDailyQuest, object[]>>()
            {
                { (int)DailyQuestType.CompleteInstance, UpdateCommonQuest},
                { (int)DailyQuestType.CleanOutInstance, UpdateCommonQuest},
                { (int)DailyQuestType.CompleteOfflineArena, UpdateCommonQuest},
                { (int)DailyQuestType.CompleteSinglePvp, UpdateCommonQuest},
                { (int)DailyQuestType.HasMonthlyCard, UpdateCommonQuest},
                { (int)DailyQuestType.WinTurnOverChessBattle, UpdateCommonQuest},
                { (int)DailyQuestType.ClaimGearFoundryReward, UpdateCommonQuest},
                { (int)DailyQuestType.CompleteCosmosCrack, UpdateCommonQuest},
                { (int)DailyQuestType.GiftEnergyToFriend, UpdateCommonQuest},
            };
            m_DailyQuestInfo = CacheSet.PlayerDailyQuestCache.FindKey(userId.ToString());
            if (m_DailyQuestInfo == null)
            {
                m_DailyQuestInfo = new PlayerDailyQuest();
                m_DailyQuestInfo.UserId = userId;
                m_DailyQuestInfo.LastRefreshTime = 0;
                ResetDailyQuestInfo();
                CacheSet.PlayerDailyQuestCache.Add(m_DailyQuestInfo);
            }
        }

        public static PlayerDailyQuestLogic GetInstance(int userId)
        {
            if(m_Instances.ContainsKey(userId) && m_Instances[userId] != null)
            {
                return m_Instances[userId];
            }
            PlayerDailyQuestLogic pdq = new PlayerDailyQuestLogic(userId);
            m_Instances.Add(userId, pdq);
            return pdq;
        }

        public CacheDictionary<int, TrackingDailyQuest> TrackingDailyQuests
        {
            get
            {
                return m_DailyQuestInfo.TrackingDailyQuests;
            }
        }

        public CacheList<int> CompletedDailyQuests
        {
            get
            {
                return m_DailyQuestInfo.CompletedDailyQuests;
            }
        }

        public CacheDictionary<int, int> CompleteDailyQuest(int id)
        {
            var aData = CacheSet.DailyQuestTable.GetData(id);
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
            if (!m_DailyQuestInfo.TrackingDailyQuests.ContainsKey(id))
            {
                TraceLog.WriteError("You don not have this quest, Id:" + id.ToString());
                return null;
            }
            var completedQuest = m_DailyQuestInfo.TrackingDailyQuests[id];
            if(completedQuest.Progress < completedQuest.RequiredProgress)
            {
                TraceLog.WriteError("You have not completed this achievement, Id:" + id.ToString());
                return null;
            }
            m_DailyQuestInfo.CompletedDailyQuests.Add(id);
            m_DailyQuestInfo.TrackingDailyQuests.Remove(id);
            return aData.RewardItems;
        }

        public void UpdateDailyQuest(DailyQuestType type, params object[] param)
        {
            foreach (var quest in m_DailyQuestInfo.TrackingDailyQuests)
            {
                if(quest.Value.Type == (int)type)
                {
                    m_DailyQuestUpdaters[quest.Value.Type](quest.Value, param);
                }
            }
        }

        public bool ResetDailyQuestInfo()
        {
            if (!GameUtils.NeedRefresh(m_DailyQuestInfo.LastRefreshTime,  GameConsts.DailyQuest.RefreshTime))
            {
                return false;
            }
            PlayerLogic player = new PlayerLogic();
            player.SetUser(m_UserId);

            var initialDailyQuests = CacheSet.DailyQuestTable.GetAllData(dq => dq.PrePlayerLevel <= player.MyPlayer.Level);
            m_DailyQuestInfo.TrackingDailyQuests.Clear();
            m_DailyQuestInfo.CompletedDailyQuests.Clear();
            foreach (var quest in initialDailyQuests)
            {
                TrackingDailyQuest tdq = new TrackingDailyQuest()
                {
                    Id = quest.Id,
                    Type = quest.QuestType,
                    Progress = 0,
                    RequiredProgress = quest.TargetProgressCount,
                };
                tdq.Params.AddRange(quest.Params);
                m_DailyQuestInfo.TrackingDailyQuests.Add(quest.Id, tdq);
            }
            m_DailyQuestInfo.LastRefreshTime = DateTime.UtcNow.Ticks;
            return true;
        }

        private void PushProgressModified(List<TrackingDailyQuest> quests)
        {
            LCPushDailyQuestProgress packet = new LCPushDailyQuestProgress();
            foreach(var quest in quests)
            {
                packet.TrackingDailyQuests.Add(new PBTrackingDailyQuest()
                {
                    QuestId = quest.Id,
                    ProgressCount = quest.Progress
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
