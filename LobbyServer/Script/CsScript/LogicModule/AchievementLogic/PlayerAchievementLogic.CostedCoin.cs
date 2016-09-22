using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitCostedCoinAchievementProgress(TrackingAchievement achievement)
        {
            achievement.Progress = 0;
        }

        private void UpdateCostedCoinAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.CostedCoin];
            achievement.Progress += (int)param[0];
            PushProgressModified(new List<TrackingAchievement>() { achievement });
        }
    }
}
