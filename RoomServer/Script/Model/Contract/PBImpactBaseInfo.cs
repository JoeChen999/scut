﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBImpactBaseInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBImpactBaseInfo")]
    public class PBImpactBaseInfo
    {
        public PBImpactBaseInfo() { }

        private int? m_OriginEntityId;
        [ProtoMember(1, Name = @"OriginEntityId", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int OriginEntityId
        {
            get { return m_OriginEntityId ?? default(int); }
            set { m_OriginEntityId = value; }
        }
        public bool HasOriginEntityId { get { return m_OriginEntityId != null; } }
        private void ResetOriginEntityId() { m_OriginEntityId = null; }
        private bool ShouldSerializeOriginEntityId() { return HasOriginEntityId; }

        private PBTransformInfo m_OriginTransform = null;
        [ProtoMember(2, Name = @"OriginTransform", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBTransformInfo OriginTransform
        {
            get { return m_OriginTransform; }
            set { m_OriginTransform = value; }
        }

        private int? m_TargetEntityId;
        [ProtoMember(3, Name = @"TargetEntityId", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int TargetEntityId
        {
            get { return m_TargetEntityId ?? default(int); }
            set { m_TargetEntityId = value; }
        }
        public bool HasTargetEntityId { get { return m_TargetEntityId != null; } }
        private void ResetTargetEntityId() { m_TargetEntityId = null; }
        private bool ShouldSerializeTargetEntityId() { return HasTargetEntityId; }

        private PBTransformInfo m_TargetTransform = null;
        [ProtoMember(4, Name = @"TargetTransform", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBTransformInfo TargetTransform
        {
            get { return m_TargetTransform; }
            set { m_TargetTransform = value; }
        }

        private int m_ImpactSourceType;
        [ProtoMember(5, Name = @"ImpactSourceType", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int ImpactSourceType
        {
            get { return m_ImpactSourceType; }
            set { m_ImpactSourceType = value; }
        }

        private int m_ImpactId;
        [ProtoMember(6, Name = @"ImpactId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int ImpactId
        {
            get { return m_ImpactId; }
            set { m_ImpactId = value; }
        }

        private int? m_SkillId;
        [ProtoMember(7, Name = @"SkillId", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int SkillId
        {
            get { return m_SkillId ?? default(int); }
            set { m_SkillId = value; }
        }
        public bool HasSkillId { get { return m_SkillId != null; } }
        private void ResetSkillId() { m_SkillId = null; }
        private bool ShouldSerializeSkillId() { return HasSkillId; }

        private float? m_CurrentTime;
        [ProtoMember(8, Name = @"CurrentTime", IsRequired = false, DataFormat = DataFormat.FixedSize)]
        public float CurrentTime
        {
            get { return m_CurrentTime ?? default(float); }
            set { m_CurrentTime = value; }
        }
        public bool HasCurrentTime { get { return m_CurrentTime != null; } }
        private void ResetCurrentTime() { m_CurrentTime = null; }
        private bool ShouldSerializeCurrentTime() { return HasCurrentTime; }
    }
}
