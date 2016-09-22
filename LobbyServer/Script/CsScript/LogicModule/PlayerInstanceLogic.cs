using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Redis;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerInstanceLogic
    {
        private int m_UserId;
        private PlayerInstanceDrop m_InstanceDrop;
        private RandomDropLogic m_RandomDrop;

        public PlayerInstanceLogic()
        {
            m_UserId = 0;
            m_InstanceDrop = null;
            m_RandomDrop = RandomDropLogic.GetInstance();
        }

        public void SetUser(int userId)
        {
            m_UserId = userId;
            m_InstanceDrop = CacheSet.InstanceDropCache.FindKey(userId.ToString(), userId);
        }

        public PlayerInstanceDrop MyInstance
        {
            get { return m_InstanceDrop; }
            set { m_InstanceDrop = value; }
        }

        public CacheDictionary<int, int> GetDropList()
        {
            if (m_InstanceDrop == null)
            {
                return new CacheDictionary<int, int>();
            }
            return m_InstanceDrop.DropList;
        }

        public List<PBDropInfo> EnterInstance(int instanceId)
        {
            if (m_InstanceDrop != null)
            {
                CacheSet.InstanceDropCache.Delete(m_InstanceDrop);
            }
            List<PBDropInfo> dropInfo;
            m_InstanceDrop = new PlayerInstanceDrop();
            m_InstanceDrop.UserId = m_UserId;
            m_InstanceDrop.InstanceId = instanceId;
            DTInstance instanceData = CacheSet.InstanceTable.GetData(instanceId);
            m_InstanceDrop.ChestList.AddRange(instanceData.InInstanceChests);
            m_InstanceDrop.DropList = GenerateDropList(instanceId, false, out dropInfo);
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            if (!pp.CheckPackageSlot(m_InstanceDrop.DropList))
            {
                return null;
            }
            CacheSet.InstanceDropCache.Add(m_InstanceDrop);
            return dropInfo;
            //RedisConnectionPool.Process(c => c.Expire("$Genesis.GameServer.LobbyServer.PlayerInstanceDrop", 3600));
        }

        public CacheDictionary<int,int> OpenInInstanceChest(int index)
        {
            CacheDictionary<int, int> dropDict = new CacheDictionary<int, int>();
            if(m_InstanceDrop.ChestList.Count <= index)
            {
                return dropDict;
            }
            DTDrop drop = CacheSet.DropTable.GetData(m_InstanceDrop.ChestList[index]);
            m_RandomDrop.GetDropDict(drop, dropDict);
            m_InstanceDrop.ChestList.RemoveAt(index);
            return dropDict;
        }

        public void LeaveInstance()
        {
            CacheSet.InstanceDropCache.Delete(m_InstanceDrop);
        }

        public CacheDictionary<int, int> GenerateDropList(int instanceId, bool isCleanOut, out List<PBDropInfo> dropInfoList)
        {
            DTInstance instanceData = CacheSet.InstanceTable.GetData(instanceId);
            CacheDictionary<int, int> dropDict = new CacheDictionary<int, int>();
            int totalCount = 0;
            dropInfoList = new List<PBDropInfo>();
            var dropIds = isCleanOut ? instanceData.CleanOutDropIds : instanceData.DropIds;
            foreach (DropGroup dropGroup in dropIds)
            {
                Random random = new Random();
                int randomValue = random.Next(1, 1000);
                int id = 0;
                foreach (var dropInfo in dropGroup.Drops)
                {
                    if(randomValue - dropInfo.Value <= 0)
                    {
                        id = dropInfo.Key;
                        break;
                    }
                }
                if(id == 0)
                {
                    continue;
                }
                DTDrop drop = CacheSet.DropTable.GetData(id);
                m_RandomDrop.GetDropDict(drop, dropDict);
                int tmpTotalCount = 0;
                foreach (var item in dropDict)
                {
                    tmpTotalCount += item.Value;
                }
                totalCount = tmpTotalCount;
            }
            return dropDict;
        }

        public static int GetInstanceEnergy(int instanceId)
        {
            return GameConsts.InstanceCostEnergy;
        }

        public static DTInstance GetInstanceData(int instanceId)
        {
            return CacheSet.InstanceTable.GetData(instanceId);
        }

        public static int GetFeedBackEnergy(int instanceId)
        {
            return GetInstanceEnergy(instanceId)-1;
        }
    }
}
