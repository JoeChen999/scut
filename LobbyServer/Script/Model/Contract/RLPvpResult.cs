﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 9003_RLPvpResult.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"RLPvpResult")]
    public partial class RLPvpResult : PacketBase, IRemoteServerPacket
    {
        public RLPvpResult() { }

        public override PacketType PacketType { get { return PacketType.RoomServerToLobbyServer; } }

        public override int PacketActionId { get { return 9003; } }

        private readonly List<int> m_PlayerIds = new List<int>();
        [ProtoMember(1, Name = @"PlayerIds", DataFormat = DataFormat.TwosComplement)]
        public List<int> PlayerIds
        {
            get { return m_PlayerIds; }
        }

        private readonly List<bool> m_HasWon = new List<bool>();
        [ProtoMember(2, Name = @"HasWon", DataFormat = DataFormat.Default)]
        public List<bool> HasWon
        {
            get { return m_HasWon; }
        }
    }
}