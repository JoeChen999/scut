using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitPlayerLevelAchievementProgress(TrackingAchievement achievement)
        {
            PlayerLogic player = new PlayerLogic();
            player.SetUser(m_UserId);
            achievement.Progress = player.MyPlayer.Level;
        }

        private void UpdatePlayerLevelAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.PlayerLevel];
            if (achievement.Progress < (int)param[0])
            {
                achievement.Progress = (int)param[0];
                PushProgressModified(new List<TrackingAchievement>() { achievement });
            }
        }
    }
}
