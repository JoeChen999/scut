using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitPvpWinCountAchievementProgress(TrackingAchievement achievement)
        {
            achievement.Progress = 0;
        }

        private void UpdatePvpWinCountAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.PvpWinCount];
            achievement.Progress += 1;
            PushProgressModified(new List<TrackingAchievement>() { achievement });
        }
    }
}
