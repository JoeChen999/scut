﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBKeyValuePair.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBKeyValuePair")]
    public partial class PBKeyValuePair
    {
        public PBKeyValuePair() { }

        private string m_Key;
        [ProtoMember(1, Name = @"Key", IsRequired = true, DataFormat = DataFormat.Default)]
        public string Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        private string m_Value;
        [ProtoMember(2, Name = @"Value", IsRequired = true, DataFormat = DataFormat.Default)]
        public string Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
    }
}
