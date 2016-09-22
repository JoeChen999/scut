using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public class RoomSessionUser : SessionUser
    {
        private int m_RoomId;

        public RoomSessionUser(int userId, int roomId)
        {
            UserId = userId;
            m_RoomId = roomId;
        }

        public int RoomId
        {
            get { return m_RoomId; } 
        }

        public void Online(GameSession session)
        {
            var OldSession = GameSession.Get(GetUserId());
            if (OldSession != null)
            {
                OldSession.Close();
            }
            session.Bind(this);
        }
    }
}
