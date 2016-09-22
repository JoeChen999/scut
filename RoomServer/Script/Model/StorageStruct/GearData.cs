using ProtoBuf;
using UnityEngine;
using ZyGames.Framework.Event;

namespace Genesis.GameServer.RoomServer
{
    [ProtoContract]
    public class GearData : EntityChangeEvent
    {
        private DTGear m_GearDataRow = null;
        private DTGear GearDataRow
        {
            get
            {
                if (m_GearDataRow == null)
                {
                    m_GearDataRow = CacheSet.GearTable.GetData(Type);
                }
                return m_GearDataRow;
            }
        }

        public GearData()
        {

        }

        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public int Type { get; set; }

        [ProtoMember(3)]
        public int Level { get; set; }

        [ProtoMember(4)]
        public int StrengthenLevel { get; set; }

        private float CalcProperty(float baseValue, float levelCoef, float strengthenLevelCoef)
        {
            return (baseValue + levelCoef * (Level - 1)) * (1 + (StrengthenLevel * strengthenLevelCoef));
        }

        public int MaxHP
        {
            get
            {
                return GetMaxHP(Level, StrengthenLevel);
            }
        }

        public int GetMaxHP(int level, int strengthenLevel)
        {
            return Mathf.RoundToInt((GearDataRow.MaxHP + GearDataRow.LCMaxHP * (level - 1)) * (1 + (strengthenLevel * GearDataRow.SLCMaxHPIncreaseRate)));
        }

        public int PhysicalAttack
        {
            get
            {
                return GetPhysicalAttack(Level, StrengthenLevel);
            }
        }

        public int GetPhysicalAttack(int level, int strengthenLevel)
        {
            return Mathf.RoundToInt((GearDataRow.PhysicalAttack + GearDataRow.LCPhysicalAttack * (level - 1)) * (1 + (strengthenLevel * GearDataRow.SLCPhysicalAtkIncreaseRate)));
        }

        public int PhysicalDefense
        {
            get
            {
                return GetPhysicalDefense(Level, StrengthenLevel);
            }
        }

        public int GetPhysicalDefense(int level, int strengthenLevel)
        {
            return Mathf.RoundToInt((GearDataRow.PhysicalDefense + GearDataRow.LCPhysicalDefense * (level - 1)) * (1 + (strengthenLevel * GearDataRow.SLCPhysicalDfsIncreaseRate)));
        }

        public int MagicAttack
        {
            get
            {
                return GetMagicAttack(Level, StrengthenLevel);
            }
        }

        public int GetMagicAttack(int level, int strengthenLevel)
        {
            return Mathf.RoundToInt((GearDataRow.MagicAttack + GearDataRow.LCMagicAttack * (level - 1)) * (1 + (strengthenLevel * GearDataRow.SLCMagicAtkIncreaseRate)));
        }

        public int MagicDefense
        {
            get
            {
                return GetMagicDefense(Level, StrengthenLevel);
            }
        }

        public int GetMagicDefense(int level, int strengthenLevel)
        {
            return Mathf.RoundToInt((GearDataRow.MagicDefense + GearDataRow.LCMagicDefense * (level - 1)) * (1 + (strengthenLevel * GearDataRow.SLCMagicDfsIncreaseRate)));
        }

        public float LCMaxHP
        {
            get
            {
                return GearDataRow.LCMaxHP;
            }
        }

        public float LCPhysicalAttack
        {
            get
            {
                return GearDataRow.LCPhysicalAttack;
            }
        }

        public float LCPhysicalDefense
        {
            get
            {
                return GearDataRow.LCPhysicalDefense;
            }
        }

        public float LCMagicAttack
        {
            get
            {
                return GearDataRow.LCMagicAttack;
            }
        }

        public float LCMagicDefense
        {
            get
            {
                return GearDataRow.LCMagicDefense;
            }
        }

        public float SLCMaxHPIncreaseRate
        {
            get
            {
                return GearDataRow.SLCMaxHPIncreaseRate;
            }
        }

        public float SLCPhysicalAtkIncreaseRate
        {
            get
            {
                return GearDataRow.SLCPhysicalAtkIncreaseRate;
            }
        }

        public float SLCPhysicalDfsIncreaseRate
        {
            get
            {
                return GearDataRow.SLCPhysicalDfsIncreaseRate;
            }
        }

        public float SLCMagicAtkIncreaseRate
        {
            get
            {
                return GearDataRow.SLCMagicAtkIncreaseRate;
            }
        }

        public float SLCMagicDfsIncreaseRate
        {
            get
            {
                return GearDataRow.SLCMagicDfsIncreaseRate;
            }
        }

        public float ReducedSkillCoolDownRate
        {
            get
            {
                return GearDataRow.ReducedSkillCoolDownRate;
            }
        }

        public int StrengthenItemId
        {
            get
            {
                return GearDataRow.StrengthenItemId;
            }
        }

        public float CriticalHitProb
        {
            get
            {
                return GearDataRow.CriticalHitProb;
            }
        }

        public float CriticalHitRate
        {
            get
            {
                return GearDataRow.CriticalHitRate;
            }
        }

        public float PhysicalAtkReflectRate
        {
            get
            {
                return GearDataRow.PhysicalAtkReflectRate;
            }
        }

        public float MagicAtkReflectRate
        {
            get
            {
                return GearDataRow.MagicAtkReflectRate;
            }
        }

        public float OppPhysicalDfsReduceRate
        {
            get
            {
                return GearDataRow.OppPhysicalDfsReduceRate;
            }
        }

        public float OppMagicDfsReduceRate
        {
            get
            {
                return GearDataRow.OppMagicDfsReduceRate;
            }
        }

        public float DamageReductionRate
        {
            get
            {
                return GearDataRow.DamageReductionRate;
            }
        }

        public int AdditionalDamage
        {
            get
            {
                return GearDataRow.AdditionalDamage;
            }
        }

        public float AntiCriticalHitProb
        {
            get
            {
                return GearDataRow.AntiCriticalHitProb;
            }
        }

        public float PhysicalAtkHPAbsorbRate
        {
            get
            {
                return GearDataRow.PhysicalAtkHPAbsorbRate;
            }
        }

        public float MagicAtkHPAbsorbRate
        {
            get
            {
                return GearDataRow.MagicAtkHPAbsorbRate;
            }
        }

        public float ReducedHeroSwitchCDRate
        {
            get
            {
                return GearDataRow.ReducedHeroSwitchCDRate;
            }
        }

        public int Quality
        {
            get
            {
                return GearDataRow.Quality;
            }
        }

        public int Position
        {
            get
            {
                return GearDataRow.Type;
            }
        }
    }
}
