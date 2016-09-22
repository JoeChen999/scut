using Genesis.GameServer.CommonLibrary;
using ZyGames.Framework.Cache.Generic;

namespace Genesis.GameServer.WorldServer
{
    public static class CacheSet
    {
        public static ShareCacheStruct<RoomServer> RoomServerCache = new ShareCacheStruct<RoomServer>();
        public static ShareCacheStruct<LobbyServer> LobbyServerCache = new ShareCacheStruct<LobbyServer>();
        public static ShareCacheStruct<PvpPlayer> PvpPlayerCache = new ShareCacheStruct<PvpPlayer>();
        public static MemoryCacheStruct<PVPMatchQueue> PVPMathcQueueCache = new MemoryCacheStruct<PVPMatchQueue>();
    }
}
