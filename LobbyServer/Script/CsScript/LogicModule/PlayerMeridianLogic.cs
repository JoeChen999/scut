using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerMeridianLogic
    {
        private PlayerMeridian m_Meridian = null;

        public PlayerMeridian MyMeridian 
        {
            get { return m_Meridian; }
        }
        public PlayerMeridianLogic()
        {

        }

        public void SetUser(int userId)
        {
            m_Meridian = CacheSet.PlayerMeridianCache.FindKey(userId.ToString());
            if (m_Meridian == null)
            {
                m_Meridian = new PlayerMeridian();
                m_Meridian.UserId = userId;
                Meridian initMeridian = new Meridian();
                m_Meridian.UnlockedMeridians.Add(1, initMeridian);
                CacheSet.PlayerMeridianCache.Add(m_Meridian);
            }
        }

        public bool OpenStar(int id, int type)
        {
            DTMeridian starData = CacheSet.MeridianTable.GetData(id - 1);
            if (starData != null && starData.Type == type)
            {
                if (m_Meridian.UnlockedMeridians[type].UnlockedStars.Contains(id - 1))
                {
                    m_Meridian.UnlockedMeridians[type].UnlockedStars.Add(id);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                m_Meridian.UnlockedMeridians[type].UnlockedStars.Add(id);
            }
            if (m_Meridian.UnlockedMeridians[type].UnlockedStars.Count >= GameConsts.UnlockNextMeridianStarCount)
            {
                if (!m_Meridian.UnlockedMeridians.ContainsKey(type + 1))
                {
                    m_Meridian.UnlockedMeridians.Add(type + 1, new Meridian());
                }
            }
            return true;
        }
    }
}
