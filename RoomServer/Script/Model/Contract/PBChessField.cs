﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBChessField.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"PBChessField")]
    public partial class PBChessField
    {
        public PBChessField() { }

        private int m_Color;
        [ProtoMember(1, Name = @"Color", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        private bool m_IsOpened;
        [ProtoMember(2, Name = @"IsOpened", IsRequired = true, DataFormat = DataFormat.Default)]
        public bool IsOpened
        {
            get { return m_IsOpened; }
            set { m_IsOpened = value; }
        }

        private int? m_FreeCount;
        [ProtoMember(3, Name = @"FreeCount", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int FreeCount
        {
            get { return m_FreeCount ?? default(int); }
            set { m_FreeCount = value; }
        }
        public bool HasFreeCount { get { return m_FreeCount != null; } }
        private void ResetFreeCount() { m_FreeCount = null; }
        private bool ShouldSerializeFreeCount() { return HasFreeCount; }

        private bool? m_IsFree;
        [ProtoMember(4, Name = @"IsFree", IsRequired = false, DataFormat = DataFormat.Default)]
        public bool IsFree
        {
            get { return m_IsFree ?? default(bool); }
            set { m_IsFree = value; }
        }
        public bool HasIsFree { get { return m_IsFree != null; } }
        private void ResetIsFree() { m_IsFree = null; }
        private bool ShouldSerializeIsFree() { return HasIsFree; }

        private PBPlayerInfo m_EnemyInfo = null;
        [ProtoMember(5, Name = @"EnemyInfo", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBPlayerInfo EnemyInfo
        {
            get { return m_EnemyInfo; }
            set { m_EnemyInfo = value; }
        }

        private int? m_EnemyAnger;
        [ProtoMember(6, Name = @"EnemyAnger", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int EnemyAnger
        {
            get { return m_EnemyAnger ?? default(int); }
            set { m_EnemyAnger = value; }
        }
        public bool HasEnemyAnger { get { return m_EnemyAnger != null; } }
        private void ResetEnemyAnger() { m_EnemyAnger = null; }
        private bool ShouldSerializeEnemyAnger() { return HasEnemyAnger; }

        private readonly List<PBLobbyHeroInfo> m_EnemyTeamInfo = new List<PBLobbyHeroInfo>();
        [ProtoMember(7, Name = @"EnemyTeamInfo", DataFormat = DataFormat.Default)]
        public List<PBLobbyHeroInfo> EnemyTeamInfo
        {
            get { return m_EnemyTeamInfo; }
        }

        private int? m_Index;
        [ProtoMember(8, Name = @"Index", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int Index
        {
            get { return m_Index ?? default(int); }
            set { m_Index = value; }
        }
        public bool HasIndex { get { return m_Index != null; } }
        private void ResetIndex() { m_Index = null; }
        private bool ShouldSerializeIndex() { return HasIndex; }

        private int? m_Parent;
        [ProtoMember(9, Name = @"Parent", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int Parent
        {
            get { return m_Parent ?? default(int); }
            set { m_Parent = value; }
        }
        public bool HasParent { get { return m_Parent != null; } }
        private void ResetParent() { m_Parent = null; }
        private bool ShouldSerializeParent() { return HasParent; }

        private readonly List<int> m_Children = new List<int>();
        [ProtoMember(10, Name = @"Children", DataFormat = DataFormat.TwosComplement)]
        public List<int> Children
        {
            get { return m_Children; }
        }
    }
}
