using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerLogic
    {
        private long RecoverTime = (new TimeSpan(0, GameConsts.EnergyRecoverMinutes, 0)).Ticks;
        private string m_AccoutName = null;

        public PlayerLogic()
        {

        }

        public static Player FindUserByName(string name)
        {
            return CacheSet.PlayerCache.Find(t => t.Name == name);
        }

        public static List<Player> FindUsersByName(string name)
        {
            var playerList = CacheSet.PlayerCache.FindAll(t => t.Name.IndexOf(name) >= 0);
            if (playerList.Count <= GameConsts.Social.MaxSearchResultCount)
            {
                return playerList;
            }
            playerList.Sort(delegate(Player a, Player b) { return a.Name.Length.CompareTo(b.Name.Length); });
            List<Player> retList = new List<Player>();
            for (int i = 0; i < GameConsts.Social.MaxSearchResultCount; i++ )
            {
                retList.Add(playerList[i]);
            }
            return retList;
        }

        public static List<Player> FindUserByUUID(int uuid)
        {
            List<Player> retList = new List<Player>();
            retList.Add(CacheSet.PlayerCache.Find(t => t.UUID == uuid));
            return retList;
        }

        public PlayerLogic SetUser(long userId)
        {
            MyPlayer = CacheSet.PlayerCache.FindKey(userId);
            return this;
        }

        public void SetUser(string accountName)
        {
            m_AccoutName = accountName;
            MyPlayer = CacheSet.PlayerCache.Find(t => t.AccountName == accountName);
        }

        public Player MyPlayer
        {
            get;
            set;
        }

        public string MyAccountName
        {
            get { return m_AccoutName; }
            set { m_AccoutName = value; }
        }

        public void AddPlayer()
        {
            if (m_AccoutName == null)
            {
                return;
            }

            Player player = new Player();
            player.Id = (int)(CacheSet.PlayerCache.GetNextNo());
            player.Name = string.Empty;
            player.Level = 1;
            player.Exp = 0;
            player.Coin = GameConsts.PlayerInitialCoin;
            player.Money = GameConsts.PlayerInitialMoney;
            player.AccountName = m_AccoutName;
            player.Energy = GetMaxEnergy(player.Level);
            player.LastEnergyRecoverTime = DateTime.UtcNow.Ticks;
            player.VIPExp = 0;
            player.VIPLevel = 0;
            player.StarEnergy = 0;
            player.LastLoginTime = DateTime.UtcNow.Ticks;
            player.ArenaToken = 0;
            player.Spirit = 0;
            player.UUID = GetUUID(player.Id);
            player.Status = PlayerStatus.Online;
            player.CreateTime = DateTime.UtcNow.Ticks;
            player.IsRobot = false;
            player.PvpToken = 0;
            CacheSet.PlayerCache.Add(player);
            MyPlayer = player;
        }

        public void AddRobot()
        {
            Player player = new Player();
            player.Id = (int)(CacheSet.PlayerCache.GetNextNo());
            player.Name = "Robot"+player.Id.ToString();
            Random r = new Random();
            player.Level = r.Next(30,60);
            player.PortraitType = 1;
            player.Status = PlayerStatus.Offline;
            player.IsRobot = true;
            CacheSet.PlayerCache.Add(player);
            MyPlayer = player;
        }

        public void AddArenaToken(int tokenCount)
        {
            MyPlayer.ArenaToken += tokenCount;
        }

        public int GetMaxEnergy(int level)
        {
            return 120;//TODO
        }

        public void OffLine()
        {
            MyPlayer.Status = PlayerStatus.Offline;
        }

        public void OnLine(GameSession session)
        {
            SessionUser user = new SessionUser();
            user.UserId = MyPlayer.Id;
            var OldSession = GameSession.Get(MyPlayer.Id);
            if (OldSession != null)
            {
                OldSession.Close();
            }
            session.Bind(user);
            MyPlayer.Status = PlayerStatus.Online;
            MyPlayer.LastLoginTime = DateTime.UtcNow.Ticks;
        }

        public void AddPvpToken(int token)
        {
            if(MyPlayer.PvpToken + token >= 0)
            {
                MyPlayer.PvpToken += token;
            }
            MyPlayer.PvpToken = 0;
        }

        public void AddLevel(int level)
        {
            if (CacheSet.PlayerTable.GetData(MyPlayer.Level + level) == null)
            {
                return;
            }
            else
            {
                MyPlayer.Level += level;
            }
            var pa = PlayerAchievementLogic.GetInstance(MyPlayer.Id);
            pa.UpdateAchievement(AchievementType.PlayerLevel, MyPlayer.Level);
            pa.OpenNewAchievement(MyPlayer.Level);
            AnnouncementLogic.PushLevelUpAnnouncement(MyPlayer.Id, MyPlayer.Level);
            MyPlayer.Exp = 0;
        }

        public bool DeductEnergy(int energy, out long nextRecoverTime)
        {
            GetNewEnergy(out nextRecoverTime);
            if (energy > MyPlayer.Energy)
            {
                return false;
            }

            int maxEnergy = GetMaxEnergy(MyPlayer.Level);
            if (MyPlayer.Energy >= maxEnergy)
            {
                MyPlayer.Energy -= energy;
                if (MyPlayer.Energy < maxEnergy)
                {
                    MyPlayer.LastEnergyRecoverTime = DateTime.UtcNow.Ticks;
                    nextRecoverTime = MyPlayer.LastEnergyRecoverTime + RecoverTime;
                }
            }
            else
            {
                MyPlayer.Energy -= energy;
            }
            return true;
        }

        public void AddEnergy(int energy, out long nextRecoverTime)
        {
            GetNewEnergy(out nextRecoverTime);
            int maxEnergy = GetMaxEnergy(MyPlayer.Level);
            MyPlayer.Energy += energy;
            if (MyPlayer.Energy < 0)
            {
                MyPlayer.Energy = 0;
            }
            if (MyPlayer.Energy > maxEnergy)
            {
                nextRecoverTime = 0;
                MyPlayer.LastEnergyRecoverTime = DateTime.UtcNow.Ticks;
            }
        }

        public void AddExp(int exp)
        {
            int newExp;
            int newLevel = GetNewLevel(exp, out newExp);
            if(newLevel > MyPlayer.Level)
            {
                var pa = PlayerAchievementLogic.GetInstance(MyPlayer.Id);
                pa.UpdateAchievement(AchievementType.PlayerLevel, newLevel);
                pa.OpenNewAchievement(newLevel);
            }
            MyPlayer.ModifyLocked(() =>
            {
                MyPlayer.Exp = newExp;
                MyPlayer.Level = newLevel;
            });
        }

        public bool DeductMoney(int money)
        {
            if (MyPlayer.Money < money)
            {
                return false;
            }
            PlayerAchievementLogic.GetInstance(MyPlayer.Id).UpdateAchievement(AchievementType.CostedMoney, money);
            MyPlayer.Money -= money;
            return true;
        }

        public void AddMoney(int money)
        {
            MyPlayer.Money += money;
            if (MyPlayer.Money < 0)
            {
                MyPlayer.Money = 0;
            }
        }

        public bool DeductCoin(int coin)
        {
            if (MyPlayer.Coin < coin)
            {
                return false;
            }
            PlayerAchievementLogic.GetInstance(MyPlayer.Id).UpdateAchievement(AchievementType.CostedCoin, coin);
            MyPlayer.Coin -= coin;
            return true;
        }

        public bool AddStarEnergy(int energy)
        {
            MyPlayer.StarEnergy += energy;
            return false;
        }

        public bool DeductStarEnergy(int energy)
        {
            if (MyPlayer.StarEnergy < energy)
            {
                return false;
            }
            MyPlayer.StarEnergy -= energy;
            return true;
        }

        public void AddCoin(int coin)
        {
            MyPlayer.Coin += coin;
            if (MyPlayer.Coin < 0)
            {
                MyPlayer.Coin = 0;
            }
        }

        public void AddDragonStripeToken(int token)
        {
            MyPlayer.DragonStripeToken += token;
            if (MyPlayer.DragonStripeToken < 0)
            {
                MyPlayer.DragonStripeToken = 0;
            }
        }
        public void AddSpirit(int spirit)
        {
            MyPlayer.Spirit += spirit;
            if (MyPlayer.Spirit < 0)
            {
                MyPlayer.Spirit = 0;
            }
        }

        public bool DeductSpirit(int spirit)
        {
            if (MyPlayer.Spirit < spirit)
            {
                return false;
            }
            MyPlayer.Spirit -= spirit;
            return true;
        }

        private int GetNewLevel(int exp, out int newExp)
        {
            int tempExp = MyPlayer.Exp + exp;
            int newLevel = MyPlayer.Level;
            int nextLevelexp = CacheSet.PlayerTable.GetData(newLevel).LevelUpExp;
            if(CacheSet.PlayerTable.GetData(newLevel + 1) == null)
            {
                if(tempExp >= nextLevelexp)
                {
                    newExp = nextLevelexp;
                    return newLevel;
                }
            }
            while (tempExp > nextLevelexp)
            {
                tempExp -= nextLevelexp;
                newLevel++;
                nextLevelexp = CacheSet.PlayerTable.GetData(newLevel).LevelUpExp;
            }
            newExp = tempExp;
            return newLevel;
        }

        public int GetNewEnergy(out long nextRecoverTime)
        {
            int maxEnergy = GetMaxEnergy(MyPlayer.Level);
            if (MyPlayer.Energy >= maxEnergy)
            {
                nextRecoverTime = 0L;
                return MyPlayer.Energy;
            }

            long now = DateTime.UtcNow.Ticks;
            long duringTime = now - MyPlayer.LastEnergyRecoverTime;
            int newEnery = MyPlayer.Energy + (int)(duringTime / RecoverTime);
            if (newEnery >= maxEnergy)
            {
                nextRecoverTime = 0L;
                MyPlayer.LastEnergyRecoverTime = now;
                MyPlayer.Energy = maxEnergy;
            }
            else
            {
                MyPlayer.Energy = newEnery;
                MyPlayer.LastEnergyRecoverTime += duringTime / RecoverTime * RecoverTime;
                nextRecoverTime = RecoverTime + MyPlayer.LastEnergyRecoverTime;
            }

            return MyPlayer.Energy;
        }

        public void RefreshMight()
        {
            HeroTeamLogic ht = new HeroTeamLogic();
            ht.SetUser(MyPlayer.Id);
            PlayerHeroLogic ph = new PlayerHeroLogic();
            ph.SetUser(MyPlayer.Id);
            int newMight = 0;
            foreach (int heroId in ht.GetTeam())
            {
                if(heroId > 0)
                {
                    newMight += ph.MyHeros.Heros[heroId].Might;
                }
            }
            MyPlayer.Might = newMight;
        }

        private static int GetUUID(int id)
        {
            int c = (id % 31) + 1;
            int newId = c << 20 | id;
            return newId;
        }
    }
}
