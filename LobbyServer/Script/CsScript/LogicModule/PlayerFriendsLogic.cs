using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerFriendsLogic
    {
        private int m_UserId;
        private PlayerFriends m_Friends;
        public PlayerFriendsLogic()
        {

        }

        public void SetUser(int userId)
        {
            m_UserId = userId;
            m_Friends = CacheSet.PlayerFriendsCache.FindKey(userId.ToString(), userId);
            if (m_Friends == null)
            {
                PlayerFriends pf = new PlayerFriends();
                pf.UserId = userId;
                pf.SendCount = 0;
                pf.ReceiveCount = 0;
                pf.LastRefreshTime = DateTime.UtcNow.Ticks;
                CacheSet.PlayerFriendsCache.Add(pf);
                m_Friends = pf;
            }
            else
            {
                if(GameUtils.NeedRefresh(m_Friends.LastRefreshTime, GameConsts.Social.RefreshTime))
                {
                    ResetSendEnergy();
                }
            }
        }

        public PlayerFriends MyFriends
        {
            get { return m_Friends; }
            set { m_Friends = value; }
        }
        public CacheDictionary<int, Friend> GetFriends()
        {
            return m_Friends.Friends;
        }

        public CacheList<int> GetInvitations()
        {
            return m_Friends.Invitations;
        }

        public bool SendEnergy(int friendId)
        {
            int userId = m_UserId;
            if (m_Friends.SendCount >= GameConsts.Social.MaxSendCount)
            {
                return false;
            }
            if (!m_Friends.Friends.ContainsKey(friendId) || m_Friends.Friends[friendId].CanSendEnergy == false )
            {
                return false;
            }
            SetUser(friendId);
            if (!m_Friends.Friends.ContainsKey(userId))
            {
                return false;
            }
            m_Friends.Friends[userId].CanReceiveEnergy = true;
            SetUser(userId);
            m_Friends.Friends[friendId].CanSendEnergy = false;
            m_Friends.SendCount += 1;
            PlayerDailyQuestLogic.GetInstance(m_UserId).UpdateDailyQuest(DailyQuestType.GiftEnergyToFriend, 1);
            return true;
        }

        public bool ReceiveEnergy(int friendId)
        {
            if (m_Friends.ReceiveCount >= GameConsts.Social.MaxReceiveCount)
            {
                return false;
            }
            if (!m_Friends.Friends.ContainsKey(friendId) || m_Friends.Friends[friendId].CanReceiveEnergy == false)
            {
                return false;
            }
            m_Friends.Friends[friendId].CanReceiveEnergy = false;
            m_Friends.ReceiveCount += 1;
            return true;
        }

        public void ResetSendEnergy()
        {
            foreach (var f in m_Friends.Friends)
            {
                f.Value.CanSendEnergy = true;
            }
            m_Friends.SendCount = 0;
            m_Friends.ReceiveCount = 0;
        }

        public bool Invited(int inviter)
        {
            if (m_Friends.Invitations.Contains(inviter) || m_Friends.Friends.ContainsKey(inviter) || m_Friends.Friends.Count >= GameConfigs.GetInt("Max_Friend_Count", 50))
            {
                return false;
            }
            if (m_Friends.Invitations.Count >= GameConfigs.GetInt("Max_Friend_Invitation_Count", 20))
            {
                m_Friends.Invitations.RemoveAt(0);
            }
            m_Friends.Invitations.Add(inviter);
            return true;
        }

        public bool RemoveInvitation(int userId)
        {
            if (m_Friends.Invitations.Contains(userId))
            {
                m_Friends.Invitations.Remove(userId);
                return true;
            }
            return false;
        }

        public List<Player> GetOnlinePlayers()
        {
            var onlinePlayers = GameSession.GetOnlineAll();
            PlayerLogic p = new PlayerLogic();
            List<Player> result = new List<Player>();
            int i = 0;
            foreach (var player in onlinePlayers)
            {
                if (player.UserId == m_UserId)
                {
                    continue;
                }
                
                p.SetUser(player.UserId);
                if(p.MyPlayer != null)
                {
                    result.Add(p.MyPlayer);
                }
                if(++i >= GameConsts.Social.MaxOnlineCount)
                {
                    break;
                }
            }
            return result;
        }

        public List<Player> GetRecommendedPlayers()
        {
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            var allPlayers = CacheSet.PlayerCache.FindAll(t => Math.Abs(t.Level - p.MyPlayer.Level) <= 5 && !string.IsNullOrEmpty(t.Name) && t.IsRobot == false);
            List<Player> result = new List<Player>();
            List<Player> offLinePlayer = new List<Player>();
            foreach (var player in allPlayers)
            {
                if (m_Friends.Friends.ContainsKey(player.Id) || player.Id == m_UserId)
                {
                    continue;
                }
                if (player.Status == PlayerStatus.Online)
                {
                    result.Add(player);
                }
                else
                {
                    offLinePlayer.Add(player);
                }
            }
            if (result.Count < GameConsts.Social.RecommendationListCount)
            {
                int onlineCount = result.Count;
                for (int i = 0; i < GameConsts.Social.RecommendationListCount - onlineCount; i++)
                {
                    if ( i + 1 > offLinePlayer.Count)
                    {
                        break;
                    }
                    result.Add(offLinePlayer[i]);
                }
            }
            else
            {
                result = result.GetRange(0, GameConsts.Social.RecommendationListCount);
            }
            return result;
        }

        private bool CheckFriend(int userId, int friendId)
        {
            SetUser(userId);
            if (m_Friends.Friends.ContainsKey(friendId) || m_Friends.Friends.Count >= GameConsts.Social.MaxFriendCount)
            {
                return false;
            }
            SetUser(friendId);
            if (m_Friends.Friends.ContainsKey(friendId) || m_Friends.Friends.Count >= GameConsts.Social.MaxFriendCount)
            {
                SetUser(userId);
                return false;
            }
            SetUser(userId);
            return true;
        }

        public bool AddFriend(int userId, int friendId)
        {
            if (!CheckFriend(userId, friendId))
            {
                return false;
            }
            if (m_Friends.RemovedFriends.ContainsKey(friendId))
            {
                m_Friends.Friends.Add(friendId, (Friend)m_Friends.RemovedFriends[friendId].Clone());
                m_Friends.RemovedFriends.Remove(friendId);
            }
            else
            {
                Friend f1 = new Friend();
                f1.CanReceiveEnergy = false;
                f1.CanSendEnergy = true;
                m_Friends.Friends.Add(friendId, f1);
            }
            SetUser(friendId);
            PlayerAchievementLogic.GetInstance(friendId).UpdateAchievement(AchievementType.FriendCount);
            if (m_Friends.RemovedFriends.ContainsKey(userId))
            {
                m_Friends.Friends.Add(userId, (Friend)m_Friends.RemovedFriends[userId].Clone());
                m_Friends.RemovedFriends.Remove(userId);
            }
            else
            {
                Friend f2 = new Friend();
                f2.CanReceiveEnergy = false;
                f2.CanSendEnergy = true;
                m_Friends.Friends.Add(userId, f2);
            }
            SetUser(userId);
            PlayerAchievementLogic.GetInstance(userId).UpdateAchievement(AchievementType.FriendCount);
            return true;
        }

        public bool DeleteFriend(int userId, int friendId)
        {
            SetUser(userId);
            if (!m_Friends.Friends.ContainsKey(friendId))
            {
                return false;
            }
            m_Friends.RemovedFriends.Add(friendId, (Friend)m_Friends.Friends[friendId].Clone());
            m_Friends.Friends.Remove(friendId);
            SetUser(friendId);
            if (!m_Friends.Friends.ContainsKey(userId))
            {
                return false;
            }
            m_Friends.RemovedFriends.Add(userId, (Friend)m_Friends.Friends[userId].Clone());
            m_Friends.Friends.Remove(userId);
            SetUser(userId);
            return true;
        }
    }
}
