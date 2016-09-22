using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTOperationActivitySevenDay : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTOperationActivitySevenDay> DTOperationActivitySevenDayCache = new MemoryCacheStruct<DTOperationActivitySevenDay>();

        public DTOperationActivitySevenDay()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int RewardId
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int RewardCount
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
            RewardId = int.Parse(rowData[index++]);
            RewardCount = int.Parse(rowData[index++]);

            DTOperationActivitySevenDayCache.TryAdd(Id.ToString(), this);
        }
    }
}