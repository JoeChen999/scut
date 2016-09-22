using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitInstanceCompletedCountAchievementProgress(TrackingAchievement achievement)
        {
            InstanceProgressLogic ip = new InstanceProgressLogic();
            ip.SetUser(m_UserId);
            achievement.Progress = ip.GetInstanceProgress().Count;
        }

        private void UpdateInstanceCompletedCountAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.InstanceCompletedCount];
            achievement.Progress += 1;
            PushProgressModified(new List<TrackingAchievement>() { achievement });
        }
    }
}
