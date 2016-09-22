using Genesis.GameServer.CommonLibrary;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.LobbyServer
{
    public class Broadcaster
    {
        private byte[] m_Message;
        public Broadcaster()
        {

        }

        public void ConstructMessage(ActionGetter actionGetter, IMessageData message)
        {
            LCPacketHead packetHead = new LCPacketHead()
            {
                MsgId = actionGetter.GetMsgId(),
                ActionId = actionGetter.GetActionId(),
                ErrorCode = 0,
                ErrorInfo = string.Empty
            };

            byte[] headBytes = ProtoBufUtils.Serialize(packetHead);
            byte[] data = ProtoBufUtils.Serialize(message);
            m_Message = BufferUtils.MergeBytes(BufferUtils.AppendHeadBytes(headBytes), data);
        }

        public void Send(GameSession user)
        {
            user.SendAsync(m_Message, 0, m_Message.Length);
        }

        public void Send(GameSession[] userList)
        {
            foreach (GameSession user in userList)
            {
                user.SendAsync(m_Message, 0, m_Message.Length);
            }
        }
    }
}
