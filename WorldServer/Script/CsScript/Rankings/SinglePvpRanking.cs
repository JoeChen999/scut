using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.WorldServer
{
    public class SinglePvpRanking : Ranking<SinglePvpRankUser>
    {
        public const string RankingKey = "SinglePvpRanking";
        private const int ListCount = 1000;

        public SinglePvpRanking()
            :base(RankingKey, ListCount, 2)
        {
        }

        protected override int ComparerTo(SinglePvpRankUser x, SinglePvpRankUser y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x != null && y == null)
            {
                return 1;
            }
            if (x == null)
            {
                return -1;
            }

            if (x.Score != y.Score)
            {
                return y.Score.CompareTo(x.Score);
            }
            return x.UserId.CompareTo(y.UserId);
        }

        protected override IList<SinglePvpRankUser> GetCacheList()
        {
            List<SinglePvpRankUser> rankingUserlv = new List<SinglePvpRankUser>();
            var rankList = CacheSet.PvpPlayerCache.FindAll();
            foreach(var rankUser in rankList)
            {
                if(rankUser.Score <= 0)
                {
                    continue;
                }
                SinglePvpRankUser user = new SinglePvpRankUser()
                {
                    UserId = rankUser.Id,
                    Score = rankUser.Score,
                    ServerId = rankUser.ServerId,
                    Name = rankUser.Name
                };
                rankingUserlv.Add(user);
            }
            return rankingUserlv;
        }
    }
}
