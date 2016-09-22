﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBItemInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBItemInfo")]
    public partial class PBItemInfo
    {
        public PBItemInfo() { }

        private int m_Type;
        [ProtoMember(1, Name = @"Type", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        private int? m_Count;
        [ProtoMember(2, Name = @"Count", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int Count
        {
            get { return m_Count ?? default(int); }
            set { m_Count = value; }
        }
        public bool HasCount { get { return m_Count != null; } }
        private void ResetCount() { m_Count = null; }
        private bool ShouldSerializeCount() { return HasCount; }
    }
}