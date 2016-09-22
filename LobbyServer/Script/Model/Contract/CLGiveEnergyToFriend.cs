﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 3109_CLGiveEnergyToFriend.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLGiveEnergyToFriend")]
    public partial class CLGiveEnergyToFriend : PacketBase
    {
        public CLGiveEnergyToFriend() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 3109; } }

        private int m_FriendPlayerId;
        [ProtoMember(1, Name = @"FriendPlayerId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int FriendPlayerId
        {
            get { return m_FriendPlayerId; }
            set { m_FriendPlayerId = value; }
        }
    }
}