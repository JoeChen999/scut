using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;

namespace Genesis.GameServer.RoomServer
{
    [ProtoContract]
    public class RoomPlayer : EntityChangeEvent
    {
        public RoomPlayer()
        {
            Heros = new CacheList<RoomHero>();
        }

        [ProtoMember(1)]
        public int PlayerId
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public string Name
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public int Level
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public int VipLevel
        {
            get;
            set;
        }

        [ProtoMember(5)]
        public RoomPlayerState State
        {
            get;
            set;
        }

        [ProtoMember(6)]
        public int PortraitType
        {
            get;
            set;
        }

        [ProtoMember(7)]
        public CacheList<RoomHero> Heros
        {
            get;
            set;
        }

        [ProtoMember(8)]
        public float PositionX
        {
            get;
            set;
        }

        [ProtoMember(9)]
        public float PositionY
        {
            get;
            set;
        }

        [ProtoMember(10)]
        public float Rotation
        {
            get;
            set;
        }

        [ProtoMember(11)]
        public int InBattleEntity
        {
            get;
            set;
        }

        [ProtoMember(12)]
        public int Camp
        {
            get;
            set;
        }

        [ProtoMember(13)]
        public int ServerId
        {
            get;
            set;
        }
    }
}
