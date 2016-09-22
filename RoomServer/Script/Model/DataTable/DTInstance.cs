using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract]
    public class DTInstance : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTInstance> DTInstanceCache = new MemoryCacheStruct<DTInstance>();

        public DTInstance()
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
        public CacheList<DropGroup> DropIds
        {
            get;
            set;
        }

        [ProtoMember(13), EntityField]
        public CacheList<DropGroup> CleanOutDropIds
        {
            get;
            set;
        }

        [ProtoMember(14), EntityField]
        public string SceneId
        {
            get;
            set;
        }

        [ProtoMember(15), EntityField]
        public int TimerType
        {
            get;
            set;
        }

        [ProtoMember(16), EntityField]
        public int TimerDuration
        {
            get;
            set;
        }

        [ProtoMember(17), EntityField]
        public int TimerAlert
        {
            get;
            set;
        }

        [ProtoMember(18), EntityField]
        public string AIBehavior
        {
            get;
            set;
        }

        [ProtoMember(19), EntityField]
        public string InstanceNpcs
        {
            get;
            set;
        }

        [ProtoMember(20), EntityField]
        public float SpawnPointX
        {
            get;
            set;
        }

        [ProtoMember(21), EntityField]
        public float SpawnPointY
        {
            get;
            set;
        }

        [ProtoMember(22), EntityField]
        public float SpawnAngle
        {
            get;
            set;
        }

        [ProtoMember(23), EntityField]
        public int PrerequisitePlayerLevel
        {
            get;
            set;
        }

        [ProtoMember(24), EntityField]
        public CacheList<int> InInstanceChests
        {
            get;
            set;
        }

        public void ParseRow(string[] rowData)
        {
            DropIds = new CacheList<DropGroup>();
            CleanOutDropIds = new CacheList<DropGroup>();
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
            PrerequisitePlayerLevel = int.Parse(rowData[index++]);
            InstanceGroupId = int.Parse(rowData[index++]);
            index++; // Skip recommend level
            PlayerExp = int.Parse(rowData[index++]);
            HeroExp = int.Parse(rowData[index++]);
            Coin = int.Parse(rowData[index++]);
            string drops = rowData[index++].Trim('"');
            foreach (string drop in drops.Split(';'))
            {
                DropGroup dropGroup = new DropGroup(drop);
                DropIds.Add(dropGroup);
            }
            string cleanOutDrops = rowData[index++].Trim('"');
            foreach (string drop in cleanOutDrops.Split(';'))
            {
                DropGroup dropGroup = new DropGroup(drop);
                CleanOutDropIds.Add(dropGroup);
            }
            SceneId = rowData[index++];
            TimerType = int.Parse(rowData[index++]);
            TimerDuration = int.Parse(rowData[index++]);
            TimerAlert = int.Parse(rowData[index++]);
            //AIBehavior = rowData[index++];
            index += 4;
            InstanceNpcs = rowData[index++];
            SpawnPointX = float.Parse(rowData[index++]);
            SpawnPointY = float.Parse(rowData[index++]);
            SpawnAngle = float.Parse(rowData[index++]);
            index += 37;
            InInstanceChests = new CacheList<int>();
            while (index < rowData.Length)
            {
                int chestId = int.Parse(rowData[index++]);
                if (chestId < 0)
                {
                    break;
                }
                InInstanceChests.Add(chestId);
            }
            DTInstanceCache.AddOrUpdate(Id.ToString(), this);
        }
    }
}

[ProtoContract]
public class DropGroup : EntityChangeEvent
{
    private CacheDictionary<int, int> m_Drops = new CacheDictionary<int, int>();
    public DropGroup(string dropStr)
    {
        foreach(string dropPack in dropStr.Split(','))
        {
            var dropInfo = dropPack.Split('*');
            m_Drops[int.Parse(dropInfo[0])] = int.Parse(dropInfo[1]);
        }
    }

    public CacheDictionary<int, int> Drops
    {
        get { return m_Drops; }
    }
}