using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitHeroStarLevelProgress(TrackingAchievement achievement)
        {
            PlayerHeroLogic ph = new PlayerHeroLogic();
            ph.SetUser(m_UserId);
            int progress = 0;
            foreach (var hero in ph.GetHeroList())
            {
                if (hero.Value.HeroStarLevel >= achievement.Params[0])
                {
                    progress++;
                }
            }
            achievement.Progress = progress;
        }

        private void UpdateHeroStarLevelAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.HeroStarLevel];
            if ((int)param[0] < achievement.Params[0] && (int)param[1] >= achievement.Params[0])
            {
                achievement.Progress += 1;
                PushProgressModified(new List<TrackingAchievement>() { achievement });
            }
        }
    }
}
