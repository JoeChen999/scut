using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Entity, false)]
    public class HeroTeam : ShareEntity
    {
        public HeroTeam()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int PlayerId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public CacheList<int> Team
        {
            get;
            set;
        }
        protected override int GetIdentityId()
        {
            return PlayerId;
        }
    }
}
