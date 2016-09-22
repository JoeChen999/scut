using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerCoinChance : BaseEntity, IPlayerChance
    {
        public PlayerCoinChance()
        {
            UnopenedChanceRewards = new CacheDictionary<int, DropItem>();
            OpenedChanceRewards = new CacheDictionary<int, DropItem>();
        }

        public int ChanceCount
        {
            get { return 10; }
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int TotalFreeCount
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public long NextFreeTime
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public long NextRefreshTime
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public CacheDictionary<int,DropItem> UnopenedChanceRewards
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public CacheDictionary<int, DropItem> OpenedChanceRewards
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }

    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerMoneyChance : BaseEntity, IPlayerChance
    {
        public PlayerMoneyChance()
        {
            UnopenedChanceRewards = new CacheDictionary<int, DropItem>();
            OpenedChanceRewards = new CacheDictionary<int, DropItem>();
        }

        public int ChanceCount
        {
            get { return 10; }
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int TotalFreeCount
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public long NextFreeTime
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public long NextRefreshTime
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public CacheDictionary<int, DropItem> UnopenedChanceRewards
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public CacheDictionary<int, DropItem> OpenedChanceRewards
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }

    public interface IPlayerChance
    {
        int ChanceCount { get; }
        int UserId { get; set; }
        int TotalFreeCount { get; set; } 
        long NextFreeTime { get; set; }
        long NextRefreshTime { get; set; }

        CacheDictionary<int, DropItem> UnopenedChanceRewards { get; set; }
        CacheDictionary<int, DropItem> OpenedChanceRewards { get; set; }
    }
}
