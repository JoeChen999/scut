﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 3104_CLDeleteFriend.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLDeleteFriend")]
    public partial class CLDeleteFriend : PacketBase
    {
        public CLDeleteFriend() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 3104; } }

        private int m_PlayerId;
        [ProtoMember(1, Name = @"PlayerId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int PlayerId
        {
            get { return m_PlayerId; }
            set { m_PlayerId = value; }
        }
    }
}
