﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 2005_LCEnterChessBattle.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCEnterChessBattle")]
    public partial class LCEnterChessBattle : PacketBase
    {
        public LCEnterChessBattle() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 2005; } }

        private bool m_Success;
        [ProtoMember(1, Name = @"Success", IsRequired = true, DataFormat = DataFormat.Default)]
        public bool Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private int m_InstanceTypeId;
        [ProtoMember(2, Name = @"InstanceTypeId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int InstanceTypeId
        {
            get { return m_InstanceTypeId; }
            set { m_InstanceTypeId = value; }
        }

        private PBHeroTeamInfo m_HeroTeam = null;
        [ProtoMember(3, Name = @"HeroTeam", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBHeroTeamInfo HeroTeam
        {
            get { return m_HeroTeam; }
            set { m_HeroTeam = value; }
        }
    }
}
