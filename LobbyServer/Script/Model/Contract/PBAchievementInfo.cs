﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBAchievementInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"PBAchievementInfo")]
    public partial class PBAchievementInfo
    {
        public PBAchievementInfo() { }

        private readonly List<PBTrackingAchievement> m_TrackingAchievements = new List<PBTrackingAchievement>();
        [ProtoMember(1, Name = @"TrackingAchievements", DataFormat = DataFormat.Default)]
        public List<PBTrackingAchievement> TrackingAchievements
        {
            get { return m_TrackingAchievements; }
        }

        private readonly List<int> m_CompletedAchievements = new List<int>();
        [ProtoMember(2, Name = @"CompletedAchievements", DataFormat = DataFormat.TwosComplement)]
        public List<int> CompletedAchievements
        {
            get { return m_CompletedAchievements; }
        }
    }
}
