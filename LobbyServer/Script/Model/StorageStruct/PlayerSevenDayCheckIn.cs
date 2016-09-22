using ProtoBuf;
using System;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerSevenDayCheckIn : BaseEntity
    {
        [ProtoMember(1), EntityField(true)]
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int ClaimedCount
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public long LastClaimedTime
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }
}
