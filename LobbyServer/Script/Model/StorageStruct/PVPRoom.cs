using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    public class PVPRoom : MemoryEntity
    {
        public PVPRoom()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public RoomState State
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public CacheList<int> Players
        {
            get;
            set;
        }
    }
}
