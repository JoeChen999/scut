﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1001_CLHeartBeat.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLHeartBeat")]
    public partial class CLHeartBeat : PacketBase
    {
        public CLHeartBeat() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 1001; } }

        private PBInt64 m_ClientTime;
        [ProtoMember(1, Name = @"ClientTime", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBInt64 ClientTime
        {
            get { return m_ClientTime; }
            set { m_ClientTime = value; }
        }
    }
}
