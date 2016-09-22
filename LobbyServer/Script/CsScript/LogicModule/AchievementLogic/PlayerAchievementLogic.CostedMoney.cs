using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitCostedMoneyAchievementProgress(TrackingAchievement achievement)
        {
            achievement.Progress = 0;
        }

        private void UpdateCostedMoneyAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.CostedMoney];
            achievement.Progress += (int)param[0];
            PushProgressModified(new List<TrackingAchievement>() { achievement });
        }
    }
}
