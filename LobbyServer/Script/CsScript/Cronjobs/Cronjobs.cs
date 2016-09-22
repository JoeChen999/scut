using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.GameServer.LobbyServer
{
    public interface ICronjob
    {
        List<DateTime> ExecuteTimes{ get; }
        void Execute();
    }

    public static class Cronjobs
    {
        private static List<ICronjob> m_DailyCronjobs = new List<ICronjob>()
        {
            new ResetFoundryRoomCron(),
            new ResetArenaChallengeCountCron(),
            new PushSystemAnnouncementCron(),
        };
        private static List<ICronjob> m_WeeklyCronjobs = new List<ICronjob>()
        {
            new InitPvpSeasonCron()
        };

        public static void CheckAndDo(object state)
        {
            foreach (var cron in m_DailyCronjobs)
            {
                if (cron.ExecuteTimes.Count == 0)
                {
                    cron.Execute();
                }
                else
                {
                    foreach (DateTime time in cron.ExecuteTimes)
                    {
                        if (time.Hour == DateTime.UtcNow.Hour && time.Minute == DateTime.UtcNow.Minute)
                        {
                            cron.Execute();
                        }
                    }
                }
            }

            foreach (var cron in m_WeeklyCronjobs)
            {
                if (cron.ExecuteTimes.Count == 0)
                {
                    break;
                }
                else
                {
                    foreach (DateTime time in cron.ExecuteTimes)
                    {
                        if (time.DayOfWeek == DateTime.UtcNow.DayOfWeek && time.Hour == DateTime.UtcNow.Hour && time.Minute == DateTime.UtcNow.Minute)
                        {
                            cron.Execute();
                        }
                    }
                }
            }
        }
    }

}
