using ProtoBuf;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerStoryInstance : BaseEntity
    {
        public PlayerStoryInstance()
        {

        }

        [ProtoMember(1), EntityField(true)]   
        public int UserId
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int Count
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return (int)UserId;
        }
    }
}
