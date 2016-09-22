using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerDefaultShop : BaseEntity
    {
        public PlayerDefaultShop()
        {
            ShopItems = new CacheList<ShopItem>();
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId
        { 
            get; 
            set; 
        }

        [ProtoMember(2), EntityField]
        public long NextRefreshTime
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public CacheList<ShopItem> ShopItems
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return UserId;
        }
    }

    [ProtoContract]
    public class ShopItem : EntityChangeEvent
    {
        public ShopItem()
        {
        }

        [ProtoMember(1)]
        public int ItemType { get; set; }

        [ProtoMember(2)]
        public int ItemCount { get; set; }

        [ProtoMember(3)]
        public int PurchasedTimes { get; set; }

        [ProtoMember(4)]
        public int CurrencyType { get; set; }

        [ProtoMember(5)]
        public int OriginalPrice { get; set; }

        [ProtoMember(6)]
        public int DiscountedPrice { get; set; }
    }
}
