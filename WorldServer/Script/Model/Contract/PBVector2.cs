﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBVector2.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract(Name = @"PBVector2")]
    public partial class PBVector2
    {
        public PBVector2() { }

        private float m_X;
        [ProtoMember(1, Name = @"X", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float X
        {
            get { return m_X; }
            set { m_X = value; }
        }

        private float m_Y;
        [ProtoMember(2, Name = @"Y", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }
    }
}
