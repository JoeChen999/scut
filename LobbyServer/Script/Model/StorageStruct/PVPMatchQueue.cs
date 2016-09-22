using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class PVPMatchQueue : MemoryEntity
    {
        public PVPMatchQueue()
        {
            InQueueGroups = new CacheList<PVPGroup>();
        }

        [ProtoMember(1), EntityField(true)]
        public int QueueType
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public CacheList<PVPGroup> InQueueGroups
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int MatchedGroupCount
        {
            get;
            set;
        }

    }

    [ProtoContract]
    public class PVPGroup : EntityChangeEvent, IComparable<PVPGroup>
    {
        public PVPGroup()
        {
            Players = new CacheList<int>();
            Params = new CacheList<int>();
        }

        [ProtoMember(1)]
        public CacheList<int> Players
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public CacheList<int> Params
        {
            get;
            set;
        }

        public int CompareTo(PVPGroup other)
        {
            for (int i=0; i < Params.Count; i++)
            {
                if(Params[i] != other.Params[i])
                {
                    return Params[i].CompareTo(other.Params[i]);
                }
            }
            return Players[0].CompareTo(other.Players[0]);
        }
    }
}
