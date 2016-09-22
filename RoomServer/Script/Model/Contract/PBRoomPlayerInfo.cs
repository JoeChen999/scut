﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBRoomPlayerInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBRoomPlayerInfo")]
    public partial class PBRoomPlayerInfo
    {
        public PBRoomPlayerInfo() { }

        private PBPlayerInfo m_PlayerInfo;
        [ProtoMember(1, Name = @"PlayerInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBPlayerInfo PlayerInfo
        {
            get { return m_PlayerInfo; }
            set { m_PlayerInfo = value; }
        }

        private readonly List<PBRoomHeroInfo> m_RoomHeroInfo = new List<PBRoomHeroInfo>();
        [ProtoMember(2, Name = @"RoomHeroInfo", DataFormat = DataFormat.Default)]
        public List<PBRoomHeroInfo> RoomHeroInfo
        {
            get { return m_RoomHeroInfo; }
        }

        private int m_LobbyServerId;
        [ProtoMember(3, Name = @"LobbyServerId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int LobbyServerId
        {
            get { return m_LobbyServerId; }
            set { m_LobbyServerId = value; }
        }

        private int m_Score;
        [ProtoMember(4, Name = @"Score", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }
    }
}