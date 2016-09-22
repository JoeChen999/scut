
using System;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public class EntityPerformSkillEndProcessor : BaseActionProcessor
    {
        private CREntityPerformSkillEnd m_Request;
        public EntityPerformSkillEndProcessor(Room roomData)
            :base(roomData)
        {
        }

        public override ActionType ActionType
        {
            get
            {
                return ActionType.CREntityPerformSkillEnd;
            }
        }

        public override bool Verify(Message message)
        {
            m_Request = message.Packet as CREntityPerformSkillEnd;
            m_Response.SerialId = m_Request.SerialId;
            if (DateTime.UtcNow.Ticks - m_Room.StartTime < GameConfigs.GetInt("Room_Battle_Start_Protection_Time", 3) * TimeSpan.TicksPerSecond)
            {
                return false;
            }
            if (m_Request.EntityId != m_Room.Players[message.Session.UserId].InBattleEntity)
            {
                return false;
            }
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
            RCPushEntityPerformSkillEnd response = new RCPushEntityPerformSkillEnd()
            {
                EntityId = m_Request.EntityId,
                Transform = m_Request.Transform,
                SkillId = m_Request.SkillId,
                Reason = m_Request.Reason,
                PlayerId = m_UserId,
            };
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCPushEntityPerformSkillEnd, ProtoBufUtils.Serialize(response));
            TraceLog.Write("{0} perform skill:{1} end at {2},{3},{4} because of reasonId:{5}", m_Room.Players[m_UserId].Name, m_Request.SkillId, m_Request.Transform.PositionX, m_Request.Transform.PositionY, m_Request.Transform.Rotation, m_Request.Reason);
            foreach (var player in m_Room.Players)
            {
                GameSession.Get(player.Key).SendAsync(buffer, 0, buffer.Length);
            }
            m_Response.Result = true;
        }
    }
}
