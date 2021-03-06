﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 3101_CLSearchPlayers.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLSearchPlayers")]
    public partial class CLSearchPlayers : PacketBase
    {
        public CLSearchPlayers() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 3101; } }

        private string m_Input;
        [ProtoMember(1, Name = @"Input", IsRequired = true, DataFormat = DataFormat.Default)]
        public string Input
        {
            get { return m_Input; }
            set { m_Input = value; }
        }

        private int? m_DisplayId;
        [ProtoMember(2, Name = @"DisplayId", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int DisplayId
        {
            get { return m_DisplayId ?? default(int); }
            set { m_DisplayId = value; }
        }
        public bool HasDisplayId { get { return m_DisplayId != null; } }
        private void ResetDisplayId() { m_DisplayId = null; }
        private bool ShouldSerializeDisplayId() { return HasDisplayId; }
    }
}
