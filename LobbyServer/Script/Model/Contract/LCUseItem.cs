﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1200_LCUseItem.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCUseItem")]
    public partial class LCUseItem : PacketBase
    {
        public LCUseItem() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 1200; } }

        private readonly List<PBItemInfo> m_ItemInfo = new List<PBItemInfo>();
        [ProtoMember(1, Name = @"ItemInfo", DataFormat = DataFormat.Default)]
        public List<PBItemInfo> ItemInfo
        {
            get { return m_ItemInfo; }
        }

        private PBLobbyHeroInfo m_HeroInfo = null;
        [ProtoMember(2, Name = @"HeroInfo", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBLobbyHeroInfo HeroInfo
        {
            get { return m_HeroInfo; }
            set { m_HeroInfo = value; }
        }

        private PBPlayerInfo m_PlayerInfo = null;
        [ProtoMember(3, Name = @"PlayerInfo", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBPlayerInfo PlayerInfo
        {
            get { return m_PlayerInfo; }
            set { m_PlayerInfo = value; }
        }
    }
}
