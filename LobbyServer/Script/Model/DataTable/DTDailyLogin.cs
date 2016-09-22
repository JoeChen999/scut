using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTDailyLogin : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTDailyLogin> DTDailyLoginCache = new MemoryCacheStruct<DTDailyLogin>();

        public DTDailyLogin()
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

            DTDailyLoginCache.TryAdd(Id.ToString(), this);
        }
    }
}