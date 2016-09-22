﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1005_LCLeaveInstance.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCLeaveInstance")]
    public partial class LCLeaveInstance : PacketBase
    {
        public LCLeaveInstance() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 1005; } }

        private int m_InstanceType;
        [ProtoMember(1, Name = @"InstanceType", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int InstanceType
        {
            get { return m_InstanceType; }
            set { m_InstanceType = value; }
        }

        private bool m_Win;
        [ProtoMember(2, Name = @"Win", IsRequired = true, DataFormat = DataFormat.Default)]
        public bool Win
        {
            get { return m_Win; }
            set { m_Win = value; }
        }

        private int m_StarCount;
        [ProtoMember(3, Name = @"StarCount", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int StarCount
        {
            get { return m_StarCount; }
            set { m_StarCount = value; }
        }

        private PBPlayerInfo m_PlayerInfo;
        [ProtoMember(4, Name = @"PlayerInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBPlayerInfo PlayerInfo
        {
            get { return m_PlayerInfo; }
            set { m_PlayerInfo = value; }
        }

        private readonly List<PBLobbyHeroInfo> m_LobbyHeroInfo = new List<PBLobbyHeroInfo>();
        [ProtoMember(5, Name = @"LobbyHeroInfo", DataFormat = DataFormat.Default)]
        public List<PBLobbyHeroInfo> LobbyHeroInfo
        {
            get { return m_LobbyHeroInfo; }
        }

        private PBReceivedItems m_ReceivedItems = null;
        [ProtoMember(6, Name = @"ReceivedItems", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBReceivedItems ReceivedItems
        {
            get { return m_ReceivedItems; }
            set { m_ReceivedItems = value; }
        }
    }
}