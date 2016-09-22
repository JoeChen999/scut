﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1017_LCGetMeridian.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCGetMeridian")]
    public partial class LCGetMeridian : PacketBase
    {
        public LCGetMeridian() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 1017; } }

        private readonly List<PBMeridianInfo> m_UnlockedMeridians = new List<PBMeridianInfo>();
        [ProtoMember(1, Name = @"UnlockedMeridians", DataFormat = DataFormat.Default)]
        public List<PBMeridianInfo> UnlockedMeridians
        {
            get { return m_UnlockedMeridians; }
        }
    }
}
