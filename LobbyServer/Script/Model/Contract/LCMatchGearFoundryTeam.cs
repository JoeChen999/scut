﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 2104_LCMatchGearFoundryTeam.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCMatchGearFoundryTeam")]
    public partial class LCMatchGearFoundryTeam : PacketBase
    {
        public LCMatchGearFoundryTeam() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 2104; } }

        private PBGearFoundryInfo m_Data = null;
        [ProtoMember(1, Name = @"Data", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBGearFoundryInfo Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }
    }
}