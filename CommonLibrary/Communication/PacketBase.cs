namespace Genesis.GameServer.CommonLibrary
{
    public abstract class PacketBase : IMessageData
    {
        public PacketBase()
        {

        }

        public abstract PacketType PacketType
        {
            get;
        }

        public abstract int PacketActionId
        {
            get;
        }
    }

    public interface IMessageData { }
}
