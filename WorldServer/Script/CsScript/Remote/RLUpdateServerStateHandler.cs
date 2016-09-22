using System;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.WorldServer
{
    public class RLUpdateServerStateHandler : RemoteStruct
    {
        public RLUpdateServerStateHandler(ActionGetter paramGetter, MessageStructure response)
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

            string serverName = session.ProxyId;
            int roomCount = int.Parse(paramGetter.RequestPackage.Params["RoomCount"]);
            float cpuLoad = int.Parse(paramGetter.RequestPackage.Params["CpuLoad"]) / 1000f;
            float memoryLoad = int.Parse(paramGetter.RequestPackage.Params["MemoryLoad"]) / 1000f;
            RoomServerManager.UpdateServerState(serverName, roomCount, cpuLoad, memoryLoad);
        }

        protected override void BuildPacket()
        {
        }
    }
}
