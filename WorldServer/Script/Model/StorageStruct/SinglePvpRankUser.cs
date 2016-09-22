using ProtoBuf;
using System;
using ZyGames.Framework.Game.Com.Model;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.WorldServer
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

        [ProtoMember(3), EntityField]
        public int ServerId
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public string Name
        {
            get;
            set;
        }
    }
}
