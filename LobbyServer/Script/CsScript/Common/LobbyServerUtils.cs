using Genesis.GameServer.CommonLibrary;
using ZyGames.Framework.Common.Timing;
using System.IO;
using System.Collections.Generic;
using System;
using System.Net;
using System.Text;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Game.Com.Rank;
using Newtonsoft.Json;
using ZyGames.Framework.RPC.IO;
using ZyGames.Framework.Common.Log;

namespace Genesis.GameServer.LobbyServer
{
    public static class LobbyServerUtils
    {
        public static void InitServer()
        {
            DataTableLoader.LoadDataTables("Lobby");
            (new SyncTimer(RoomServerManager.CheckServerStates, 60000, 60000)).Start();
            (new SyncTimer(Cronjobs.CheckAndDo, 60000, 60000)).Start();
            (new SyncTimer(UpdateServerLoadToMaster, 60000, 60000)).Start();
            (new SyncTimer(PVPLogic.ExecuteMatch, 10000, 10000)).Start();
            RoomServerManager.InitRoomServerList();
            RoomServerManager.InitRoomList();
            GameConfigs.Reload();
            ArenaRankLogic.InitRankList();
            AllQualitiesOfGears.LoadGears();
            InitRanking();
            RegisterLobbyServer();
        }

        public static void InitRanking()
        {
            int timeOut = 3000;
            RankingFactory.Add(new SinglePvpRanking());
            RankingFactory.Add(new PlayerMightRanking());
            RankingFactory.Start(timeOut);
        }

        public static void CheckAndUpdateDataTable(object state)
        {
            DBProvider db = new DBProvider("Game");
            var versions = db.Select("dataTableVersions", "Name,UsedVersion,LatestVersion","");
            foreach(var version in versions)
            {
                if((int)version["LatestVersion"] > (int)version["UsedVersion"])
                {
                    FileInfo dtfile = new FileInfo("DataTables/"+ (string)version["Name"] + ".txt");
                    DataTableLoader.LoadDataTableFile(dtfile);
                    Dictionary<string, object> sqlparams = new Dictionary<string, object>();
                    sqlparams["UsedVersion"] = version["LatestVersion"];
                    db.Update("dataTableVersions", sqlparams, "Name = '" + version["Name"] + "'");
                }
            }
        }

        public static void UpdateServerLoadToMaster(object state)
        {
            int load = GetOnlinePlayerCount();
            string sid = ConfigUtils.GetSetting("Server.Id");
            string url = ConfigUtils.GetSetting("MasterServer") + "/update/server/load";
            var serverInfo = new ServerInfo()
            {
                ServerId = int.Parse(ConfigUtils.GetSetting("Server.Id")),
                Load = GetOnlinePlayerCount(),
                Rooms = new List<RoomServerInfo>(),
            };
            var rooms = CacheSet.RoomServerCache.FindAll();
            foreach(var room in rooms)
            {
                var roomInfo = new RoomServerInfo()
                {
                    RoomServerId = room.Id,
                    RoomServerIp = room.IP,
                    RoomCount = room.RoomCount,
                    RoomState = room.State,
                };
                serverInfo.Rooms.Add(roomInfo);
            }
            string postData = JsonConvert.SerializeObject(serverInfo);
            string param = string.Format("sid={0}&load={1}", sid, load);
            var res = HttpGet(url, param);
        }

        public static int GetOnlinePlayerCount()
        {
            List<GameSession> onlineSessions = new List<GameSession>();
            onlineSessions.AddRange(GameSession.GetOnlineAll());
            return onlineSessions.Count;
        }

        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public static int GetActivityStatus(DTActivity activityInfo, int level)
        {
            if(activityInfo.UnlockLevel > level)
            {
                return (int)ActivityStatusType.Locked;
            }
            var startTime = Convert.ToDateTime(activityInfo.StartTime);
            var endTime = Convert.ToDateTime(activityInfo.EndTime);

            if (activityInfo.RepeatType == (int)ActivityRepeatType.NotRepeat)
            {
                if (DateTime.UtcNow >= startTime && DateTime.UtcNow <= endTime)
                {
                    return (int)ActivityStatusType.Open;
                }
            }
            else if (activityInfo.RepeatType == (int)ActivityRepeatType.daily)
            {
                startTime = DateTime.Today.Add(new TimeSpan(startTime.Hour, startTime.Minute, startTime.Second));
                endTime = DateTime.Today.Add(new TimeSpan(endTime.Hour, endTime.Minute, endTime.Second));
                if (DateTime.UtcNow >= startTime && DateTime.UtcNow <= endTime)
                {
                    return (int)ActivityStatusType.Open;
                }
            }
            else if (activityInfo.RepeatType == (int)ActivityRepeatType.Weekly)
            {
                var duration = startTime.Ticks - endTime.Ticks;
                if ((DateTime.UtcNow.Ticks - startTime.Ticks) % (TimeSpan.TicksPerDay * 7) <= duration)
                {
                    return (int)ActivityStatusType.Open;
                }
                
            }
            else if (activityInfo.RepeatType > (int)ActivityRepeatType.Monthly)
            {
                if ((DateTime.UtcNow.Day == startTime.Day && DateTime.UtcNow.TimeOfDay >= startTime.TimeOfDay) || (DateTime.UtcNow.Day > startTime.Day && DateTime.UtcNow.Day < endTime.Day) || (DateTime.UtcNow.Day == endTime.Day && DateTime.UtcNow.TimeOfDay <= endTime.TimeOfDay))
                {
                    return (int)ActivityStatusType.Open;
                }
            }
            if (activityInfo.DisplayWhenClose)
            {
                return (int)ActivityStatusType.Close;
            }
            return (int)ActivityStatusType.Hidden;
        }

        private static void RegisterLobbyServer()
        {
            RequestParam param = new RequestParam();
            param.Add("Port", ConfigUtils.GetSetting("Game.Port"));
            WorldServerSender.Send("LWRegisterLobbyServerHandler", param, callback =>
            {
                TraceLog.ReleaseWrite("RegisterLobbyServer OK.");
            });
        }
    }
}
