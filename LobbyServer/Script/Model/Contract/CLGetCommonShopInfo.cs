﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1100_CLGetCommonShopInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLGetCommonShopInfo")]
    public partial class CLGetCommonShopInfo : PacketBase
    {
        public CLGetCommonShopInfo() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 1100; } }
    }
}
