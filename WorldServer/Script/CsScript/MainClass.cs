using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Timing;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Message;
using ZyGames.Framework.Game.Runtime;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;
using ZyGames.Framework.RPC.Sockets;
using ZyGames.Framework.Game.Com.Rank;

namespace Genesis.GameServer.WorldServer
{
    public class MainClass : GameSocketHost
    {
        public MainClass()
        {
            GameEnvironment.Setting.ActionDispatcher = new CustomActionDispatcher();
        }
        protected override void OnStartAffer()
        {
            (new SyncTimer(PVPLogic.ExecuteMatch, 10000, 10000)).Start();
            InitRanking();
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
            TraceLog.ReleaseWrite("Client '{0}' disconnect OK.", session.RemoteAddress);
        }

        protected override void OnCallRemote(string routePath, ActionGetter actionGetter, MessageStructure response)
        {
            base.OnCallRemote(routePath, actionGetter, response);
        }

        protected override void OnHeartbeat(GameSession session)
        {
            base.OnHeartbeat(session);
            TraceLog.ReleaseWrite("Client '{0}' heart beat.", session.RemoteAddress);
        }

        protected override void OnReceivedBefore(ConnectionEventArgs e)
        {
            base.OnReceivedBefore(e);
        }

        protected override void OnRequested(ActionGetter actionGetter, BaseGameResponse response)
        {
            base.OnRequested(actionGetter, response);
            TraceLog.WriteInfo("Client {0}:{1} request action {2}.", actionGetter.GetSession().UserId, actionGetter.GetSession().RemoteAddress, actionGetter.GetActionId());
        }

        private static void InitRanking()
        {
            int timeOut = 3000;
            RankingFactory.Add(new SinglePvpRanking());
            RankingFactory.Start(timeOut);
        }
    }
}
