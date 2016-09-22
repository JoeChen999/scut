using System;
using System.Collections.Generic;
using Genesis.GameServer.CommonLibrary;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;

namespace Genesis.GameServer.LobbyServer
{
    public class RandomDropLogic
    {
        private Random random;
        private static RandomDropLogic m_Instance = null;
        private static long m_CreateInstanceTime = 0;

        public static RandomDropLogic GetInstance()
        {
            if(m_Instance == null || DateTime.UtcNow.Ticks - m_CreateInstanceTime > TimeSpan.TicksPerHour)
            {
                m_Instance = new RandomDropLogic();
                m_CreateInstanceTime = DateTime.UtcNow.Ticks;
            }
            return m_Instance;
        }

        private RandomDropLogic()
        {
            random = new Random();
        }

        public void GetDropDict(DTDrop dropData, CacheDictionary<int, int> dropDict)
        {
            for (int i = 0; i < dropData.RepeatCount; i++)
            {
                int randomValue = random.Next(0, GameConsts.DropItemTotalWeight);
                foreach (DropItem di in dropData.DropList)
                {
                    randomValue -= di.ItemWeight;
                    if (randomValue < 0)
                    {
                        int itemId, itemCount;
                        if(di.ItemId < 0)
                        {
                            switch ((RandomDropSetType)di.ItemId)
                            {
                                case RandomDropSetType.RandomWhiteGear:
                                    itemId = GetOneSpecifiedQualityGear(GearQuality.White);
                                    break;
                                case RandomDropSetType.RandomGreenGear:
                                    itemId = GetOneSpecifiedQualityGear(GearQuality.Green);
                                    break;
                                case RandomDropSetType.RandomBlueGear:
                                    itemId = GetOneSpecifiedQualityGear(GearQuality.Blue);
                                    break;
                                case RandomDropSetType.RandomPurpleGear:
                                    itemId = GetOneSpecifiedQualityGear(GearQuality.Purple);
                                    break;
                                case RandomDropSetType.RandomOrangeGear:
                                    itemId = GetOneSpecifiedQualityGear(GearQuality.Orange);
                                    break;
                                default:
                                    continue;
                            }
                            itemCount = di.ItemCount;
                        }
                        else
                        {
                            itemId = di.ItemId;
                            itemCount = di.ItemCount;
                        }
                        if (dropDict.ContainsKey(itemId))
                        {
                            dropDict[itemId] += itemCount;
                        }
                        else
                        {
                            dropDict[itemId] = itemCount;
                        }
                        break;
                    }
                }
            }
        }

        public int GetOneSpecifiedQualityGear(GearQuality quality)
        {
            var allgears = AllQualitiesOfGears.GetSpecifiedQualityGears(quality);
            int index = random.Next(0, allgears.Count);
            return allgears[index].Id;
        }

        public DropItem GetChanceRewards(int dropId)
        {
            DTChance dataRow = CacheSet.ChanceTable.GetData(dropId);
            int randomValue = random.Next(0, GameConsts.DropItemTotalWeight);
            foreach (DropItem di in dataRow.DropList)
            {
                randomValue -= di.ItemWeight;
                if (randomValue < 0)
                {
                    return di.Clone() as DropItem;
                }
            }
            return null;
        }

        public int OpenChanceBox(CacheDictionary<int, DropItem> rewards)
        {
            int totalWeight = 0;
            foreach (var reward in rewards)
            {
                totalWeight += reward.Value.ItemWeight;
            }
            int randomValue = random.Next(0, totalWeight);
            foreach (var reward in rewards)
            {
                randomValue -= reward.Value.ItemWeight;
                if (randomValue < 0)
                {
                    return reward.Key;
                }
            }
            return 0;
        }
    }

    public class ItemListItem
    {
        public ItemListItem()
        {
        }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
