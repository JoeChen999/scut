using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTOperationActivity : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTOperationActivity> DTOperationActivityCache = new MemoryCacheStruct<DTOperationActivity>();

        public DTOperationActivity()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public string ActivityName
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public string ActivityDesc
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int ActivityIconId
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public string ActivityUIPath
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public bool AutoShow
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public DateTime StartTime
        {
            get;
            set;
        }

        [ProtoMember(8), EntityField]
        public DateTime EndTime
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public string ProcessorName
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
            ActivityName = rowData[index++];
            ActivityDesc = rowData[index++];
            ActivityIconId = int.Parse(rowData[index++]);
            ActivityUIPath = rowData[index++];
            AutoShow = bool.Parse(rowData[index++]);
            StartTime = DateTime.Parse(rowData[index++]);
            EndTime = DateTime.Parse(rowData[index++]);
            ProcessorName = rowData[index++];

            DTOperationActivityCache.TryAdd(Id.ToString(), this);
        }
    }
}