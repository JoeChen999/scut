using ProtoBuf;
using System;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Entity, false)]
    public class PlayerSinglePvp : ShareEntity
    {
        public PlayerSinglePvp()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int SinglePvpScore
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int RoomServerId
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int RoomId
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int RemainingCount
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public long LastResetTime
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public long DeductedScore
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return UserId;
        }
    }
}
