using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class DTAnnouncement : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTAnnouncement> DTAnnouncementCache = new MemoryCacheStruct<DTAnnouncement>();

        public DTAnnouncement()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public string AnnouncementMessage
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public bool NeedScroll
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int Priority
        {
            get;
            set;
        }

        public void ParseRow(string[] rowData)
        {
            int index = 0;
            index++;
            Id = int.Parse(rowData[index++]);
            index++;
            AnnouncementMessage = rowData[index++];
            NeedScroll = bool.Parse(rowData[index++]);
            Priority = int.Parse(rowData[index++]);

            DTAnnouncementCache.TryAdd(Id.ToString(), this);
        }
    }
}