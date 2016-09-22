using System;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.LobbyServer
{
    public class WLMatchSuccessHandler : RemoteStruct
    {
        private WLMatchSuccess m_RequestPacket;
        public WLMatchSuccessHandler(ActionGetter paramGetter, MessageStructure response)
            : base(paramGetter, response)
        {

        }

        protected override bool Check()
        {
            m_RequestPacket = ProtoBufUtils.Deserialize<WLMatchSuccess>(Convert.FromBase64String(paramGetter.RequestPackage.Params["Data"]));
            return true;
        }

        protected override void TakeRemote()
        {
            TraceLog.ReleaseWrite("Player:{0} match success", m_RequestPacket.PlayerId.ToString());
            LCPushPvpMatchSuccess package = new LCPushPvpMatchSuccess()
            {
                RoomId = m_RequestPacket.RoomId,
                InstanceId = m_RequestPacket.InstanceId,
                Token = m_RequestPacket.Token,
                RoomServerHost = m_RequestPacket.RoomServerHost,
                RoomServerPort = m_RequestPacket.RoomServerPort,
                EnemyInfo = m_RequestPacket.EnemyInfo,
            };
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream(package.PacketActionId, ProtoBufUtils.Serialize(package));
            try
            {
                GameSession.Get(m_RequestPacket.PlayerId).SendAsync(buffer, 0, buffer.Length);
            }
            catch
            {
                return;
            }
        }

        protected override void BuildPacket()
        {
        }
    }
}
