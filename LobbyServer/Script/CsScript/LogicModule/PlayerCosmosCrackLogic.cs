
using Genesis.GameServer.CommonLibrary;
using System;
using ZyGames.Framework.Cache.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerCosmosCrackLogic
    {
        private const int MinCosmosCrackInstanceId = 1;
        private const int MinRewardLevel = 1;
        private const int MaxRewardLevel = 3;
        private const int InstanceCount = 3;
        private const int RefreshTimeHour = 4;

        private int m_UserId;
        private PlayerCosmosCrack m_CosmosCrack;
        private Random random;

        public PlayerCosmosCrackLogic()
        {
            random = new Random();
        }

        public PlayerCosmosCrackLogic SetUser(int userId)
        {
            m_UserId = userId;
            m_CosmosCrack = CacheSet.PlayerCosmosCrackCache.FindKey(m_UserId.ToString());
            if (m_CosmosCrack == null)
            {
                m_CosmosCrack = new PlayerCosmosCrack();
                m_CosmosCrack.UserId = m_UserId;
                m_CosmosCrack.PassedRoundCount = 0;
                m_CosmosCrack.LastRefreshTime = DateTime.UtcNow.Ticks;
                m_CosmosCrack.ChosenInstance = new CacheDictionary<int, CosmosCrackInstance>();
                RefreshCosmosCrackInstance();
                CacheSet.PlayerCosmosCrackCache.Add(m_CosmosCrack);
            }
            return this;
        }

        public void RefreshCosmosCrackInstance()
        {
            m_CosmosCrack.ChosenInstance.Clear();
            var instances = GameUtils.RandomChoose(CacheSet.InstanceCosmosCrackTable.GetAllData(), InstanceCount);
            foreach (var instance in instances)
            {
                CosmosCrackInstance cci = new CosmosCrackInstance();
                cci.RewardLevel = random.Next(MinRewardLevel, MaxRewardLevel + 1);
                DTDrop dropData = CacheSet.DropTable.GetData(cci.RewardLevel + 1000);
                RandomDropLogic rd = RandomDropLogic.GetInstance();
                CacheDictionary<int, int> dropDict = new CacheDictionary<int, int>();
                rd.GetDropDict(dropData, dropDict);
                cci.RewardItem = dropDict;
                m_CosmosCrack.ChosenInstance[instance.Id] = cci;
            }
        }

        public PlayerCosmosCrack GetCosmosCrackInstanceInfo()
        {
            long todayRefreshTime = DateTime.Today.AddHours(RefreshTimeHour).Ticks;
            if (m_CosmosCrack.LastRefreshTime < todayRefreshTime && DateTime.Now.Ticks > todayRefreshTime)
            {
                RefreshPlayerCosmosCrack();
            }
            return m_CosmosCrack;
        }

        public void RefreshPlayerCosmosCrack()
        {
            m_CosmosCrack.PassedRoundCount = 0;
            m_CosmosCrack.LastRefreshTime = DateTime.Now.Ticks;
            RefreshCosmosCrackInstance();
        }

        public int GetNPCAndPlayerDeltaLevel(int instanceId)
        {
            int range = GameConfigs.GetInt("Cosmos_Crack_NPC_Level_Range", 5);
            int minlevel = (0 - range) + (m_CosmosCrack.ChosenInstance[instanceId].RewardLevel - 1) * (range * 2 / MaxRewardLevel);
            int maxlevel = (0 + range) - (MaxRewardLevel - m_CosmosCrack.ChosenInstance[instanceId].RewardLevel) * (range * 2 / MaxRewardLevel);
            Random random = new Random();
            return random.Next(minlevel, maxlevel);
        }

        public bool EnterInstance(int instanceId)
        {
            if (m_CosmosCrack.PassedRoundCount >= GameConfigs.GetInt("Cosmos_Crack_Round_Limit", 10))
            {
                return false;
            }
            if (!m_CosmosCrack.ChosenInstance.ContainsKey(instanceId))
            {
                return false;
            }
            return true;
        }

        public CacheDictionary<int, int> LeaveInstance(int instanceId)
        {
            if (m_CosmosCrack.PassedRoundCount >= GameConfigs.GetInt("Cosmos_Crack_Round_Limit", 10) || !m_CosmosCrack.ChosenInstance.ContainsKey(instanceId))
            {
                return null;
            }
            var retval = m_CosmosCrack.ChosenInstance[instanceId].RewardItem;
            RefreshCosmosCrackInstance();
            m_CosmosCrack.PassedRoundCount += 1;
            PlayerDailyQuestLogic.GetInstance(m_UserId).UpdateDailyQuest(DailyQuestType.CompleteCosmosCrack, 1);
            return retval;
        }
    }
}
