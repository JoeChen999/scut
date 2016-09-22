using ProtoBuf;
using UnityEngine;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;

namespace Genesis.GameServer.WorldServer
{
    [ProtoContract]
    public class Hero : EntityChangeEvent
    {
        public Hero()
        {
            Souls = new CacheDictionary<int, int>();
            Gears = new CacheDictionary<GearType, int>();
            SkillLevels = new CacheList<int>();
            SkillExps = new CacheList<int>();
        }
        [ProtoMember(1)]
        public int HeroType { get; set; }

        [ProtoMember(2)]
        public int HeroLv { get; set; }

        [ProtoMember(3)]
        public int HeroExp { get; set; }

        [ProtoMember(4)]
        public int HeroStarLevel { get; set; }

        [ProtoMember(5)]
        public int ConsciousnessLevel { get; set; }

        [ProtoMember(6)]
        public int ElevationLevel { get; set; }

        [ProtoMember(7)]
        public CacheDictionary<int, int> Souls { get; set; }

        [ProtoMember(8)]
        public CacheDictionary<GearType, int> Gears { get; set; }

        [ProtoMember(9)]
        public CacheList<int> SkillLevels { get; set; }

        [ProtoMember(10)]
        public CacheList<int> SkillExps { get; set; }

        [ProtoMember(11)]
        public int Might { get; set; }
    }
}
