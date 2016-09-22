using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerEpigraph : BaseEntity
    {
        public PlayerEpigraph()
        {
            Epigraphs = new CacheList<Epigraph>();
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId { get; set; }

        [ProtoMember(2), EntityField]
        public CacheList<Epigraph> Epigraphs { get; set; }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }

    [ProtoContract]
    public class Epigraph : EntityChangeEvent
    {
        public Epigraph()
        {

        }

        [ProtoMember(1)]
        public int Type
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public int Level
        {
            get;
            set;
        }
    }
}
