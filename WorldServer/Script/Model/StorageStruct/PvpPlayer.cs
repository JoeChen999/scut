using ProtoBuf;
using System;
using UnityEngine;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Entity, false)]
    public class PvpPlayer : ShareEntity
    {
        public PvpPlayer()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField(true)]
        public int ServerId
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int Score
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public string Name
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int PortraitType
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
