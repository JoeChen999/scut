﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 4104_RCPushEntitySwitchHero.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"RCPushEntitySwitchHero")]
    public partial class RCPushEntitySwitchHero : PacketBase
    {
        public RCPushEntitySwitchHero() { }

        public override PacketType PacketType { get { return PacketType.RoomServerToClient; } }

        public override int PacketActionId { get { return 4104; } }

        private int m_PlayerId;
        [ProtoMember(1, Name = @"PlayerId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int PlayerId
        {
            get { return m_PlayerId; }
            set { m_PlayerId = value; }
        }

        private int m_OldEntityId;
        [ProtoMember(2, Name = @"OldEntityId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int OldEntityId
        {
            get { return m_OldEntityId; }
            set { m_OldEntityId = value; }
        }

        private int m_NewEntityId;
        [ProtoMember(3, Name = @"NewEntityId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int NewEntityId
        {
            get { return m_NewEntityId; }
            set { m_NewEntityId = value; }
        }

        private int m_HP;
        [ProtoMember(4, Name = @"HP", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int HP
        {
            get { return m_HP; }
            set { m_HP = value; }
        }
    }
}
