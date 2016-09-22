using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract]
    public class DTHeroConsciousnessBase : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTHeroConsciousnessBase> DTHeroConsciousnessBaseCache = new MemoryCacheStruct<DTHeroConsciousnessBase>();

        public DTHeroConsciousnessBase()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int LevelUpItemId
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int LevelUpItemCount
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int MaxHPBase
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int PhysicalAttackBase
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int PhysicalDefenseBase
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public int MagicAttackBase
        {
            get;
            set;
        }

        [ProtoMember(8), EntityField]
        public int MagicDefenseBase
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public float RecoverHPBase
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
            LevelUpItemId = int.Parse(rowData[index++]);
            LevelUpItemCount = int.Parse(rowData[index++]);
            MaxHPBase = int.Parse(rowData[index++]);
            PhysicalAttackBase = int.Parse(rowData[index++]);
            PhysicalDefenseBase = int.Parse(rowData[index++]);
            MagicAttackBase = int.Parse(rowData[index++]);
            MagicDefenseBase = int.Parse(rowData[index++]);
            RecoverHPBase = float.Parse(rowData[index++]);

            DTHeroConsciousnessBaseCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}