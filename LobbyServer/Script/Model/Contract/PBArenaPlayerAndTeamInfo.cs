﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBArenaPlayerAndTeamInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"PBArenaPlayerAndTeamInfo")]
    public partial class PBArenaPlayerAndTeamInfo
    {
        public PBArenaPlayerAndTeamInfo() { }

        private int m_Rank;
        [ProtoMember(1, Name = @"Rank", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Rank
        {
            get { return m_Rank; }
            set { m_Rank = value; }
        }

        private PBPlayerInfo m_PlayerInfo;
        [ProtoMember(2, Name = @"PlayerInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBPlayerInfo PlayerInfo
        {
            get { return m_PlayerInfo; }
            set { m_PlayerInfo = value; }
        }

        private readonly List<PBLobbyHeroInfo> m_HeroTeam = new List<PBLobbyHeroInfo>();
        [ProtoMember(3, Name = @"HeroTeam", DataFormat = DataFormat.Default)]
        public List<PBLobbyHeroInfo> HeroTeam
        {
            get { return m_HeroTeam; }
        }
    }
}