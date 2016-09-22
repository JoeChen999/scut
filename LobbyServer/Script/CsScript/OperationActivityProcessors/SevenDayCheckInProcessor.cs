using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public class SevenDayCheckInProcessor : BaseOpActivityProcessor
    {
        private PlayerSevenDayCheckIn m_SevenDayCheckIn;
        public SevenDayCheckInProcessor()
        {

        }

        public override void SetUser(int userId)
        {
            m_SevenDayCheckIn = CacheSet.PlayerSevenDayCheckInCache.FindKey(userId.ToString());
            if(m_SevenDayCheckIn == null)
            {
                m_SevenDayCheckIn = new PlayerSevenDayCheckIn()
                {
                    UserId = userId,
                    ClaimedCount = 0,
                    LastClaimedTime = 0
                };
                CacheSet.PlayerSevenDayCheckInCache.Add(m_SevenDayCheckIn);
            }
        }

        public override List<PBKeyValuePair> Process(List<PBKeyValuePair> Params, out PBReceivedItems receivedItems, out PBPlayerInfo playerInfo, out List<PBLobbyHeroInfo> heroInfos)
        {
            receivedItems = null;
            playerInfo = null;
            heroInfos = null;
            if (m_SevenDayCheckIn == null)
            {
                return null;
            }
            string op = Params.Find(p => p.Key == "Op").Value;
            List<PBKeyValuePair> retval = null;
            switch (op)
            {
                case "GetData":
                    retval = ProcessGetData(Params, out receivedItems, out playerInfo, out heroInfos);
                    break;
                case "ClaimReward":
                    retval = ProcessClaimReward(Params, out receivedItems, out playerInfo, out heroInfos);
                    break;
                default:
                    return null;
            }
            return retval;
        }

        private List<PBKeyValuePair> ProcessClaimReward(List<PBKeyValuePair> Params, out PBReceivedItems receivedItems, out PBPlayerInfo playerInfo, out List<PBLobbyHeroInfo> heroInfos)
        {
            receivedItems = null;
            playerInfo = null;
            heroInfos = null;
            int dayCnt = CacheSet.OperationActivitySevenDayTable.Count;
            if (DateTime.UtcNow.Date == new DateTime(m_SevenDayCheckIn.LastClaimedTime).Date || m_SevenDayCheckIn.ClaimedCount >= dayCnt)
            {
                return null;
            }
            List<PBKeyValuePair> retval = new List<PBKeyValuePair>();
            retval.AddRange(Params);
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_SevenDayCheckIn.UserId);
            var reward = CacheSet.OperationActivitySevenDayTable.GetData(m_SevenDayCheckIn.ClaimedCount + 1);
            pp.GetItems(new Dictionary<int, int>() { { reward.RewardId, reward.RewardCount } }, ReceiveItemMethodType.None, out receivedItems);
            m_SevenDayCheckIn.ClaimedCount += 1;
            m_SevenDayCheckIn.LastClaimedTime = DateTime.UtcNow.Ticks;
            retval.Add(new PBKeyValuePair() { Key = "ClaimedCnt", Value= m_SevenDayCheckIn.ClaimedCount.ToString() });
            retval.Add(new PBKeyValuePair() { Key = "HasClaimed", Value = "true" });
            return retval;
        }

        private List<PBKeyValuePair> ProcessGetData(List<PBKeyValuePair> Params, out PBReceivedItems receivedItems, out PBPlayerInfo playerInfo, out List<PBLobbyHeroInfo> heroInfos)
        {
            receivedItems = null;
            playerInfo = null;
            heroInfos = null;
            List<PBKeyValuePair> retval = new List<PBKeyValuePair>();
            retval.AddRange(Params);
            int dayCnt = CacheSet.OperationActivitySevenDayTable.Count;
            retval.Add(new PBKeyValuePair() { Key = "DayCnt", Value = dayCnt.ToString() });
            retval.Add(new PBKeyValuePair() { Key = "ClaimedCnt", Value = m_SevenDayCheckIn.ClaimedCount.ToString() });
            string hasClaimed = (new DateTime(m_SevenDayCheckIn.LastClaimedTime).Date == DateTime.UtcNow.Date).ToString();
            retval.Add(new PBKeyValuePair() { Key = "HasClaimed", Value = hasClaimed });
            for(int i = 0; i < dayCnt; i++)
            {
                var data = CacheSet.OperationActivitySevenDayTable.GetData(i+1);
                retval.Add(new PBKeyValuePair() { Key = "Reward." + i.ToString() + ".ItemId", Value = data.RewardId.ToString() });
                retval.Add(new PBKeyValuePair() { Key = "Reward." + i.ToString() + ".ItemCnt", Value = data.RewardCount.ToString() });
            }
            return retval;
        }
    }
}
