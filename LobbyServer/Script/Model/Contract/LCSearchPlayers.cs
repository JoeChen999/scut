﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 3101_LCSearchPlayers.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCSearchPlayers")]
    public partial class LCSearchPlayers : PacketBase
    {
        public LCSearchPlayers() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 3101; } }

        private readonly List<PBPlayerInfo> m_Players = new List<PBPlayerInfo>();
        [ProtoMember(1, Name = @"Players", DataFormat = DataFormat.Default)]
        public List<PBPlayerInfo> Players
        {
            get { return m_Players; }
        }

        private readonly List<bool> m_IsMyFriend = new List<bool>();
        [ProtoMember(2, Name = @"IsMyFriend", DataFormat = DataFormat.Default)]
        public List<bool> IsMyFriend
        {
            get { return m_IsMyFriend; }
        }
    }
}
