﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 5101_CREntityPerformSkillStart.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"CREntityPerformSkillStart")]
    public partial class CREntityPerformSkillStart : PacketBase
    {
        public CREntityPerformSkillStart() { }

        public override PacketType PacketType { get { return PacketType.ClientToRoomServer; } }

        public override int PacketActionId { get { return 5101; } }

        private int m_SerialId;
        [ProtoMember(1, Name = @"SerialId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int SerialId
        {
            get { return m_SerialId; }
            set { m_SerialId = value; }
        }

        private int m_EntityId;
        [ProtoMember(2, Name = @"EntityId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int EntityId
        {
            get { return m_EntityId; }
            set { m_EntityId = value; }
        }

        private PBTransformInfo m_Transform;
        [ProtoMember(3, Name = @"Transform", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBTransformInfo Transform
        {
            get { return m_Transform; }
            set { m_Transform = value; }
        }

        private int m_SkillId;
        [ProtoMember(4, Name = @"SkillId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int SkillId
        {
            get { return m_SkillId; }
            set { m_SkillId = value; }
        }
    }
}
