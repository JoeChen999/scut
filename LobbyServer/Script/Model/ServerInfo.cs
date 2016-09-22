using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public class ServerInfo
    {
        public int ServerId { get; set; }

        public int Load { get; set; }

        public IList<RoomServerInfo> Rooms { get; set; }
    }

    public class RoomServerInfo
    {
        public int RoomServerId { get; set; }

        public string RoomServerIp { get; set; }

        public int RoomCount { get; set; }

        public RoomServerState RoomState { get; set; }
    }
}
