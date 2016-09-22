﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 4110_RCPushEntityDie.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"RCPushEntityDie")]
    public partial class RCPushEntityDie : PacketBase
    {
        public RCPushEntityDie() { }

        public override PacketType PacketType { get { return PacketType.RoomServerToClient; } }

        public override int PacketActionId { get { return 4110; } }

        private int m_PlayerId;
        [ProtoMember(1, Name = @"PlayerId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int PlayerId
        {
            get { return m_PlayerId; }
            set { m_PlayerId = value; }
        }

        private int m_DeadEntityId;
        [ProtoMember(2, Name = @"DeadEntityId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int DeadEntityId
        {
            get { return m_DeadEntityId; }
            set { m_DeadEntityId = value; }
        }
    }
}
