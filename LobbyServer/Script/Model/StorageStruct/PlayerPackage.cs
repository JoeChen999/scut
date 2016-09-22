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
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerPackage : BaseEntity
    {
        public PlayerPackage()
            : base(false)
        {
            Inventories = new CacheDictionary<int, int>();
            Gears = new CacheDictionary<int, int>();
            Souls = new CacheDictionary<int, int>();
            Epigraphs = new CacheDictionary<int, int>();
        }
        [ProtoMember(1), EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2), EntityField]
        public CacheDictionary<int, int> Inventories { get; set; }

        [ProtoMember(3), EntityField]
        public CacheDictionary<int, int> Gears { get; set; }

        [ProtoMember(4), EntityField]
        public CacheDictionary<int, int> Souls { get; set; }

        [ProtoMember(5), EntityField]
        public CacheDictionary<int, int> Epigraphs { get; set; }

        protected override int GetIdentityId()
        {
            return UserId;
        }
    }
}
