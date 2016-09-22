using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public class PushSystemAnnouncementCron : ICronjob
    {
        private const string Database = "Game";
        private const string Table = "announcement";
        private enum AnnounceType
        {
            SystemNotice = 0,
            Advertisement = 1,
        };

        public List<DateTime> ExecuteTimes
        {
            get
            {
                return new List<DateTime>();
            }
        }

        public PushSystemAnnouncementCron()
        {

        }

        public void Execute()
        {
            DBProvider db = new DBProvider("Game");
            string now = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            string condition = string.Format("BeginTime < '{0}' and ExpireTime > '{1}'", now, now);
            var announces = db.Select(Table, "Id,Type,Message,BeginTime,IntervalMinutes", condition);
            foreach(var announce in announces)
            {
                if ((int)announce["IntervalMinutes"] == 0 && (int)announce["Pushed"] == 0)
                {
                    var dt = (DateTime)announce["BeginTime"];
                    if (DateTime.UtcNow.Ticks - dt.Ticks <= TimeSpan.TicksPerMinute)
                    {
                        if ((int)announce["Type"] == (int)AnnounceType.SystemNotice)
                        {
                            AnnouncementLogic.PushSystemNotice((string)announce["Message"]);
                        }
                        else
                        {
                            AnnouncementLogic.PushAdvertisement((string)announce["Message"]);
                        }
                    }
                }
                else if ((int)announce["IntervalMinutes"] != 0)
                {
                    var dt = (DateTime)announce["BeginTime"];
                    if (((DateTime.UtcNow.Ticks - dt.Ticks) / TimeSpan.TicksPerMinute) % (int)announce["IntervalMinutes"] == 0)
                    {
                        if ((int)announce["Type"] == (int)AnnounceType.SystemNotice)
                        {
                            AnnouncementLogic.PushSystemNotice((string)announce["Message"]);
                        }
                        else
                        {
                            AnnouncementLogic.PushAdvertisement((string)announce["Message"]);
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
