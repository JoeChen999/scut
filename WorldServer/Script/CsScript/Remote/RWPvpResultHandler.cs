using Genesis.GameServer.CommonLibrary;
using System;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.WorldServer
{
    public class RWPvpResultHandler : RemoteStruct
    {
        private RWPvpResult m_RequestPacket = new RWPvpResult();
        public RWPvpResultHandler(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {

        }

        protected override bool Check()
        {
            m_RequestPacket = ProtoBufUtils.Deserialize<RWPvpResult>(Convert.FromBase64String(paramGetter.RequestPackage.Params["Data"]));
            return true;
        }

        protected override void TakeRemote()
        {
            PvpPlayer player1 = null;
            PvpPlayer player2 = null;
            if (m_RequestPacket.PlayerIds.Count < 2)
            {
                return;
            }
            for (int i = 0; i < m_RequestPacket.PlayerIds.Count; i++)
            {
                PvpPlayer p = CacheSet.PvpPlayerCache.FindKey(m_RequestPacket.PlayerIds[i], m_RequestPacket.ServerId[i]);
                if (p == null)
                {
                    return;
                }
                if (i == 0) player1 = p;
                else if (i == 1) player2 = p;
            }
            int p1score = PVPLogic.GetDeltaScore(player1.Score, player2.Score, m_RequestPacket.Result[0]);
            int p2score = PVPLogic.GetDeltaScore(player2.Score, player1.Score, m_RequestPacket.Result[1]);
            WLPvpResult package = new WLPvpResult();
            for (int i = 0; i < m_RequestPacket.PlayerIds.Count; i++)
            {
                package.PlayerId = m_RequestPacket.PlayerIds[i];
                package.Result = m_RequestPacket.Result[i];
                package.Score = i == 0 ? p1score : p2score;
                LobbyServerSender.Send(m_RequestPacket.ServerId[i], "WLPvpResultHandler", package, delegate (RemotePackage callback) {
                    LWPvpResult res = RemoteCommunication.ParseRemotePackage<LWPvpResult>(callback.Message as byte[]);
                    if (res.PlayerId == player1.Id) player1.Score = res.Score;
                    else if (res.PlayerId == player2.Id) player2.Score = res.Score;
                    RankingFactory.Get<SinglePvpRankUser>("SinglePvpRanking").Refresh();
                });
            }
        }

        protected override void BuildPacket()
        {

        }
    }
}
