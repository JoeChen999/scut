using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerPackageLogic
    {
        private PlayerPackage m_Package;
        private int m_UserId;
        public PlayerPackageLogic()
        {
            m_UserId = 0;
            m_Package = null;
        }

        public void SetUser(int userId)
        {
            m_UserId = userId;
            m_Package = CacheSet.playerPackageCache.FindKey(m_UserId.ToString(), userId);
            if (m_Package == null)
            {
                m_Package = new PlayerPackage();
                m_Package.UserId = m_UserId;
                CacheSet.playerPackageCache.Add(m_Package);
            }
        }

        public PlayerPackage MyPackage
        {
            get { return m_Package; }
            set { m_Package = value; }
        }

        public bool CheckPackageSlot(IDictionary<int, int> gotItems)
        {
            foreach(var item in gotItems)
            {
                if (IsItem(item.Key) && m_Package.Inventories.ContainsKey(item.Key) && m_Package.Inventories[item.Key] + item.Value > GameConfigs.GetInt("Inventory_Item_Max_Count", 9999))
                {
                    return false;
                }
                else if (IsItem(item.Key) && !m_Package.Inventories.ContainsKey(item.Key) && m_Package.Inventories.Count >= GameConfigs.GetInt("Inventory_Slots_Per_Tab", 200))
                {
                    return false;
                }
                else if (IsGear(item.Key) && m_Package.Gears.Count >= GameConfigs.GetInt("Inventory_Slots_Per_Tab", 200))
                {
                    return false;
                }
                else if (IsSoul(item.Key) && m_Package.Souls.Count >= GameConfigs.GetInt("Inventory_Slots_Per_Tab", 200))
                {
                    return false;
                }
            }
            return true;
        }

        public void AddInventory(ItemListItem item)
        {
            if (m_Package.Inventories.ContainsKey(item.Id))
            {
                m_Package.Inventories[item.Id] += item.Count;
                if (m_Package.Inventories[item.Id] > GameConfigs.GetInt("Inventory_Item_Max_Count", 9999))
                {
                    int count = m_Package.Inventories[item.Id] - GameConfigs.GetInt("Inventory_Item_Max_Count", 9999);
                    m_Package.Inventories[item.Id] = GameConfigs.GetInt("Inventory_Item_Max_Count", 9999);
                    PlayerMailLogic pm = new PlayerMailLogic();
                    pm.SetUser(m_UserId);
                    pm.AddNewMail("package full", item.Id, item.Count);
                    pm.SendNotification();
                }
            }
            else
            {
                m_Package.Inventories.Add(item.Id, item.Count);
            }
        }

        public void AddGear(int gear, int gearType)
        {
            if(m_Package.Gears.Count >= GameConfigs.GetInt("Inventory_Slots_Per_Tab", 200))
            {
                PlayerMailLogic pm = new PlayerMailLogic();
                pm.SetUser(m_UserId);
                pm.AddNewMail("package full", gearType, 1);
                pm.SendNotification();
            }
            m_Package.Gears.Add(gear, gearType);
        }

        public void AddSoul(int soulId, int soulType)
        {
            if (m_Package.Souls.Count >= GameConfigs.GetInt("Inventory_Slots_Per_Tab", 200))
            {
                PlayerMailLogic pm = new PlayerMailLogic();
                pm.SetUser(m_UserId);
                pm.AddNewMail("package full", soulType, 1);
                pm.SendNotification();
            }
            m_Package.Souls.Add(soulId, soulType);
        }

        public void AddEpigraph(int Type, int Level)
        {
            if (m_Package.Epigraphs.Count >= GameConfigs.GetInt("Inventory_Slots_Per_Tab", 200))
            {
                PlayerMailLogic pm = new PlayerMailLogic();
                pm.SetUser(m_UserId);
                pm.AddNewMail("package full", Type, 1);
                pm.SendNotification();
            }
            m_Package.Epigraphs.Add(Type, Level);
        }

        public bool DeductInventory(ItemListItem item)
        {
            if (item.Id == -1)
            {
                return true;
            }
            if (!m_Package.Inventories.ContainsKey(item.Id) || m_Package.Inventories[item.Id] < item.Count)
            {
                return false;
            }
            else
            {
                m_Package.Inventories[item.Id] -= item.Count;
            }
            return true;
        }

        public bool DeductGear(int gear)
        {
            if (!m_Package.Gears.ContainsKey(gear))
            {
                return false;
            }
            else
            {
                m_Package.Gears.Remove(gear);
            }
            return true;
        }

        public bool DeductSoul(int soul)
        {
            if (!m_Package.Souls.ContainsKey(soul))
            {
                return false;
            }
            else
            {
                m_Package.Souls.Remove(soul);
            }
            return true;
        }

        public bool DeductEpigraph(int type, int level)
        {
            if (!m_Package.Epigraphs.ContainsKey(type) || m_Package.Epigraphs[type] != level)
            {
                return false;
            }
            else
            {
                m_Package.Epigraphs.Remove(type);
            }
            return true;
        }

        public int UpgradeEpigraph(int typeId)
        {
            if (!m_Package.Epigraphs.ContainsKey(typeId))
            {
                return -1;
            }
            int curLevel = m_Package.Epigraphs[typeId];
            if (curLevel >= GameConsts.MaxEpigraphLevel)
            {
                return -1;
            }
            var epigraphData = CacheSet.EpigraphTable.GetData(typeId);
            ItemListItem cost = new ItemListItem();
            cost.Id = epigraphData.PieceId;
            cost.Count = epigraphData.CostPieceCount[curLevel - 1];
            if (!DeductInventory(cost))
            {
                return -1;
            }
            m_Package.Epigraphs[typeId] += 1;
            return cost.Id;
        }

        public void UpgradeSoul(int Id, int newType)
        {
            if (m_Package.Souls.ContainsKey(Id))
            {
                m_Package.Souls[Id] = newType;
            }
        }

        public void GetItems(IDictionary<int, int> gotItems, ReceiveItemMethodType method, out PBReceivedItems receivedItems)
        {
            GearLogic gear = new GearLogic();
            SoulLogic soul = new SoulLogic();
            Dictionary<int, int> itemDict = new Dictionary<int, int>();
            receivedItems = new PBReceivedItems();
            foreach (var reward in gotItems)
            {
                if (IsGear(reward.Key))
                {
                    for (int i = 0; i < reward.Value; i++)
                    {
                        int gearId = gear.AddNewGear(reward.Key, m_UserId, method);
                        AddGear(gearId, reward.Key);
                        receivedItems.GearInfo.Add(new PBGearInfo() { Id = gearId, Type = reward.Key, Level = 1 });
                    }
                }
                else if (IsItem(reward.Key))
                {
                    AddInventory(new ItemListItem() { Id = reward.Key, Count = reward.Value });
                    GameUtils.MergeItem(itemDict, reward.Key, reward.Value);
                }
                else if (IsSoul(reward.Key))
                {
                    for (int i = 0; i < reward.Value; i++)
                    {
                        int soulId = soul.AddNewSoul(reward.Key);
                        AddSoul(soulId, reward.Key);
                        receivedItems.SoulInfo.Add(new PBSoulInfo() { Id = soulId, Type = reward.Key });
                    }
                }
                else if (IsEpigraph(reward.Key))
                {
                    for (int i = 0; i < reward.Value; i++)
                    {
                        ItemListItem item;
                        if (GetNewEpigraph(reward.Key, out item))
                        {
                            receivedItems.EpigraphInfo.Add(new PBEpigraphInfo() { Type = reward.Key, Level = m_Package.Epigraphs[reward.Key] });
                        }
                        else
                        {
                            GameUtils.MergeItem(itemDict, item.Id, item.Count);
                        }
                    }
                }
                else
                {
                    PlayerLogic p = new PlayerLogic();
                    p.SetUser(m_UserId);
                    switch ((GiftItemType)reward.Key)
                    {
                        case GiftItemType.Coin:
                            p.AddCoin(reward.Value);
                            break;
                        case GiftItemType.Money:
                            p.AddMoney(reward.Value);
                            break;
                        case GiftItemType.Energy:
                            long nextRecoverTime;
                            p.AddEnergy(reward.Value, out nextRecoverTime);
                            break;
                        case GiftItemType.MeridianEnergy:
                            p.AddStarEnergy(reward.Value);
                            break;
                        case GiftItemType.Spirit:
                            p.AddSpirit(reward.Value);
                            break;
                        case GiftItemType.DragonStripeToken:
                            p.AddDragonStripeToken(reward.Value);
                            break;
                    }
                }
            }
            foreach (var item in itemDict)
            {
                receivedItems.ItemInfo.Add(new PBItemInfo() { Type = item.Key, Count = m_Package.Inventories[item.Key] });
            }
        }

        public bool GetNewEpigraph(int type, out ItemListItem item)
        {
            item = null;
            PlayerEpigraphLogic pe = new PlayerEpigraphLogic();
            pe.SetUser(m_UserId);
            if (m_Package.Epigraphs.ContainsKey(type) || pe.HasEpigraph(type))
            {
                DTEpigraph dataRow = CacheSet.EpigraphTable.GetData(type);
                item = new ItemListItem() { Id = dataRow.PieceId, Count = dataRow.PieceCount };
                AddInventory(item);
                return false;
            }
            else
            {
                AddEpigraph(type, 1);
                return true;
            }
        }

        public static bool IsGear(int iid)
        {
            if (iid > GameConsts.MinGearId && iid < GameConsts.MaxGearId)
            {
                return true;
            }
            return false;
        }

        public static bool IsItem(int iid)
        {
            if (iid > GameConsts.MaxEpigraphId)
            {
                return true;
            }
            return false;
        }

        public static bool IsSoul(int iid)
        {
            if (iid > GameConsts.MinSoulId && iid < GameConsts.MaxSoulId)
            {
                return true;
            }
            return false;
        }

        public static bool IsEpigraph(int iid)
        {
            if (iid > GameConsts.MinEpigraphId && iid < GameConsts.MaxEpigraphId)
            {
                return true;
            }
            return false;
        }
    }
}
