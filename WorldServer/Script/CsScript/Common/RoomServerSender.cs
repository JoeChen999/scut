using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.RPC.IO;
using ZyGames.Framework.RPC.Service;

namespace Genesis.GameServer.WorldServer
{
    public class RoomServerSender
    {
        private static readonly IDictionary<int, RemoteService> m_TcpRemotes = new CacheDictionary<int, RemoteService>();
        public RoomServerSender(int serverId)
        {
        }

        public static void AddServer(int serverId, string ip, int port)
        {
            RemoteService remoteService = RemoteService.CreateTcpProxy(serverId.ToString(), ip, port, 30 * 1000);
            remoteService.PushedHandle += OnPushCallback;
            m_TcpRemotes.Add(serverId, remoteService);
        }

        public static void RemoveServer(int serverId)
        {
            m_TcpRemotes.Remove(serverId);
        }

        public static void Broadcast(string routePath, RequestParam param, Action<RemotePackage> callback)
        {
            foreach (RemoteService remoteService in m_TcpRemotes.Values)
            {
                remoteService.Call(routePath, param, callback);
            }
        }

        public static void Send(int serverId, string routePath, RequestParam param, Action<RemotePackage> callback)
        {
            m_TcpRemotes[serverId].Call(routePath, param, callback);
        }

        public static void Send(int serverId, string routePath, IRemoteServerPacket param, Action<RemotePackage> callback)
        {
            m_TcpRemotes[serverId].Call(routePath, param, callback);
        }

        private static void OnPushCallback(object sender, RemoteEventArgs e)
        {
            byte[] data = e.Data;
        }

        public static byte[] GetData(object source)
        {
            byte[] message = source as byte[];
            int Length = message.Length - 4;
            byte[] data = new byte[Length];
            Array.Copy(message, 4, data, 0, Length);
            return data;
        }
    }
}
