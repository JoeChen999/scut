using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerVipShop : BaseEntity
    {
        public PlayerVipShop()
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
}