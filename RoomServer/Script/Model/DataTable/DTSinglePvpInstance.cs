using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract]
    public class DTSinglePvpInstance : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTSinglePvpInstance> DTSinglePvpInstanceCache = new MemoryCacheStruct<DTSinglePvpInstance>();

        public DTSinglePvpInstance()
        {

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
        public string Description
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public string SceneId
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int TimerType
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int TimerDuration
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public int TimerAlert
        {
            get;
            set;
        }

        [ProtoMember(8), EntityField]
        public float SpawnPointX
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public float SpawnPointY
        {
            get;
            set;
        }

        [ProtoMember(10), EntityField]
        public float SpawnAngle
        {
            get;
            set;
        }

        [ProtoMember(11), EntityField]
        public float SpawnPointX2
        {
            get;
            set;
        }

        [ProtoMember(12), EntityField]
        public float SpawnPointY2
        {
            get;
            set;
        }

        [ProtoMember(13), EntityField]
        public float SpawnAngle2
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
            Description = rowData[index++];
            SceneId = rowData[index++];
            TimerType = int.Parse(rowData[index++]);
            TimerDuration = int.Parse(rowData[index++]);
            TimerAlert = int.Parse(rowData[index++]);
            SpawnPointX = float.Parse(rowData[index++]);
            SpawnPointY = float.Parse(rowData[index++]);
            SpawnAngle = float.Parse(rowData[index++]);
            SpawnPointX2 = float.Parse(rowData[index++]);
            SpawnPointY2 = float.Parse(rowData[index++]);
            SpawnAngle2 = float.Parse(rowData[index++]);

            DTSinglePvpInstanceCache.TryAdd(Id.ToString(), this);
        }
    }
}