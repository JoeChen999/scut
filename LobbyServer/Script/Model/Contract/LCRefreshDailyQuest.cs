﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 1044_LCRefreshDailyQuest.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCRefreshDailyQuest")]
    public partial class LCRefreshDailyQuest : PacketBase
    {
        public LCRefreshDailyQuest() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 1044; } }

        private PBDailyQuestInfo m_DailyQuestInfo = null;
        [ProtoMember(1, Name = @"DailyQuestInfo", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBDailyQuestInfo DailyQuestInfo
        {
            get { return m_DailyQuestInfo; }
            set { m_DailyQuestInfo = value; }
        }
    }
}