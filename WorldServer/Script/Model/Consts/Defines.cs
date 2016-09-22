using ProtoBuf;
using System;

namespace Genesis.GameServer.WorldServer
{
    public enum RoomServerState
    {
        Normal,
        Exception,
    }

    public enum RoomState
    {
        Waiting,
        Ready,
        Fighting,
        Closed,
    }

    public enum GearType
    {
        Weapon = 1,
        Helmet,
        Armor,
        Shoes,
        Ring,
        Necklace
    }

    public enum PVPType
    {
        Single = 1,
        Triple = 3,
    }
}
