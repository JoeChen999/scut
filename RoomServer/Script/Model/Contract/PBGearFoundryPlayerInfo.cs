﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBGearFoundryPlayerInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBGearFoundryPlayerInfo")]
    public partial class PBGearFoundryPlayerInfo
    {
        public PBGearFoundryPlayerInfo() { }

        private PBPlayerInfo m_Player;
        [ProtoMember(1, Name = @"Player", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBPlayerInfo Player
        {
            get { return m_Player; }
            set { m_Player = value; }
        }

        private int m_FoundryCount;
        [ProtoMember(2, Name = @"FoundryCount", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int FoundryCount
        {
            get { return m_FoundryCount; }
            set { m_FoundryCount = value; }
        }
    }
}
