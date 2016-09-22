using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.RPC.IO;
using ZyGames.Framework.RPC.Service;

namespace Genesis.GameServer.LobbyServer
{
    public static class WorldServerSender
    {
        private static RemoteService m_TcpRemote;

        static WorldServerSender()
        {
            string worldServerIP = ConfigUtils.GetSetting("Server.WorldIP");
            int worldServerPort = int.Parse(ConfigUtils.GetSetting("Server.WorldPort"));
            m_TcpRemote = RemoteService.CreateTcpProxy(ConfigUtils.GetSetting("Server.Id"), worldServerIP, worldServerPort, 30 * 1000);
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