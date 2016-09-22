using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.RoomServer
{
    public class Message
    {
        public ActionType Type
        {
            get;
            set;
        }

        public GameSession Session
        {
            get;
            set;
        }

        public PacketBase Packet
        {
            get;
            set;
        }
    }
}
