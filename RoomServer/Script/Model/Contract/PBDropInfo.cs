﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBDropInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBDropInfo")]
    public partial class PBDropInfo
    {
        public PBDropInfo() { }

        private int m_DropId;
        [ProtoMember(1, Name = @"DropId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int DropId
        {
            get { return m_DropId; }
            set { m_DropId = value; }
        }

        private int m_DropCount;
        [ProtoMember(2, Name = @"DropCount", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int DropCount
        {
            get { return m_DropCount; }
            set { m_DropCount = value; }
        }
    }
}
