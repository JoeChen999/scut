using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public abstract class BaseActionProcessor : IUpdateable
    {
        protected Room m_Room;
        protected RCRequestResult m_Response;
        protected int m_UserId;
        protected GameSession m_Session;

        public BaseActionProcessor(Room roomData)
        {
            m_Room = roomData;
            m_Response = new RCRequestResult();
        }

        public abstract ActionType ActionType
        {
            get;
        }

        public virtual void Update(float elapseSeconds)
        {

        }

        public virtual void GetData(Message message)
        {
            m_Session = message.Session;
            m_UserId = message.Session.UserId;
            m_Response.Result = false;
        }

        public virtual bool Verify(Message message)
        {
            return true;
        }

        public abstract void Process();

        public virtual void PushResult()
        {
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream((int)ActionType.RCRequestResult, ProtoBufUtils.Serialize(m_Response));
            GameSession.Get(m_UserId).SendAsync(buffer, 0, buffer.Length);
        }
    }
}
