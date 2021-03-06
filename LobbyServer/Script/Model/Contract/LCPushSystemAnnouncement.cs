﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: 4006_LCPushSystemAnnouncement.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract(Name = @"LCPushSystemAnnouncement")]
    public partial class LCPushSystemAnnouncement : PacketBase
    {
        public LCPushSystemAnnouncement() { }

        public override PacketType PacketType { get { return PacketType.LobbyServerToClient; } }

        public override int PacketActionId { get { return 4006; } }

        private int m_AnnouncementId;
        [ProtoMember(1, Name = @"AnnouncementId", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int AnnouncementId
        {
            get { return m_AnnouncementId; }
            set { m_AnnouncementId = value; }
        }

        private PBPlayerInfo m_Sender = null;
        [ProtoMember(2, Name = @"Sender", IsRequired = false, DataFormat = DataFormat.Default)]
        [System.ComponentModel.DefaultValue(null)]
        public PBPlayerInfo Sender
        {
            get { return m_Sender; }
            set { m_Sender = value; }
        }

        private readonly List<string> m_Params = new List<string>();
        [ProtoMember(3, Name = @"Params", DataFormat = DataFormat.Default)]
        public List<string> Params
        {
            get { return m_Params; }
        }
    }
}
