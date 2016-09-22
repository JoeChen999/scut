using System;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.LobbyServer
{
    public class WLPvpResultHandler : RemoteStruct
    {
        private WLPvpResult m_RequestPacket;
        private LWPvpResult m_ResponsePacket = new LWPvpResult();
        public WLPvpResultHandler(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {

        }

        protected override bool Check()
        {
            m_RequestPacket = ProtoBufUtils.Deserialize<WLPvpResult>(Convert.FromBase64String(paramGetter.RequestPackage.Params["Data"]));
            return true;
        }

        protected override void TakeRemote()
        {
            PlayerSinglePvpLogic psp = new PlayerSinglePvpLogic();
            psp.SetUser(m_RequestPacket.PlayerId);
            psp.DealPvpResult(m_RequestPacket.Result, m_RequestPacket.Score);
            m_ResponsePacket.Score = psp.MyPvp.SinglePvpScore;
            m_ResponsePacket.PlayerId = m_RequestPacket.PlayerId;
            PlayerSinglePvpLogic.RefreshRank();
        }

        protected override void BuildPacket()
        {
            response.PushIntoStack(m_ResponsePacket, false);
        }
    }
}
