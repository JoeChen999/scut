using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerVipShopLogic
    {
        private int m_UserId;
        private PlayerVipShop m_Shop;
        public PlayerVipShopLogic()
        {

        }

        public void SetUser(int userId)
        {
            m_Shop = CacheSet.PlayerVipShopCache.FindKey(userId.ToString(), userId);
            m_UserId = userId;
            if (m_Shop == null)
            {
                m_Shop = new PlayerVipShop();
                m_Shop.UserId = userId;
                m_Shop.NextRefreshTime = GetNextRefreshTime();
                CacheSet.PlayerVipShopCache.Add(m_Shop);
                ResetShopItems();
            }
        }

        public PlayerVipShop GetShopInfo()
        {
            if (DateTime.UtcNow.Ticks > m_Shop.NextRefreshTime)
            {
                m_Shop.NextRefreshTime = GetNextRefreshTime();
                ResetShopItems();
            }
            return m_Shop;
        }

        public void ResetShopItems()
        {
            m_Shop.ShopItems.Clear();
            var MoneyShopItems = GetNewShopItem((int)GiftItemType.Money);
            m_Shop.ShopItems.AddRange(MoneyShopItems);
            var CoinShopItems = GetNewShopItem((int)GiftItemType.Coin);
            m_Shop.ShopItems.AddRange(CoinShopItems);
            var SpiritShopItems = GetNewShopItem((int)GiftItemType.Spirit);
            m_Shop.ShopItems.AddRange(SpiritShopItems);
        }

        public bool PurchaseItem(int index, int type, int count)
        {
            var itemData = m_Shop.ShopItems[index];
            if (itemData.ItemType != type || itemData.ItemCount != count || itemData.PurchasedTimes >= 1)
            {
                return false;
            }
            PlayerLogic player = new PlayerLogic();
            player.SetUser(m_UserId);
            switch ((GiftItemType)itemData.CurrencyType)
            {
                case GiftItemType.Money:
                    if (!player.DeductMoney(itemData.OriginalPrice))
                    {
                        return false;
                    }
                    break;
                case GiftItemType.Coin:
                    if (!player.DeductCoin(itemData.OriginalPrice))
                    {
                        return false;
                    }
                    break;
                case GiftItemType.Spirit:
                    if (!player.DeductSpirit(itemData.OriginalPrice))
                    {
                        return false;
                    }
                    break;
                default:
                    break;
            }
            m_Shop.ShopItems[index].PurchasedTimes += 1;
            return true;
        }

        private List<ShopItem> GetNewShopItem(int type)
        {
            List<ShopItem> shopItemList = new List<ShopItem>();
            var shopData = CacheSet.VipShopTable.GetAllData(t => t.CurrencyType == type);
            var indexes = GameUtils.RandomChoose(0, shopData.Count - 1, GameConsts.Shop.ShopItemCount[type]);
            foreach (var index in indexes)
            {
                var shopItem = shopData[index];
                ShopItem si = new ShopItem()
                {
                    ItemType = shopItem.DropItemId,
                    ItemCount = shopItem.DropItemCount,
                    PurchasedTimes = 0,
                    CurrencyType = type,
                    OriginalPrice = shopItem.CurrencyPrice,
                    DiscountedPrice = shopItem.CurrencyPrice
                };
                shopItemList.Add(si);
            }
            return shopItemList;
        }

        private static long GetNextRefreshTime()
        {
            DateTime dt = DateTime.UtcNow.AddHours(1);
            DateTime nextRefreshTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 30, 0);
            return nextRefreshTime.Ticks;
        }
    }
}
