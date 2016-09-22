using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTDailyQuest : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTDailyQuest> DTDailyQuestCache = new MemoryCacheStruct<DTDailyQuest>();

        public DTDailyQuest()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int QuestType
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int TargetProgressCount
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public CacheList<int> Params
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int PrePlayerLevel
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int RewardPlayerExp
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public CacheDictionary<int, int> RewardItems
        {
            get;
            set;
        }

        public void ParseRow(string[] rowData)
        {
            int index = 0;
            index++;
            Id = int.Parse(rowData[index++]);
            index += 3;
            QuestType = int.Parse(rowData[index++]);
            TargetProgressCount = int.Parse(rowData[index++]);
            Params = new CacheList<int>();
            for (int i = 0; i < 5; i++)
            {
                Params.Add(int.Parse(rowData[index++]));
            }
            PrePlayerLevel = int.Parse(rowData[index++]);
            RewardPlayerExp = int.Parse(rowData[index++]);
            RewardItems = new CacheDictionary<int, int>();
            for (int i = 0; i < 5; i++)
            {
                int key = int.Parse(rowData[index++]);
                int value = int.Parse(rowData[index++]);
                if (key != 0)
                {
                    RewardItems.Add(key, value);
                }
            }

            DTDailyQuestCache.TryAdd(Id.ToString(), this);
        }
    }
}