﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBGearFoundryProgressInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBGearFoundryProgressInfo")]
    public partial class PBGearFoundryProgressInfo
    {
        public PBGearFoundryProgressInfo() { }

        private int m_CurrentLevel;
        [ProtoMember(1, Name = @"CurrentLevel", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int CurrentLevel
        {
            get { return m_CurrentLevel; }
            set { m_CurrentLevel = value; }
        }

        private int m_CurrentProgress;
        [ProtoMember(2, Name = @"CurrentProgress", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int CurrentProgress
        {
            get { return m_CurrentProgress; }
            set { m_CurrentProgress = value; }
        }
    }
}
