﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBStiffnessImpact.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBStiffnessImpact")]
    public partial class PBStiffnessImpact
    {
        public PBStiffnessImpact() { }

        private int m_ImpactId;
        [ProtoMember(1, Name = @"ImpactId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int ImpactId
        {
            get { return m_ImpactId; }
            set { m_ImpactId = value; }
        }

        private int m_StateCategory;
        [ProtoMember(2, Name = @"StateCategory", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int StateCategory
        {
            get { return m_StateCategory; }
            set { m_StateCategory = value; }
        }

        private PBVector3 m_RepulseDirection;
        [ProtoMember(3, Name = @"RepulseDirection", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBVector3 RepulseDirection
        {
            get { return m_RepulseDirection; }
            set { m_RepulseDirection = value; }
        }

        private PBVector3 m_FloatFallingSpeed;
        [ProtoMember(4, Name = @"FloatFallingSpeed", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBVector3 FloatFallingSpeed
        {
            get { return m_FloatFallingSpeed; }
            set { m_FloatFallingSpeed = value; }
        }

        private float m_FloatFallingTime;
        [ProtoMember(5, Name = @"FloatFallingTime", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float FloatFallingTime
        {
            get { return m_FloatFallingTime; }
            set { m_FloatFallingTime = value; }
        }

        private float m_DownTime;
        [ProtoMember(6, Name = @"DownTime", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float DownTime
        {
            get { return m_DownTime; }
            set { m_DownTime = value; }
        }

        private float m_StiffnessTime;
        [ProtoMember(7, Name = @"StiffnessTime", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float StiffnessTime
        {
            get { return m_StiffnessTime; }
            set { m_StiffnessTime = value; }
        }

        private float m_RepulseStartTime;
        [ProtoMember(8, Name = @"RepulseStartTime", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float RepulseStartTime
        {
            get { return m_RepulseStartTime; }
            set { m_RepulseStartTime = value; }
        }

        private PBVector3 m_RepulseSpeed;
        [ProtoMember(9, Name = @"RepulseSpeed", IsRequired = true, DataFormat = DataFormat.Default)]
        public PBVector3 RepulseSpeed
        {
            get { return m_RepulseSpeed; }
            set { m_RepulseSpeed = value; }
        }

        private float m_RepulseTime;
        [ProtoMember(10, Name = @"RepulseTime", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float RepulseTime
        {
            get { return m_RepulseTime; }
            set { m_RepulseTime = value; }
        }

        private int m_ImpactAnimationType;
        [ProtoMember(11, Name = @"ImpactAnimationType", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int ImpactAnimationType
        {
            get { return m_ImpactAnimationType; }
            set { m_ImpactAnimationType = value; }
        }

        private int m_FallingAnimationType;
        [ProtoMember(12, Name = @"FallingAnimationType", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int FallingAnimationType
        {
            get { return m_FallingAnimationType; }
            set { m_FallingAnimationType = value; }
        }
    }
}
