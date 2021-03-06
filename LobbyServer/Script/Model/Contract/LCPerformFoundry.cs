﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 2108_LCPerformFoundry.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCPerformFoundry")]
    public partial class LCPerformFoundry : PacketBase
    {
        public LCPerformFoundry() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 2108; } }

        private int m_PerformerPlayerId;
        [ProtoMember(1, Name = @"PerformerPlayerId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int PerformerPlayerId
        {
            get { return m_PerformerPlayerId; }
            set { m_PerformerPlayerId = value; }
        }

        private PBGearFoundryProgressInfo m_Progress;
        [ProtoMember(2, Name = @"Progress", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBGearFoundryProgressInfo Progress
        {
            get { return m_Progress; }
            set { m_Progress = value; }
        }

        private readonly List<bool> m_RewardFlags = new List<bool>();
        [ProtoMember(3, Name = @"RewardFlags", DataFormat = DataFormat.Default)]
        public List<bool> RewardFlags
        {
            get { return m_RewardFlags; }
        }

        private PBInt64 m_NextFoundryTimeInTicks = null;
        [ProtoMember(4, Name = @"NextFoundryTimeInTicks", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBInt64 NextFoundryTimeInTicks
        {
            get { return m_NextFoundryTimeInTicks; }
            set { m_NextFoundryTimeInTicks = value; }
        }
    }
}
