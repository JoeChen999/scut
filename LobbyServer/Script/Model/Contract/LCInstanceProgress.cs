﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1008_LCInstanceProgress.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCInstanceProgress")]
    public partial class LCInstanceProgress : PacketBase
    {
        public LCInstanceProgress() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 1008; } }

        private readonly List<PBInstance> m_InstanceProgress = new List<PBInstance>();
        [ProtoMember(1, Name = @"InstanceProgress", DataFormat = DataFormat.Default)]
        public List<PBInstance> InstanceProgress
        {
            get { return m_InstanceProgress; }
        }
    }
}
