
using ZyGames.Framework.Cache.Generic;
namespace Genesis.GameServer.LobbyServer
{
    public class InstanceProgressLogic
    {
        private InstanceProgress m_InstanceProgress;
        
        public InstanceProgressLogic(){
            m_InstanceProgress = null;
        }

        public InstanceProgress MyInstanceProgress
        {
            get { return m_InstanceProgress; }
            set { m_InstanceProgress = value; }
        }

        public void SetUser(int userId)
        {
            m_InstanceProgress = CacheSet.InstanceProgressCache.FindKey(userId.ToString(), userId);
            if (m_InstanceProgress == null)
            {
                m_InstanceProgress = new InstanceProgress() { UserId = userId };
                CacheSet.InstanceProgressCache.Add(m_InstanceProgress);
            }
        }

        public CacheDictionary<int, int> GetInstanceProgress()
        {
            if (m_InstanceProgress.Progress == null)
            {
                m_InstanceProgress.Progress = new CacheDictionary<int, int>();
            }
            return m_InstanceProgress.Progress;
        }

        public bool IsInstanceCompleted(int instanceId)
        {
            if (m_InstanceProgress.Progress.ContainsKey(instanceId))
            {
                return true;
            }
            return false;
        }

        public void InstanceCompleted(int instanceId, int stars)
        {
            if (m_InstanceProgress.Progress.ContainsKey(instanceId))
            {
                if (m_InstanceProgress.Progress[instanceId] < stars)
                {
                    m_InstanceProgress.Progress[instanceId] = stars;
                }
            }
            else
            {
                m_InstanceProgress.Progress.Add(instanceId, stars);
                PlayerAchievementLogic.GetInstance(m_InstanceProgress.UserId).UpdateAchievement(AchievementType.InstanceCompletedCount);
            }
            PlayerDailyQuestLogic.GetInstance(m_InstanceProgress.UserId).UpdateDailyQuest(DailyQuestType.CompleteInstance, 1);
        }
    }
}
