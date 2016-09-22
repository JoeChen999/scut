using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTEpigraph : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTEpigraph> DTEpigraphCache = new MemoryCacheStruct<DTEpigraph>();

        public DTEpigraph()
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
        public int IconId
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int MinLevel
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public int Price
        {
            get;
            set;
        }

        [ProtoMember(8), EntityField]
        public int AttributeType
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public CacheList<float> AttributeValue
        {
            get;
            set;
        }

        [ProtoMember(10), EntityField]
        public CacheList<int> CostPieceCount
        {
            get;
            set;
        }

        [ProtoMember(11), EntityField]
        public int PieceId
        {
            get;
            set;
        }

        [ProtoMember(12), EntityField]
        public int PieceCount
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
            index++; // Skip Quality
            IconId = int.Parse(rowData[index++]);
            MinLevel = int.Parse(rowData[index++]);
            Price = int.Parse(rowData[index++]);
            AttributeType = int.Parse(rowData[index++]);
            AttributeValue = new CacheList<float>();
            CostPieceCount = new CacheList<int>();
            for (int i = 0; i < 5; i++)
            {
                AttributeValue.Add(float.Parse(rowData[index++]));
                CostPieceCount.Add(int.Parse(rowData[index++]));
            }
            PieceId = int.Parse(rowData[index++]);
            PieceCount = int.Parse(rowData[index++]);

            DTEpigraphCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}