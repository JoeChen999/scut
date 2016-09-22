using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract]
    public class DTGear : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTGear> DTGearCache = new MemoryCacheStruct<DTGear>();

        public DTGear()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public string Name
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public string Description
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int Type
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int Quality
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int IconId
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public int MinLevel
        {
            get;
            set;
        }

        [ProtoMember(8), EntityField]
        public int Price
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public bool Broadcast
        {
            get;
            set;
        }

        [ProtoMember(11), EntityField]
        public int StrengthenItemId
        {
            get;
            set;
        }

        [ProtoMember(13), EntityField]
        public int MaxHP
        {
            get;
            set;
        }

        [ProtoMember(14), EntityField]
        public float LCMaxHP
        {
            get;
            set;
        }

        [ProtoMember(16), EntityField]
        public float SLCMaxHPIncreaseRate
        {
            get;
            set;
        }

        [ProtoMember(17), EntityField]
        public int PhysicalAttack
        {
            get;
            set;
        }

        [ProtoMember(18), EntityField]
        public float LCPhysicalAttack
        {
            get;
            set;
        }

        [ProtoMember(19), EntityField]
        public int PhysicalDefense
        {
            get;
            set;
        }

        [ProtoMember(20), EntityField]
        public float LCPhysicalDefense
        {
            get;
            set;
        }

        [ProtoMember(22), EntityField]
        public float SLCPhysicalAtkIncreaseRate
        {
            get;
            set;
        }


        [ProtoMember(24), EntityField]
        public float SLCPhysicalDfsIncreaseRate
        {
            get;
            set;
        }

        [ProtoMember(25), EntityField]
        public float PhysicalAtkHPAbsorbRate
        {
            get;
            set;
        }

        [ProtoMember(26), EntityField]
        public float PhysicalAtkReflectRate
        {
            get;
            set;
        }

        [ProtoMember(27), EntityField]
        public float OppPhysicalDfsReduceRate
        {
            get;
            set;
        }

        [ProtoMember(28), EntityField]
        public int MagicAttack
        {
            get;
            set;
        }

        [ProtoMember(29), EntityField]
        public float LCMagicAttack
        {
            get;
            set;
        }

        [ProtoMember(30), EntityField]
        public int MagicDefense
        {
            get;
            set;
        }

        [ProtoMember(31), EntityField]
        public float LCMagicDefense
        {
            get;
            set;
        }

        [ProtoMember(33), EntityField]
        public float SLCMagicAtkIncreaseRate
        {
            get;
            set;
        }

        [ProtoMember(35), EntityField]
        public float SLCMagicDfsIncreaseRate
        {
            get;
            set;
        }

        [ProtoMember(36), EntityField]
        public float MagicAtkHPAbsorbRate
        {
            get;
            set;
        }

        [ProtoMember(37), EntityField]
        public float MagicAtkReflectRate
        {
            get;
            set;
        }

        [ProtoMember(38), EntityField]
        public float OppMagicDfsReduceRate
        {
            get;
            set;
        }

        [ProtoMember(39), EntityField]
        public float DamageReductionRate
        {
            get;
            set;
        }

        [ProtoMember(40), EntityField]
        public int AdditionalDamage
        {
            get;
            set;
        }

        [ProtoMember(41), EntityField]
        public float CriticalHitRate
        {
            get;
            set;
        }

        [ProtoMember(42), EntityField]
        public float CriticalHitProb
        {
            get;
            set;
        }

        [ProtoMember(43), EntityField]
        public float AntiCriticalHitProb
        {
            get;
            set;
        }

        [ProtoMember(44), EntityField]
        public float ReducedSkillCoolDownRate
        {
            get;
            set;
        }

        [ProtoMember(45), EntityField]
        public float ReducedHeroSwitchCDRate
        {
            get;
            set;
        }

        public void ParseRow(string[] rowData)
        {
            int index = 0;
            index++;
            Id = int.Parse(rowData[index++]);
            index++;
            Name = rowData[index++];
            Description = rowData[index++];
            Type = int.Parse(rowData[index++]);
            Quality = int.Parse(rowData[index++]);
            IconId = int.Parse(rowData[index++]);
            MinLevel = int.Parse(rowData[index++]);
            Price = int.Parse(rowData[index++]);
            Broadcast = bool.Parse(rowData[index++]);
            StrengthenItemId = int.Parse(rowData[index++]);
            MaxHP = int.Parse(rowData[index++]);
            LCMaxHP = float.Parse(rowData[index++]);
            SLCMaxHPIncreaseRate = float.Parse(rowData[index++]);
            PhysicalAttack = int.Parse(rowData[index++]);
            LCPhysicalAttack = float.Parse(rowData[index++]);
            PhysicalDefense = int.Parse(rowData[index++]);
            LCPhysicalDefense = float.Parse(rowData[index++]);
            SLCPhysicalAtkIncreaseRate = float.Parse(rowData[index++]);
            SLCPhysicalDfsIncreaseRate = float.Parse(rowData[index++]);
            PhysicalAtkHPAbsorbRate = float.Parse(rowData[index++]);
            PhysicalAtkReflectRate = float.Parse(rowData[index++]);
            OppPhysicalDfsReduceRate = float.Parse(rowData[index++]);
            MagicAttack = int.Parse(rowData[index++]);
            LCMagicAttack = float.Parse(rowData[index++]);
            MagicDefense = int.Parse(rowData[index++]);
            LCMagicDefense = float.Parse(rowData[index++]);
            SLCMagicAtkIncreaseRate = float.Parse(rowData[index++]);
            SLCMagicDfsIncreaseRate = float.Parse(rowData[index++]);
            MagicAtkHPAbsorbRate = float.Parse(rowData[index++]);
            MagicAtkReflectRate = float.Parse(rowData[index++]);
            OppMagicDfsReduceRate = float.Parse(rowData[index++]);
            DamageReductionRate = float.Parse(rowData[index++]);
            AdditionalDamage = int.Parse(rowData[index++]);
            CriticalHitRate = float.Parse(rowData[index++]);
            CriticalHitProb = float.Parse(rowData[index++]);
            AntiCriticalHitProb = float.Parse(rowData[index++]);
            ReducedSkillCoolDownRate = float.Parse(rowData[index++]);
            ReducedHeroSwitchCDRate = float.Parse(rowData[index++]);
            DTGearCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}