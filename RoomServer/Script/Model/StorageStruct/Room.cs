using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract]
    public class Room : MemoryEntity
    {
        public Room()
        {
            Players = new CacheDictionary<int, RoomPlayer>();
        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public RoomState State
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public long CreateTime
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public long StartTime
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public string Token
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int InstanceId
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public CacheDictionary<int, RoomPlayer> Players
        {
            get;
            set;
        }

        [ProtoMember(8), EntityField]
        public InstanceSuccessReason EndReason
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public long RemainingTime
        {
            get;
            set;
        }
    }
}
