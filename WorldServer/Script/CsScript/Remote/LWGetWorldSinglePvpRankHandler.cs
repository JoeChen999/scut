using System;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.WorldServer
{
    public class LWGetWorldSinglePvpRankHandler : RemoteStruct
    {
        private LWGetWorldSinglePvpRank m_RequestPacket;
        private WLGetWorldSinglePvpRank m_ResponsePacket = new WLGetWorldSinglePvpRank();
        public LWGetWorldSinglePvpRankHandler(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {

        }

        protected override bool Check()
        {
            m_RequestPacket = ProtoBufUtils.Deserialize<LWGetWorldSinglePvpRank>(Convert.FromBase64String(paramGetter.RequestPackage.Params["Data"]));
            return true;
        }

        protected override void TakeRemote()
        {
            var rank = RankingFactory.Get<SinglePvpRankUser>("SinglePvpRanking");
            var players = rank.GetRange(0, 100);
            foreach(var player in players)
            {
                m_ResponsePacket.PlayerInfo.Add(new PBPlayerInfo() {
                    Id = player.UserId,
                    Name = player.Name,
                });
                m_ResponsePacket.ServerId.Add(player.ServerId);
                m_ResponsePacket.Score.Add(player.Score);
            }
            var me = rank.Find(t => t.ServerId == m_RequestPacket.ServerId && t.UserId == m_RequestPacket.UserId);
            if(me == null)
            {
                m_ResponsePacket.MyRank = -1;
                m_ResponsePacket.MyScore = 0;
            }
            else
            {
                m_ResponsePacket.MyRank = me.RankId;
                m_ResponsePacket.MyScore = me.Score;
            }
        }

        protected override void BuildPacket()
        {
            response.PushIntoStack(m_ResponsePacket, false);
        }
    }
}
