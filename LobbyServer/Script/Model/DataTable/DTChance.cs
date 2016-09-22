using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTChance : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTChance> DTChanceCache = new MemoryCacheStruct<DTChance>();

        public DTChance()
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
        public int PackageWeight
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
            PackageWeight = int.Parse(rowData[index++]);
            
            while (index < rowData.Length)
            {
                DropItem di = new DropItem();
                di.ItemId = int.Parse(rowData[index++]);
                if (di.ItemId == -1)
                {
                    break;
                }
                di.ItemCount = int.Parse(rowData[index++]);
                di.ItemWeight = int.Parse(rowData[index++]);
                DropList.Add(di);
            }

            DTChanceCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}