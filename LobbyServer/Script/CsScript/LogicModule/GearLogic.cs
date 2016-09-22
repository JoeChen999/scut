
using System;
using System.Collections.Generic;
namespace Genesis.GameServer.LobbyServer
{
    public class GearLogic
    {
        private Gears m_Gear;
        public GearLogic()
        {
            m_Gear = null;
        }

        public void SetGear(int gearId)
        {
            m_Gear = CacheSet.GearCache.FindKey(gearId);
        }

        public Gears MyGear
        {
            get { return m_Gear; }
            set { m_Gear = value; }
        }

        public int AddNewGear(int gearTypeId, int userId, ReceiveItemMethodType method)
        {
            m_Gear = new Gears();
            m_Gear.Id = (int)CacheSet.GearCache.GetNextNo();
            m_Gear.TypeId = gearTypeId;
            m_Gear.Level = 1;
            m_Gear.StrengthenLevel = 0;
            CacheSet.GearCache.Add(m_Gear);
            PlayerAchievementLogic.GetInstance(userId).UpdateAchievement(AchievementType.GearQuality, m_Gear.Quality);
            AnnouncementLogic.PushReceiveGearAnnouncement(userId, method, m_Gear.TypeId);
            return m_Gear.Id;
        }

        public bool RemoveGear()
        {
            return CacheSet.GearCache.Delete(m_Gear);
        }

        public void GearLevelUp(int targetLevel, int userId)
        {
            PlayerAchievementLogic.GetInstance(userId).UpdateAchievement(AchievementType.GearLevel, m_Gear.Level, targetLevel);
            m_Gear.Level = targetLevel;
        }

        public bool Strengthen(int userId)
        {
            if(m_Gear.StrengthenLevel >= GameConsts.Gear.MaxStrengthenLevel)
            {
                return false;
            }
            Random r = new Random();
            if(r.Next(100) > GameConsts.Gear.StrengthenSuccessRate[m_Gear.Quality][m_Gear.StrengthenLevel])
            {
                return false;
            }
            PlayerAchievementLogic.GetInstance(userId).UpdateAchievement(AchievementType.GearLevel, m_Gear.StrengthenLevel, m_Gear.StrengthenLevel + 1);
            m_Gear.StrengthenLevel += 1;
            AnnouncementLogic.PushGearStrengthenAnnouncement(userId, m_Gear.StrengthenLevel, m_Gear.TypeId);
            return true;
        }

        public Gears GetComposeResult(int quality, int position, int userId)
        {
            Random r = new Random();
            List<DTGear> gearList;
            if (position == 0)
            {
                if (r.Next(100) > GameConfigs.GetInt("Gear_Compose_Success_Rate_" + quality.ToString(), 50))
                {
                    return null;
                }
                gearList = CacheSet.GearTable.GetAllData(t => t.Quality == quality + 1);
            }
            else
            {
                gearList = CacheSet.GearTable.GetAllData(t => t.Quality == quality + 1 && t.Type == position);
            }
            int randomIndex = r.Next(gearList.Count);
            AddNewGear(gearList[randomIndex].Id, userId, ReceiveItemMethodType.GearCompose);
            return m_Gear;
        }

        public int GetComposeCost(int quality)
        {
            return 0;
        }

        public int GetLevelUpCost(int targetLevel)
        {
            int quality = CacheSet.GearTable.GetData(m_Gear.TypeId).Quality;
            DTGearLevelUp dataRow = CacheSet.GearLevelUpTable.GetData(quality);
            int totalCost = 0;
            for (int i = m_Gear.Level; i < targetLevel; i++)
            {
                totalCost += dataRow.LevelUpCostCoin[i - 1];
            }
            return totalCost;
        }
    }
}
