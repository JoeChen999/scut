﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 2009_LCChangeHeroTeamChess.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCChangeHeroTeamChess")]
    public partial class LCChangeHeroTeamChess : PacketBase
    {
        public LCChangeHeroTeamChess() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 2009; } }

        private PBHeroTeamInfo m_HeroTeamInfo;
        [ProtoMember(3, Name = @"HeroTeamInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBHeroTeamInfo HeroTeamInfo
        {
            get { return m_HeroTeamInfo; }
            set { m_HeroTeamInfo = value; }
        }
    }
}
