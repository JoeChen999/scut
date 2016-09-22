using System;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.WorldServer
{
    public class LWCancelSinglePvpMatchingHandler : RemoteStruct
    {
        private LWCancelSinglePvpMatching m_RequestPacket;
        private WLCancelSinglePvpMatching m_ResponsePacket = new WLCancelSinglePvpMatching();
        public LWCancelSinglePvpMatchingHandler(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {

        }

        protected override bool Check()
        {
            m_RequestPacket = ProtoBufUtils.Deserialize<LWCancelSinglePvpMatching>(Convert.FromBase64String(paramGetter.RequestPackage.Params["Data"]));
            return true;
        }

        protected override void TakeRemote()
        {
            m_ResponsePacket.Success = PVPLogic.StopSingleMatch(m_RequestPacket.PlayerId);
        }

        protected override void BuildPacket()
        {
            response.PushIntoStack(m_ResponsePacket, false);
        }
    }
}
