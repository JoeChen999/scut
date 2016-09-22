using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerFriends : BaseEntity
    {
        public PlayerFriends()
        {
            Friends = new CacheDictionary<int,Friend>();
            Invitations = new CacheList<int>();
            RemovedFriends = new CacheDictionary<int, Friend>();
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public CacheDictionary<int, Friend> Friends
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public CacheList<int> Invitations
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int SendCount
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public CacheDictionary<int, Friend> RemovedFriends
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int ReceiveCount
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public long LastRefreshTime
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return UserId;
        }
    }

    [ProtoContract]
    public class Friend : EntityChangeEvent
    {
        [ProtoMember(1)]
        public bool CanSendEnergy
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public bool CanReceiveEnergy
        {
            get;
            set;
        }
    }
}
