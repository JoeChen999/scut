using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public class ArenaRankLogic
    {
        public ArenaRankLogic()
        {

        }

        public static void AddNewPlayer(int userId)
        {
            ArenaRank ar = new ArenaRank();
            ar.RankId = (int)CacheSet.ArenaRankCache.GetNextNo();
            ar.PlayerId = userId;
            CacheSet.ArenaRankCache.Add(ar);
        }

        public static void InitRankList()
        {
            if (CacheSet.ArenaRankCache.FindAll().Count == 0)
            {
                PlayerLogic player = new PlayerLogic();
                HeroTeamLogic ht = new HeroTeamLogic();
                for (int i = 0; i < GameConsts.Arena.RobotCount; i++)
                {
                    player.AddRobot();
                    PlayerHeroLogic ph = new PlayerHeroLogic();
                    ph.SetUser(player.MyPlayer.Id);
                    ht.SetUser(player.MyPlayer.Id);
                    var heros = new int[] {1, 2, 5};//GameUtils.RandomChoose(1, 4, GameConsts.Hero.MaxHeroTeamCount);
                    foreach (int heroId in heros)
                    {
                        ph.AddNewHero(heroId);
                        ph.MyHeros.Heros[heroId].HeroLv = player.MyPlayer.Level;
                    }
                    List<int> heroTeam = new List<int>();
                    heroTeam.AddRange(heros);
                    ht.AssignHero(heroTeam);
                    AddNewPlayer(player.MyPlayer.Id);
                    PlayerArena pa = new PlayerArena();
                    pa.UserId = player.MyPlayer.Id;
                    pa.EnemyId = 0;
                    CacheSet.PlayerArenaCache.Add(pa);
                }
            }
        }

        public int GetPlayerRank(int playerId)
        {
            var rank = CacheSet.ArenaRankCache.Find(t => t.PlayerId == playerId);
            if(rank == null)
            {
                AddNewPlayer(playerId);
            }
            return rank.RankId;
        }

        public bool TryGetRankList(int pageIndex, out List<ArenaRank> rankList)
        {
            rankList = new List<ArenaRank>();
            int playerCount = GameConfigs.GetInt("Arena_Rank_Player_Count_Per_Page", 20);
            int startIndex = playerCount * pageIndex + 1;
            int endIndex = playerCount * (pageIndex + 1);
            int i;
            for (i = startIndex; i <= endIndex; i++)
            {
                var rankPlayer = CacheSet.ArenaRankCache.FindKey(i);
                if (rankPlayer == null)
                {
                    return false;
                }
                rankList.Add(rankPlayer);
            }
            if (CacheSet.ArenaRankCache.FindKey(i) == null)
            {
                return false;
            }
            return true;
        }

        public void SwapRank(int MyId, int EnemyId)
        {
            var myRank = CacheSet.ArenaRankCache.Find(t => t.PlayerId == MyId);
            var enemyRank = CacheSet.ArenaRankCache.Find(t => t.PlayerId == EnemyId);
            if (myRank.RankId > enemyRank.RankId)
            {
                myRank.ModifyLocked(() =>
                {
                    myRank.PlayerId = EnemyId;
                });
                enemyRank.ModifyLocked(() =>
                {
                    enemyRank.PlayerId = MyId;
                });
            }
        }

        public List<ArenaRank> GetMatchPlayers(int playerId)
        {
            List<ArenaRank> result = new List<ArenaRank>();
            int rank = GetPlayerRank(playerId);
            if (rank <= 5)
            {
                for (int i = 1; i < rank; i++)
                {
                    var rankPlayer = CacheSet.ArenaRankCache.FindKey(i);
                    result.Add(rankPlayer);
                }
                var otherPlayers = GameUtils.RandomChoose(rank + 1, rank + 10, 6 - rank);
                foreach (var player in otherPlayers)
                {
                    var rankPlayer = CacheSet.ArenaRankCache.FindKey(player);
                    result.Add(rankPlayer);
                }
            }
            else if (rank <= 15)
            {
                var players = GameUtils.RandomChoose(1, rank - 1, 5);
                foreach (var player in players)
                {
                    var rankPlayer = CacheSet.ArenaRankCache.FindKey(player);
                    result.Add(rankPlayer);
                }
            }
            else if (rank <= 50)
            {
                Random r = new Random();
                int firstRank = r.Next(1, 11);
                var rankPlayer = CacheSet.ArenaRankCache.FindKey(firstRank);
                result.Add(rankPlayer);
                var otherPlayers = GameUtils.RandomChoose(11, rank - 1, 4);
                foreach (var player in otherPlayers)
                {
                    var otherPlayer = CacheSet.ArenaRankCache.FindKey(player);
                    result.Add(otherPlayer);
                }
            }
            else if (rank <= 200)
            {
                Random r = new Random();
                int firstRank = r.Next(rank - 50, rank - 39);
                var rankPlayer = CacheSet.ArenaRankCache.FindKey(firstRank);
                result.Add(rankPlayer);
                var otherPlayers = GameUtils.RandomChoose(rank - 39, rank - 1, 4);
                foreach (var player in otherPlayers)
                {
                    var otherPlayer = CacheSet.ArenaRankCache.FindKey(player);
                    result.Add(otherPlayer);
                }
            }
            else if (rank <= 1000)
            {
                Random r = new Random();
                int firstRank = r.Next(rank - 200, rank - 189);
                var rankPlayer = CacheSet.ArenaRankCache.FindKey(firstRank);
                result.Add(rankPlayer);
                var otherPlayers = GameUtils.RandomChoose(rank - 189, rank - 1, 4);
                foreach (var player in otherPlayers)
                {
                    var otherPlayer = CacheSet.ArenaRankCache.FindKey(player);
                    result.Add(otherPlayer);
                }
            }
            else
            {
                Random r = new Random();
                int firstRank = r.Next(rank - 500, rank - 489);
                var rankPlayer = CacheSet.ArenaRankCache.FindKey(firstRank);
                result.Add(rankPlayer);
                var otherPlayers = GameUtils.RandomChoose(rank - 489, rank - 1, 4);
                foreach (var player in otherPlayers)
                {
                    var otherPlayer = CacheSet.ArenaRankCache.FindKey(player);
                    result.Add(otherPlayer);
                }
            }
            return result;
        }
    }
}
