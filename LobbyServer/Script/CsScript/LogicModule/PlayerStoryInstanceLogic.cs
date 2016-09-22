using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;


namespace Genesis.GameServer.LobbyServer
{
    public class PlayerStoryInstanceLogic
    {
        private PlayerStoryInstance m_Instance;
        public PlayerStoryInstanceLogic()
        {

        }

        public void SetUser(int userId)
        {
            m_Instance = CacheSet.PlayerStoryInstanceCache.FindKey(userId.ToString(), userId);
            if (m_Instance == null)
            {
                m_Instance = new PlayerStoryInstance();
                m_Instance.UserId = userId;
                m_Instance.Count = GameConfigs.GetInt("Story_Instance_Daily_Count", 5);
                CacheSet.PlayerStoryInstanceCache.Add(m_Instance);
            }
        }

        public int GetCount()
        {
            return m_Instance.Count;
        }

        public CacheDictionary<int, int> GetDropItems(int dropId, int starLevel)
        {
            if (m_Instance.Count <= 0)
            {
                return null;
            }
            RandomDropLogic random = RandomDropLogic.GetInstance();
            CacheDictionary<int, int> dropItems = new CacheDictionary<int, int>();
            var dropData = CacheSet.DropTable.GetData(dropId);
            for (int i = 0; i < starLevel; i++)
            {
                random.GetDropDict(dropData, dropItems);
            }
            m_Instance.Count -= 1;
            return dropItems;
        }
    }
}
