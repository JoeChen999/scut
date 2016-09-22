using System;
using UnityEngine;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public class EntitySwitchHeroProcessor : BaseActionProcessor
    {
        private CREntitySwitchHero m_Request;
        public EntitySwitchHeroProcessor(Room roomData)
            : base(roomData)
        {

        }

        public override ActionType ActionType
        {
            get
            {
                return ActionType.EntitySwitchHero;
            }
        }

        public override bool Verify(Message message)
        {
            m_Request = message.Packet as CREntitySwitchHero;
            m_Response.SerialId = m_Request.SerialId;
            int playerId = message.Session.UserId;
            if (DateTime.UtcNow.Ticks - m_Room.StartTime < GameConfigs.GetInt("Room_Battle_Start_Protection_Time", 3) * TimeSpan.TicksPerSecond)
            {
                return false;
            }
            if (m_Request.OldEntityId != m_Room.Players[playerId].InBattleEntity)
            {
                return false;
            }
            var newhero = m_Room.Players[playerId].Heros.Find(t => t.EntityId == m_Request.NewEntityId);
            if (newhero == null)
            {
                return false;
            }
            if(newhero.HP <= 0)
            {
                return false;
            }
            return true;
        }

        public override void Process()
        {
            var newHero = m_Room.Players[m_UserId].Heros.Find(h => h.EntityId == m_Request.NewEntityId);
            var oldHero = m_Room.Players[m_UserId].Heros.Find(h => h.EntityId == m_Request.OldEntityId);
            long now = DateTime.UtcNow.Ticks;
            int newHP = Mathf.Min(Mathf.RoundToInt(newHero.MaxHP * newHero.RecoverHP * ((now - newHero.LastLeaveBattleTime)/TimeSpan.TicksPerSecond)) + newHero.HP, newHero.MaxHP);
            newHero.HP = newHP;
            oldHero.LastLeaveBattleTime = now;
            RCPushEntitySwitchHero pushPacket = new RCPushEntitySwitchHero()
            {
                PlayerId = m_UserId,
                NewEntityId = m_Request.NewEntityId,
                OldEntityId = m_Request.OldEntityId,
                HP = newHP,
            };
            byte[] bufferToOthers = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCPushEntitySwitchHero, ProtoBufUtils.Serialize(pushPacket));
            foreach (var player in m_Room.Players)
            {
                GameSession.Get(player.Key).SendAsync(bufferToOthers, 0, bufferToOthers.Length);
            }
            m_Room.Players[m_UserId].InBattleEntity = m_Request.NewEntityId;
            TraceLog.Write("{0} switch entity from {1} to {2}, now entity {3} is in battle", m_Room.Players[m_UserId].Name, m_Request.OldEntityId, m_Request.NewEntityId, m_Room.Players[m_UserId].InBattleEntity);
            m_Response.Result = true;
        }
    }
}
