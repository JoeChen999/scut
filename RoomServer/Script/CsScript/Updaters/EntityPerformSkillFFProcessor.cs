using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public class EntityPerformSkillFFProcessor : BaseActionProcessor
    {
        private CREntityPerformSkillFF m_Request;
        public EntityPerformSkillFFProcessor(Room roomData)
            : base(roomData)
        {

        }

        public override ActionType ActionType
        {
            get
            {
                return ActionType.CREntityPerformSkillFF;
            }
        }

        public override bool Verify(Message message)
        {
            m_Request = message.Packet as CREntityPerformSkillFF;
            m_Response.SerialId = m_Request.SerialId;
            var originHero = m_Room.Players[m_UserId].Heros.Find(t => t.EntityId == m_Request.EntityId);
            if (originHero == null || originHero.HP <= 0)
            {
                return false;
            }
            return true;
        }

        public override void Process()
        {
            RCPushEntityPerformSkillFF pushPacket = new RCPushEntityPerformSkillFF()
            {
                EntityId = m_Request.EntityId,
                SkillId = m_Request.SkillId,
                TargetTime = m_Request.TargetTime,
                Transform = m_Request.Transform,
                PlayerId = m_UserId,
            };
            byte[] bufferToOthers = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCPushEntityPerformSkillFF, ProtoBufUtils.Serialize(pushPacket));
            TraceLog.Write("{0} perform skill:{1} fastforward at {2},{3},{4}, TargetTime:{5}", m_Room.Players[m_UserId].Name, m_Request.SkillId, m_Request.Transform.PositionX, m_Request.Transform.PositionY, m_Request.Transform.Rotation, m_Request.TargetTime);
            foreach (var player in m_Room.Players)
            {
                GameSession.Get(player.Key).SendAsync(bufferToOthers, 0, bufferToOthers.Length);
            }
            m_Response.Result = true;
        }
    }
}
