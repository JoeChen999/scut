﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBTrackingDailyQuest.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"PBTrackingDailyQuest")]
    public partial class PBTrackingDailyQuest
    {
        public PBTrackingDailyQuest() { }

        private int m_QuestId;
        [ProtoMember(1, Name = @"QuestId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int QuestId
        {
            get { return m_QuestId; }
            set { m_QuestId = value; }
        }

        private int m_ProgressCount;
        [ProtoMember(2, Name = @"ProgressCount", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int ProgressCount
        {
            get { return m_ProgressCount; }
            set { m_ProgressCount = value; }
        }
    }
}
