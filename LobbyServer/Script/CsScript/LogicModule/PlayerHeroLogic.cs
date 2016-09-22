using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerHeroLogic
    {
        private int m_UserId;
        private int m_HeroId;
        private PlayerHeros m_MyHeros;
        private Hero m_Hero = null;
        
        public PlayerHeroLogic()
        {
            m_HeroId = 0;
            m_UserId = 0;
            m_MyHeros = null;
        }

        public PlayerHeroLogic SetUser(int userId)
        {
            m_UserId = userId;
            m_MyHeros = CacheSet.PlayerHeroCache.FindKey(m_UserId.ToString(), m_UserId);
            if(m_MyHeros == null)
            {
                m_MyHeros = new PlayerHeros()
                {
                    UserId = userId
                };
                CacheSet.PlayerHeroCache.Add(m_MyHeros);
            }
            return this;
        }

        public PlayerHeroLogic SetHero(int heroId)
        {
            m_HeroId = heroId;
            if(m_MyHeros != null && m_MyHeros.Heros.ContainsKey(m_HeroId))
            {
                m_Hero = m_MyHeros.Heros[m_HeroId];
            }
            return this;
        }

        public PlayerHeros MyHeros
        {
            get
            {
                return m_MyHeros;
            }
            set
            {
                m_MyHeros = value;
            }
        }

        public CacheDictionary<int, Hero> GetHeroList()
        {
            if (m_MyHeros == null)
            {
                return null;
            }
            return m_MyHeros.Heros;
        }

        public Hero GetHeroInfo()
        {
            return m_Hero;
        }

        public ItemListItem AddNewHero(int heroId, ReceiveItemMethodType method = ReceiveItemMethodType.None)
        {
            Hero playerHero;
            var HeroData = CacheSet.HeroTable.GetData(heroId);
            if (m_MyHeros.Heros.TryGetValue(heroId, out playerHero))
            {
                int itemId = HeroData.StarLevelUpItemId;
                ItemListItem item = new ItemListItem()
                {
                    Id = itemId,
                    Count = GameConfigs.GetInt("Hero_Piece_Count_For_Star_Level_" + HeroData.DefaultStarLevel, 10)
                };
                return item;
            }
            else
            {
                Hero hero = new Hero();
                int star = HeroData.DefaultStarLevel;//TODO read config
                hero.HeroType = heroId;
                hero.HeroExp = 0;
                hero.HeroLv = 1;
                hero.HeroStarLevel = star;
                hero.ConsciousnessLevel = 0;
                hero.ElevationLevel = 0;
                m_HeroId = heroId;
                hero.SkillLevels.AddRange(new int[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 1 });
                hero.SkillExps.AddRange(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                m_MyHeros.ModifyLocked(() =>
                {
                    m_MyHeros.Heros[heroId] = hero;
                    m_Hero = m_MyHeros.Heros[heroId];
                    RefreshMight();
                });
                PlayerAchievementLogic.GetInstance(m_UserId).UpdateAchievement(AchievementType.HeroCount, MyHeros.Heros.Count);
                AnnouncementLogic.PushReceiveHeroAnnouncement(m_UserId, method, hero.HeroType);
            }
            return null;
        }

        public void AddSkillExp(int skillIndex, int exp)
        {
            m_Hero.SkillExps[skillIndex] += exp;
        }

        public bool SkillLevelUp(int skillIndex, int costExp)
        {
            if(m_Hero.SkillExps[skillIndex] >= costExp)
            {
                m_Hero.SkillExps[skillIndex] -= costExp;
                m_Hero.SkillLevels[skillIndex] += 1;
                return true;
            }
            RefreshMight();
            return false;
        }

        public void AddLevel(int addLevel)
        {
            PlayerLogic player = new PlayerLogic();
            player.SetUser(m_UserId);
            int oldLevel = m_Hero.HeroLv;
            if (m_Hero.HeroLv + addLevel > player.MyPlayer.Level)
            {
                m_Hero.HeroLv = player.MyPlayer.Level;
            }
            else
            {
                m_Hero.HeroLv += addLevel;
            }
            PlayerAchievementLogic.GetInstance(m_UserId).UpdateAchievement(AchievementType.HeroLevel, m_Hero.HeroLv);
            RefreshMight();
            m_Hero.HeroExp = 0;
        }

        public void AddExp(int exp)
        {
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            AddExp(exp, p.MyPlayer.Level);
        }

        public void AddExp(int exp, int playerLevel)
        {
            int newExp;
            int newLevel = GetNewLevel(exp, playerLevel, out newExp);
            PlayerAchievementLogic.GetInstance(m_UserId).UpdateAchievement(AchievementType.HeroLevel, newLevel);
            m_MyHeros.ModifyLocked(() =>
            {
                m_Hero.HeroExp = newExp;
                m_Hero.HeroLv = newLevel;
            });
        }

        private int GetNewLevel(int exp, int playerLevel, out int newExp)
        {
            int tempExp = m_Hero.HeroExp + exp;
            int newLevel = m_Hero.HeroLv;
            var a = CacheSet.HeroBaseTable.GetData(newLevel);
            int nextLevelExp = a.LevelUpExp;
            bool levelUp = false;
            while (tempExp - nextLevelExp > 0)
            {
                if (newLevel == playerLevel)
                {
                    newExp = nextLevelExp;
                    return newLevel;
                }
                tempExp -= nextLevelExp;
                newLevel++;
                nextLevelExp = CacheSet.HeroBaseTable.GetData(newLevel).LevelUpExp;
                levelUp = true;
            }
            if (levelUp)
            {
                RefreshMight();
            }
            newExp = tempExp;
            return newLevel;
        }

        public void DecomposeHero()
        {
            m_MyHeros.ModifyLocked(() =>
            {
                m_MyHeros.Heros.Remove(m_HeroId);
            });
        }

        public void StarLevelUp()
        {
            if (m_Hero.HeroStarLevel >= GameConsts.Hero.MaxStarLevel)
            {
                return;
            }
            m_MyHeros.ModifyLocked(() =>
            {
                m_Hero.HeroStarLevel += 1;
            });
            PlayerAchievementLogic.GetInstance(m_UserId).UpdateAchievement(AchievementType.HeroStarLevel, m_Hero.HeroStarLevel - 1, m_Hero.HeroStarLevel);
            AnnouncementLogic.PushHeroStarLevelUpAnnouncement(m_UserId, m_Hero.HeroStarLevel, m_HeroId);
            RefreshMight();
        }

        public void ConsciousnessLevelUp()
        {
            if (m_Hero.HeroStarLevel >= GameConsts.Hero.MaxConsciousnessLevel)
            {
                return;
            }
            m_MyHeros.ModifyLocked(() =>
            {
                m_Hero.ConsciousnessLevel += 1;
            });
            PlayerAchievementLogic.GetInstance(m_UserId).UpdateAchievement(AchievementType.HeroConsiousnessLevel, m_Hero.ConsciousnessLevel);
            AnnouncementLogic.PushHeroConsciousnessAnnouncement(m_UserId, m_Hero.ConsciousnessLevel, m_Hero.HeroType);
            RefreshMight();
        }

        public void ElevationLevelUp()
        {
            m_MyHeros.ModifyLocked(() =>
            {
                m_Hero.ElevationLevel += 1;
            });
            PlayerAchievementLogic.GetInstance(m_UserId).UpdateAchievement(AchievementType.HeroElevationLevel, m_Hero.ElevationLevel);
            AnnouncementLogic.PushHeroElevationAnnouncement(m_UserId, m_Hero.ElevationLevel, m_Hero.HeroType);
            RefreshMight();
        }

        public int WearGear(GearType type, int gearId)
        {
            int oldGearId;
            if (!m_Hero.Gears.TryGetValue(type, out oldGearId))
            {
                oldGearId = 0;
            }
            if(m_Hero.Gears.ContainsKey(type) && gearId == 0)
            {
                m_Hero.Gears.Remove(type);
            }
            else
            {
                m_Hero.Gears[type] = gearId;
            }
            RefreshMight();
            return oldGearId;
        }

        public bool DressSoul(int eid, int soulId)
        {
            Hero hero = GetHeroInfo();
            if (hero.Souls.ContainsKey(eid) || hero.Souls.Count >= GameConsts.Hero.MaxSoulSlot)
            {
                return false;
            }
            hero.Souls.Add(eid, soulId);
            RefreshMight();
            return true;
        }

        public bool UndressSoul(int eid, int soulId)
        {
            Hero hero = GetHeroInfo();
            if (!hero.Souls.ContainsKey(eid) || hero.Souls[eid] != soulId)
            {
                return false;
            }
            hero.Souls.Remove(eid);
            RefreshMight();
            return true;
        }

        public void RefreshMight()
        {
            int oldMight = m_Hero.Might;
            var atkData = CacheSet.MightLevelParamTable.GetData(m_Hero.HeroLv);
            float physicalA_Atk = atkData.PhysicalAttack * atkData.PhysicalAttack / (atkData.PhysicalAttack + m_Hero.PhysicalDefense);
            float magicalA_Atk = atkData.MagicAttack * atkData.MagicAttack / (atkData.MagicAttack + m_Hero.MagicDefense);
            //attack relate might
            float basePhysicalAtk = m_Hero.PhysicalAttack * m_Hero.PhysicalAttack / (m_Hero.PhysicalAttack + atkData.PhysicalDefense * (1 - m_Hero.OppPhysicalDfsReduceRate));
            float baseMagicAtk = m_Hero.MagicAttack * m_Hero.MagicAttack / (m_Hero.MagicAttack + atkData.MagicDefense * (1 - m_Hero.OppMagicDfsReduceRate));
            float baseAtk = basePhysicalAtk + baseMagicAtk;
            float cooldownAtk = baseAtk * m_Hero.ReducedSkillCoolDownRate * 1000;
            float waitingAtk = baseAtk * m_Hero.ReduceSwitchHeroCoolDownRate * 1000;
            float criticalAtk = baseAtk * m_Hero.CriticalHitRate * m_Hero.CriticalHitProb * 10000;
            float physicalReflectAtk = physicalA_Atk * m_Hero.PhysicalAtkReflectRate * 100;
            float magicalReflectAtk = magicalA_Atk * m_Hero.MagicAtkReflectRate * 100;
            float skillAtk = 50;
            //             for (int i = 0; i < m_Hero.SkillLevels.Count; i++)
            //             {
            //                 DTSkillLevelUp skillData = CacheSet.SkillLevelUpTable.GetData(h => (h.HeroType == m_Hero.HeroType && h.SkillIndex == i && h.SkillLevel == m_Hero.SkillLevels[i]));
            //                 skillAtk += 10;//skillData.MightParam;
            //             }
            float atkMight = baseAtk + cooldownAtk + waitingAtk + criticalAtk + physicalReflectAtk + magicalReflectAtk + m_Hero.AdditionalDamage + skillAtk;
            //HP related might
            float baseHP = m_Hero.MaxHP;
            float defenseHP = (m_Hero.PhysicalDefense + m_Hero.MagicDefense) * 6;
            float antiCriticalHitHP = (physicalA_Atk + magicalA_Atk) * (1 - m_Hero.AntiCriticalHitProb);
            float RecoverHP = m_Hero.RecoverHP * 0.1f;
            float damageReduceHP = (m_Hero.PhysicalDefense + m_Hero.MagicDefense) * (1 - m_Hero.DamageReductionRate);
            float physicalAbsorbHP = basePhysicalAtk * m_Hero.PhysicalAtkHPAbsorbRate * 100;
            float magicalAbsorbHP = baseMagicAtk * m_Hero.MagicAtkHPAbsorbRate * 100;
            float hpMight = baseHP + defenseHP - antiCriticalHitHP + RecoverHP - damageReduceHP + physicalAbsorbHP + magicalAbsorbHP;
            int might = Mathf.RoundToInt(atkMight * hpMight / (atkMight + hpMight));
            m_Hero.Might = might;
//             TraceLog.WriteError("Calculating user:{0}->hero:{1} might:\nMaxHP => {2}\nPhysicalAttack => {3}\nMagicalAttack => {4}\nPhysicalDefense => {5}\nMagicalDefense=>{6}\n" +
//                 "OppPhysicalDfsReduceRate => {7}\nOppMagicDfsReduceRate => {8}\nPhysicalAtkHPAbsorbRate => {9}\nMagicAtkHPAbsorbRate => {10}\nPhysicalAtkReflectRate => {11}\n" +
//                 "MagicAtkReflectRate => {12}\nDamageReductionRate => {13}\nReducedSkillCoolDownRate => {14}\nReduceSwitchHeroCoolDownRate => {15}\nCriticalHitProb => {16}\n" +
//                 "CriticalHitRate => {17}\nAntiCriticalHitProb => {18}\nAdditionalDamage => {19}\nRecoverHP => {20}\nBaseAtk => {21}\nAtkMight => {22}\nHPMight => {23}", m_UserId, m_HeroId, m_Hero.MaxHP, m_Hero.PhysicalAttack, m_Hero.MagicAttack,
//                 m_Hero.PhysicalDefense, m_Hero.MagicDefense, m_Hero.OppPhysicalDfsReduceRate, m_Hero.OppMagicDfsReduceRate, m_Hero.PhysicalAtkHPAbsorbRate, m_Hero.MagicAtkHPAbsorbRate,
//                 m_Hero.PhysicalAtkReflectRate, m_Hero.MagicAtkReflectRate, m_Hero.DamageReductionRate, m_Hero.ReducedSkillCoolDownRate, m_Hero.ReduceSwitchHeroCoolDownRate,
//                 m_Hero.CriticalHitProb, m_Hero.CriticalHitRate, m_Hero.AntiCriticalHitProb, m_Hero.AdditionalDamage, m_Hero.RecoverHP, baseAtk, atkMight, hpMight);
            PlayerAchievementLogic.GetInstance(m_UserId).UpdateAchievement(AchievementType.HeroMight, m_Hero.Might);
            AnnouncementLogic.PushHeroMightAnnouncement(m_UserId, oldMight, might, m_Hero.HeroType);
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            p.RefreshMight();
        }

        public bool DataCheck()
        {
            if (m_MyHeros == null)
            {
                TraceLog.WriteInfo("wrong userId");
                return false;
            }
            if (!m_MyHeros.Heros.ContainsKey(m_HeroId))
            {
                TraceLog.WriteInfo("wrong m_HeroId");
                return false;
            }
            return true;
        }
    }
}
