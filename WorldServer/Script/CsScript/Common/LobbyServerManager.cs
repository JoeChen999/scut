using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;

namespace Genesis.GameServer.WorldServer
{
    public static class LobbyServerManager
    {
        private const float TimeoutSeconds = 60f;

        static LobbyServerManager()
        {

        }

        public static void InitLobbyServerList()
        {
            var oldCache = CacheSet.LobbyServerCache.FindAll();
            foreach (LobbyServer serverinfo in oldCache)
            {
                CacheSet.LobbyServerCache.Delete(serverinfo);
            }
        }
        public static bool RegisterServer(int serverId, string ip, int port)
        {
            LobbyServer lobbyServer = CacheSet.LobbyServerCache.FindKey(serverId);
            if (lobbyServer != null)
            {
                //if (roomServer.State == (int)RoomServerState.Normal)
                //{
                //    return false;
                //}

                CacheSet.LobbyServerCache.RemoveCache(lobbyServer);
                LobbyServerSender.RemoveServer(serverId);
            }

            lobbyServer = new LobbyServer();
            lobbyServer.Id = serverId;
            lobbyServer.IP = ip;
            lobbyServer.Port = port;
            lobbyServer.LastUpdatedTime = DateTime.UtcNow.Ticks;
            CacheSet.LobbyServerCache.Add(lobbyServer);

            LobbyServerSender.AddServer(serverId, ip, port);

            return true;
        }

        public static void CheckServerStates(object state)
        {
            List<LobbyServer> lobbyServerList = CacheSet.LobbyServerCache.FindAll();
            DateTime now = DateTime.UtcNow;
            foreach (LobbyServer lobbyServer in lobbyServerList)
            {
                if (now.AddSeconds(-TimeoutSeconds).Ticks >= lobbyServer.LastUpdatedTime)
                {
                    lobbyServer.ModifyLocked(() =>
                    {
                        lobbyServer.State = RoomServerState.Exception;
                    });

                    TraceLog.ReleaseWriteFatal(string.Format("No response on server '{0}', ip '{1}:{2}'.", lobbyServer.Id.ToString(), lobbyServer.IP, lobbyServer.Port.ToString()));
                }
            }
        }

        public static void UpdateServerState(string name, float cpuLoad, float memoryLoad)
        {
            LobbyServer lobbyServer = CacheSet.LobbyServerCache.FindKey(name);
            if (lobbyServer == null)
            {
                TraceLog.ReleaseWriteFatal(string.Format("Can not find server '{0}'.", name));
                return;
            }

            lobbyServer.ModifyLocked(() =>
            {
                lobbyServer.State = RoomServerState.Normal;
                lobbyServer.LastUpdatedTime = DateTime.UtcNow.Ticks;
                lobbyServer.CpuLoad = cpuLoad;
                lobbyServer.MemoryLoad = memoryLoad;
            });
        }
    }
}
