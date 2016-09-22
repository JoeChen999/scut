using System;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.LobbyServer
{
    public class RLPvpResultHandler : RemoteStruct
    {
        private RLPvpResult m_RequestPacket = new RLPvpResult();
        public RLPvpResultHandler(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {

        }

        protected override bool Check()
        {
            m_RequestPacket = ProtoBufUtils.Deserialize<RLPvpResult>(Convert.FromBase64String(paramGetter.RequestPackage.Params["Data"]));
            return true;
        }

        protected override void TakeRemote()
        {
            PlayerSinglePvpLogic psp = new PlayerSinglePvpLogic();
            for (int i = 0; i < m_RequestPacket.PlayerIds.Count; i++)
            {
                psp.SetUser(m_RequestPacket.PlayerIds[i]);
                //psp.DealPvpResult(m_RequestPacket.HasWon[i]);
            }
            PlayerSinglePvpLogic.RefreshRank();
        }

        protected override void BuildPacket()
        {

        }
    }
}
