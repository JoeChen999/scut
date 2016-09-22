using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitOpenedMoneyChanceCountAchievementProgress(TrackingAchievement achievement)
        {
            achievement.Progress = 0;
        }

        private void UpdateOpenedMoneyChanceCountAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.OpenedMoneyChanceCount];
            achievement.Progress += (int)param[0];
            PushProgressModified(new List<TrackingAchievement>() { achievement });
        }
    }
}
