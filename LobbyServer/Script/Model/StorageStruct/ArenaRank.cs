using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Entity, false)]
    public class ArenaRank : ShareEntity
    {
        public ArenaRank()
        {
        }

        [ProtoMember(1), EntityField(true)]
        public int RankId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int PlayerId
        {
            get;
            set;
        }


        protected override int GetIdentityId()
        {
            return RankId;
        }
    }
}
