using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Entity, false)]
    public class FoundryRoom : ShareEntity
    {
        public FoundryRoom()
        {
            Players = new CacheList<int>();
        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int Level
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int Progress
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public CacheList<int> Players
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
