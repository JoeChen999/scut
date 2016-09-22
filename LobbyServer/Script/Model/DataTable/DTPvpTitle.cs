using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTPvpTitle : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTPvpTitle> DTPvpTitleCache = new MemoryCacheStruct<DTPvpTitle>();

        public DTPvpTitle()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public string TitleName
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int TitleTextureId
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int TitleMinScore
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int TitleMaxScore
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public CacheDictionary<int, int> Reward
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
            TitleName = rowData[index++];
            TitleTextureId = int.Parse(rowData[index++]);
            TitleMinScore = int.Parse(rowData[index++]);
            TitleMaxScore = int.Parse(rowData[index++]);
            Reward = new CacheDictionary<int, int>();
            for(int i = 0; i < 5; i++)
            {
                int key = int.Parse(rowData[index++]);
                int value = int.Parse(rowData[index++]);
                Reward.Add(key, value);
            }
            DTPvpTitleCache.TryAdd(Id.ToString(), this);
        }
    }
}