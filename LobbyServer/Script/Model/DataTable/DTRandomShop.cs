using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTRandomShop : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTRandomShop> DTRandomShopCache = new MemoryCacheStruct<DTRandomShop>();

        public DTRandomShop()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int DropItemId
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int DropItemCount
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int CurrencyType
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int CurrencyPrice
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
            DropItemId = int.Parse(rowData[index++]);
            DropItemCount = int.Parse(rowData[index++]);
            CurrencyType = int.Parse(rowData[index++]);
            CurrencyPrice = int.Parse(rowData[index++]);

            DTRandomShopCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}