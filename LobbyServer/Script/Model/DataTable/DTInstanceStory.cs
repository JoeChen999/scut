using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTInstanceStory : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTInstanceStory> DTInstanceStoryCache = new MemoryCacheStruct<DTInstanceStory>();

        public DTInstanceStory()
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
        public string RequestDescription0
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public string RequestDescription1
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public string RequestDescription2
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public int PrerequisiteInstanceId
        {
            get;
            set;
        }

        [ProtoMember(8), EntityField]
        public int InstanceGroupId
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public int PlayerExp
        {
            get;
            set;
        }

        [ProtoMember(10), EntityField]
        public int HeroExp
        {
            get;
            set;
        }

        [ProtoMember(11), EntityField]
        public int Coin
        {
            get;
            set;
        }

        [ProtoMember(12), EntityField]
        public CacheList<int> DropIds
        {
            get;
            set;
        }

        [ProtoMember(13), EntityField]
        public string SceneId
        {
            get;
            set;
        }

        [ProtoMember(14), EntityField]
        public int TimerType
        {
            get;
            set;
        }

        [ProtoMember(15), EntityField]
        public int TimerDuration
        {
            get;
            set;
        }

        [ProtoMember(16), EntityField]
        public int TimerAlert
        {
            get;
            set;
        }

        [ProtoMember(17), EntityField]
        public string AIBehavior0
        {
            get;
            set;
        }

        [ProtoMember(18), EntityField]
        public string AIBehavior1
        {
            get;
            set;
        }

        [ProtoMember(19), EntityField]
        public string AIBehavior2
        {
            get;
            set;
        }

        [ProtoMember(20), EntityField]
        public string AIBehavior3
        {
            get;
            set;
        }

        [ProtoMember(21), EntityField]
        public string InstanceNpcs
        {
            get;
            set;
        }

        [ProtoMember(22), EntityField]
        public float SpawnPointX
        {
            get;
            set;
        }

        [ProtoMember(23), EntityField]
        public float SpawnPointY
        {
            get;
            set;
        }

        [ProtoMember(24), EntityField]
        public float SpawnAngle
        {
            get;
            set;
        }

        [ProtoMember(49), EntityField]
        public int HeroId
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
            RequestDescription0 = rowData[index++];
            RequestDescription1 = rowData[index++];
            RequestDescription2 = rowData[index++];
            PrerequisiteInstanceId = int.Parse(rowData[index++]);
            InstanceGroupId = int.Parse(rowData[index++]);
            PlayerExp = int.Parse(rowData[index++]);
            HeroExp = int.Parse(rowData[index++]);
            Coin = int.Parse(rowData[index++]);
            DropIds = new CacheList<int>();
            string Ids = rowData[index++].Trim('"');
            foreach (string id in Ids.Split(','))
            {
                DropIds.Add(int.Parse(id));
            }
            SceneId = rowData[index++];
            TimerType = int.Parse(rowData[index++]);
            TimerDuration = int.Parse(rowData[index++]);
            TimerAlert = int.Parse(rowData[index++]);
            index += 4;
            InstanceNpcs = rowData[index++];
            SpawnPointX = float.Parse(rowData[index++]);
            SpawnPointY = float.Parse(rowData[index++]);
            SpawnAngle = float.Parse(rowData[index++]);
            
            index += 37;
            HeroId = int.Parse(rowData[index++]);

            DTInstanceStoryCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}