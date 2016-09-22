namespace Genesis.GameServer.CommonLibrary
{
    public enum PacketType : byte
    {
        Undefined = 0,
        ClientToLobbyServer = 1,
        LobbyServerToClient = 2,
        ClientToRoomServer = 3,
        RoomServerToClient = 4,
        LobbyServerToRoomServer = 5,
        RoomServerToLobbyServer = 6,
        WorldServerToLobbyServer = 7,
        WorldServerToRoomServer = 8,
        LobbyServerToWorldServer = 9,
        RoomServerToWorldServer = 10,
    }
}
