﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBHPRecoverImpact.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract(Name = @"PBHPRecoverImpact")]
    public partial class PBHPRecoverImpact
    {
        public PBHPRecoverImpact() { }

        private int m_ImpactId;
        [ProtoMember(1, Name = @"ImpactId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int ImpactId
        {
            get { return m_ImpactId; }
            set { m_ImpactId = value; }
        }

        private int m_RecoverHP;
        [ProtoMember(2, Name = @"RecoverHP", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int RecoverHP
        {
            get { return m_RecoverHP; }
            set { m_RecoverHP = value; }
        }
    }
}
