using ProtoBuf;

namespace Genesis.GameServer.CommonLibrary
{
    [ProtoContract]
    public class RCPacketHead : PacketHeadBase
    {
        public override PacketType PacketType
        {
            get
            {
                return PacketType.RoomServerToClient;
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
        public int ErrorCode
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public string ErrorInfo
        {
            get;
            set;
        }
    }
}
