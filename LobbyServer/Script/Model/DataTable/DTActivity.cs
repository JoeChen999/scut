using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTActivity : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTActivity> DTActivityCache = new MemoryCacheStruct<DTActivity>();

        public DTActivity()
        {
            RewardItems = new CacheList<int>();
        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public string Name
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public string Description1
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public string Description2
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int Icon
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int UnlockLevel
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public int RepeatType
        {
            get;
            set;
        }

        [ProtoMember(8), EntityField]
        public string StartTime
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public string EndTime
        {
            get;
            set;
        }

        [ProtoMember(10), EntityField]
        public bool DisplayWhenClose
        {
            get;
            set;
        }

        [ProtoMember(11), EntityField]
        public int RewardExp
        {
            get;
            set;
        }

        [ProtoMember(12), EntityField]
        public int RewardCoin
        {
            get;
            set;
        }

        [ProtoMember(13), EntityField]
        public CacheList<int> RewardItems
        {
            get;
            set;
        }

        [ProtoMember(14), EntityField]
        public string HintWhenClose
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
            Name = rowData[index++];
            HintWhenClose = rowData[index++];
            Description1 = rowData[index++];
            Description2 = rowData[index++];
            Icon = int.Parse(rowData[index++]);
            UnlockLevel = int.Parse(rowData[index++]);
            RepeatType = int.Parse(rowData[index++]);
            StartTime = rowData[index++].Trim('"');
            EndTime = rowData[index++].Trim('"');
            DisplayWhenClose = bool.Parse(rowData[index++]);
            RewardExp = int.Parse(rowData[index++]);
            RewardCoin = int.Parse(rowData[index++]);
            while (index < rowData.Length)
            {
                RewardItems.Add(int.Parse(rowData[index++]));
            }

            DTActivityCache.TryAdd(Id.ToString(), this);
        }
    }
}