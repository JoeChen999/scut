using System;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.RPC.IO;
using ZyGames.Framework.RPC.Service;

namespace Genesis.GameServer.RoomServer
{
    public static class LobbyServerSender
    {
        private static RemoteService m_TcpRemote;

        static LobbyServerSender()
        {
            string lobbyServerIP = ConfigUtils.GetSetting("Server.LobbyIP");
            int lobbyServerPort = int.Parse(ConfigUtils.GetSetting("Server.LobbyPort"));
            m_TcpRemote = RemoteService.CreateTcpProxy(ConfigUtils.GetSetting("Server.Id"), lobbyServerIP, lobbyServerPort, 30 * 1000);
            m_TcpRemote.PushedHandle += OnPushCallback;
        }

        public static void Send(string routePath, RequestParam param, Action<RemotePackage> callback)
        {
            m_TcpRemote.Call(routePath, param, callback);
        }
        public static void Send(string routePath, IRemoteServerPacket param, Action<RemotePackage> callback)
        {
            m_TcpRemote.Call(routePath, param, callback);
        }

        private static void OnPushCallback(object sender, RemoteEventArgs e)
        {
        }
    }
}
