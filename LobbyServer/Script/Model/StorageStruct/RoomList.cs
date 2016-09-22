using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Event;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class RoomList : MemoryEntity
    {
        public RoomList()
        {
            PlayerList = new RoomPlayer[6];
        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public RoomPlayer[] PlayerList
        {
            get;
            set;
        }

    }

    [ProtoContract]
    public class RoomPlayer : EntityChangeEvent
    {
        public RoomPlayer()
        {
            heroTeam = new Hero[3];
        }

        [ProtoMember(1)]
        public long PlayerId
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public string Name
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public int PortraitType
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public Hero[] heroTeam
        {
            get;
            set;
        }
    }
}
