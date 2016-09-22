using System;
using System.Text;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public class EntityPerformSkillImpactProcessor : BaseActionProcessor
    {
        private CREntityImpact m_Request;
        private RoomHero m_OriginHero;
        private RoomHero m_TargetHero;
        private int m_TargetPlayerId = 0;

        public EntityPerformSkillImpactProcessor(Room roomData)
            : base(roomData)
        {
            m_Room = roomData;
        }

        public override ActionType ActionType
        {
            get
            {
                return ActionType.CREntityImpact;
            }
        }

        public override bool Verify(Message message)
        {
            m_OriginHero = null;
            m_TargetHero = null;
            m_Request = message.Packet as CREntityImpact;
            m_Response.SerialId = m_Request.SerialId;
            if (DateTime.UtcNow.Ticks - m_Room.StartTime < GameConfigs.GetInt("Room_Battle_Start_Protection_Time", 3) * TimeSpan.TicksPerSecond)
            {
                return false;
            }
            m_OriginHero = m_Room.Players[m_UserId].Heros.Find(t => t.EntityId == m_Request.OriginOwnerEntityId);
            foreach(var player in m_Room.Players)
            {
                if(player.Value.InBattleEntity == m_Request.TargetEntityId)
                {
                    m_TargetHero = m_Room.Players[player.Key].Heros.Find(t => t.EntityId == m_Request.TargetEntityId);
                }
            }
            if (m_OriginHero == null || m_TargetHero == null)
            {
                return false;
            }
            if(m_OriginHero.HP <= 0 || m_TargetHero.HP <= 0)
            {
                return false;
            }
            return true;
        }

        public override void Process()
        {
            StringBuilder sb = new StringBuilder("Perform Impact:");
            sb.Append(m_Room.Players[m_UserId].Name);
            sb.Append(" impacted ");
            foreach (var player in m_Room.Players)
            {
                if (player.Value.InBattleEntity == m_Request.TargetEntityId)
                {
                    sb.Append(player.Value.Name);
                    m_TargetPlayerId = player.Value.PlayerId;
                }
            }
            sb.Append(":\n");
            foreach (var impact in m_Request.HPDamageImpacts)
            {
                m_TargetHero.HP -= impact.DamageHP;
                if (m_OriginHero != null)
                {
                    m_OriginHero.HP += impact.RecoverHP + impact.SkillRecoverHP - impact.CounterHP;
                    sb.AppendFormat("origin({0}) HP:{1}, target({2}) HP:{3} <=HP Damage Impact=> Damage HP:{4}, Recover HP:{5}, SkillRecoverHP:{6}, CounterHP{7}\n", m_OriginHero.EntityId, m_OriginHero.HP, m_TargetHero.EntityId, m_TargetHero.HP, impact.DamageHP, impact.RecoverHP, impact.SkillRecoverHP, impact.CounterHP);
                }
            }

            foreach (var impact in m_Request.HPRecoverImpacts)
            {
                if (m_OriginHero != null)
                {
                    sb.AppendFormat("origin HP:{0}, target HP:{1} <=HP Recover Impact=> Recover HP:{2}\n", m_OriginHero.HP, m_TargetHero.HP, impact.RecoverHP);
                }
                m_TargetHero.HP += impact.RecoverHP;
            }

            foreach (var impact in m_Request.SteadyDamageImpacts)
            {
                if (m_OriginHero != null)
                {
                    sb.AppendFormat("origin HP:{0}, target HP:{1} <=Steady Damage Impact=> Damage Steady:{2}\n", m_OriginHero.HP, m_TargetHero.HP, impact.DamageSteady);
                }
            }
            TraceLog.Write(sb.ToString());
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCPushEntityImpact, ProtoBufUtils.Serialize(ToRCPushEntityImpact(m_Request)));
            foreach (var player in m_Room.Players)
            {
                GameSession.Get(player.Key).SendAsync(buffer, 0, buffer.Length);
            }
            if (m_TargetHero.HP <= 0)
            {
                RCPushEntityDie packet = new RCPushEntityDie();
                packet.DeadEntityId = m_TargetHero.EntityId;
                packet.PlayerId = m_TargetPlayerId;
                byte[] bytes = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCPushEntityHeroDie, ProtoBufUtils.Serialize(packet));
                foreach (var player in m_Room.Players)
                {
                    GameSession.Get(player.Key).SendAsync(bytes, 0, bytes.Length);
                }
            }
            m_Response.Result = true;
        }

        private RCPushEntityImpact ToRCPushEntityImpact(CREntityImpact request)
        {
            RCPushEntityImpact response = new RCPushEntityImpact()
            {
                ImpactSourceType = request.ImpactSourceType,
                OriginTransform = request.OriginTransform,
                TargetTransform = request.TargetTransform,
                OriginPlayerId = m_UserId,
                TargetPlayerId = m_TargetPlayerId,
            };
            if (request.HasAIId) { response.AIId = request.AIId; }
            if (request.HasCurrentTime) { response.CurrentTime = request.CurrentTime; }
            if (request.HasOriginEntityId) { response.OriginEntityId = request.OriginEntityId; }
            if (request.HasSkillId) { response.SkillId = request.SkillId; }
            if (request.HasBuffId) { response.BuffId = request.BuffId; }
            if (request.HasTargetEntityId) { response.TargetEntityId = request.TargetEntityId; }
            response.BlownAwayImpacts.AddRange(request.BlownAwayImpacts);
            response.FloatImpacts.AddRange(request.FloatImpacts);
            response.HardHitImpacts.AddRange(request.HardHitImpacts);
            response.HPDamageImpacts.AddRange(request.HPDamageImpacts);
            response.HPRecoverImpacts.AddRange(request.HPRecoverImpacts);
            response.SoundAndEffectImpacts.AddRange(request.SoundAndEffectImpacts);
            response.SteadyDamageImpacts.AddRange(request.SteadyDamageImpacts);
            response.StiffnessImpacts.AddRange(request.StiffnessImpacts);
            response.StunImpacts.AddRange(request.StunImpacts);
            response.FreezeImpacts.AddRange(request.FreezeImpacts);
            response.BuffIdsAddedToTarget.AddRange(request.BuffIdsAddedToTarget);
            return response;
        }
    }
}
