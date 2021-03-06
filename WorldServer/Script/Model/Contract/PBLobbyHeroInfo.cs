﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBLobbyHeroInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract(Name = @"PBLobbyHeroInfo")]
    public partial class PBLobbyHeroInfo
    {
        public PBLobbyHeroInfo() { }

        private int m_Type;
        [ProtoMember(1, Name = @"Type", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        private int? m_Level;
        [ProtoMember(2, Name = @"Level", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int Level
        {
            get { return m_Level ?? default(int); }
            set { m_Level = value; }
        }
        public bool HasLevel { get { return m_Level != null; } }
        private void ResetLevel() { m_Level = null; }
        private bool ShouldSerializeLevel() { return HasLevel; }

        private int? m_Exp;
        [ProtoMember(3, Name = @"Exp", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int Exp
        {
            get { return m_Exp ?? default(int); }
            set { m_Exp = value; }
        }
        public bool HasExp { get { return m_Exp != null; } }
        private void ResetExp() { m_Exp = null; }
        private bool ShouldSerializeExp() { return HasExp; }

        private int? m_StarLevel;
        [ProtoMember(4, Name = @"StarLevel", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int StarLevel
        {
            get { return m_StarLevel ?? default(int); }
            set { m_StarLevel = value; }
        }
        public bool HasStarLevel { get { return m_StarLevel != null; } }
        private void ResetStarLevel() { m_StarLevel = null; }
        private bool ShouldSerializeStarLevel() { return HasStarLevel; }

        private int? m_ConsciousnessLevel;
        [ProtoMember(5, Name = @"ConsciousnessLevel", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int ConsciousnessLevel
        {
            get { return m_ConsciousnessLevel ?? default(int); }
            set { m_ConsciousnessLevel = value; }
        }
        public bool HasConsciousnessLevel { get { return m_ConsciousnessLevel != null; } }
        private void ResetConsciousnessLevel() { m_ConsciousnessLevel = null; }
        private bool ShouldSerializeConsciousnessLevel() { return HasConsciousnessLevel; }

        private int? m_ElevationLevel;
        [ProtoMember(6, Name = @"ElevationLevel", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int ElevationLevel
        {
            get { return m_ElevationLevel ?? default(int); }
            set { m_ElevationLevel = value; }
        }
        public bool HasElevationLevel { get { return m_ElevationLevel != null; } }
        private void ResetElevationLevel() { m_ElevationLevel = null; }
        private bool ShouldSerializeElevationLevel() { return HasElevationLevel; }

        private readonly List<PBGearInfo> m_GearInfo = new List<PBGearInfo>();
        [ProtoMember(7, Name = @"GearInfo", DataFormat = DataFormat.Default)]
        public List<PBGearInfo> GearInfo
        {
            get { return m_GearInfo; }
        }

        private readonly List<PBSoulInfo> m_SoulInfo = new List<PBSoulInfo>();
        [ProtoMember(8, Name = @"SoulInfo", DataFormat = DataFormat.Default)]
        public List<PBSoulInfo> SoulInfo
        {
            get { return m_SoulInfo; }
        }

        private readonly List<int> m_SkillLevels = new List<int>();
        [ProtoMember(9, Name = @"SkillLevels", DataFormat = DataFormat.TwosComplement)]
        public List<int> SkillLevels
        {
            get { return m_SkillLevels; }
        }

        private readonly List<int> m_SkillExps = new List<int>();
        [ProtoMember(10, Name = @"SkillExps", DataFormat = DataFormat.TwosComplement)]
        public List<int> SkillExps
        {
            get { return m_SkillExps; }
        }

        private int? m_Might;
        [ProtoMember(11, Name = @"Might", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int Might
        {
            get { return m_Might ?? default(int); }
            set { m_Might = value; }
        }
        public bool HasMight { get { return m_Might != null; } }
        private void ResetMight() { m_Might = null; }
        private bool ShouldSerializeMight() { return HasMight; }
    }
}
