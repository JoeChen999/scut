using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitGearStrengthenLevelAchievementProgress(TrackingAchievement achievement)
        {
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            PlayerHeroLogic ph = new PlayerHeroLogic();
            ph.SetUser(m_UserId);
            int progress = 0;
            GearLogic g = new GearLogic();
            foreach(var hero in ph.GetHeroList())
            {
                foreach(var gear in hero.Value.Gears)
                {
                    g.SetGear(gear.Value);
                    if(g.MyGear.StrengthenLevel >= achievement.Params[0])
                    {
                        progress++;
                    }
                }
            }
            foreach (var gear in pp.MyPackage.Gears)
            {
                g.SetGear(gear.Key);
                if (g.MyGear.StrengthenLevel >= achievement.Params[0])
                {
                    progress++;
                }
            }
            achievement.Progress = progress;
        }

        private void UpdateGearStrengthenLevelAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.GearStrengthenLevel];
            if ((int)param[0] < achievement.Params[0] && (int)param[1] >= achievement.Params[0])
            {
                achievement.Progress += 1;
                PushProgressModified(new List<TrackingAchievement>() { achievement });
            }
        }
    }
}
