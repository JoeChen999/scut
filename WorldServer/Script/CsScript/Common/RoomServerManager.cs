using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;

namespace Genesis.GameServer.WorldServer
{
    public static class RoomServerManager
    {
        private const float TimeoutSeconds = 60f;

        static RoomServerManager()
        {

        }
        public static void InitRoomList()
        {
            for (int i = 1; i <= GameConsts.MaxRoomCount; i++)
            {
                RoomList rl = new RoomList();
                rl.Id = i;
            }
        }
        public static void InitRoomServerList()
        {
            var oldCache = CacheSet.RoomServerCache.FindAll();
            foreach (RoomServer serverinfo in oldCache)
            {
                CacheSet.RoomServerCache.Delete(serverinfo);
            }
        }

        public static bool RegisterServer(int serverId, string ip, string host, int port)
        {
            RoomServer roomServer = CacheSet.RoomServerCache.FindKey(serverId);
            if (roomServer != null)
            {
//                 if (roomServer.State == (int)RoomServerState.Normal)
//                 {
//                     return false;
//                 }

                CacheSet.RoomServerCache.RemoveCache(roomServer);
                RoomServerSender.RemoveServer(serverId);
            }

            roomServer = new RoomServer();
            roomServer.Id = serverId;
            roomServer.IP = ip;
            roomServer.Port = port;
            roomServer.Host = host;
            roomServer.LastUpdatedTime = DateTime.UtcNow.Ticks;
            CacheSet.RoomServerCache.Add(roomServer);

            RoomServerSender.AddServer(serverId, ip, port);

            return true;
        }

        public static void CheckServerStates(object state)
        {
            List<RoomServer> roomServerList = CacheSet.RoomServerCache.FindAll();
            DateTime now = DateTime.UtcNow;
            foreach (RoomServer roomServer in roomServerList)
            {
                if (now.AddSeconds(-TimeoutSeconds).Ticks >= roomServer.LastUpdatedTime)
                {
                    roomServer.ModifyLocked(() =>
                    {
                        roomServer.State = RoomServerState.Exception;
                    });

                    TraceLog.ReleaseWriteFatal(string.Format("No response on server '{0}', ip '{1}:{2}'.", roomServer.Id.ToString(), roomServer.IP, roomServer.Port.ToString()));
                }
            }
        }

        public static void UpdateServerState(string name, int roomCount, float cpuLoad, float memoryLoad)
        {
            RoomServer roomServer = CacheSet.RoomServerCache.FindKey(name);
            if (roomServer == null)
            {
                TraceLog.ReleaseWriteFatal(string.Format("Can not find server '{0}'.", name));
                return;
            }

            roomServer.ModifyLocked(() =>
            {
                roomServer.State = RoomServerState.Normal;
                roomServer.LastUpdatedTime = DateTime.UtcNow.Ticks;
                roomServer.RoomCount = roomCount;
                roomServer.CpuLoad = cpuLoad;
                roomServer.MemoryLoad = memoryLoad;
            });
        }

        public static RoomServer GetLowestLoadedRoomServer()
        {
            var rooms = CacheSet.RoomServerCache.FindAll();
            return rooms[0];
        }
    }
}
