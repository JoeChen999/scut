using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract]
    public class DTSoul : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTSoul> DTSoulCache = new MemoryCacheStruct<DTSoul>();

        public DTSoul()
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
        public int UpgradedId
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
        public int MinLevel
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
        public bool Broadcast
        {
            get;
            set;
        }

        [ProtoMember(11), EntityField]
        public float AddValue
        {
            get;
            set;
        }

        [ProtoMember(12), EntityField]
        public int StrengthenItemId
        {
            get;
            set;
        }

        [ProtoMember(13), EntityField]
        public int StrengthenItemCount
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
            UpgradedId = int.Parse(rowData[index++]);
            IconId = int.Parse(rowData[index++]);
            MinLevel = int.Parse(rowData[index++]);
            Price = int.Parse(rowData[index++]);
            Broadcast = bool.Parse(rowData[index++]);
            AddValue = float.Parse(rowData[index++]);
            StrengthenItemId = int.Parse(rowData[index++]);
            StrengthenItemCount = int.Parse(rowData[index++]);
            
            DTSoulCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}