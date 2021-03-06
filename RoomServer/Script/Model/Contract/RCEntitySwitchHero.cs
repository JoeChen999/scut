﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 5104_RCEntitySwitchHero.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract(Name = @"RCEntitySwitchHero")]
    public partial class RCEntitySwitchHero : PacketBase
    {
        public RCEntitySwitchHero() { }

        public override PacketType PacketType { get { return PacketType.RoomServerToClient; } }

        public override int PacketActionId { get { return 5104; } }

        private int m_OldEntityId;
        [ProtoMember(1, Name = @"OldEntityId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int OldEntityId
        {
            get { return m_OldEntityId; }
            set { m_OldEntityId = value; }
        }

        private int m_NewEntityId;
        [ProtoMember(2, Name = @"NewEntityId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int NewEntityId
        {
            get { return m_NewEntityId; }
            set { m_NewEntityId = value; }
        }

        private int? m_HP;
        [ProtoMember(3, Name = @"HP", IsRequired = false, DataFormat = DataFormat.TwosComplement)]
        public int HP
        {
            get { return m_HP ?? default(int); }
            set { m_HP = value; }
        }
        public bool HasHP { get { return m_HP != null; } }
        private void ResetHP() { m_HP = null; }
        private bool ShouldSerializeHP() { return HasHP; }
    }
}
