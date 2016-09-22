﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBEpigraphInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract(Name = @"PBEpigraphInfo")]
    public partial class PBEpigraphInfo
    {
        public PBEpigraphInfo() { }

        private int m_Type;
        [ProtoMember(1, Name = @"Type", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        private int? m_Level;
        [ProtoMember(2, Name = @"Level", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int Level
        {
            get { return m_Level ?? default(int); }
            set { m_Level = value; }
        }
        public bool HasLevel { get { return m_Level != null; } }
        private void ResetLevel() { m_Level = null; }
        private bool ShouldSerializeLevel() { return HasLevel; }
    }
}
