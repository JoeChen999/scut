﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1003_CLSignIn.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLSignIn")]
    public partial class CLSignIn : PacketBase
    {
        public CLSignIn() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 1003; } }
    }
}
