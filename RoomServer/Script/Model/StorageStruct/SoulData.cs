using ProtoBuf;
using ZyGames.Framework.Event;

namespace Genesis.GameServer.RoomServer
{
    [ProtoContract]
    public class SoulData : EntityChangeEvent
    {
        private DTSoul m_SoulDataRow = null;
        private DTSoul m_Soul
        {
            get
            {
                if (m_SoulDataRow == null)
                {
                    m_SoulDataRow = CacheSet.SoulTable.GetData(TypeId);
                }
                return m_SoulDataRow;
            }
        }

        public SoulData()
        {

        }

        [ProtoMember(1)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public int TypeId
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public bool Removed
        {
            get;
            set;
        }

        public int EffectId { get { return m_Soul.Type; } }

        public int Quality { get { return m_Soul.Quality; } }

        public float EffectValue { get { return m_Soul.AddValue; } }

        public string Name { get { return m_Soul.Name; } }

        public string Description { get { return m_Soul.Description; } }

        public int IconId { get { return m_Soul.IconId; } }

        public int StrengthenItemId { get { return m_Soul.StrengthenItemId; } }

        public int StrengthenItemCount { get { return m_Soul.StrengthenItemCount; } }

        public int UpgradedId { get { return m_Soul.UpgradedId; } }

        public int MaxHP
        {
            get
            {
                return EffectId == (int)SoulEffect.MaxHP ? (int)EffectValue : 0;
            }
        }

        public int PhysicalAttack
        {
            get
            {
                return EffectId == (int)SoulEffect.PhysicalAttack ? (int)EffectValue : 0;
            }
        }

        public int MagicAttack
        {
            get
            {
                return EffectId == (int)SoulEffect.MagicAttack ? (int)EffectValue : 0;
            }
        }

        public int PhysicalDefense
        {
            get
            {
                return EffectId == (int)SoulEffect.PhysicalDefense ? (int)EffectValue : 0;
            }
        }

        public int MagicDefense
        {
            get
            {
                return EffectId == (int)SoulEffect.MagicDefense ? (int)EffectValue : 0;
            }
        }

        public float CriticalHitProb
        {
            get
            {
                return EffectId == (int)SoulEffect.CriticalHitProb ? EffectValue : 0;
            }
        }

        public float CriticalHitRate
        {
            get
            {
                return EffectId == (int)SoulEffect.CriticalHitRate ? EffectValue : 0;
            }
        }

        public float PhysicalAtkHPAbsorbRate
        {
            get
            {
                return EffectId == (int)SoulEffect.PhysicalAtkHPAbsorbRate ? EffectValue : 0;
            }
        }

        public float MagicAtkHPAbsorbRate
        {
            get
            {
                return EffectId == (int)SoulEffect.MagicAtkHPAbsorbRate ? EffectValue : 0;
            }
        }

        public float AntiCriticalHitProb
        {
            get
            {
                return EffectId == (int)SoulEffect.AntiCriticalHitProb ? EffectValue : 0;
            }
        }

        public float OppPhysicalDfsReduceRate
        {
            get
            {
                return EffectId == (int)SoulEffect.OppPhysicalDfsReduceRate ? EffectValue : 0;
            }
        }

        public float OppMagicDfsReduceRate
        {
            get
            {
                return EffectId == (int)SoulEffect.OppMagicDfsReduceRate ? EffectValue : 0;
            }
        }

        public float PhysicalAtkReflectRate
        {
            get
            {
                return EffectId == (int)SoulEffect.PhysicalAtkReflectRate ? EffectValue : 0;
            }
        }

        public float MagicAtkReflectRate
        {
            get
            {
                return EffectId == (int)SoulEffect.MagicAtkReflectRate ? EffectValue : 0;
            }
        }

        public float RecoverHP
        {
            get
            {
                return EffectId == (int)SoulEffect.RecoverHP ? EffectValue : 0;
            }
        }
    }
}
