﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 3108_LCRespondToFriendRequest.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCRespondToFriendRequest")]
    public partial class LCRespondToFriendRequest : PacketBase
    {
        public LCRespondToFriendRequest() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 3108; } }

        private PBPlayerInfo m_Player;
        [ProtoMember(1, Name = @"Player", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBPlayerInfo Player
        {
            get { return m_Player; }
            set { m_Player = value; }
        }

        private bool m_Accept;
        [ProtoMember(2, Name = @"Accept", IsRequired = true, DataFormat = DataFormat.Default)]
        public bool Accept
        {
            get { return m_Accept; }
            set { m_Accept = value; }
        }

        private bool? m_CanGiveEnergy;
        [ProtoMember(3, Name = @"CanGiveEnergy", IsRequired = false, DataFormat = DataFormat.Default)]
        public bool CanGiveEnergy
        {
            get { return m_CanGiveEnergy ?? default(bool); }
            set { m_CanGiveEnergy = value; }
        }
        public bool HasCanGiveEnergy { get { return m_CanGiveEnergy != null; } }
        private void ResetCanGiveEnergy() { m_CanGiveEnergy = null; }
        private bool ShouldSerializeCanGiveEnergy() { return HasCanGiveEnergy; }

        private bool? m_CanReceiveEnergy;
        [ProtoMember(4, Name = @"CanReceiveEnergy", IsRequired = false, DataFormat = DataFormat.Default)]
        public bool CanReceiveEnergy
        {
            get { return m_CanReceiveEnergy ?? default(bool); }
            set { m_CanReceiveEnergy = value; }
        }
        public bool HasCanReceiveEnergy { get { return m_CanReceiveEnergy != null; } }
        private void ResetCanReceiveEnergy() { m_CanReceiveEnergy = null; }
        private bool ShouldSerializeCanReceiveEnergy() { return HasCanReceiveEnergy; }
    }
}
