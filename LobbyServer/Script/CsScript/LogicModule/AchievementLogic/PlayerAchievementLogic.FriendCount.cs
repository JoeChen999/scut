using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerAchievementLogic
    {
        private void InitFriendCountAchievementProgress(TrackingAchievement achievement)
        {
            PlayerFriendsLogic pf = new PlayerFriendsLogic();
            pf.SetUser(m_UserId);
            achievement.Progress = pf.GetFriends().Count;
        }

        private void UpdateFriendCountAchievement(object[] param)
        {
            var achievement = m_AchievementInfo.TrackingAchievements[(int)AchievementType.FriendCount];
            achievement.Progress += 1;
            PushProgressModified(new List<TrackingAchievement>() { achievement });
        }
    }
}
