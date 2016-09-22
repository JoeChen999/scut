﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 2302_LCGetArenaRankList.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCGetArenaRankList")]
    public partial class LCGetArenaRankList : PacketBase
    {
        public LCGetArenaRankList() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 2302; } }

        private readonly List<PBArenaPlayerAndTeamInfo> m_Enemies = new List<PBArenaPlayerAndTeamInfo>();
        [ProtoMember(1, Name = @"Enemies", DataFormat = DataFormat.Default)]
        public List<PBArenaPlayerAndTeamInfo> Enemies
        {
            get { return m_Enemies; }
        }

        private bool m_IsLastPage;
        [ProtoMember(2, Name = @"IsLastPage", IsRequired = true, DataFormat = DataFormat.Default)]
        public bool IsLastPage
        {
            get { return m_IsLastPage; }
            set { m_IsLastPage = value; }
        }

        private int m_PageIndex;
        [ProtoMember(3, Name = @"PageIndex", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int PageIndex
        {
            get { return m_PageIndex; }
            set { m_PageIndex = value; }
        }

        private int m_MyRank;
        [ProtoMember(4, Name = @"MyRank", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int MyRank
        {
            get { return m_MyRank; }
            set { m_MyRank = value; }
        }
    }
}
