using ProtoBuf;
using UnityEngine;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;

namespace Genesis.GameServer.LobbyServer
{
    [ProtoContract]
    public class Hero : EntityChangeEvent
    {
        private DTHero m_HeroDataRow = null;
        private DTHeroConsciousnessBase m_HeroConsciousnessDataRow = null;
        private DTHeroElevationBase m_HeroElevationDataRow = null;
        private DTHeroBase m_HeroBaseDataRow = null;

        public Hero()
        {
            Souls = new CacheDictionary<int, int>();
            Gears = new CacheDictionary<GearType, int>();
            SkillLevels = new CacheList<int>();
            SkillExps = new CacheList<int>();
        }
        [ProtoMember(1)]
        public int HeroType { get; set; }

        [ProtoMember(2)]
        public int HeroLv { get; set; }

        [ProtoMember(3)]
        public int HeroExp { get; set; }

        [ProtoMember(4)]
        public int HeroStarLevel { get; set; }

        [ProtoMember(5)]
        public int ConsciousnessLevel { get; set; }

        [ProtoMember(6)]
        public int ElevationLevel { get; set; }

        [ProtoMember(7)]
        public CacheDictionary<int, int> Souls { get; set; }

        [ProtoMember(8)]
        public CacheDictionary<GearType, int> Gears { get; set; }

        [ProtoMember(9)]
        public CacheList<int> SkillLevels { get; set; }

        [ProtoMember(10)]
        public CacheList<int> SkillExps { get; set; }

        [ProtoMember(11)]
        public int Might { get; set; }

        public DTHero HeroDataRow
        {
            get
            {
                if (m_HeroDataRow == null)
                {
                    m_HeroDataRow = CacheSet.HeroTable.GetData(HeroType);
                }
                return m_HeroDataRow;
            }
        }

        public DTHeroConsciousnessBase HeroConsciousnessDataRow
        {
            get
            {
                if (m_HeroConsciousnessDataRow == null || m_HeroConsciousnessDataRow.Id != ConsciousnessLevel)
                {
                    m_HeroConsciousnessDataRow = CacheSet.HeroConsciousnessBaseTable.GetData(ConsciousnessLevel);
                }
                return m_HeroConsciousnessDataRow;
            }
        }

        public DTHeroElevationBase HeroElevationDataRow
        {
            get
            {
                if (m_HeroElevationDataRow == null || m_HeroElevationDataRow.Id != ElevationLevel)
                {
                    m_HeroElevationDataRow = CacheSet.HeroElevationBaseTable.GetData(ElevationLevel);
                }
                return m_HeroElevationDataRow;
            }
        }

        public DTHeroBase HeroBaseDataRow
        {
            get
            {
                if (m_HeroBaseDataRow == null || m_HeroBaseDataRow.Id != HeroLv)
                {
                    m_HeroBaseDataRow = CacheSet.HeroBaseTable.GetData(HeroLv);
                }
                return m_HeroBaseDataRow;
            }
        }

        public int MaxHPBase
        {
            get
            {
                return HeroBaseDataRow.MaxHPBase;
            }
        }

        public float MaxHPFactor
        {
            get
            {
                return HeroDataRow.MaxHPFactor;
            }
        }

        public int ConsciousnessMaxHP
        {
            get
            {
                return HeroConsciousnessDataRow.MaxHPBase;
            }
        }

        public int ElevationMaxHP
        {
            get
            {
                return HeroElevationDataRow.MaxHPBase;
            }
        }

        public int MaxHP
        {
            get
            {
                int baseVal = Mathf.Max(1, (int)Mathf.Round(MaxHPBase * MaxHPFactor * HeroDataRow.GetStarFactor(HeroStarLevel)) + ConsciousnessMaxHP + ElevationMaxHP);

                int delta = 0;
                float rate = 0f;
                foreach (var gear in Gears)
                {
                    delta += CacheSet.GearCache.FindKey(gear.Value).MaxHP;
                }
                foreach (var soul in Souls)
                {
                    delta += CacheSet.SoulCache.FindKey(soul.Value).MaxHP;
                }
                return CalcIntProperty(baseVal, delta, rate);
            }
        }

        public int PhysicalAttackBase
        {
            get
            {
                return HeroBaseDataRow.PhysicalAttackBase;
            }
        }

        /// <summary>
        /// 战意物理攻击。
        /// </summary>
        public int ConsciousnessPhysicalAttack
        {
            get
            {
                return HeroConsciousnessDataRow.PhysicalAttackBase;
            }
        }

        /// <summary>
        /// 飞升物理攻击。
        /// </summary>
        public int ElevationPhysicalAttack
        {
            get
            {
                return HeroElevationDataRow.PhysicalAttackBase;
            }
        }

        public float PhysicalAttackFactor
        {
            get
            {
                return HeroDataRow.PhysicalAttackFactor;
            }
        }

