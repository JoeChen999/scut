using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerChat : BaseEntity
    {
        public PlayerChat()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int UserId 
        { 
            get; 
            set; 
        }

        [ProtoMember(2)]
        public long LastWorldChatTime
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public string Content
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
