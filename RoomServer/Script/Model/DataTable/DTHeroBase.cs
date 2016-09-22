using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract]
    public class DTHeroBase : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTHeroBase> DTHeroBaseCache = new MemoryCacheStruct<DTHeroBase>();

        public DTHeroBase()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int LevelUpExp
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int MaxHPBase
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int PhysicalAttackBase
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int PhysicalDefenseBase
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int MagicAttackBase
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public int MagicDefenseBase
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
            LevelUpExp = int.Parse(rowData[index++]);
            MaxHPBase = int.Parse(rowData[index++]);
            PhysicalAttackBase = int.Parse(rowData[index++]);
            PhysicalDefenseBase = int.Parse(rowData[index++]);
            MagicAttackBase = int.Parse(rowData[index++]);
            MagicDefenseBase = int.Parse(rowData[index++]);

            DTHeroBaseCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}