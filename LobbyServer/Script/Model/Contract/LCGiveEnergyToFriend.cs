﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 3109_LCGiveEnergyToFriend.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCGiveEnergyToFriend")]
    public partial class LCGiveEnergyToFriend : PacketBase
    {
        public LCGiveEnergyToFriend() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 3109; } }

        private int m_FriendPlayerId;
        [ProtoMember(1, Name = @"FriendPlayerId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int FriendPlayerId
        {
            get { return m_FriendPlayerId; }
            set { m_FriendPlayerId = value; }
        }

        private int m_RemainCount;
        [ProtoMember(2, Name = @"RemainCount", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int RemainCount
        {
            get { return m_RemainCount; }
            set { m_RemainCount = value; }
        }
    }
}