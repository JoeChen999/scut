﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBTrackingAchievement.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract(Name = @"PBTrackingAchievement")]
    public partial class PBTrackingAchievement
    {
        public PBTrackingAchievement() { }

        private int m_AchievementId;
        [ProtoMember(1, Name = @"AchievementId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int AchievementId
        {
            get { return m_AchievementId; }
            set { m_AchievementId = value; }
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
