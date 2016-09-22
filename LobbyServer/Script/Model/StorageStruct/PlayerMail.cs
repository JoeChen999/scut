using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    [EntityTable(AccessLevel.ReadWrite, CacheType.Dictionary, false)]
    public class PlayerMail : BaseEntity 
    {
        public PlayerMail()
        {
            Mails = new CacheDictionary<int, Mail>();
        }

        [ProtoMember(1), EntityField(true)]
        public int UserId 
        {
            get; 
            set; 
        }

        [ProtoMember(2), EntityField]
        public int ReceivedId
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public CacheDictionary<int, Mail> Mails
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return UserId;
        }
    }

    [ProtoContract]
    public class Mail : EntityChangeEvent
    {
        public Mail()
        {
            
        }
        [ProtoMember(1)]
        public string Message { get; set; }

        [ProtoMember(2)]
        public int AttachedId { get; set; }

        [ProtoMember(3)]
        public int AttachedCount { get; set; }

        [ProtoMember(4)]
        public long ExpireTime { get; set; }

        [ProtoMember(5)]
        public long StartTime { get; set; }
    }
}
