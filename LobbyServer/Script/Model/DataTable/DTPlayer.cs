using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTPlayer : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTPlayer> DTPlayerCache = new MemoryCacheStruct<DTPlayer>();

        public DTPlayer()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int LevelUpExp
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
            LevelUpExp = int.Parse(rowData[index++]);

            DTPlayerCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}