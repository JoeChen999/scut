﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 5002_RCHeartBeat.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"RCHeartBeat")]
    public partial class RCHeartBeat : PacketBase
    {
        public RCHeartBeat() { }

        public override PacketType PacketType { get { return PacketType.RoomServerToClient; } }

        public override int PacketActionId { get { return 5002; } }

        private PBInt64 m_ClientTime;
        [ProtoMember(1, Name = @"ClientTime", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBInt64 ClientTime
        {
            get { return m_ClientTime; }
            set { m_ClientTime = value; }
        }

        private PBInt64 m_ServerTime;
        [ProtoMember(2, Name = @"ServerTime", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBInt64 ServerTime
        {
            get { return m_ServerTime; }
            set { m_ServerTime = value; }
        }
    }
}
