using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTMightLevelParam : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTMightLevelParam> DTMightLevelParamCache = new MemoryCacheStruct<DTMightLevelParam>();

        public DTMightLevelParam()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int PhysicalAttack
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int PhysicalDefense
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int MagicAttack
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int MagicDefense
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
            PhysicalAttack = int.Parse(rowData[index++]);
            PhysicalDefense = int.Parse(rowData[index++]);
            MagicAttack = int.Parse(rowData[index++]);
            MagicDefense = int.Parse(rowData[index++]);

            DTMightLevelParamCache.TryAdd(Id.ToString(), this);
        }
    }
}