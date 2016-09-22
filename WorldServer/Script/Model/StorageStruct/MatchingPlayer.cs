using ProtoBuf;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;

namespace Genesis.GameServer.WorldServer
{
    [ProtoContract]
    public class MatchingPlayer : EntityChangeEvent
    {
        public MatchingPlayer()
        {
            HeroTeam = new CacheList<Hero>();
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
        public int PortraitType
        {
            get;
            set;
        }

        [ProtoMember(6)]
        public int Score
        {
            get;
            set;
        }

        [ProtoMember(7)]
        public CacheList<Hero> HeroTeam
        {
            get;
            set;
        }
    }
}
