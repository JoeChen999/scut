using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Context;

namespace Genesis.GameServer.LobbyServer
{
    public class User:IUser
    {
        private long m_UserId;
        public string Token { get; set; }
 
        /// <summary>
        /// is online
        /// </summary>
        public bool IsOnlining { get { return true; } }

        public User(long userId)
        {
            m_UserId = userId;
        }

        public bool IsReplaced
        {
            get;
            set;
        }

        public int GetUserId()
        {
            return (int)m_UserId;
        }

        public string GetPassportId(){
            string retval = m_UserId.ToString();
            return retval;
        }

        public void RefleshOnlineDate(){
            m_UserId = 0;
        }

        public void SetExpired(DateTime time)
        {

        }
    }
}
