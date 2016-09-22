using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false, 3600)]
    public class InstanceProgress : BaseEntity
    {
        public InstanceProgress()
            : base(false)
        {
        }
        [ProtoMember(1), EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2), EntityField]
        public CacheDictionary<int, int> Progress { get; set; }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }

}
