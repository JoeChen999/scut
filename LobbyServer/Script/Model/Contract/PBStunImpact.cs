﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBStunImpact.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"PBStunImpact")]
    public partial class PBStunImpact
    {
        public PBStunImpact() { }

        private int m_ImpactId;
        [ProtoMember(1, Name = @"ImpactId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int ImpactId
        {
            get { return m_ImpactId; }
            set { m_ImpactId = value; }
        }

        private float m_StunTime;
        [ProtoMember(2, Name = @"StunTime", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float StunTime
        {
            get { return m_StunTime; }
            set { m_StunTime = value; }
        }
    }
}
