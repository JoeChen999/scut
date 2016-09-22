using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTMeridian : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTMeridian> DTMeridianCache = new MemoryCacheStruct<DTMeridian>();

        public DTMeridian()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        public int Type
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int CostMeridianEnergy
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int CostMoney
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int RewardId
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int RewardCount
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int RewardMoney
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public int RewardCoin
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
        public float AttributeValue
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
            Type = int.Parse(rowData[index++]);
            CostMeridianEnergy = int.Parse(rowData[index++]);
            CostMoney = int.Parse(rowData[index++]);
            RewardId = int.Parse(rowData[index++]);
            RewardCount = int.Parse(rowData[index++]);
            RewardMoney = int.Parse(rowData[index++]);
            RewardCoin = int.Parse(rowData[index++]);
            AttributeType = int.Parse(rowData[index++]);
            AttributeValue = float.Parse(rowData[index++]);

            DTMeridianCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}