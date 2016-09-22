﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 8003_WLMatchSuccess.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract(Name = @"WLMatchSuccess")]
    public partial class WLMatchSuccess : PacketBase, IRemoteServerPacket
    {
        public WLMatchSuccess() { }

        public override PacketType PacketType { get { return PacketType.WorldServerToLobbyServer; } }

        public override int PacketActionId { get { return 8003; } }

        private int m_PlayerId;
        [ProtoMember(1, Name = @"PlayerId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int PlayerId
        {
            get { return m_PlayerId; }
            set { m_PlayerId = value; }
        }

        private int m_RoomId;
        [ProtoMember(2, Name = @"RoomId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int RoomId
        {
            get { return m_RoomId; }
            set { m_RoomId = value; }
        }

        private int m_InstanceId;
        [ProtoMember(3, Name = @"InstanceId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int InstanceId
        {
            get { return m_InstanceId; }
            set { m_InstanceId = value; }
        }

        private int m_RoomServerPort;
        [ProtoMember(4, Name = @"RoomServerPort", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int RoomServerPort
        {
            get { return m_RoomServerPort; }
            set { m_RoomServerPort = value; }
        }

        private string m_RoomServerHost;
        [ProtoMember(5, Name = @"RoomServerHost", IsRequired = true, DataFormat = DataFormat.Default)]
        public string RoomServerHost
        {
            get { return m_RoomServerHost; }
            set { m_RoomServerHost = value; }
        }

        private string m_Token;
        [ProtoMember(6, Name = @"Token", IsRequired = true, DataFormat = DataFormat.Default)]
        public string Token
        {
            get { return m_Token; }
            set { m_Token = value; }
        }

        private PBRoomPlayerInfo m_EnemyInfo;
        [ProtoMember(7, Name = @"EnemyInfo", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBRoomPlayerInfo EnemyInfo
        {
            get { return m_EnemyInfo; }
            set { m_EnemyInfo = value; }
        }
    }
}
