using System;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.RoomServer
{
    public class LRCreateNewRoomHandler : RemoteStruct
    {
        private RLCreateNewRoom m_ResponsePacket = new RLCreateNewRoom();
        private LRCreateNewRoom m_RequestPacket = new LRCreateNewRoom();
        public LRCreateNewRoomHandler(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {
        }

        protected override bool Check()
        {
            m_RequestPacket = ProtoBufUtils.Deserialize<LRCreateNewRoom>(Convert.FromBase64String(paramGetter.RequestPackage.Params["Data"]));
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
            if (!session.IsSocket)
            {
                return;
            }

            RoomManager roomManager = RoomManager.GetInstance(m_RequestPacket);
            m_ResponsePacket.RoomId = roomManager.RoomInfo.Id;
            m_ResponsePacket.InstanceId = roomManager.RoomInfo.InstanceId;
        }

        protected override void BuildPacket()
        {
            response.PushIntoStack(m_ResponsePacket, false);
        }
    }
}
