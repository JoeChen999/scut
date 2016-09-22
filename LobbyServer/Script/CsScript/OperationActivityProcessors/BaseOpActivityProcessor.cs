using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.GameServer.LobbyServer
{
    public abstract class BaseOpActivityProcessor
    {
        public abstract void SetUser(int userId);

        public abstract List<PBKeyValuePair> Process(List<PBKeyValuePair> Params, out PBReceivedItems ri, out PBPlayerInfo p, out List<PBLobbyHeroInfo> h);
    }
}
