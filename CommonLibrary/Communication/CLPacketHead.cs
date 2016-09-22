using ProtoBuf;

namespace Genesis.GameServer.CommonLibrary
{
    [ProtoContract]
    public class CLPacketHead : PacketHeadBase
    {
        public override PacketType PacketType
        {
            get
            {
                return PacketType.ClientToLobbyServer;
            }
        }

        public override int PacketActionId
        {
            get
            {
                return ActionId;
            }
        }

        [ProtoMember(1)]
        public int MsgId
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public int ActionId
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public string SessionId
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public int UserId
        {
            get;
            set;
        }
    }
}
