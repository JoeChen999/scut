﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1026_CLCompleteStoryInstance.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLCompleteStoryInstance")]
    public partial class CLCompleteStoryInstance : PacketBase
    {
        public CLCompleteStoryInstance() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 1026; } }

        private int m_StarLevel;
        [ProtoMember(1, Name = @"StarLevel", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int StarLevel
        {
            get { return m_StarLevel; }
            set { m_StarLevel = value; }
        }

        private int m_InstanceId;
        [ProtoMember(2, Name = @"InstanceId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int InstanceId
        {
            get { return m_InstanceId; }
            set { m_InstanceId = value; }
        }
    }
}
