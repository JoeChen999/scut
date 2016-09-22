using ProtoBuf;
using System;
using UnityEngine;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.RoomServer
{
    [ProtoContract]
    public class RoomHero : EntityChangeEvent
    {
        private DTHero m_HeroDataRow = null;
        private DTHeroConsciousnessBase m_HeroConsciousnessDataRow = null;
        private DTHeroElevationBase m_HeroElevationDataRow = null;
        private DTHeroBase m_HeroBaseDataRow = null;

        public RoomHero()
        {
            Souls = new CacheList<SoulData>();
            Gears = new CacheList<GearData>();
            Skills = new CacheList<int>();
            LastLeaveBattleTime = DateTime.UtcNow.Ticks;
        }
        [ProtoMember(1), EntityField(true)]
        public int EntityId { get; set; }

        [ProtoMember(2)]
        public int HeroType { get; set; }

        [ProtoMember(3)]
        public int HeroLv { get; set; }

        [ProtoMember(4)]
        public int HeroExp { get; set; }

        [ProtoMember(5)]
        public int HeroStarLevel { get; set; }

        [ProtoMember(6)]
        public int ConsciousnessLevel { get; set; }

        [ProtoMember(7)]
        public int ElevationLevel { get; set; }

        [ProtoMember(8)]
        public CacheList<SoulData> Souls { get; set; }

        [ProtoMember(9)]
        public CacheList<GearData> Gears { get; set; }

        [ProtoMember(10)]
        public int HP { get; set; }

        [ProtoMember(11)]
        public CacheList<int> Skills
        {
            get;
            set;
        }

        [ProtoMember(12)]
        public float SteadyBarValue
        {
            get;
            set;
        }

        [ProtoMember(13)]
        public bool HasSteadyBar
        {
            get;
            set;
        }

        [ProtoMember(14)]
        public long LastBreakSteadyTime
        {
            get;
            set;
        }

        [ProtoMember(15)]
        public long LastLeaveBattleTime
        {
            get;
            set;
        }


        public DTHero HeroDataRow
        {
            get
            {
                if(m_HeroDataRow == null)
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
                if (m_HeroConsciousnessDataRow == null)
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
                if (m_HeroElevationDataRow == null)
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
                if (m_HeroBaseDataRow == null)
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
                    delta += gear.MaxHP;
                }
                foreach (var soul in Souls)
                {
                    delta += soul.MaxHP;
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
                    delta += gear.PhysicalAttack;
                }
                foreach (var soul in Souls)
                {
                    delta += soul.PhysicalAttack;
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
                    delta += gear.PhysicalDefense;
                }
                foreach (var soul in Souls)
                {
                    delta += soul.PhysicalDefense;
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
                    delta += gear.MagicAttack;
                }
                foreach (var soul in Souls)
                {
                    delta += soul.MagicAttack;
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
                    delta += gear.MagicDefense;
                }
                foreach (var soul in Souls)
                {
                    delta += soul.MagicDefense;
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
                    ret += gear.OppPhysicalDfsReduceRate;
                }
                foreach (var soul in Souls)
                {
                    ret += soul.OppPhysicalDfsReduceRate;
                }

                return Mathf.Clamp(ret, 0f, Constants.Hero.OppPhysicalDfsReduceRateMaxVal);
            }
        }

        public float OppMagicDfsReduceRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += gear.OppMagicDfsReduceRate;
                }
                foreach (var soul in Souls)
                {
                    ret += soul.OppMagicDfsReduceRate;
                }

                return Mathf.Clamp(ret, 0f, Constants.Hero.OppMagicDfsReduceRateMaxVal);
            }
        }

        public float PhysicalAtkHPAbsorbRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += gear.PhysicalAtkHPAbsorbRate;
                }
                foreach (var soul in Souls)
                {
                    ret += soul.PhysicalAtkHPAbsorbRate;
                }

                return Mathf.Clamp(ret, 0f, Constants.Hero.PhysicalAtkHPAbsorbRateMaxVal);
            }
        }

        public float MagicAtkHPAbsorbRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += gear.MagicAtkHPAbsorbRate;
                }
                foreach (var soul in Souls)
                {
                    ret += soul.MagicAtkHPAbsorbRate;
                }

                return Mathf.Clamp(ret, 0f, Constants.Hero.MagicAtkHPAbsorbRateMaxVal);
            }
        }

        public float PhysicalAtkReflectRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += gear.PhysicalAtkReflectRate;
                }
                foreach (var soul in Souls)
                {
                    ret += soul.PhysicalAtkReflectRate;
                }

                return Mathf.Clamp(ret, 0f, Constants.Hero.PhysicalAtkReflectRateMaxVal);
            }
        }

        public float MagicAtkReflectRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += gear.MagicAtkReflectRate;
                }
                foreach (var soul in Souls)
                {
                    ret += soul.MagicAtkReflectRate;
                }

                return Mathf.Clamp(ret, 0f, Constants.Hero.MagicAtkReflectRateMaxVal);
            }
        }

        public float DamageReductionRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += gear.DamageReductionRate;
                }

                return Mathf.Clamp(ret, 0f, Constants.Hero.DamageReductionRateMaxVal);
            }
        }

        public float ReducedSkillCoolDownRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += gear.ReducedSkillCoolDownRate;
                }

                return Mathf.Clamp(ret, 0f, Constants.Hero.ReducedSkillCoolDownRateMaxVal);
            }
        }

        public float ReduceSwitchHeroCoolDownRate
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += gear.ReducedHeroSwitchCDRate;
                }
                return Mathf.Clamp(ret, 0f, Constants.Hero.ReducedHeroSwitchCDRate);
            }
        }

        public float CriticalHitProb
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += gear.CriticalHitProb;
                }
                foreach (var soul in Souls)
                {
                    ret += soul.CriticalHitProb;
                }

                return Mathf.Clamp(ret, 0f, Constants.Hero.CriticalHitProbMaxVal);
            }
        }

        public float CriticalHitRate
        {
            get
            {
                var ret = HeroDataRow.CriticalHitRate;
                foreach (var gear in Gears)
                {
                    ret += gear.CriticalHitRate;
                }
                foreach (var soul in Souls)
                {
                    ret += soul.CriticalHitRate;
                }

                return Mathf.Clamp(ret, 0f, Constants.Hero.CriticalHitRateMaxVal);
            }
        }

        public float AntiCriticalHitProb
        {
            get
            {
                var ret = 0f;
                foreach (var gear in Gears)
                {
                    ret += gear.AntiCriticalHitProb;
                }
                foreach (var soul in Souls)
                {
                    ret += soul.AntiCriticalHitProb;
                }

                return Mathf.Clamp(ret, 0f, Constants.Hero.AntiCriticalHitProbMaxVal);
            }
        }

        public int AdditionalDamage
        {
            get
            {
                var ret = 0;
                foreach (var gear in Gears)
                {
                    ret += gear.AdditionalDamage;
                }

                return ret;
            }
        }

        public float RecoverHP
        {
            get
            {
                var ret = HeroElevationDataRow.RecoverHPBase + HeroConsciousnessDataRow.RecoverHPBase;
                foreach (var soul in Souls)
                {
                    ret += soul.RecoverHP;
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
