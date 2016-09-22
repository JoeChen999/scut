﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1011_CLHeroElevationLevelUp.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLHeroElevationLevelUp")]
    public partial class CLHeroElevationLevelUp : PacketBase
    {
        public CLHeroElevationLevelUp() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 1011; } }

        private int m_HeroType;
        [ProtoMember(1, Name = @"HeroType", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int HeroType
        {
            get { return m_HeroType; }
            set { m_HeroType = value; }
        }

        private readonly List<int> m_GearId = new List<int>();
        [ProtoMember(2, Name = @"GearId", DataFormat = DataFormat.TwosComplement)]
        public List<int> GearId
        {
            get { return m_GearId; }
        }
    }
}
