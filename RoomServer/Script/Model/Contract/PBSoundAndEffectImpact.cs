﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBSoundAndEffectImpact.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBSoundAndEffectImpact")]
    public partial class PBSoundAndEffectImpact
    {
        public PBSoundAndEffectImpact() { }

        private int m_ImpactId;
        [ProtoMember(1, Name = @"ImpactId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int ImpactId
        {
            get { return m_ImpactId; }
            set { m_ImpactId = value; }
        }
    }
}
