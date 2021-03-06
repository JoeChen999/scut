﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1028_LCCleanOutInstance.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCCleanOutInstance")]
    public partial class LCCleanOutInstance : PacketBase
    {
        public LCCleanOutInstance() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 1028; } }

        private int m_InstanceId;
        [ProtoMember(1, Name = @"InstanceId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int InstanceId
        {
            get { return m_InstanceId; }
            set { m_InstanceId = value; }
        }

        private int m_Count;
        [ProtoMember(2, Name = @"Count", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Count
        {
            get { return m_Count; }
            set { m_Count = value; }
        }

        private PBPlayerInfo m_PlayerInfo;
        [ProtoMember(3, Name = @"PlayerInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBPlayerInfo PlayerInfo
        {
            get { return m_PlayerInfo; }
            set { m_PlayerInfo = value; }
        }

        private readonly List<PBInstanceDrop> m_Drops = new List<PBInstanceDrop>();
        [ProtoMember(4, Name = @"Drops", DataFormat = DataFormat.Default)]
        public List<PBInstanceDrop> Drops
        {
            get { return m_Drops; }
        }

        private PBReceivedItems m_ReceivedItems = null;
        [ProtoMember(5, Name = @"ReceivedItems", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBReceivedItems ReceivedItems
        {
            get { return m_ReceivedItems; }
            set { m_ReceivedItems = value; }
        }

        private readonly List<PBLobbyHeroInfo> m_LobbyHeroInfo = new List<PBLobbyHeroInfo>();
        [ProtoMember(6, Name = @"LobbyHeroInfo", DataFormat = DataFormat.Default)]
        public List<PBLobbyHeroInfo> LobbyHeroInfo
        {
            get { return m_LobbyHeroInfo; }
        }
    }
}
