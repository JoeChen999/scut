﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1034_CLPlayerMove.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLPlayerMove")]
    public partial class CLPlayerMove : PacketBase
    {
        public CLPlayerMove() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 1034; } }

        private float m_LobbyPositionX;
        [ProtoMember(1, Name = @"LobbyPositionX", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float LobbyPositionX
        {
            get { return m_LobbyPositionX; }
            set { m_LobbyPositionX = value; }
        }

        private float m_LobbyPositionY;
        [ProtoMember(2, Name = @"LobbyPositionY", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float LobbyPositionY
        {
            get { return m_LobbyPositionY; }
            set { m_LobbyPositionY = value; }
        }
    }
}
