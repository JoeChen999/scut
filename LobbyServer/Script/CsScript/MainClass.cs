using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Message;
using ZyGames.Framework.Game.Runtime;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Redis;
using ZyGames.Framework.RPC.IO;
using ZyGames.Framework.RPC.Sockets;


namespace Genesis.GameServer.LobbyServer
{
    public class MainClass : GameSocketHost
    {
        public MainClass()
        {
            GameEnvironment.Setting.ActionDispatcher = new CustomActionDispatcher();
        }
        protected override void OnStartAffer()
        {
            LobbyServerUtils.InitServer();
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
            if (session.UserId != 0)
            {
                PlayerLogic p = new PlayerLogic();
                p.SetUser(session.UserId);
                p.OffLine();
                PVPLogic.StopSingleMatch(session.UserId);
                PlayerArenaLogic pa = new PlayerArenaLogic();
                pa.SetUser(session.UserId);
                if(pa.MyArena.EnemyId > 0)
                {
                    int token;
                    pa.EndBattle(pa.MyArena.EnemyId, false, false, out token);
                }   
            }
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
            if(actionGetter.GetActionId() != 1034){
                TraceLog.WriteInfo("Client {0}:{1} request action {2}.", actionGetter.GetSession().UserId, actionGetter.GetSession().RemoteAddress, actionGetter.GetActionId());
            }
        }
    }
}
