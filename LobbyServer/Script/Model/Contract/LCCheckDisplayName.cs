﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1040_LCCheckDisplayName.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCCheckDisplayName")]
    public partial class LCCheckDisplayName : PacketBase
    {
        public LCCheckDisplayName() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 1040; } }

        private bool m_Usable;
        [ProtoMember(1, Name = @"Usable", IsRequired = true, DataFormat = DataFormat.Default)]
        public bool Usable
        {
            get { return m_Usable; }
            set { m_Usable = value; }
        }
    }
}
