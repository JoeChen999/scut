﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1003_LCSignIn.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCSignIn")]
    public partial class LCSignIn : PacketBase
    {
        public LCSignIn() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 1003; } }

        private PBPlayerInfo m_PlayerInfo;
        [ProtoMember(1, Name = @"PlayerInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBPlayerInfo PlayerInfo
        {
            get { return m_PlayerInfo; }
            set { m_PlayerInfo = value; }
        }

        private readonly List<PBLobbyHeroInfo> m_LobbyHeroInfo = new List<PBLobbyHeroInfo>();
        [ProtoMember(2, Name = @"LobbyHeroInfo", DataFormat = DataFormat.Default)]
        public List<PBLobbyHeroInfo> LobbyHeroInfo
        {
            get { return m_LobbyHeroInfo; }
        }

        private PBHeroTeamInfo m_HeroTeamInfo;
        [ProtoMember(3, Name = @"HeroTeamInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBHeroTeamInfo HeroTeamInfo
        {
            get { return m_HeroTeamInfo; }
            set { m_HeroTeamInfo = value; }
        }

        private PBAchievementInfo m_AchievementInfo;
        [ProtoMember(4, Name = @"AchievementInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBAchievementInfo AchievementInfo
        {
            get { return m_AchievementInfo; }
            set { m_AchievementInfo = value; }
        }

        private PBDailyQuestInfo m_DailyQuestInfo;
        [ProtoMember(5, Name = @"DailyQuestInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBDailyQuestInfo DailyQuestInfo
        {
            get { return m_DailyQuestInfo; }
            set { m_DailyQuestInfo = value; }
        }
    }
}
