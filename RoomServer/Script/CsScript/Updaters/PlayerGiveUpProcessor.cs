
using System;
using ZyGames.Framework.Common.Serialization;

namespace Genesis.GameServer.RoomServer
{
    public class PlayerGiveUpProcessor : BaseActionProcessor
    {
        public PlayerGiveUpProcessor(Room roomData)
            :base(roomData)
        {
            m_Room = roomData;
        }

        public override ActionType ActionType
        {
            get { return ActionType.GiveUpBattle; }
        }

        public override void Process()
        {
            RCGiveUpBattle response = new RCGiveUpBattle();
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream((int)ActionType, ProtoBufUtils.Serialize(response));
            m_Session.SendAsync(buffer, 0, buffer.Length);
            RoomManager rm = RoomManager.GetInstance(m_Room.Id);
            rm.PlayerGaveUp(m_UserId);
        }

        public override void PushResult()
        {
        }
    }
}
