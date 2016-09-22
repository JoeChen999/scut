using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Com.Rank;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerSinglePvpLogic
    {
        private PlayerSinglePvp m_Pvp;
        private int m_UserId;

        public PlayerSinglePvpLogic()
        {
        }

        public PlayerSinglePvp MyPvp
        {
            get
            {
                return m_Pvp;
            }
        }

        public void SetUser(int userId)
        {
            m_UserId = userId;
            m_Pvp = CacheSet.PlayerSinglePvpCache.FindKey(userId);
            if(m_Pvp == null)
            {
                m_Pvp = new PlayerSinglePvp()
                {
                    UserId = userId,
                    DeductedScore = 0,
                };
                ResetPvpInfo();
                CacheSet.PlayerSinglePvpCache.Add(m_Pvp);
                RankingFactory.Get<SinglePvpRankUser>("SinglePvpRanking").Refresh();
            }
            if(m_Pvp.LastResetTime - DateTime.UtcNow.Ticks > TimeSpan.TicksPerDay || new DateTime(m_Pvp.LastResetTime).Day != DateTime.UtcNow.Day)
            {
                ResetChallengeCount();
            }
        }

        public void DealPvpResult(int result, int score)
        {
            if(m_Pvp.RemainingCount > 0)
            {
                m_Pvp.RemainingCount -= 1;
            }
            if(m_Pvp.SinglePvpScore + score <= 0)
            {
                m_Pvp.SinglePvpScore = 0;
            }
            else
            {
                m_Pvp.SinglePvpScore += score;
            }
            m_Pvp.RoomId = 0;
            m_Pvp.RoomServerId = 0;
            if (result == 1)
            {
                PlayerDailyQuestLogic.GetInstance(m_UserId).UpdateDailyQuest(DailyQuestType.CompleteSinglePvp, 1);
            }
        }

        public static void RefreshRank()
        {
            RankingFactory.Get<SinglePvpRankUser>("SinglePvpRanking").Refresh();
        }

        private void ResetPvpInfo()
        {
            m_Pvp.RoomId = 0;
            m_Pvp.RoomServerId = 0;
            m_Pvp.SinglePvpScore = 0;
            ResetChallengeCount();
        }

        private void ResetChallengeCount()
        {
            m_Pvp.RemainingCount = GameConsts.Pvp.PvpPlayCount;
            m_Pvp.LastResetTime = DateTime.UtcNow.Ticks;
        }
    }
}
