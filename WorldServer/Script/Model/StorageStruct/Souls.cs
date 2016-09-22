using ProtoBuf;
using System;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Entity, false)]
    public class Souls : ShareEntity
    {
        public Souls()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int TypeId
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public bool Removed
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return Id;
        }
    }
}
