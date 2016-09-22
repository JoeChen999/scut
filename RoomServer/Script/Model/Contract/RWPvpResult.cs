﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 7003_RWPvpResult.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"RWPvpResult")]
    public partial class RWPvpResult : PacketBase, IRemoteServerPacket
    {
        public RWPvpResult() { }

        public override PacketType PacketType { get { return PacketType.RoomServerToWorldServer; } }

        public override int PacketActionId { get { return 7003; } }

        private readonly List<int> m_PlayerIds = new List<int>();
        [ProtoMember(1, Name = @"PlayerIds", DataFormat = DataFormat.TwosComplement)]
        public List<int> PlayerIds
        {
            get { return m_PlayerIds; }
        }

        private readonly List<int> m_ServerId = new List<int>();
        [ProtoMember(2, Name = @"ServerId", DataFormat = DataFormat.TwosComplement)]
        public List<int> ServerId
        {
            get { return m_ServerId; }
        }

        private readonly List<int> m_Result = new List<int>();
        [ProtoMember(3, Name = @"Result", DataFormat = DataFormat.TwosComplement)]
        public List<int> Result
        {
            get { return m_Result; }
        }
    }
}
