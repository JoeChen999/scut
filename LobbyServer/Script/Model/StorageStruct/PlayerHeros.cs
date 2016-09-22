using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerHeros : BaseEntity
    {
        public PlayerHeros()
            : base(false)
        {
            Heros = new CacheDictionary<int, Hero>();     
        }
        [ProtoMember(1)]
        [EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public CacheDictionary<int, Hero> Heros { get; set; }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }
}
