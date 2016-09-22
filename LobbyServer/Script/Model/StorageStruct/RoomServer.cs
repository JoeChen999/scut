using ProtoBuf;
using System;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Entity, false)]
    public class RoomServer : ShareEntity, IComparable<RoomServer>
    {
        public RoomServer()
        {
            State = RoomServerState.Normal;
        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public string IP
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int Port
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public RoomServerState State
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public float CpuLoad
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public float MemoryLoad
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public long LastUpdatedTime
        {
            get;
            set;
        }

        [ProtoMember(8), EntityField]
        public int RoomCount
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public string Host
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return Id;
        }

        public int CompareTo(RoomServer other)
        {
            if(RoomCount == other.RoomCount)
            {
                return Id.CompareTo(other.Id);
            }
            return RoomCount.CompareTo(other.RoomCount);
        }
    }
}
