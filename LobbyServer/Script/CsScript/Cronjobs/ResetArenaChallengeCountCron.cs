using System;
using System.Collections.Generic;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.LobbyServer
{
    public class ResetArenaChallengeCountCron : ICronjob
    {
        public List<DateTime> ExecuteTimes
        {
            get
            {
                return new List<DateTime>()
                {
                    new DateTime(2015, 1, 1, 20, 0, 0),
                };
            }
        }

        public ResetArenaChallengeCountCron()
        {

        }

        public void Execute()
        {
            var arenaData = CacheSet.ArenaRankCache.FindAll();
            PlayerArenaLogic pa = new PlayerArenaLogic();
            foreach (var arena in arenaData)
            {
                pa.SetUser(arena.PlayerId);
                pa.ResetChallengeCount();
                if (DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday)
                {
                    pa.ResetLivenessReward();
                }
            }
        }
    }
}
