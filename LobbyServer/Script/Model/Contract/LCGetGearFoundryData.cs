﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 2100_LCGetGearFoundryData.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCGetGearFoundryData")]
    public partial class LCGetGearFoundryData : PacketBase
    {
        public LCGetGearFoundryData() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 2100; } }

        private PBGearFoundryInfo m_Data;
        [ProtoMember(1, Name = @"Data", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBGearFoundryInfo Data
        {
            get { return m_Data; }
            set { m_Data = value; }
        }
    }
}
