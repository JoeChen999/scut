﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBTransformInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBTransformInfo")]
    public partial class PBTransformInfo
    {
        public PBTransformInfo() { }

        private float m_PositionX;
        [ProtoMember(1, Name = @"PositionX", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float PositionX
        {
            get { return m_PositionX; }
            set { m_PositionX = value; }
        }

        private float m_PositionY;
        [ProtoMember(2, Name = @"PositionY", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float PositionY
        {
            get { return m_PositionY; }
            set { m_PositionY = value; }
        }

        private float m_Rotation;
        [ProtoMember(3, Name = @"Rotation", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }
    }
}
