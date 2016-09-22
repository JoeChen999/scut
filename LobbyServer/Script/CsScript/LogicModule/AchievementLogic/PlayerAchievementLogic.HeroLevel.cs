using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitHeroLevelAchievementProgress(TrackingAchievement achievement)
        {
            PlayerHeroLogic ph = new PlayerHeroLogic();
            ph.SetUser(m_UserId);
            int progress = 0;
            foreach(var hero in ph.GetHeroList())
            {
                if(hero.Value.HeroLv > progress)
                {
                    progress = hero.Value.HeroLv;
                }
            }
            achievement.Progress = progress;
        }

        private void UpdateHeroLevelAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.HeroLevel];
            if ((int)param[0] > achievement.Progress)
            {
                achievement.Progress = (int)param[0];
                PushProgressModified(new List<TrackingAchievement>() { achievement });
            }
        }
    }
}
