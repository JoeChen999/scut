using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerMeridian : BaseEntity
    {
        public PlayerMeridian()
            : base(false)
        {
            UnlockedMeridians = new CacheDictionary<int, Meridian>();
        }

        [ProtoMember(1)]
        [EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public CacheDictionary<int, Meridian> UnlockedMeridians { get; set; }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }

    [ProtoContract]
    public class Meridian : EntityChangeEvent
    {
        public Meridian()
        {
            UnlockedStars = new CacheList<int>();
        }

        [ProtoMember(1)]
        [EntityField]
        public CacheList<int> UnlockedStars { get; set; }
    }
}
