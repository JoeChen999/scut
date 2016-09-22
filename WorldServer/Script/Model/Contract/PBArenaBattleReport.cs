﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBArenaBattleReport.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract(Name = @"PBArenaBattleReport")]
    public partial class PBArenaBattleReport
    {
        public PBArenaBattleReport() { }

        private PBArenaPlayerAndTeamInfo m_Enemy;
        [ProtoMember(1, Name = @"Enemy", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBArenaPlayerAndTeamInfo Enemy
        {
            get { return m_Enemy; }
            set { m_Enemy = value; }
        }

        private bool m_Win;
        [ProtoMember(2, Name = @"Win", IsRequired = true, DataFormat = DataFormat.Default)]
        public bool Win
        {
            get { return m_Win; }
            set { m_Win = value; }
        }

        private bool m_IsActive;
        [ProtoMember(3, Name = @"IsActive", IsRequired = true, DataFormat = DataFormat.Default)]
        public bool IsActive
        {
            get { return m_IsActive; }
            set { m_IsActive = value; }
        }

        private PBInt64 m_BattleEndTime;
        [ProtoMember(4, Name = @"BattleEndTime", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBInt64 BattleEndTime
        {
            get { return m_BattleEndTime; }
            set { m_BattleEndTime = value; }
        }
    }
}
