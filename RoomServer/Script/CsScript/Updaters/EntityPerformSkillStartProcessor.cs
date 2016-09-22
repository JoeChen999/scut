
using System;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public class EntityPerformSkillStartProcessor : BaseActionProcessor
    {
        private CREntityPerformSkillStart m_Request;
        public EntityPerformSkillStartProcessor(Room roomData)
            :base(roomData)
        {
            m_Room = roomData;
        }

        public override ActionType ActionType
        {
            get
            {
                return ActionType.CREntityPerformSkillStart;
            }
        }

        public override bool Verify(Message message)
        {
            m_Request = message.Packet as CREntityPerformSkillStart;
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
            RCPushEntityPerformSkillStart response = new RCPushEntityPerformSkillStart()
            {
                EntityId = m_Request.EntityId,
                Transform = m_Request.Transform,
                SkillId = m_Request.SkillId,
                PlayerId = m_UserId,
            };
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCPushEntityPerformSkillStart, ProtoBufUtils.Serialize(response));
            TraceLog.Write("{0}'s Skill:{1} was started at {2},{3},{4}", m_Room.Players[m_UserId].Name, m_Request.SkillId, m_Request.Transform.PositionX, m_Request.Transform.PositionY, m_Request.Transform.Rotation);
            foreach (var player in m_Room.Players)
            {
                GameSession.Get(player.Key).SendAsync(buffer, 0, buffer.Length);
            }
            m_Response.Result = true;
        }
    }
}
