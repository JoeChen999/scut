﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1040_CLCheckDisplayName.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLCheckDisplayName")]
    public partial class CLCheckDisplayName : PacketBase
    {
        public CLCheckDisplayName() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 1040; } }

        private string m_DisplayName;
        [ProtoMember(1, Name = @"DisplayName", IsRequired = true, DataFormat = DataFormat.Default)]
        public string DisplayName
        {
            get { return m_DisplayName; }
            set { m_DisplayName = value; }
        }
    }
}