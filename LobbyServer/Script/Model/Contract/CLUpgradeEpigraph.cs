﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1024_CLUpgradeEpigraph.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLUpgradeEpigraph")]
    public partial class CLUpgradeEpigraph : PacketBase
    {
        public CLUpgradeEpigraph() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 1024; } }

        private int m_Id;
        [ProtoMember(1, Name = @"Id", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }
    }
}
