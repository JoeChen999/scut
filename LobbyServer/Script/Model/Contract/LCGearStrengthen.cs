﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1014_LCGearStrengthen.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCGearStrengthen")]
    public partial class LCGearStrengthen : PacketBase
    {
        public LCGearStrengthen() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 1014; } }

        private PBGearInfo m_LevelUpedGear;
        [ProtoMember(1, Name = @"LevelUpedGear", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBGearInfo LevelUpedGear
        {
            get { return m_LevelUpedGear; }
            set { m_LevelUpedGear = value; }
        }

        private PBItemInfo m_ItemInfo;
        [ProtoMember(2, Name = @"ItemInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBItemInfo ItemInfo
        {
            get { return m_ItemInfo; }
            set { m_ItemInfo = value; }
        }

        private PBLobbyHeroInfo m_HeroInfo = null;
        [ProtoMember(3, Name = @"HeroInfo", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBLobbyHeroInfo HeroInfo
        {
            get { return m_HeroInfo; }
            set { m_HeroInfo = value; }
        }
    }
}
