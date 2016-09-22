using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTDrop : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTDrop> DTDropCache = new MemoryCacheStruct<DTDrop>();
        public DTDrop()
        {
            DropList = new CacheList<DropItem>();
        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int RepeatCount
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public CacheList<DropItem> DropList
        {
            get;
            private set;
        }



        public void ParseRow(string[] rowData)
        {
            int index = 0;
            index++;
            Id = int.Parse(rowData[index++]);
            index++;
            RepeatCount = int.Parse(rowData[index++]);

            while (index < rowData.Length)
            {
                DropItem di = new DropItem();
                di.ItemId = int.Parse(rowData[index++]);
                if (di.ItemId == 0)
                {
                    break;
                }
                di.ItemCount = int.Parse(rowData[index++]);
                di.ItemWeight = int.Parse(rowData[index++]);
                DropList.Add(di);
            }
            DTDropCache.AddOrUpdate(Id.ToString(), this);
        }
    }

    [ProtoContract]
    public class DropItem : EntityChangeEvent
    {
        public DropItem()
        {
        }
        
        [ProtoMember(1)]
        public int ItemId { get; set; }
        
        [ProtoMember(2)]
        public int ItemWeight { get; set; }
        
        [ProtoMember(3)]
        public int ItemCount { get; set; }
    }
}