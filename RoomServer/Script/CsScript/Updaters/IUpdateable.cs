using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.GameServer.RoomServer
{
    public interface IUpdateable
    {
        void Update(float elapseSeconds);
    }
}
