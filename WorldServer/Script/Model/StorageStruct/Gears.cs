using ProtoBuf;
using System;
using UnityEngine;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Entity, false)]
    public class Gears : ShareEntity
    {
        public Gears()
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

        [ProtoMember(3), EntityField]
        public int Level
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int StrengthenLevel
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
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
