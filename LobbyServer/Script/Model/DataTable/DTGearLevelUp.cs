using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTGearLevelUp : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTGearLevelUp> DTGearCache = new MemoryCacheStruct<DTGearLevelUp>();

        public DTGearLevelUp()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            private set;
        }

        [ProtoMember(2), EntityField]
        public CacheList<int> LevelUpCostCoin
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
            LevelUpCostCoin = new CacheList<int>();
            for (; index < rowData.Length; index++)
            {
                LevelUpCostCoin.Add(int.Parse(rowData[index]));
            }
            DTGearCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}
