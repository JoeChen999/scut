using System;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Timing;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Runtime;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;
using ZyGames.Framework.RPC.Sockets;
using Genesis.GameServer.CommonLibrary;

namespace Genesis.GameServer.RoomServer
{
    public class MainClass : GameSocketHost
    {
        public MainClass()
        {
            GameEnvironment.Setting.ActionDispatcher = new CustomActionDispatcher();
        }

        protected override void OnStartAffer()
        {
            DataTableLoader.LoadDataTables("Room");
            GameConfigs.Reload();
            RegisterRoomServer();
            (new SyncTimer(UpdateServerState, 30000, 30000)).Start();
            GameUtils.PrintBuddha();
        }

        protected override void OnServiceStop()
        {
            GameEnvironment.Stop();
        }

        protected override void OnConnectCompleted(object sender, ConnectionEventArgs e)
        {
            base.OnConnectCompleted(sender, e);
            
            TraceLog.ReleaseWrite("Client '{0}' connected OK.", e.Socket.RemoteEndPoint);
        }

        protected override void OnDisconnected(GameSession session)
        {
            base.OnDisconnected(session);
            if (session.User == null)
            {
                return;
            }
            var user = session.User as RoomSessionUser;
            RoomManager rm = RoomManager.GetInstance(user.RoomId);
            if(rm != null)
            {
                rm.PlayerGaveUp(user.UserId);
            }
            TraceLog.ReleaseWrite("Client '{0}' disconnect OK.", session.RemoteAddress);
        }

        protected override void OnHeartbeat(GameSession session)
        {
            base.OnHeartbeat(session);

            TraceLog.ReleaseWrite("Client '{0}' heart beat.", session.RemoteAddress);
        }

        protected override void OnReceivedBefore(ConnectionEventArgs e)
        {
            base.OnReceivedBefore(e);

            TraceLog.ReleaseWrite(System.Text.Encoding.UTF8.GetString(e.Data));
        }

        protected override void OnRequested(ActionGetter actionGetter, BaseGameResponse response)
        {
            base.OnRequested(actionGetter, response);

            TraceLog.WriteInfo("Client {0} request action {1}.", actionGetter.GetSessionId(), actionGetter.GetActionId());
        }

        private void RegisterRoomServer()
        {
            RequestParam param = new RequestParam();
            param.Add("Port", ConfigUtils.GetSetting("Game.Port"));
            param.Add("Host", ConfigUtils.GetSetting("Game.Host"));
            LobbyServerSender.Send("RLRegisterRoomServerHandler", param, callback =>
            {
                TraceLog.ReleaseWrite("RegisterRoomServer OK.");
            });
        }

        private void UpdateServerState(object state)
        {
            RequestParam param = new RequestParam();
            param.Add("RoomCount", CacheSet.RoomCache.Count);
            param.Add("CpuLoad", 20 * 1000);
            param.Add("MemoryLoad", 20 * 1000);
            LobbyServerSender.Send("RLUpdateServerStateHandler", param, callback =>
            {
                TraceLog.ReleaseWrite("UpdateServerState OK.");
            });
        }
    }
}
