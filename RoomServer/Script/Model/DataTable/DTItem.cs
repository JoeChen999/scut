using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract]
    public class DTItem : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTItem> DTItemCache = new MemoryCacheStruct<DTItem>();

        public DTItem()
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
        public int MaxCount
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public int Price
        {
            get;
            set;
        }

        [ProtoMember(10), EntityField]
        public bool AutoUse
        {
            get;
            set;
        }

        [ProtoMember(11), EntityField]
        public bool Broadcast
        {
            get;
            set;
        }

        [ProtoMember(12), EntityField]
        public int FunctionId
        {
            get;
            set;
        }

        [ProtoMember(13), EntityField]
        public string FunctionParams
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
            index++; // Skip Order
            Type = int.Parse(rowData[index++]);
            Quality = int.Parse(rowData[index++]);
            IconId = int.Parse(rowData[index++]);
            MinLevel = int.Parse(rowData[index++]);
            MaxCount = int.Parse(rowData[index++]);
            Price = int.Parse(rowData[index++]);
            AutoUse = bool.Parse(rowData[index++]);
            Broadcast = bool.Parse(rowData[index++]);
            FunctionId = int.Parse(rowData[index++]);
            FunctionParams = rowData[index++];

            DTItemCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}