﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBSkillImpactInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBSkillImpactInfo")]
    public class PBSkillImpactInfo
    {
        public PBSkillImpactInfo() { }

        private int m_EntityId;
        [ProtoMember(1, Name = @"EntityId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int EntityId
        {
            get { return m_EntityId; }
            set { m_EntityId = value; }
        }

        private float m_PositionX;
        [ProtoMember(2, Name = @"PositionX", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float PositionX
        {
            get { return m_PositionX; }
            set { m_PositionX = value; }
        }

        private float m_PositionY;
        [ProtoMember(3, Name = @"PositionY", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float PositionY
        {
            get { return m_PositionY; }
            set { m_PositionY = value; }
        }

        private float m_Rotation;
        [ProtoMember(4, Name = @"Rotation", IsRequired = true, DataFormat = DataFormat.FixedSize)]
        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }

        private int? m_NewStateId;
        [ProtoMember(5, Name = @"NewStateId", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int NewStateId
        {
            get { return m_NewStateId ?? default(int); }
            set { m_NewStateId = value; }
        }
        public bool HasNewStateId { get { return m_NewStateId != null; } }
        private void ResetNewStateId() { m_NewStateId = null; }
        private bool ShouldSerializeNewStateId() { return HasNewStateId; }

        private int? m_HPDamage;
        [ProtoMember(6, Name = @"HPDamage", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int HPDamage
        {
            get { return m_HPDamage ?? default(int); }
            set { m_HPDamage = value; }
        }
        public bool HasHPDamage { get { return m_HPDamage != null; } }
        private void ResetHPDamage() { m_HPDamage = null; }
        private bool ShouldSerializeHPDamage() { return HasHPDamage; }

        private int? m_HPRecover;
        [ProtoMember(7, Name = @"HPRecover", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int HPRecover
        {
            get { return m_HPRecover ?? default(int); }
            set { m_HPRecover = value; }
        }
        public bool HasHPRecover { get { return m_HPRecover != null; } }
        private void ResetHPRecover() { m_HPRecover = null; }
        private bool ShouldSerializeHPRecover() { return HasHPRecover; }

        private int? m_ImpactId;
        [ProtoMember(8, Name = @"ImpactId", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int ImpactId
        {
            get { return m_ImpactId ?? default(int); }
            set { m_ImpactId = value; }
        }
        public bool HasImpactId { get { return m_ImpactId != null; } }
        private void ResetImpactId() { m_ImpactId = null; }
        private bool ShouldSerializeImpactId() { return HasImpactId; }
    }
}
