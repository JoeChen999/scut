using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTInstanceCosmosCrack : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTInstanceCosmosCrack> DTInstanceCosmosCrackCache = new MemoryCacheStruct<DTInstanceCosmosCrack>();

        public DTInstanceCosmosCrack()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        public void ParseRow(string[] rowData)
        {
            int index = 0;
            index++;
            Id = int.Parse(rowData[index++]);

            DTInstanceCosmosCrackCache.TryAdd(Id.ToString(), this);
        }
    }
}