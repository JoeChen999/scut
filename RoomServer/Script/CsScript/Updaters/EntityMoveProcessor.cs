using System;
using Genesis.GameServer.CommonLibrary;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Common.Log;

namespace Genesis.GameServer.RoomServer
{
    public class EntityMoveProcessor : BaseActionProcessor
    {
        private long m_LastPushTime = 0;
        private const float m_PushInterval = 0.0f * TimeSpan.TicksPerSecond;
        private CREntityMove m_Request;

        public EntityMoveProcessor(Room room)
            : base(room)
        {
        }

        public override ActionType ActionType
        {
            get { return ActionType.CREntityMove; }
        }

        public override bool Verify(Message message)
        {
            m_Request = message.Packet as CREntityMove;
            m_Response.SerialId = m_Request.SerialId;
            if (DateTime.UtcNow.Ticks - m_Room.StartTime < GameConfigs.GetInt("Room_Battle_Start_Protection_Time", 3) * TimeSpan.TicksPerSecond)
            {
                return false;
            }
            if (!m_Request.IsKey && DateTime.UtcNow.Ticks - m_LastPushTime < m_PushInterval)
            {
                TraceLog.WriteError("too frequency");
                return false;
            }
            if (m_Room.Players[message.Session.UserId].InBattleEntity != m_Request.EntityId)
            {
                TraceLog.WriteError("wrong entityId, true entity is:{0}, receive:{1}", m_Room.Players[message.Session.UserId].InBattleEntity, m_Request.EntityId);
                return false;
            }
            TraceLog.WriteInfo("verify success");
            return true;
        }

        public override void Process()
        {
            RCPushEntityMove response = new RCPushEntityMove()
            {
                EntityId = m_Request.EntityId,
                Transform = m_Request.Transform,
                PlayerId = m_UserId
            };
            var movingPlayer = m_Room.Players[m_UserId];
            movingPlayer.PositionX = m_Request.Transform.PositionX;
            movingPlayer.PositionY = m_Request.Transform.PositionY;
            movingPlayer.Rotation = m_Request.Transform.Rotation;
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCPushEntityMove, ProtoBufUtils.Serialize(response));
            foreach (var player in m_Room.Players)
            {
                GameSession.Get(player.Key).SendAsync(buffer, 0, buffer.Length);
            }
            m_LastPushTime = DateTime.UtcNow.Ticks;
            m_Response.Result = true;
        }

        public override void PushResult()
        {

        }
    }
}
