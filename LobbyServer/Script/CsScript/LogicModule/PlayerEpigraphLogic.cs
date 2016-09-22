using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerEpigraphLogic
    {
        private PlayerEpigraph m_Epigraph;
        private int m_UserId;
        public PlayerEpigraphLogic()
        {
            m_UserId = 0;
            m_Epigraph = null;
        }

        public void SetUser(int userId)
        {
            m_UserId = userId;
            m_Epigraph = CacheSet.PlayerEpigraphCache.FindKey(userId.ToString(), userId);
            if (m_Epigraph == null)
            {
                m_Epigraph = new PlayerEpigraph();
                m_Epigraph.UserId = userId;
                m_Epigraph.Epigraphs.Add(new Epigraph() { Type = 0, Level = 0 });
                CacheSet.PlayerEpigraphCache.Add(m_Epigraph);
            }
        }

        public CacheList<Epigraph> GetEpigraphs()
        {
            return m_Epigraph.Epigraphs;
        }

        public int GetLevel()
        {
            return m_Epigraph.Epigraphs.Count;
        }

        public bool HasEpigraph(int type)
        {
            foreach (var epigraph in m_Epigraph.Epigraphs)
            {
                if (epigraph.Type == type)
                {
                    return true;
                }
            }
            return false;
        }

        public void DressEpigraph(int type, int level, int slot)
        {
            m_Epigraph.Epigraphs[slot].Type = type;
            m_Epigraph.Epigraphs[slot].Level = level;
        }

        public bool UndressEpigraph(int type, int level, int slot)
        {
            if (m_Epigraph.Epigraphs[slot].Type != type || m_Epigraph.Epigraphs[slot].Level != level)
            {
                return false;
            }
            m_Epigraph.Epigraphs[slot].Type = 0;
            m_Epigraph.Epigraphs[slot].Level = 0;
            return true;
        }

        public void UnlockSlot()
        {
            if (m_Epigraph.Epigraphs.Count >= GameConsts.MaxEpigraphSlot)
            {
                return;
            }
            m_Epigraph.Epigraphs.Add(new Epigraph() { Type = 0, Level = 0 });
        }
    }
}
