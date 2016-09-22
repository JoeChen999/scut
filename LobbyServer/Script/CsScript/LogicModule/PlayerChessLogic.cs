using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Com.Rank;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerChessLogic
    {
        private const float MaxHP = 100f;
        private int m_UserId;
        private PlayerChess m_Chess;

        public PlayerChessLogic()
        {

        }

        public PlayerChess MyChess
        {
            get { return m_Chess; }
            set { m_Chess = value; }
        }

        public void SetUser(int userId)
        {
            m_UserId = userId;
            m_Chess = CacheSet.PlayerChessCache.FindKey(userId.ToString(), userId);
            if (m_Chess == null)
            {
                Init();
            }
        }

        private void Init()
        {
            PlayerChess newChess = new PlayerChess();
            newChess.UserId = m_UserId;
            CacheSet.PlayerChessCache.Add(newChess);
            m_Chess = newChess;
            HeroTeamLogic heroTeam = new HeroTeamLogic();
            heroTeam.SetUser(m_UserId);
            ChangeTeam(heroTeam.GetTeam());
            ResetToken();
            Reset();
        }

        public bool Reset()
        {
            if (m_Chess.Token < 1)
            {
                return false;
            }
            m_Chess.Anger = 0;
            m_Chess.Count = GameConsts.PlayerChess.DailyPlayCount;
            m_Chess.Token -= 1;
            m_Chess.GotCoin = 0;
            m_Chess.GotMoney = 0;
            m_Chess.GotStarEnergy = 0;
            m_Chess.GotGears.Clear();
            m_Chess.GotItems.Clear();
            m_Chess.GotSouls.Clear();
            m_Chess.GotEpigraphs.Clear();
            PlayerHeroLogic playerHero = new PlayerHeroLogic();
            m_Chess.HP = new CacheDictionary<int, int>();
            foreach (var hero in playerHero.SetUser(m_UserId).MyHeros.Heros)
            {
                m_Chess.HP[hero.Key] = hero.Value.MaxHP;
            }
            m_Chess.ChessBoard = GetChessBoard();
            m_Chess.OpenCount = GameConsts.PlayerChess.EmptyGrayFieldCount + GameConsts.PlayerChess.RewardGrayFieldCount + GameConsts.PlayerChess.EmptyFieldCount;
            return true;
        }

        public void ResetToken()
        {
            m_Chess.LastResetTime = DateTime.Today;
            m_Chess.Token = GetDailyToken();
        }

        public void ChangeTeam(IList<int> newTeam)
        {
            m_Chess.MyTeam = new CacheList<int>();
            m_Chess.MyTeam.AddRange(newTeam);
        }

        public void SetMyAnger(int anger)
        {
            m_Chess.Anger = anger;
        }

        public bool SetMyHeroStatus(List<PBLobbyHeroStatus> heroStatus)
        {
            if (heroStatus.Count > GameConsts.Hero.MaxHeroTeamCount)
            {
                return false;
            }
            if (m_Chess.MyTeam == null || m_Chess.MyTeam.Count == 0)
            {
                foreach (var hero in heroStatus)
                {
                    m_Chess.MyTeam.Add(hero.Type);
                }
            }
            foreach (var hero in heroStatus)
            {
                m_Chess.HP[hero.Type] = hero.CurHP;
            }
            return true;
        }

        public void SetEnemyAnger(int fieldId, int anger)
        {
            if (m_Chess.ChessBoard[fieldId].Color == ChessFieldColor.EmptyGray || m_Chess.ChessBoard[fieldId].Color == ChessFieldColor.Empty || m_Chess.ChessBoard[fieldId].Color == ChessFieldColor.RewardGray)
            {
                return;
            }
            BattleChessField target = m_Chess.ChessBoard[fieldId] as BattleChessField;
            target.EnemyAnger = anger;
        }

        public void SetEnemyStatus(int fieldId, List<int> status)
        {
            if (m_Chess.ChessBoard[fieldId].Color == ChessFieldColor.EmptyGray || m_Chess.ChessBoard[fieldId].Color == ChessFieldColor.Empty || m_Chess.ChessBoard[fieldId].Color == ChessFieldColor.RewardGray)
            {
                return;
            }
            BattleChessField target = m_Chess.ChessBoard[fieldId] as BattleChessField;
            for (int i = 0; i < status.Count; i++)
            {
                target.EnemyHeroHP[i] = status[i];
            }
        }

        public Dictionary<int, int> BombAll(out int coin, out int money, out int starEnergy)
        {
            coin = 0;
            money = 0;
            starEnergy = 0;
            Dictionary<int, int> rewardItems = new Dictionary<int, int>();
            m_Chess.Count = 0;
            int totalCost = 0;
            int OpenedCount = 0;
            foreach (var chessField in m_Chess.ChessBoard)
            {
                if ((chessField.Color == ChessFieldColor.EmptyGray || chessField.Color == ChessFieldColor.RewardGray || chessField.Color == ChessFieldColor.Empty))
                {
                    if (!chessField.IsOpened)
                    {
                        totalCost += GetOpenCost(OpenedCount++);
                    }
                }
            }
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            if (!p.DeductMoney(totalCost))
            {
                return null;
            }
            foreach (var chessField in m_Chess.ChessBoard)
            {
                if ((chessField.Color == ChessFieldColor.EmptyGray || chessField.Color == ChessFieldColor.RewardGray || chessField.Color == ChessFieldColor.Empty))
                {
                    if (!chessField.IsOpened)
                    {
                        RewardChessField rewardField = chessField as RewardChessField;
                        foreach (var reward in rewardField.RewardItems)
                        {
                            if (rewardItems.ContainsKey(reward.Key))
                            {
                                rewardItems[reward.Key] += reward.Value;
                            }
                            else
                            {
                                rewardItems.Add(reward.Key, reward.Value);
                            }
                        }
                    }
                }
            }
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            pp.CheckPackageSlot(rewardItems);
            foreach (var chessField in m_Chess.ChessBoard)
            {
                if ((chessField.Color == ChessFieldColor.EmptyGray || chessField.Color == ChessFieldColor.RewardGray || chessField.Color == ChessFieldColor.Empty))
                {
                    if (!chessField.IsOpened)
                    {
                        RewardChessField rewardField = chessField as RewardChessField;
                        coin += rewardField.RewardCoin;
                        money += rewardField.RewardMoney;
                        starEnergy += rewardField.RewardStarEnergy;
                        rewardField.IsOpened = true;
                        rewardField.ParentId = -1;
                    }
                }
                else
                {
                    BattleChessField battleField = chessField as BattleChessField;
                    battleField.ChildrenId = new CacheList<int>();
                }
            }
            return rewardItems;
        }

        public void DeductOpenCount()
        {
            m_Chess.OpenCount -= 1;
            if (m_Chess.OpenCount == 0)
            {
                m_Chess.Count = 0;
            }
        }

        public CacheList<ChessField> GetChessBoard()
        {
            CacheList<ChessField> chessBoard = new CacheList<ChessField>();
            List<int> weightList = new List<int>() { GameConsts.PlayerChess.EmptyFieldCount, GameConsts.PlayerChess.EmptyGrayFieldCount, GameConsts.PlayerChess.RewardGrayFieldCount, GameConsts.PlayerChess.RedFieldCount, GameConsts.PlayerChess.YellowFieldCount, GameConsts.PlayerChess.GreenFieldCount };
            Random random = new Random();
            int totalWeight;
            int rIndex = 0;
            int yIndex = 0;
            int gIndex = 0;
            var enemies = GetEnemies();
            for (int i = 0; i < GameConsts.PlayerChess.ChessBoardSize; i++)
            {
                totalWeight = 0;
                foreach (int weight in weightList)
                {
                    totalWeight += weight;
                }
                int randomValue = random.Next(0, totalWeight);
                int colorId = 0;
                for (int j = 0; j < weightList.Count; j++)
                {
                    randomValue -= weightList[j];
                    if (randomValue < 0)
                    {
                        colorId = j;
                        break;
                    }
                }
                switch (colorId)
                {
                    case 0:
                        chessBoard[i] = GetRewardChessField(m_UserId, ChessFieldColor.Empty, random);
                        break;
                    case 1:
                        chessBoard[i] = GetRewardChessField(m_UserId, ChessFieldColor.EmptyGray, random);
                        break;
                    case 2:
                        chessBoard[i] = GetRewardChessField(m_UserId, ChessFieldColor.RewardGray, random);
                        break;
                    case 3:
                        chessBoard[i] = GetBattleChessField(ChessFieldColor.Red, enemies[ChessFieldColor.Red][rIndex++], random.Next(GameConsts.PlayerChess.RedFieldMinCount, GameConsts.PlayerChess.RedFieldMaxCount));
                        break;
                    case 4:
                        chessBoard[i] = GetBattleChessField(ChessFieldColor.Yellow, enemies[ChessFieldColor.Yellow][yIndex++], random.Next(GameConsts.PlayerChess.YellowFieldMinCount, GameConsts.PlayerChess.YellowFieldMaxCount));
                        break;
                    case 5:
                        chessBoard[i] = GetBattleChessField(ChessFieldColor.Green, enemies[ChessFieldColor.Green][gIndex++], random.Next(GameConsts.PlayerChess.GreenFieldMinCount, GameConsts.PlayerChess.GreenFieldMaxCount));
                        break;
                    default:
                        TraceLog.WriteError("wrong colorId");
                        break;
                }
                weightList[colorId]--;
            }
            return chessBoard;
        }

        private int GetOpenCost(int count)
        {
            for(int i = 1; i <= GameConfigs.GetInt("Chess_Bomb_Period_Count", 5); i++)
            {
                if(count < GameConfigs.GetInt("Chess_Bomb_Period_" + i, 5))
                {
                    return GameConfigs.GetInt("Chess_Bomb_Cost_" + i, 0);
                }
            }
            return 0;
        }

        private Dictionary<ChessFieldColor, int[]> GetEnemies()
        {
            var mightRank = RankingFactory.Get<MightRankUser>("PlayerMightRanking");
            int myRank = mightRank.GetRankNo(p => p.UserId == m_UserId);
            var Candidates = mightRank.GetRange(myRank - 31, myRank + 31);
            List<int> redPlayers = new List<int>();
            List<int> yellowPlayers = new List<int>();
            List<int> greenPlayers = new List<int>();
            foreach(var user in Candidates)
            {
                if (user.UserId == m_UserId)
                {
                    continue;
                }
                if (user.RankId < myRank - 11)
                {
                    redPlayers.Add(user.UserId);
                }
                else if(user.RankId > myRank + 11)
                {
                    greenPlayers.Add(user.UserId);
                }
                else
                {
                    yellowPlayers.Add(user.UserId);
                }
            }
            if(redPlayers.Count < GameConsts.PlayerChess.RedFieldCount)
            {
                int NeedCount = GameConsts.PlayerChess.RedFieldCount - redPlayers.Count;
                for(int i = 0; i < NeedCount; i++)
                {
                    redPlayers.Add(yellowPlayers[0]);
                    yellowPlayers.RemoveAt(0);
                }
            }
            if (greenPlayers.Count < GameConsts.PlayerChess.GreenFieldCount)
            {
                int NeedCount = GameConsts.PlayerChess.GreenFieldCount - greenPlayers.Count;
                for (int i = 0; i < NeedCount; i++)
                {
                    greenPlayers.Add(yellowPlayers[yellowPlayers.Count - 1]);
                    yellowPlayers.RemoveAt(yellowPlayers.Count - 1);
                }
            }
            Dictionary<ChessFieldColor, int[]> retDict = new Dictionary<ChessFieldColor, int[]>();
            retDict[ChessFieldColor.Red] = GameUtils.RandomChoose(redPlayers, GameConsts.PlayerChess.RedFieldCount);
            retDict[ChessFieldColor.Yellow] = GameUtils.RandomChoose(yellowPlayers, GameConsts.PlayerChess.YellowFieldCount);
            retDict[ChessFieldColor.Green] = GameUtils.RandomChoose(greenPlayers, GameConsts.PlayerChess.GreenFieldCount);
            return retDict;
        }

        public static BattleChessField GetBattleChessField(ChessFieldColor color, int enemyId, int count)
        {
            BattleChessField field = new BattleChessField();
            field.Color = color;
            field.IsOpened = false;
            field.Count = count;
            field.EnemyAnger = 0;
            field.EnemyPlayerId = enemyId;
            HeroTeamLogic heroTeam = new HeroTeamLogic();
            heroTeam.SetUser(field.EnemyPlayerId);
            PlayerHeroLogic playerHero = new PlayerHeroLogic();
            playerHero.SetUser(field.EnemyPlayerId);
            int i = 0;
            foreach (int heroId in heroTeam.MyHeroTeam.Team)
            {
                if (heroId == 0)
                {
                    break;
                }
                field.EnemyPlayerHeroTeam[i] = new Hero();
                field.EnemyPlayerHeroTeam[i].HeroType = playerHero.MyHeros.Heros[heroId].HeroType;
                field.EnemyPlayerHeroTeam[i].HeroLv = playerHero.MyHeros.Heros[heroId].HeroLv;
                field.EnemyPlayerHeroTeam[i].HeroStarLevel = playerHero.MyHeros.Heros[heroId].HeroStarLevel;
                field.EnemyPlayerHeroTeam[i].ElevationLevel = playerHero.MyHeros.Heros[heroId].ElevationLevel;
                field.EnemyPlayerHeroTeam[i].ConsciousnessLevel = playerHero.MyHeros.Heros[heroId].ConsciousnessLevel;
                field.EnemyPlayerHeroTeam[i].Gears = new CacheDictionary<GearType, int>();
                field.EnemyPlayerHeroTeam[i].SkillLevels.AddRange(playerHero.MyHeros.Heros[heroId].SkillLevels);
                foreach (var gear in playerHero.MyHeros.Heros[heroId].Gears)
                {
                    field.EnemyPlayerHeroTeam[i].Gears[gear.Key] = gear.Value;
                }
                field.EnemyPlayerHeroTeam[i].Souls = new CacheDictionary<int, int>();
                foreach (var soul in playerHero.MyHeros.Heros[heroId].Souls)
                {
                    field.EnemyPlayerHeroTeam[i].Souls[soul.Key] = soul.Value;
                }
                //field.EnemyHeroHP.Add(MaxHP);
                i++;
            }
            return field;
        }

        public static RewardChessField GetRewardChessField(int userId, ChessFieldColor color, Random r)
        {
            RewardChessField field = new RewardChessField();
            field.Color = color;
            field.IsOpened = false;
            field.IsFree = false;
            field.ParentId = -1;
            field.RewardCoin = 0;
            field.RewardMoney = 0;
            if (color != ChessFieldColor.Empty)
            {
                PlayerLogic p = new PlayerLogic();
                p.SetUser(userId);
                double baseRewardCoin = GetBaseRewardCoin(p.MyPlayer.Level);
                field.RewardCoin = r.Next((int)Math.Round(baseRewardCoin * 0.5), (int)Math.Round(baseRewardCoin * 1.5));
            }
            if (color == ChessFieldColor.RewardGray)
            {
                RandomDropLogic rd = RandomDropLogic.GetInstance();
                var dropDict = new CacheDictionary<int, int>();
                if (r.Next(100) < GameConsts.PlayerChess.TopRewardRate)
                {
                    var dropData = CacheSet.DropTable.GetData(GameConsts.PlayerChess.TopRewardDropId);
                    rd.GetDropDict(dropData, dropDict);
                }
                else
                {
                    var dropData = CacheSet.DropTable.GetData(GameConsts.PlayerChess.MidRewardDropId);
                    rd.GetDropDict(dropData, dropDict);
                }
                if (dropDict.ContainsKey((int)GiftItemType.Money))
                {
                    field.RewardMoney += dropDict[(int)GiftItemType.Money];
                    dropDict.Remove((int)GiftItemType.Money);
                }
                foreach (var kv in dropDict)
                {
                    field.RewardItems.Add(kv);
                }
            }
            return field;
        }

        private static double GetBaseRewardCoin(int playerLevel)
        {
            return 0.004 * (playerLevel * playerLevel * playerLevel + 5 * playerLevel);
        }

        private int GetDailyToken()
        {
            return 99;
        }
    }
}