        public int PhysicalAttack
        {
            get
            {
                int baseVal = Mathf.Max(0, Mathf.RoundToInt(PhysicalAttackBase * PhysicalAttackFactor * HeroDataRow.GetStarFactor(HeroStarLevel)) + ConsciousnessPhysicalAttack + ElevationPhysicalAttack);

                int delta = 0;
                float rate = 0f;
                foreach (var gear in Gears)
                {
                    delta += CacheSet.GearCache.FindKey(gear.Value).PhysicalAttack;
                }
                foreach (var soul in Souls)
                {
                    delta += CacheSet.SoulCache.FindKey(soul.Value).PhysicalAttack;
                }

                return CalcIntProperty(baseVal, delta, rate);
            }
        }

        /// <summary>
        /// 基础物理防御。
        /// </summary>
        public int PhysicalDefenseBase
        {
            get
            {
                return HeroBaseDataRow.PhysicalDefenseBase;
            }
        }

        /// <summary>
        /// 战意物理防御。
        /// </summary>
        public int ConsciousnessPhysicalDefense
        {
            get
            {
                return HeroConsciousnessDataRow.PhysicalDefenseBase;
            }
        }

        /// <summary>
        /// 飞升物理防御。
        /// </summary>
        public int ElevationPhysicalDefense
        {
            get
            {
                return HeroElevationDataRow.PhysicalDefenseBase;
            }
        }

        public float PhysicalDefenseFactor
        {
            get
            {
                return HeroDataRow.PhysicalDefenseFactor;
            }
        }

        public int PhysicalDefense
        {
            get
            {
                int baseVal = Mathf.Max(0, Mathf.RoundToInt(PhysicalDefenseBase * PhysicalDefenseFactor * HeroDataRow.GetStarFactor(HeroStarLevel)) + ConsciousnessPhysicalDefense + ElevationPhysicalDefense);

                int delta = 0;
                float rate = 0f;
                foreach (var gear in Gears)
                {
                    delta += CacheSet.GearCache.FindKey(gear.Value).PhysicalDefense;
                }
                foreach (var soul in Souls)
                {
                    delta += CacheSet.SoulCache.FindKey(soul.Value).PhysicalDefense;
                }

                return CalcIntProperty(baseVal, delta, rate);
            }
        }

        public int MagicAttackBase
        {
            get
            {
                return HeroBaseDataRow.MagicAttackBase;
            }
        }

        public float MagicAttackFactor
        {
            get
            {
                return HeroDataRow.MagicAttackFactor;
            }
        }

        public int ConsciousnessMagicAttack
        {
            get
            {
                return HeroConsciousnessDataRow.MagicAttackBase;
            }
        }

        /// <summary>
        /// 飞升法术攻击。
        /// </summary>
        public int ElevationMagicAttack
        {
            get
            {
                return HeroElevationDataRow.MagicAttackBase;
            }
        }

        public int MagicAttack
        {
            get
            {
                int baseVal = Mathf.Max(0, Mathf.RoundToInt(MagicAttackBase * MagicAttackFactor * HeroDataRow.GetStarFactor(HeroStarLevel)) + ConsciousnessMagicAttack + ElevationMagicAttack);

                int delta = 0;
                float rate = 0f;
                foreach (var gear in Gears)
                {
                    delta += CacheSet.GearCache.FindKey(gear.Value).MagicAttack;
                }
                foreach (var soul in Souls)
                {
                    delta += CacheSet.SoulCache.FindKey(soul.Value).MagicAttack;
                }

                return CalcIntProperty(baseVal, delta, rate);
            }
        }

        public int MagicDefenseBase
        {
            get
            {
                return HeroBaseDataRow.MagicDefenseBase;
            }
        }

        public float MagicDefenseFactor
        {
            get
            {
                return HeroDataRow.MagicDefenseFactor;
            }
        }

        public int ConsciousnessMagicDefense
        {
            get
            {
                return HeroConsciousnessDataRow.MagicDefenseBase;
            }
        }

        public int ElevationMagicDefense
        {
            get
            {
                return HeroElevationDataRow.MagicDefenseBase;
            }
        }

        public int MagicDefense
        {
            get
            {
                int baseVal = Mathf.Max(0, Mathf.RoundToInt(MagicDefenseBase * MagicDefenseFactor * HeroDataRow.GetStarFactor(HeroStarLevel)) + ConsciousnessMagicDefense + ElevationMagicDefense);

                int delta = 0;
                float rate = 0f;
                foreach (var gear in Gears)
                {
                    delta += CacheSet.GearCache.FindKey(gear.Value).MagicDefense;
                }
                foreach (var soul in Souls)
                {
                    delta += CacheSet.SoulCache.FindKey(soul.Value).MagicDefense;
                }
                
                return CalcIntProperty(baseVal, delta, rate);
            }
        }

        public float OppPhysicalDfsReduceRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).OppPhysicalDfsReduceRate;
                }
                foreach (var soul in Souls)
                {
                    ret += CacheSet.SoulCache.FindKey(soul.Value).OppPhysicalDfsReduceRate;
                }

                return Mathf.Clamp(ret, 0f, GameConsts.Hero.OppPhysicalDfsReduceRateMaxVal);
            }
        }

        public float OppMagicDfsReduceRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).OppMagicDfsReduceRate;
                }
                foreach (var soul in Souls)
                {
                    ret += CacheSet.SoulCache.FindKey(soul.Value).OppMagicDfsReduceRate;
                }

                return Mathf.Clamp(ret, 0f, GameConsts.Hero.OppMagicDfsReduceRateMaxVal);
            }
        }

        public float PhysicalAtkHPAbsorbRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).PhysicalAtkHPAbsorbRate;
                }
                foreach (var soul in Souls)
                {
                    ret += CacheSet.SoulCache.FindKey(soul.Value).PhysicalAtkHPAbsorbRate;
                }

                return Mathf.Clamp(ret, 0f, GameConsts.Hero.PhysicalAtkHPAbsorbRateMaxVal);
            }
        }

        public float MagicAtkHPAbsorbRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).MagicAtkHPAbsorbRate;
                }
                foreach (var soul in Souls)
                {
                    ret += CacheSet.SoulCache.FindKey(soul.Value).MagicAtkHPAbsorbRate;
                }

                return Mathf.Clamp(ret, 0f, GameConsts.Hero.MagicAtkHPAbsorbRateMaxVal);
            }
        }

        public float PhysicalAtkReflectRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).PhysicalAtkReflectRate;
                }
                foreach (var soul in Souls)
                {
                    ret += CacheSet.SoulCache.FindKey(soul.Value).PhysicalAtkReflectRate;
                }

                return Mathf.Clamp(ret, 0f, GameConsts.Hero.PhysicalAtkReflectRateMaxVal);
            }
        }

        public float MagicAtkReflectRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).MagicAtkReflectRate;
                }
                foreach (var soul in Souls)
                {
                    ret += CacheSet.SoulCache.FindKey(soul.Value).MagicAtkReflectRate;
                }

                return Mathf.Clamp(ret, 0f, GameConsts.Hero.MagicAtkReflectRateMaxVal);
            }
        }

        public float DamageReductionRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).DamageReductionRate;
                }

                return Mathf.Clamp(ret, 0f, GameConsts.Hero.DamageReductionRateMaxVal);
            }
        }

        public float ReducedSkillCoolDownRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).ReducedSkillCoolDownRate;
                }

                return Mathf.Clamp(ret, 0f, GameConsts.Hero.ReducedSkillCoolDownRateMaxVal);
            }
        }

        public float ReduceSwitchHeroCoolDownRate
        {
            get
            {
                return 0;
            }
        }

        public float CriticalHitProb
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).CriticalHitProb;
                }
                foreach (var soul in Souls)
                {
                    ret += CacheSet.SoulCache.FindKey(soul.Value).CriticalHitProb;
                }

                return Mathf.Clamp(ret, 0f, GameConsts.Hero.CriticalHitProbMaxVal);
            }
        }

        public float CriticalHitRate
        {
            get
            {
                var ret = HeroDataRow.CriticalHitRate;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).CriticalHitRate;
                }
                foreach (var soul in Souls)
                {
                    ret += CacheSet.SoulCache.FindKey(soul.Value).CriticalHitRate;
                }

                return Mathf.Clamp(ret, 0f, GameConsts.Hero.CriticalHitRateMaxVal);
            }
        }

        public float AntiCriticalHitProb
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).AntiCriticalHitProb;
                }
                foreach (var soul in Souls)
                {
                    ret += CacheSet.SoulCache.FindKey(soul.Value).AntiCriticalHitProb;
                }

                return Mathf.Clamp(ret, 0f, GameConsts.Hero.AntiCriticalHitProbMaxVal);
            }
        }

        public int AdditionalDamage
        {
            get
            {
                var ret = 0;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).AdditionalDamage;
                }

                return ret;
            }
        }

        public float RecoverHP
        {
            get
            {
                var ret = HeroElevationDataRow.RecoverHPBase;
                foreach (var soul in Souls)
                {
                    ret += CacheSet.SoulCache.FindKey(soul.Value).RecoverHP;
                }
                return ret;
            }
        }

        public float ReducedHeroSwitchCDRate
        {
            get
            {
                float ret = 0;
                foreach (var gear in Gears)
                {
                    ret += CacheSet.GearCache.FindKey(gear.Value).ReducedHeroSwitchCDRate;
                }
                return ret;
            }
        }

        public int ElementId
        {
            get
            {
                return HeroDataRow.ElementId;
            }
        }

        public static int CalcIntProperty(int baseVal, int increseAmount, float increaseRate)
        {
            return Mathf.RoundToInt((baseVal + increseAmount) * (1f + increaseRate));
        }

        public static float CalcFloatProperty(float baseVal, float increaseAmount, float increaseRate)
        {
            return (baseVal + increaseAmount) * (1f + increaseRate);
        }
    }
}
