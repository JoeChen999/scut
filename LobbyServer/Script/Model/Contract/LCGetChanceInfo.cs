﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 2200_LCGetChanceInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCGetChanceInfo")]
    public partial class LCGetChanceInfo : PacketBase
    {
        public LCGetChanceInfo() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 2200; } }

        private int m_ChanceType;
        [ProtoMember(1, Name = @"ChanceType", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int ChanceType
        {
            get { return m_ChanceType; }
            set { m_ChanceType = value; }
        }

        private readonly List<PBItemInfo> m_GoodInfo = new List<PBItemInfo>();
        [ProtoMember(2, Name = @"GoodInfo", DataFormat = DataFormat.Default)]
        public List<PBItemInfo> GoodInfo
        {
            get { return m_GoodInfo; }
        }

        private readonly List<int> m_OpenedIndex = new List<int>();
        [ProtoMember(3, Name = @"OpenedIndex", DataFormat = DataFormat.TwosComplement)]
        public List<int> OpenedIndex
        {
            get { return m_OpenedIndex; }
        }

        private readonly List<PBItemInfo> m_OpenedGoodInfo = new List<PBItemInfo>();
        [ProtoMember(4, Name = @"OpenedGoodInfo", DataFormat = DataFormat.Default)]
        public List<PBItemInfo> OpenedGoodInfo
        {
            get { return m_OpenedGoodInfo; }
        }

        private PBInt64 m_NextFreeTime;
        [ProtoMember(5, Name = @"NextFreeTime", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBInt64 NextFreeTime
        {
            get { return m_NextFreeTime; }
            set { m_NextFreeTime = value; }
        }

        private PBInt64 m_NextRefreshTime;
        [ProtoMember(6, Name = @"NextRefreshTime", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBInt64 NextRefreshTime
        {
            get { return m_NextRefreshTime; }
            set { m_NextRefreshTime = value; }
        }

        private int m_FreeChanceTimes;
        [ProtoMember(7, Name = @"FreeChanceTimes", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int FreeChanceTimes
        {
            get { return m_FreeChanceTimes; }
            set { m_FreeChanceTimes = value; }
        }
    }
}
