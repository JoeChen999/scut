﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// Proto source: PBActivityInfo.proto
//----------------------------------------------------------------------------------------------------

using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Genesis.GameServer.WorldServer
{
    [Serializable, ProtoContract(Name = @"PBActivityInfo")]
    public partial class PBActivityInfo
    {
        public PBActivityInfo() { }

        private int m_Id;
        [ProtoMember(1, Name = @"Id", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private int m_Status;
        [ProtoMember(2, Name = @"Status", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        private int m_Progress;
        [ProtoMember(3, Name = @"Progress", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int Progress
        {
            get { return m_Progress; }
            set { m_Progress = value; }
        }

        private int m_CountLimit;
        [ProtoMember(4, Name = @"CountLimit", IsRequired = true, DataFormat = DataFormat.TwosComplement)]
        public int CountLimit
        {
            get { return m_CountLimit; }
            set { m_CountLimit = value; }
        }
    }
}