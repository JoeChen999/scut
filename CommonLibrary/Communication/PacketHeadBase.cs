namespace Genesis.GameServer.CommonLibrary
{
    public abstract class PacketHeadBase
    {
        public abstract PacketType PacketType
        {
            get;
        }

        public abstract int PacketActionId
        {
            get;
        }
    }
}
