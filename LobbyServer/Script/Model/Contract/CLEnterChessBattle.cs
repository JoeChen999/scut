﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 2005_CLEnterChessBattle.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLEnterChessBattle")]
    public partial class CLEnterChessBattle : PacketBase
    {
        public CLEnterChessBattle() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 2005; } }

        private int m_ChessFieldIndex;
        [ProtoMember(1, Name = @"ChessFieldIndex", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int ChessFieldIndex
        {
            get { return m_ChessFieldIndex; }
            set { m_ChessFieldIndex = value; }
        }
    }
}
