using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerCosmosCrack : BaseEntity
    {
        public PlayerCosmosCrack()
        {
            ChosenInstance = new CacheDictionary<int, CosmosCrackInstance>();
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2), EntityField]
        public int PassedRoundCount { get; set; }

        [ProtoMember(3), EntityField]
        public CacheDictionary<int, CosmosCrackInstance> ChosenInstance { get; set; }

        [ProtoMember(4), EntityField]
        public long LastRefreshTime { get; set; }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }

    [ProtoContract]
    public class CosmosCrackInstance : EntityChangeEvent
    {
        public CosmosCrackInstance()
        {
            RewardItem = new CacheDictionary<int, int>();
        }

        [ProtoMember(1)]
        public int RewardLevel { get; set; }

        [ProtoMember(2)]
        public CacheDictionary<int, int> RewardItem { get; set; }
    }
}
