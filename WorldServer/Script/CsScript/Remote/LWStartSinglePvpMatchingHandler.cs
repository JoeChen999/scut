using System;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.WorldServer
{
    public class LWStartSinglePvpMatchingHandler : RemoteStruct
    {
        private LWStartSinglePvpMatching m_RequestPacket;
        private WLStartSinglePvpMatching m_ResponsePacket = new WLStartSinglePvpMatching();
        public LWStartSinglePvpMatchingHandler(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {

        }

        protected override bool Check()
        {
            m_RequestPacket = ProtoBufUtils.Deserialize<LWStartSinglePvpMatching>(Convert.FromBase64String(paramGetter.RequestPackage.Params["Data"]));
            return true;
        }

        protected override void TakeRemote()
        {
            TraceLog.ReleaseWrite("Player:{0} from Server:{1} start matching", m_RequestPacket.RoomPlayerInfo.PlayerInfo.Id.ToString(), m_RequestPacket.RoomPlayerInfo.LobbyServerId);
            m_ResponsePacket.Success = PVPLogic.StartSingleMatch(m_RequestPacket.RoomPlayerInfo);
        }

        protected override void BuildPacket()
        {
            response.PushIntoStack(m_ResponsePacket, false);
        }
    }
}
