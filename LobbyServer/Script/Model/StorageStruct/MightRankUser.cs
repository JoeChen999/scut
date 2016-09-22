using ProtoBuf;
using System;
using ZyGames.Framework.Game.Com.Model;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class MightRankUser : RankingItem
    {
        public MightRankUser()
        {
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int Might
        {
            get;
            set;
        }
    }
}
