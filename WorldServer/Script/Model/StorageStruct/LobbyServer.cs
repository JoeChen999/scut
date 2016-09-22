using ProtoBuf;
using System;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Entity, false)]
    public class LobbyServer : ShareEntity
    {
        public LobbyServer()
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

        protected override int GetIdentityId()
        {
            return Id;
        }
    }
}
