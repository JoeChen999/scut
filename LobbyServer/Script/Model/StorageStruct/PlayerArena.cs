using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerArena : BaseEntity
    {
        public PlayerArena() 
        {
            ClaimedLivenessRewardFlag = new CacheList<bool>();
            BattleReports = new CacheList<ArenaBattleReport>();
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int ChallengeCount
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int ArenaTokenCount
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int WinCount
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public CacheList<bool> ClaimedLivenessRewardFlag
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int EnemyId
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public CacheList<ArenaBattleReport> BattleReports
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
    public class ArenaBattleReport : EntityChangeEvent
    {
        public ArenaBattleReport()
        {
        }

        [ProtoMember(1)]
        public int EnemyId { get; set; }

        [ProtoMember(2)]
        public bool IsWin { get; set; }

        [ProtoMember(3)]
        public bool IsActive { get; set; }

        [ProtoMember(4)]
        public long BattleTime { get; set; }
    }
}
