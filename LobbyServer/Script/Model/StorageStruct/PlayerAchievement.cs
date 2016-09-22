using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerAchievement : BaseEntity
    {
        public PlayerAchievement()
        {
            TrackingAchievements = new CacheDictionary<int, TrackingAchievement>();
            CompletedAchievements = new CacheList<int>();
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public CacheDictionary<int, TrackingAchievement> TrackingAchievements
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public CacheList<int> CompletedAchievements
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
    public class TrackingAchievement : EntityChangeEvent
    {
        public TrackingAchievement()
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
        public int Progress
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public int RequiredProgress
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public CacheList<int> Params
        {
            get;
            set;
        }
    }
}
