using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitHeroMightAchievementProgress(TrackingAchievement achievement)
        {
            PlayerHeroLogic ph = new PlayerHeroLogic();
            ph.SetUser(m_UserId);
            int progress = 0;
            foreach(var hero in ph.GetHeroList())
            {
                if(hero.Value.Might > progress)
                {
                    progress = hero.Value.Might;
                }
            }
            achievement.Progress = progress;
        }

        private void UpdateHeroMightAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.HeroMight];
            if ((int)param[0] > achievement.Progress)
            {
                achievement.Progress = (int)param[0];
                PushProgressModified(new List<TrackingAchievement>() { achievement });
            }
        }
    }
}
