﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 4109_RCPushEntityRemoveBuff.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"RCPushEntityRemoveBuff")]
    public partial class RCPushEntityRemoveBuff : PacketBase
    {
        public RCPushEntityRemoveBuff() { }

        public override PacketType PacketType { get { return PacketType.RoomServerToClient; } }

        public override int PacketActionId { get { return 4109; } }

        private int m_OriginPlayerId;
        [ProtoMember(1, Name = @"OriginPlayerId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int OriginPlayerId
        {
            get { return m_OriginPlayerId; }
            set { m_OriginPlayerId = value; }
        }

        private int m_OriginEntityId;
        [ProtoMember(2, Name = @"OriginEntityId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int OriginEntityId
        {
            get { return m_OriginEntityId; }
            set { m_OriginEntityId = value; }
        }

        private PBTransformInfo m_OriginTransform;
        [ProtoMember(3, Name = @"OriginTransform", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBTransformInfo OriginTransform
        {
            get { return m_OriginTransform; }
            set { m_OriginTransform = value; }
        }

        private int m_TargetPlayerId;
        [ProtoMember(4, Name = @"TargetPlayerId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int TargetPlayerId
        {
            get { return m_TargetPlayerId; }
            set { m_TargetPlayerId = value; }
        }

        private int m_TargetEntityId;
        [ProtoMember(5, Name = @"TargetEntityId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int TargetEntityId
        {
            get { return m_TargetEntityId; }
            set { m_TargetEntityId = value; }
        }

        private PBTransformInfo m_TargetTransform;
        [ProtoMember(6, Name = @"TargetTransform", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBTransformInfo TargetTransform
        {
            get { return m_TargetTransform; }
            set { m_TargetTransform = value; }
        }

        private readonly List<int> m_BuffTypeIds = new List<int>();
        [ProtoMember(7, Name = @"BuffTypeIds", DataFormat = DataFormat.TwosComplement)]
        public List<int> BuffTypeIds
        {
            get { return m_BuffTypeIds; }
        }
    }
}
