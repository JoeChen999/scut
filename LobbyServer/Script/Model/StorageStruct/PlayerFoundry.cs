using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerFoundry : BaseEntity
    {
        public PlayerFoundry()
        {
            HasReceivedRewards = new CacheList<bool>();
            CanReceiveRewards = new CacheList<bool>();
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2), EntityField]
        public int CurrentRoomId { get; set; }

        [ProtoMember(3), EntityField]
        public int FoundryCount { get; set; }

        [ProtoMember(4), EntityField]
        public long NextFoundryTime { get; set; }

        [ProtoMember(5), EntityField]
        public CacheList<bool> HasReceivedRewards { get; set; }

        [ProtoMember(6), EntityField]
        public CacheList<bool> CanReceiveRewards { get; set; }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }
}
