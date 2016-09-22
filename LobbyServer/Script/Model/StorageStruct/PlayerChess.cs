using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerChess : BaseEntity
    {
        public PlayerChess()
        {
            ChessBoard = new CacheList<ChessField>();
            HP = new CacheDictionary<int, int>();
            GotGears = new CacheList<int>();
            GotSouls = new CacheList<int>();
            GotEpigraphs = new CacheList<int>();
            GotItems = new CacheDictionary<int, int>();
            MyTeam = new CacheList<int>();
        }

        [ProtoMember(1)]
        [EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2)]
        [EntityField]
        public int Count { get; set; }

        [ProtoMember(3)]
        [EntityField]
        public int Token { get; set; }

        [ProtoMember(4)]
        [EntityField]
        public int Anger { get; set; }

        [ProtoMember(5)]
        [EntityField]
        public CacheDictionary<int, int> HP { get; set; }

        [ProtoMember(6)]
        [EntityField]
        public CacheList<ChessField> ChessBoard { get; set; }

        [ProtoMember(7)]
        [EntityField]
        public int GotCoin { get; set; }

        [ProtoMember(8)]
        [EntityField]
        public int GotMoney { get; set; }

        [ProtoMember(9)]
        [EntityField]
        public int GotStarEnergy { get; set; }

        [ProtoMember(10)]
        [EntityField]
        public CacheList<int> GotGears { get; set; }

        [ProtoMember(11)]
        [EntityField]
        public CacheDictionary<int, int> GotItems { get; set; }

        [ProtoMember(12)]
        [EntityField]
        public DateTime LastResetTime { get; set; }

        [ProtoMember(13)]
        [EntityField]
        public CacheList<int> MyTeam { get; set; }

        [ProtoMember(14)]
        [EntityField]
        public int OpenCount { get; set; }

        [ProtoMember(15)]
        [EntityField]
        public CacheList<int> GotSouls { get; set; }

        [ProtoMember(16)]
        [EntityField]
        public CacheList<int> GotEpigraphs { get; set; }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }

    [ProtoContract]
    public class ChessField : EntityChangeEvent
    {
        [ProtoMember(1)]
        public ChessFieldColor Color { get; set; }

        [ProtoMember(2)]
        public bool IsOpened { get; set; }
    }

    [ProtoContract]
    public class BattleChessField : ChessField
    {
        public BattleChessField()
        {
            EnemyPlayerHeroTeam = new CacheList<Hero>();
            EnemyHeroHP = new CacheList<int>();
            ChildrenId = new CacheList<int>();
        }

        [ProtoMember(3)]
        public int Count { get; set; }

        [ProtoMember(4)]
        public int EnemyPlayerId { get; set; }

        [ProtoMember(5)]
        public CacheList<Hero> EnemyPlayerHeroTeam { get; set; }

        [ProtoMember(6)]
        public int EnemyAnger { get; set; }

        [ProtoMember(7)]
        public CacheList<int> EnemyHeroHP { get; set; }

        [ProtoMember(8)]
        public CacheList<int> ChildrenId { get; set; }

    }

    [ProtoContract]
    public class RewardChessField : ChessField
    {
        public RewardChessField()
        {
            RewardItems = new CacheDictionary<int, int>();
        }

        [ProtoMember(3)]
        public bool IsFree { get; set; }

        [ProtoMember(4)]
        public int RewardCoin { get; set; }

        [ProtoMember(5)]
        public CacheDictionary<int, int> RewardItems { get; set; }

        [ProtoMember(6)]
        public int ParentId { get; set; }

        [ProtoMember(7)]
        public int RewardMoney { get; set; }

        [ProtoMember(8)]
        public int RewardStarEnergy { get; set; }
    }
}
