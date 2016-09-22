using System;
using Genesis.GameServer.CommonLibrary;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Common.Log;

namespace Genesis.GameServer.RoomServer
{
    public class EntityRemoveBuffProcessor : BaseActionProcessor
    {
        private CREntityRemoveBuff m_Request;
        private long m_LastPushTime = 0;

        public EntityRemoveBuffProcessor(Room room)
            : base(room)
        {
        }

        public override ActionType ActionType
        {
            get { return ActionType.CREntityRemoveBuff; }
        }

        public override bool Verify(Message message)
        {
            m_Request = message.Packet as CREntityRemoveBuff;
            m_Response.SerialId = m_Request.SerialId;
            return true;
        }

        public override void Process()
        {
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCPushEntityRemoveBuff, ProtoBufUtils.Serialize(ToRCPushEntityRemoveBuff(m_Request)));
            foreach (var player in m_Room.Players)
            {
                GameSession.Get(player.Key).SendAsync(buffer, 0, buffer.Length);
            }
            m_Response.Result = true;
        }

        private RCPushEntityRemoveBuff ToRCPushEntityRemoveBuff(CREntityRemoveBuff request)
        {
            int originPlayer = m_UserId;
            int targetPlayer = 0;
            foreach (var player in m_Room.Players)
            {
                if (player.Value.InBattleEntity == m_Request.TargetEntityId)
                {
                    targetPlayer = player.Key;
                }
            }
            RCPushEntityRemoveBuff response = new RCPushEntityRemoveBuff()
            {
                OriginEntityId = request.OriginEntityId,
                TargetEntityId = request.TargetEntityId,
                OriginTransform = request.OriginTransform,
                TargetTransform = request.TargetTransform,
                OriginPlayerId = originPlayer,
                TargetPlayerId = targetPlayer,
            };
            response.BuffTypeIds.AddRange(request.BuffTypeIds);
            return response;
        }
    }
}
