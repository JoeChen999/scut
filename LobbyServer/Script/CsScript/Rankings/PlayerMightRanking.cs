using System.Collections.Generic;
using ZyGames.Framework.Game.Com.Rank;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerMightRanking : Ranking<MightRankUser>
    {
        public const string RankingKey = "PlayerMightRanking";
        private const int ListCount = 100000;

        public PlayerMightRanking()
            :base(RankingKey, ListCount, 10)
        {
        }

        protected override int ComparerTo(MightRankUser x, MightRankUser y)
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

            if (x.Might != y.Might)
            {
                return y.Might.CompareTo(x.Might);
            }
            return x.UserId.CompareTo(y.UserId);
        }

        protected override IList<MightRankUser> GetCacheList()
        {
            List<MightRankUser> mightRankUser = new List<MightRankUser>();
            var rankList = CacheSet.PlayerCache.FindAll();
            foreach (var rankUser in rankList)
            {
                if (string.IsNullOrWhiteSpace(rankUser.Name))
                {
                    continue;
                }
                MightRankUser user = new MightRankUser()
                {
                    UserId = rankUser.Id,
                    Might = rankUser.Might
                };
                mightRankUser.Add(user);
            }
            return mightRankUser;
        }
    }
}
