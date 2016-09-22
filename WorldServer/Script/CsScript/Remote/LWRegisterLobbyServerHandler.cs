using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.WorldServer
{
    class LWRegisterLobbyServerHandler : RemoteStruct
    {
        public LWRegisterLobbyServerHandler(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {

        }

        protected override bool Check()
        {
            return true;
        }

        protected override void TakeRemote()
        {
            GameSession session = paramGetter.GetSession();
            if (session == null)
            {
                TraceLog.ReleaseWrite("Session is invalid.");
                return;
            }

            int serverId = int.Parse(session.ProxyId);
            string ip = session.RemoteAddress.Split(':')[0];
            int port = int.Parse(paramGetter.RequestPackage.Params["Port"]);
            bool retval = LobbyServerManager.RegisterServer(serverId, ip, port);
            if (!retval)
            {
                TraceLog.ReleaseWrite("regist server failure [serverId]:{0} [ip]:{1} [port]:{2}", serverId, ip, port);
                session.Close();
                return;
            }
            TraceLog.ReleaseWrite("regist server success [serverId]:{0} [ip]:{1} [port]:{2}", serverId, ip, port);
        }

        protected override void BuildPacket()
        {
        }
    }
}
