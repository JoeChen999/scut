using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerChanceLogic
    {
        private int m_UserId;
        private ChanceType m_Type;
        private IPlayerChance m_Chance;
        private static readonly TimeSpan m_RefreshCoolDownTime = new TimeSpan(48, 0, 0);

        public PlayerChanceLogic()
        {

        }

        public void SetUserAndType(int userId, int type)
        {
            m_UserId = userId;
            m_Type = (ChanceType)type;
            switch (m_Type)
            {
                case ChanceType.Coin:
                    m_Chance = CacheSet.PlayerCoinChanceCache.FindKey(userId.ToString(), userId);
                    if (m_Chance == null)
                    {
                        m_Chance = new PlayerCoinChance();
                        m_Chance.UserId = m_UserId;
                        CacheSet.PlayerCoinChanceCache.Add(m_Chance as PlayerCoinChance);
                        Init();
                    }
                    break;
                case ChanceType.Money:
                    m_Chance = CacheSet.PlayerMoneyChanceCache.FindKey(userId.ToString(), userId);
                    if (m_Chance == null)
                    {
                        m_Chance = new PlayerMoneyChance();
                        m_Chance.UserId = m_UserId;
                        CacheSet.PlayerMoneyChanceCache.Add(m_Chance as PlayerMoneyChance);
                        Init();
                    }
                    break;
            }
            if (m_Chance.OpenedChanceRewards.Count >= m_Chance.ChanceCount)
            {
                ResetRewards(true);
            }
            if (DateTime.UtcNow.Ticks > m_Chance.NextRefreshTime)
            {
                ResetRewards(true);
                m_Chance.NextRefreshTime = DateTime.UtcNow.Ticks + m_RefreshCoolDownTime.Ticks;
            }
            if (m_Chance.UnopenedChanceRewards.Count + m_Chance.OpenedChanceRewards.Count <= 0)
            {
                ResetRewards(true);
            }
        }

        public IPlayerChance MyChance
        {
            get
            {
                switch (m_Type)
                {
                    case ChanceType.Coin:
                        return m_Chance as PlayerCoinChance;
                    case ChanceType.Money:
                        return m_Chance as PlayerMoneyChance;
                    default:
                        return m_Chance;
                }
            }
        }

        public void Init()
        {
            m_Chance.NextFreeTime = DateTime.UtcNow.Ticks;
            m_Chance.NextRefreshTime = DateTime.UtcNow.Ticks;
            ResetRewards(true);
        }

        public bool ResetRewards(bool IsFree)
        {
            RandomDropLogic random = RandomDropLogic.GetInstance();
            PlayerLogic player = new PlayerLogic();
            player.SetUser(m_UserId);
            m_Chance.OpenedChanceRewards.Clear();
            m_Chance.UnopenedChanceRewards.Clear();
            switch (m_Type)
            {
                case ChanceType.Coin:
                    if (!IsFree)
                    {
                        if (!player.DeductCoin(GameConfigs.GetInt("ChanceRefreshCost0")))
                            return false;
                    }
                    for (int i = GameConsts.Chance.MinCoinChancePackId; i <= GameConsts.Chance.MaxCoinChancePackId; i++)
                    {
                        m_Chance.UnopenedChanceRewards.Add(i, random.GetChanceRewards(i));
                    }
                    m_Chance.TotalFreeCount = 0;
                    break;
                case ChanceType.Money:
                    if (!IsFree)
                    {
                        if (!player.DeductMoney(GameConfigs.GetInt("ChanceRefreshCost1")))
                            return false;
                    }
                    for (int i = GameConsts.Chance.MinMoneyChancePackId; i <= GameConsts.Chance.MaxMoneyChancePackId; i++)
                    {
                        m_Chance.UnopenedChanceRewards.Add(i - GameConsts.Chance.MaxCoinChancePackId, random.GetChanceRewards(i));
                    }
                    m_Chance.TotalFreeCount = 0;
                    break;
                default:
                    break;
            }
            m_Chance.OpenedChanceRewards.Clear();
            return true;
        }

        public DropItem OpenChance(int index, bool isFree)
        {
            if (isFree)
            {
                if (DateTime.UtcNow.Ticks < m_Chance.NextFreeTime)
                {
                    return null;
                }
                switch (m_Type)
                {
                    case ChanceType.Coin:
                        PlayerCoinChance coinChance = m_Chance as PlayerCoinChance;
                        if (coinChance.TotalFreeCount >= GameConsts.Chance.MaxFreeCountForCoinChance)
                        {
                            return null;
                        }
                        coinChance.TotalFreeCount += 1;
                        m_Chance.NextFreeTime = DateTime.UtcNow.AddSeconds(GameConsts.Chance.FreeCoinChanceCDSeconds).Ticks;
                        break;
                    case ChanceType.Money:
                        m_Chance.NextFreeTime = DateTime.UtcNow.AddSeconds(GameConsts.Chance.FreeMoneyChanceCDSeconds).Ticks;
                        PlayerAchievementLogic.GetInstance(m_UserId).UpdateAchievement(AchievementType.OpenedMoneyChanceCount, 1);
                        break;
                    default:
                        return null;
                }
            }
            else
            {
                var dataRow = CacheSet.ChanceCostTable.GetData(m_Chance.OpenedChanceRewards.Count + 1);
                PlayerLogic p = new PlayerLogic();
                p.SetUser(m_UserId);
                switch (m_Type)
                {
                    case ChanceType.Coin:
                        if (!p.DeductCoin(dataRow.CoinCost))
                        {
                            return null;
                        }
                        break;
                    case ChanceType.Money:
                        if (!p.DeductMoney(dataRow.MoneyCost))
                        {
                            return null;
                        }
                        PlayerAchievementLogic.GetInstance(m_UserId).UpdateAchievement(AchievementType.OpenedMoneyChanceCount, 1);
                        break;
                    default:
                        return null;
                }
            }

            RandomDropLogic random = RandomDropLogic.GetInstance();
            int id = random.OpenChanceBox(m_Chance.UnopenedChanceRewards);
            DropItem di = m_Chance.UnopenedChanceRewards[id];
            try
            {
                m_Chance.OpenedChanceRewards.Add(index, di.Clone() as DropItem);
            }
            catch
            {
                return null;
            }
            m_Chance.UnopenedChanceRewards.Remove(id);
            return di;
        }

        public Dictionary<int, DropItem> OpenAllChance()
        {
            Dictionary<int, DropItem> dropItems = new Dictionary<int, DropItem>();
            var dataRow = CacheSet.ChanceCostTable.GetData(m_Chance.OpenedChanceRewards.Count + 1);
            if (dataRow == null)
            {
                return null;
            }
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            switch (m_Type)
            {
                case ChanceType.Coin:
                    if (!p.DeductCoin(dataRow.CoinCostAll))
                    {
                        return null;
                    }
                    AnnouncementLogic.PushOpenTenCoinChanceAnnouncement(m_UserId);
                    break;
                case ChanceType.Money:
                    if (!p.DeductMoney(dataRow.MoneyCostAll))
                    {
                        return null;
                    }
                    AnnouncementLogic.PushOpenTenMoneyChanceAnnouncement(m_UserId);
                    PlayerAchievementLogic.GetInstance(m_UserId).UpdateAchievement(AchievementType.OpenedMoneyChanceCount, m_Chance.UnopenedChanceRewards.Count);
                    break;
                default:
                    return null;
            }
            int i = 0;
            foreach (var item in m_Chance.UnopenedChanceRewards)
            {
                while (m_Chance.OpenedChanceRewards.ContainsKey(i))
                {
                    i++;
                }
                m_Chance.OpenedChanceRewards.Add(i, item.Value.Clone() as DropItem);
                dropItems.Add(i, item.Value);
                i++;
            }
            m_Chance.UnopenedChanceRewards.Clear();
            return dropItems;
        }
    }
}
