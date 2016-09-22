using ProtoBuf;
using System;
using ZyGames.Framework.Game.Com.Model;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class SinglePvpRankUser : RankingItem
    {
        public SinglePvpRankUser()
        {
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int Score
        {
            get;
            set;
        }
    }
}
