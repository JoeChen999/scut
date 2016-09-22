using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract]
    public class DTHero : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTHero> DTHeroCache = new MemoryCacheStruct<DTHero>();

        public DTHero()
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
        public int Profession
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int CharacterId
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public float Scale
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public int IconId
        {
            get;
            set;
        }

        [ProtoMember(8), EntityField]
        public int DefaultWeaponSuiteId
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public int DefaultStarLevel
        {
            get;
            set;
        }

        [ProtoMember(10), EntityField]
        public float Steady
        {
            get;
            set;
        }

        [ProtoMember(11), EntityField]
        public float SteadyRecoverSpeed
        {
            get;
            set;
        }  

        [ProtoMember(12), EntityField]
        public float DeadDurationBeforeAutoSwitch
        {
            get;
            set;
        }

        [ProtoMember(13), EntityField]
        public int AvoidancePriority
        {
            get;
            set;
        }


        [ProtoMember(14), EntityField]
        public float CDAfterChangeHero
        {
            get;
            set;
        }

        [ProtoMember(15), EntityField]
        public int SwitchSkillGroupId
        {
            get;
            set;
        }

        [ProtoMember(16), EntityField]
        public CacheDictionary<int, int> SkillGroupId
        {
            get;
            set;
        }

        [ProtoMember(17), EntityField]
        public float Speed
        {
            get;
            set;
        }

        [ProtoMember(18), EntityField]
        public float DamageRandomRate
        {
            get;
            set;
        }

        [ProtoMember(19), EntityField]
        public int StarLevelUpItemId
        {
            get;
            set;
        }

        [ProtoMember(20), EntityField]
        public CacheDictionary<int, float> StarFactor
        {
            get;
            set;
        }

        [ProtoMember(21), EntityField]
        public CacheDictionary<int, int> StarLevelUpItemCount
        {
            get;
            set;
        }

        [ProtoMember(22), EntityField]
        public float MaxHPFactor
        {
            get;
            set;
        }

        [ProtoMember(23), EntityField]
        public float PhysicalAttackFactor
        {
            get;
            set;
        }

        [ProtoMember(24), EntityField]
        public float PhysicalDefenseFactor
        {
            get;
            set;
        }

        [ProtoMember(25), EntityField]
        public float MagicAttackFactor
        {
            get;
            set;
        }

        [ProtoMember(26), EntityField]
        public float MagicDefenseFactor
        {
            get;
            set;
        }

        [ProtoMember(28), EntityField]
        public string AIBehavior
        {
            get;
            set;
        }

        [ProtoMember(29), EntityField]
        public float AngerIncreaseRate
        {
            get;
            set;
        }

        [ProtoMember(30), EntityField]
        public float CriticalHitRate
        {
            get;
            set;
        }

        [ProtoMember(31), EntityField]
        public string FightCharacteristic
        {
            get;
            set;
        }

        [ProtoMember(32), EntityField]
        public int ElementId
        {
            get;
            set;
        }

        [ProtoMember(33), EntityField]
        public int PortraitTextureId
        {
            get;
            set;
        }

        [ProtoMember(34), EntityField]
        public int SteadyBuffId
        {
            get;
            set;
        }

        [ProtoMember(35), EntityField]
        public float CDWhenStart
        {
            get;
            set;
        }

        [ProtoMember(36), EntityField]
        public float CriticalHitProb
        {
            get;
            set;
        }

        public float GetStarFactor(int starLevel)
        {
            return StarFactor[starLevel];
        }

        public void ParseRow(string[] rowData)
        {
            int index = 0;
            index++;
            Id = int.Parse(rowData[index++]);
            index++;
            Name = rowData[index++];
            Description = rowData[index++];
            Profession = int.Parse(rowData[index++]);
            CharacterId = int.Parse(rowData[index++]);
            Scale = float.Parse(rowData[index++]);
            IconId = int.Parse(rowData[index++]);
            DefaultWeaponSuiteId = int.Parse(rowData[index++]);
            DefaultStarLevel = int.Parse(rowData[index++]);
            Steady = float.Parse(rowData[index++]);
            SteadyRecoverSpeed = float.Parse(rowData[index++]);
            SteadyBuffId = int.Parse(rowData[index++]);
            DeadDurationBeforeAutoSwitch = float.Parse(rowData[index++]);
            AvoidancePriority = int.Parse(rowData[index++]);
            CDWhenStart = float.Parse(rowData[index++]);
            CDAfterChangeHero = float.Parse(rowData[index++]);
            SwitchSkillGroupId = int.Parse(rowData[index++]);
            SkillGroupId = new CacheDictionary<int,int>();
            for (int i = 0; i < 5; i++)
            {
                SkillGroupId[i] = int.Parse(rowData[index++]);
            }
            Speed = float.Parse(rowData[index++]);
            DamageRandomRate = float.Parse(rowData[index++]);
            StarLevelUpItemId = int.Parse(rowData[index++]);
            StarFactor = new CacheDictionary<int,float>();
            StarLevelUpItemCount = new CacheDictionary<int,int>();
            for (int i = 1; i <= 5; i++)
            {
                StarFactor[i] = float.Parse(rowData[index++]);
                StarLevelUpItemCount[i] = int.Parse(rowData[index++]);
            }
            MaxHPFactor = float.Parse(rowData[index++]);
            PhysicalAttackFactor = float.Parse(rowData[index++]);
            PhysicalDefenseFactor = float.Parse(rowData[index++]);
            MagicAttackFactor = float.Parse(rowData[index++]);
            MagicDefenseFactor = float.Parse(rowData[index++]);
            AIBehavior = rowData[index++];
            AngerIncreaseRate = float.Parse(rowData[index++]);
            CriticalHitProb = float.Parse(rowData[index++]);
            CriticalHitRate = float.Parse(rowData[index++]);
            FightCharacteristic = rowData[index++];
            ElementId = int.Parse(rowData[index++]);
            PortraitTextureId = int.Parse(rowData[index++]);

            DTHeroCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}