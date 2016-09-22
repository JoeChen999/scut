using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.LobbyServer
{
    public class RLGetGameConfigsHandler : RemoteStruct
    {
        private LRGetGameConfigs m_ResponsePacket = new LRGetGameConfigs();
        public RLGetGameConfigsHandler(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {

        }

        protected override bool Check()
        {
            return true;
        }

        protected override void TakeRemote()
        {
            GameSession session = paramGetter.GetSession();
            var configs = GameConfigs.GetConfigs(2);
            foreach(var config in configs)
            {
                m_ResponsePacket.Keys.Add(config.Key);
                m_ResponsePacket.Values.Add(config.Value);
            }
        }

        protected override void BuildPacket()
        {
            response.PushIntoStack(m_ResponsePacket, false);
        }
    }
}
