using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false, 3600)]
    public class PlayerInstanceDrop : BaseEntity
    {
        public PlayerInstanceDrop()
            : base(false)
        {
            ChestList = new CacheList<int>();
        }

        [ProtoMember(1),EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2),EntityField]
        public int InstanceId { get; set; }

        [ProtoMember(3), EntityField]
        public CacheDictionary<int, int> DropList { get; set; }

        [ProtoMember(4), EntityField]
        public CacheList<int> ChestList { get; set; }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }

}

