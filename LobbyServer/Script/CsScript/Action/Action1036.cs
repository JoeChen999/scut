﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// You may need to modify 'TakeAction' method.
// Don't modify the rest unless you know what you're doing.
//----------------------------------------------------------------------------------------------------

using System;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;

namespace Genesis.GameServer.LobbyServer
{
    public class Action1036 : AuthorizeAction
    {
        private CLGetActivitiesInfo m_RequestPacket;
        private LCGetActivitiesInfo m_ResponsePacket;
        private int m_UserId;

        public Action1036(ActionGetter actionGetter)
            : base((short)1036, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetActivitiesInfo();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetActivitiesInfo>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            var allActivities = CacheSet.ActivityTable.GetAllData();
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            foreach(var activity in allActivities)
            {
                PBActivityInfo activityInfo = new PBActivityInfo();
                activityInfo.Id = activity.Id;
                activityInfo.Status = LobbyServerUtils.GetActivityStatus(activity, p.MyPlayer.Level);
                if(activityInfo.Status == (int)ActivityStatusType.Hidden)
                {
                    continue;
                }
                switch ((ActivityType)activity.Id)
                {
                    case ActivityType.TurnOverChess:
                        PlayerChessLogic pc = new PlayerChessLogic();
                        pc.SetUser(m_UserId);
                        activityInfo.Progress = pc.MyChess.Count == 0 ? 0 : 1;
                        activityInfo.CountLimit = 1;
                        break;
                    case ActivityType.OfflineArena:
                        PlayerArenaLogic pa = new PlayerArenaLogic();
                        pa.SetUser(m_UserId);
                        activityInfo.Progress = pa.MyArena.ChallengeCount;
                        activityInfo.CountLimit = GameConsts.Arena.DailyChallengeCount;
                        break;
                    case ActivityType.GearFoundry:
                        PlayerFoundryLogic pf = new PlayerFoundryLogic();
                        pf.SetUser(m_UserId);
                        activityInfo.Progress = pf.GetProgress();
                        activityInfo.CountLimit = 1;
                        break;
                    case ActivityType.CosmosCrack:
                        PlayerCosmosCrackLogic pcc = new PlayerCosmosCrackLogic();
                        pcc.SetUser(m_UserId);
                        var cosmosInfo = pcc.GetCosmosCrackInstanceInfo();
                        activityInfo.Progress = cosmosInfo == null ? 0 : cosmosInfo.PassedRoundCount;
                        activityInfo.CountLimit = cosmosInfo == null ? 0 : GameConfigs.GetInt("Cosmos_Crack_Round_Limit", 10);
                        break;
                    default:
                        ErrorCode = (int)ErrorType.CannotOpenChance;
                        ErrorInfo = "invalid activity type";
                        return false;
                }
                m_ResponsePacket.ActivityInfo.Add(activityInfo);
            }
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}