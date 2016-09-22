using System;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;
using ZyGames.Framework.Common.Serialization;
using Genesis.GameServer.CommonLibrary;

namespace Genesis.GameServer.WorldServer
{
    public class RLRegisterRoomServerHandler : RemoteStruct
    {
        public RLRegisterRoomServerHandler(ActionGetter paramGetter, MessageStructure response)
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
            string host = paramGetter.RequestPackage.Params["Host"];
            int port = int.Parse(paramGetter.RequestPackage.Params["Port"]);
            bool retval = RoomServerManager.RegisterServer(serverId, ip, host, port);
            if (!retval)
            {
                TraceLog.ReleaseWrite("regist server failure [serverId]:{0} [ip]:{1} [port]:{2}", serverId, ip, port);
                session.Close();
                return;
            }
            TraceLog.ReleaseWrite("regist server success [serverId]:{0} [ip]:{1} [port]:{2} [host]:{3}", serverId, ip, port, host);
        }

        protected override void BuildPacket()
        {
        }
    }
}
