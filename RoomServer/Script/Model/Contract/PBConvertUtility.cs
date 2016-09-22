using UnityEngine;

namespace Genesis.GameServer.RoomServer
{
    public partial class PBInt64
    {
        public static implicit operator long (PBInt64 pb)
        {
            return (long)pb.High << 32 | (long)(uint)pb.Low;
        }

        public static implicit operator PBInt64(long l)
        {
            return new PBInt64 { Low = unchecked((int)l), High = unchecked((int)(l >> 32)) };
        }
    }

    public partial class PBVector2
    {
        public static implicit operator Vector2(PBVector2 pb)
        {
            return new Vector2(pb.X, pb.Y);
        }

        public static implicit operator PBVector2(Vector2 v)
        {
            return new PBVector2 { X = v.x, Y = v.y };
        }
    }

    public partial class PBVector3
    {
        public static implicit operator Vector3(PBVector3 pb)
        {
            return new Vector3(pb.X, pb.Y, pb.Z);
        }

        public static implicit operator PBVector3(Vector3 v)
        {
            return new PBVector3 { X = v.x, Y = v.y, Z = v.z };
        }
    }
}
