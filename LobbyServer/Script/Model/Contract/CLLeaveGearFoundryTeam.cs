﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 2107_CLLeaveGearFoundryTeam.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLLeaveGearFoundryTeam")]
    public partial class CLLeaveGearFoundryTeam : PacketBase
    {
        public CLLeaveGearFoundryTeam() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 2107; } }
    }
}