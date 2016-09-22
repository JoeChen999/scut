﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 2202_LCOpenAllChances.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCOpenAllChances")]
    public partial class LCOpenAllChances : PacketBase
    {
        public LCOpenAllChances() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 2202; } }

        private int m_ChanceType;
        [ProtoMember(1, Name = @"ChanceType", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int ChanceType
        {
            get { return m_ChanceType; }
            set { m_ChanceType = value; }
        }

        private readonly List<int> m_OpenedIndex = new List<int>();
        [ProtoMember(2, Name = @"OpenedIndex", DataFormat = DataFormat.TwosComplement)]
        public List<int> OpenedIndex
        {
            get { return m_OpenedIndex; }
        }

        private readonly List<PBItemInfo> m_OpenedGoodInfo = new List<PBItemInfo>();
        [ProtoMember(3, Name = @"OpenedGoodInfo", DataFormat = DataFormat.Default)]
        public List<PBItemInfo> OpenedGoodInfo
        {
            get { return m_OpenedGoodInfo; }
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