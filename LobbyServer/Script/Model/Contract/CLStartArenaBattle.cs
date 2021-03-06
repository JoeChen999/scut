﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 2303_CLStartArenaBattle.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"CLStartArenaBattle")]
    public partial class CLStartArenaBattle : PacketBase
    {
        public CLStartArenaBattle() { }

        public override PacketType PacketType { get { return PacketType.ClientToLobbyServer; } }

        public override int PacketActionId { get { return 2303; } }

        private int m_EnemyPlayerId;
        [ProtoMember(1, Name = @"EnemyPlayerId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int EnemyPlayerId
        {
            get { return m_EnemyPlayerId; }
            set { m_EnemyPlayerId = value; }
        }

        private bool m_IsRevenge;
        [ProtoMember(2, Name = @"IsRevenge", IsRequired = true, DataFormat = DataFormat.Default)]
        public bool IsRevenge
        {
            get { return m_IsRevenge; }
            set { m_IsRevenge = value; }
        }

        private int m_MyRank;
        [ProtoMember(3, Name = @"MyRank", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int MyRank
        {
            get { return m_MyRank; }
            set { m_MyRank = value; }
        }

        private int m_EnemyRank;
        [ProtoMember(4, Name = @"EnemyRank", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int EnemyRank
        {
            get { return m_EnemyRank; }
            set { m_EnemyRank = value; }
        }
    }
}
