using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTChanceCost : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTChanceCost> DTChanceCostCache = new MemoryCacheStruct<DTChanceCost>();

        public DTChanceCost()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int CoinCost
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int CoinCostAll
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int MoneyCost
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int MoneyCostAll
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
            CoinCost = int.Parse(rowData[index++]);
            CoinCostAll = int.Parse(rowData[index++]);
            MoneyCost = int.Parse(rowData[index++]);
            MoneyCostAll = int.Parse(rowData[index++]);

            DTChanceCostCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}