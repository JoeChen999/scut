using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitHeroCountProgress(TrackingAchievement achievement)
        {
            PlayerHeroLogic ph = new PlayerHeroLogic();
            ph.SetUser(m_UserId);
            achievement.Progress = ph.GetHeroList().Count;
        }

        private void UpdateHeroCountAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.HeroCount];
            achievement.Progress = (int)param[0];
            PushProgressModified(new List<TrackingAchievement>() { achievement });
        }
    }
}
