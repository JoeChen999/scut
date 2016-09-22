using System;
using Genesis.GameServer.CommonLibrary;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Common.Log;

namespace Genesis.GameServer.RoomServer
{
    public class EntityAddBuffProcessor : BaseActionProcessor
    {
        private CREntityAddBuff m_Request = null;

        public EntityAddBuffProcessor(Room room)
            : base(room)
        {
        }

        public override ActionType ActionType
        {
            get { return ActionType.CREntityAddBuff; }
        }

        public override bool Verify(Message message)
        {
            m_Request = message.Packet as CREntityAddBuff;
            return true;
        }

        public override void Process()
        {
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCPushEntityAddBuff, ProtoBufUtils.Serialize(ToRCPushEntityAddBuff(m_Request)));
            foreach (var player in m_Room.Players)
            {
                GameSession.Get(player.Key).SendAsync(buffer, 0, buffer.Length);
            }
            m_Response.SerialId = m_Request.SerialId;
            m_Response.Result = true;
        }

        private RCPushEntityAddBuff ToRCPushEntityAddBuff(CREntityAddBuff request)
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
            RCPushEntityAddBuff response = new RCPushEntityAddBuff()
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
