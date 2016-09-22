using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public class EntitySkillRushingProcessor : BaseActionProcessor
    {
        private CREntitySkillRushing m_Request;
        public EntitySkillRushingProcessor(Room roomData)
            : base(roomData)
        {

        }

        public override ActionType ActionType
        {
            get
            {
                return ActionType.CREntitySKillRushing;
            }
        }

        public override bool Verify(Message message)
        {
            m_Request = message.Packet as CREntitySkillRushing;
            m_Response.SerialId = m_Request.SerialId;
            int playerId = message.Session.UserId;
            var originHero = m_Room.Players[playerId].Heros.Find(t => t.EntityId == m_Request.EntityId);
            if (originHero == null || originHero.HP <= 0)
            {
                return false;
            }
            return true;
        }

        public override void Process()
        {
            RCPushEntitySkillRushing pushPacket = new RCPushEntitySkillRushing()
            {
                EntityId = m_Request.EntityId,
                SkillId = m_Request.SkillId,
                Transform = m_Request.Transform,
                PlayerId = m_UserId,
            };
            byte[] bufferToOthers = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCPushEntitySkillRushing, ProtoBufUtils.Serialize(pushPacket));
            TraceLog.Write("{0} is performing rushing skill:{1} at {2},{3},{4}", m_Room.Players[m_UserId].Name, m_Request.SkillId, m_Request.Transform.PositionX, m_Request.Transform.PositionY, m_Request.Transform.Rotation);
            foreach (var player in m_Room.Players)
            {
                GameSession.Get(player.Key).SendAsync(bufferToOthers, 0, bufferToOthers.Length);
            }
            m_Response.Result = true;
        }
    }
}

