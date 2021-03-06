﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 5099_RCRequestResult.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"RCRequestResult")]
    public partial class RCRequestResult : PacketBase
    {
        public RCRequestResult() { }

        public override PacketType PacketType { get { return PacketType.RoomServerToClient; } }

        public override int PacketActionId { get { return 5099; } }

        private int m_SerialId;
        [ProtoMember(1, Name = @"SerialId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int SerialId
        {
            get { return m_SerialId; }
            set { m_SerialId = value; }
        }

        private bool m_Result;
        [ProtoMember(2, Name = @"Result", IsRequired = true, DataFormat = DataFormat.Default)]
        public bool Result
        {
            get { return m_Result; }
            set { m_Result = value; }
        }

        private readonly List<PBRoomHeroInfo> m_RoomHeroInfo = new List<PBRoomHeroInfo>();
        [ProtoMember(3, Name = @"RoomHeroInfo", DataFormat = DataFormat.Default)]
        public List<PBRoomHeroInfo> RoomHeroInfo
        {
            get { return m_RoomHeroInfo; }
        }
    }
}
