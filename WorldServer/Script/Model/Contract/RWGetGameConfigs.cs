﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 7002_RWGetGameConfigs.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract(Name = @"RWGetGameConfigs")]
    public partial class RWGetGameConfigs : PacketBase, IRemoteServerPacket
    {
        public RWGetGameConfigs() { }

        public override PacketType PacketType { get { return PacketType.RoomServerToWorldServer; } }

        public override int PacketActionId { get { return 7002; } }

        private readonly List<string> m_Keys = new List<string>();
        [ProtoMember(1, Name = @"Keys", DataFormat = DataFormat.Default)]
        public List<string> Keys
        {
            get { return m_Keys; }
        }

        private readonly List<string> m_Values = new List<string>();
        [ProtoMember(2, Name = @"Values", DataFormat = DataFormat.Default)]
        public List<string> Values
        {
            get { return m_Values; }
        }
    }
}
