using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerDailyQuest : BaseEntity
    {
        public PlayerDailyQuest()
        {
            TrackingDailyQuests = new CacheDictionary<int, TrackingDailyQuest>();
            CompletedDailyQuests = new CacheList<int>();
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public CacheDictionary<int, TrackingDailyQuest> TrackingDailyQuests
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public CacheList<int> CompletedDailyQuests
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public long LastRefreshTime
        {
            get;
            set;
        }


        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }

    [ProtoContract]
    public class TrackingDailyQuest : EntityChangeEvent
    {
        public TrackingDailyQuest()
        {
            Params = new CacheList<int>();
        }

        [ProtoMember(1)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public int Type
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public int Progress
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public int RequiredProgress
        {
            get;
            set;
        }

        [ProtoMember(5)]
        public CacheList<int> Params
        {
            get;
            set;
        }
    }
}
