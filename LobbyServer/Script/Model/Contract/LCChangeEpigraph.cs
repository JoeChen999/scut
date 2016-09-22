﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1020_LCChangeEpigraph.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCChangeEpigraph")]
    public partial class LCChangeEpigraph : PacketBase
    {
        public LCChangeEpigraph() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 1020; } }

        private int m_Index;
        [ProtoMember(1, Name = @"Index", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Index
        {
            get { return m_Index; }
            set { m_Index = value; }
        }

        private PBEpigraphInfo m_DressedEpigraph = null;
        [ProtoMember(2, Name = @"DressedEpigraph", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBEpigraphInfo DressedEpigraph
        {
            get { return m_DressedEpigraph; }
            set { m_DressedEpigraph = value; }
        }

        private PBEpigraphInfo m_UndressedEpigraph = null;
        [ProtoMember(3, Name = @"UndressedEpigraph", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBEpigraphInfo UndressedEpigraph
        {
            get { return m_UndressedEpigraph; }
            set { m_UndressedEpigraph = value; }
        }
    }
}
