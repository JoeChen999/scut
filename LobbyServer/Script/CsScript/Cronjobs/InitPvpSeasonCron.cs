using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Redis;

namespace Genesis.GameServer.LobbyServer
{
    public class InitPvpSeasonCron : ICronjob
    {
        public List<DateTime> ExecuteTimes
        {
            get
            {
                return new List<DateTime>()
                {
                    new DateTime(2016, 6, 5, 20, 0, 0),
                };
            }
        }

        public InitPvpSeasonCron()
        {

        }

        public void Execute()
        {
            var data = CacheSet.PvpTitleTable.GetAllData();
            var highestTitle = data[data.Count - 1];
            int maxScore = highestTitle.TitleMinScore;
            var players = CacheSet.PlayerSinglePvpCache.FindAll();
            foreach(var player in players)
            {
                if(player.SinglePvpScore > maxScore)
                {
                    player.DeductedScore += player.SinglePvpScore - maxScore;
                    player.SinglePvpScore = maxScore;
                }
            }
            if(PVPLogic.Year == 0 || PVPLogic.Year != DateTime.UtcNow.Year)
            {
                PVPLogic.SeasonId = 1;
                PVPLogic.Year = DateTime.UtcNow.Year;
                RedisConnectionPool.GetClient().Set(GameConsts.Pvp.PvpSeasonOfYearKey, PVPLogic.Year);
            }
            else
            {
                PVPLogic.SeasonId += 1;
            }
            RedisConnectionPool.GetClient().Set(GameConsts.Pvp.PvpSeasonCountKey, PVPLogic.SeasonId);
        }
    }
}
