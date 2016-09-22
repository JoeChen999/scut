﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 5001_RCGetRoomInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"RCGetRoomInfo")]
    public partial class RCGetRoomInfo : PacketBase
    {
        public RCGetRoomInfo() { }

        public override PacketType PacketType { get { return PacketType.RoomServerToClient; } }

        public override int PacketActionId { get { return 5001; } }

        private PBRoomInfo m_RoomInfo;
        [ProtoMember(1, Name = @"RoomInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBRoomInfo RoomInfo
        {
            get { return m_RoomInfo; }
            set { m_RoomInfo = value; }
        }
    }
}