using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.WorldServer
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

        }

        [ProtoMember(1)]
        public PBRoomPlayerInfo Player
        {
            get;
            set;
        }

        public int CompareTo(PVPGroup other)
        {
            return Player.Score.CompareTo(other.Player.Score);
        }
    }
}
