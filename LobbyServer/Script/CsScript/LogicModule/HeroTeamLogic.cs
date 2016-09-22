using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public class HeroTeamLogic
    {
        private int m_UserId;
        private HeroTeam m_Team;
        
        public HeroTeamLogic()
        {
            m_Team = null;
            m_UserId = 0;
        }

        public void SetUser(int userId)
        {
            m_UserId = userId;
            m_Team = CacheSet.HeroTeamCache.FindKey(m_UserId);
            if (m_Team == null)
            {
                m_Team = new HeroTeam();
                m_Team.PlayerId = m_UserId;
                m_Team.Team = new CacheList<int> { 0, 0, 0 };
                CacheSet.HeroTeamCache.Add(m_Team);
            }
        }

        public HeroTeam MyHeroTeam
        {
            get
            {
                return m_Team;
            }
            set
            {
                m_Team = value;
            }
        }

        public CacheList<int> GetTeam()
        {
            return m_Team.Team;
        }

        public bool AssignHero(List<int> heroTeam)
        {
            if (heroTeam.Count > 3 || heroTeam.Count == 0)
            {
                return false;
            }
            PlayerHeroLogic ph = new PlayerHeroLogic();
            ph.SetUser(m_UserId);
            foreach (int heroId in heroTeam)
            {
                if (!ph.SetHero(heroId).DataCheck())
                {
                    return false;
                }
            }
            for (int i = 0; i < GameConsts.Hero.MaxHeroTeamCount; i++)
            {
                if (i + 1 > heroTeam.Count)
                {
                    m_Team.Team[i] = 0;
                }
                else
                {
                    m_Team.Team[i] = heroTeam[i];
                }
            }
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            p.RefreshMight();
            return true;
        }
    }
}
