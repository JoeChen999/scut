using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.LobbyServer
{
    public class NearbyPlayerLogic
    {
        private int m_UserId;
        private PlayerNearbyPosition m_NearbyPosition;
        private const float DefaultXCoordinate = 2.4f;
        private const float DefaultYCoordinate = -4.6f;

        public NearbyPlayerLogic()
        {
        }

        public PlayerNearbyPosition NearbyPlayers
        {
            get { return m_NearbyPosition; }
        }

        public void SetUser(int userId)
        {
            m_UserId = userId;
            m_NearbyPosition = CacheSet.PlayerNearbyPositionCache.FindKey(userId.ToString(), userId);
            if (m_NearbyPosition == null)
            {
                m_NearbyPosition = new PlayerNearbyPosition();
                m_NearbyPosition.UserId = userId;
                m_NearbyPosition.MyPositionX = DefaultXCoordinate;
                m_NearbyPosition.MyPositionY = DefaultYCoordinate;
                CacheSet.PlayerNearbyPositionCache.Add(m_NearbyPosition);
            }
        }

        public void AddVisitor(int nearbyPlayer)
        {
            m_NearbyPosition.Visitors.Add(nearbyPlayer);
        }

        public void Move(float XCoord, float YCoord)
        {
            m_NearbyPosition.MyPositionX = XCoord;
            m_NearbyPosition.MyPositionY = YCoord;
            List<int> disconnectedPlayers = new List<int>();
            foreach(int playerId in m_NearbyPosition.Visitors)
            {
                var target = GameSession.Get(playerId);
                if (target != null && target.IsSocket)
                {
                    LCPlayerMove package = new LCPlayerMove();
                    package.LobbyPositionX = XCoord;
                    package.LobbyPositionY = YCoord;
                    package.PlayerId = m_UserId;
                    byte[] buffer = CustomActionDispatcher.GeneratePackageStream(package.PacketActionId, ProtoBufUtils.Serialize(package));
                    target.SendAsync(buffer, 0, buffer.Length);
                }
                else
                {
                    disconnectedPlayers.Add(playerId);
                }
            }            
            m_NearbyPosition.ModifyLocked(() =>
            {
                foreach (int playerId in disconnectedPlayers)
                {
                    m_NearbyPosition.Visitors.Remove(playerId);
                }
            });
        }
    }
}
