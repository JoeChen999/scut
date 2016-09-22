using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerArenaLogic
    {
        private int m_UserId;
        private PlayerArena m_Arena;
        public PlayerArenaLogic()
        {

        }

        public void SetUser(int userId)
        {
            m_UserId = userId;
            m_Arena = CacheSet.PlayerArenaCache.FindKey(userId.ToString(), userId);
            if (m_Arena == null)
            {
                m_Arena = new PlayerArena();
                m_Arena.UserId = userId;
                m_Arena.WinCount = 0;
                m_Arena.ArenaTokenCount = 0;
                m_Arena.ChallengeCount = GameConsts.Arena.DailyChallengeCount;
                for(int i = 0; i < GameConfigs.GetInt("Offline_Arena_Liveness_Reward_Count",3); i++)
                {
                    m_Arena.ClaimedLivenessRewardFlag.Add(false);
                }
                CacheSet.PlayerArenaCache.Add(m_Arena);
                ArenaRankLogic.AddNewPlayer(userId);
            }
        }

        public PlayerArena MyArena
        {
            get { return m_Arena; }
            set { m_Arena = value; }
        }

        public bool StartBattle(int enemyId, int myRank, int enemyRank, bool IsRevenge)
        {
            if (m_Arena.ChallengeCount < 1)
            {
                return false;
            }
            int userId = m_UserId;
            ArenaRankLogic ar = new ArenaRankLogic();
            if (!IsRevenge && enemyRank != ar.GetPlayerRank(enemyId))
            {
                return false;
            }
            SetUser(enemyId);
            if (m_Arena.EnemyId > 0)
            {
                SetUser(userId);
                return false;
            }
            if (!IsRevenge)
            {
                m_Arena.EnemyId = userId;
            }
            SetUser(userId);
            if (!IsRevenge)
            {
                m_Arena.EnemyId = enemyId;
                m_Arena.ChallengeCount -= 1;
            }
            return true;
        }

        public bool EndBattle(int enemyId, bool isWin, bool isRevenge, out int token)
        {
            int userId = m_UserId;
            token = 0;
            if (isRevenge)
            {
                SetUser(enemyId);
                AddBattleReport(userId, false, !isWin);
                SetUser(userId);
                return true;
            }
            if (m_Arena.EnemyId <= 0)
            {
                SetUser(enemyId);
                m_Arena.EnemyId = 0;
                SetUser(userId);
                return false;
            }
            
            SetUser(enemyId);
            if (m_Arena.EnemyId <= 0)
            {
                SetUser(userId);
                m_Arena.EnemyId = 0;
                return false;
            }
            token = GetTokenCount(isWin);     
            m_Arena.EnemyId = 0;
            AddBattleReport(userId, false, !isWin);
            SetUser(userId);
            if (isWin)
            {
                ArenaRankLogic ar = new ArenaRankLogic();
                ar.SwapRank(userId, enemyId);
                m_Arena.WinCount += 1;
                PlayerAchievementLogic.GetInstance(userId).UpdateAchievement(AchievementType.PvpWinCount);
            }
            m_Arena.ArenaTokenCount += token;
            m_Arena.EnemyId = 0;
            AddBattleReport(enemyId, true, isWin);
            PlayerDailyQuestLogic.GetInstance(m_UserId).UpdateDailyQuest(DailyQuestType.CompleteOfflineArena, 1);
            return true;
        }

        public void ResetChallengeCount()
        {
            m_Arena.ModifyLocked(() =>
            {
                m_Arena.ChallengeCount = GameConsts.Arena.DailyChallengeCount;
            });
        }

        public void ResetLivenessReward()
        {
            m_Arena.ModifyLocked(() =>
            {
                m_Arena.WinCount = 0;
                for (int i = 0; i < GameConfigs.GetInt("Offline_Arena_Liveness_Reward_Count", 3); i++)
                {
                    m_Arena.ClaimedLivenessRewardFlag[i] = false;
                }
            });
        }

        private int GetTokenCount(bool IsWin)
        {
            if (IsWin)
            {
                return GameConfigs.GetInt("Arena_Token_For_Winner", 2);
            }
            else
            {
                return GameConfigs.GetInt("Arena_Token_For_Loser", 1);
            }
        }

        public void AddBattleReport(int enemyId, bool isActive, bool isWin)
        {
            if (m_Arena.BattleReports.Count >= GameConsts.Arena.BattleReportCount)
            {
                m_Arena.BattleReports.RemoveAt(0);
            }
            ArenaBattleReport report = new ArenaBattleReport();
            report.EnemyId = enemyId;
            report.IsActive = isActive;
            report.IsWin = isWin;
            report.BattleTime = DateTime.UtcNow.Ticks;
            m_Arena.BattleReports.Add(report);
        }

        public bool GetLivenessReward(int id, out Dictionary<int, int> itemDict, out int rewardMoney)
        {
            rewardMoney = 0;
            itemDict = new Dictionary<int, int>();
            switch (id)
            {
                case 0:
                    if (m_Arena.ClaimedLivenessRewardFlag[0] || m_Arena.WinCount < GameConfigs.GetInt("Offline_Arena_Liveness_Reward_Win_Count_0",2))
                    {
                        return false;
                    }
                    rewardMoney = 10;
                    itemDict.Add(111001,1);
                    itemDict.Add(171001,1);
                    itemDict.Add(202101,1);
                    m_Arena.ClaimedLivenessRewardFlag[0] = true;
                    break;
                case 1:
                    if (m_Arena.ClaimedLivenessRewardFlag[1] || m_Arena.WinCount < GameConfigs.GetInt("Offline_Arena_Liveness_Reward_Win_Count_1",3))
                    {
                        return false;
                    }
                    rewardMoney = 50;
                    itemDict.Add(111002,1);
                    itemDict.Add(171002,1);
                    itemDict.Add(202101,2);
                    m_Arena.ClaimedLivenessRewardFlag[1] = true;
                    break;
                case 2:
                    if (m_Arena.ClaimedLivenessRewardFlag[2] || m_Arena.WinCount < GameConfigs.GetInt("Offline_Arena_Liveness_Reward_Win_Count_2",4))
                    {
                        return false;
                    }
                    rewardMoney = 200;
                    itemDict.Add(111003,1);
                    itemDict.Add(171003,1);
                    itemDict.Add(202101,3);
                    m_Arena.ClaimedLivenessRewardFlag[2] = true;
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}
