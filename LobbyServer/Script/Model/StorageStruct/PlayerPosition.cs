using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerNearbyPosition : BaseEntity
    {
        public PlayerNearbyPosition()
            : base(false)
        {
            Visitors = new CacheList<int>();
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public CacheList<int> Visitors
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public float MyPositionX
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public float MyPositionY
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return UserId;
        }
    }
}
