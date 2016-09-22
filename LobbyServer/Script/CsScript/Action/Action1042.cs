﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// You may need to modify 'TakeAction' method.
// Don't modify the rest unless you know what you're doing.
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;

namespace Genesis.GameServer.LobbyServer
{
    public class Action1042 : AuthorizeAction
    {
        private CLClaimAchievementReward m_RequestPacket;
        private LCClaimAchievementReward m_ResponsePacket;
        private int m_UserId;

        public Action1042(ActionGetter actionGetter)
            : base((short)1042, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCClaimAchievementReward();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLClaimAchievementReward>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            var pa = PlayerAchievementLogic.GetInstance(m_UserId);
            TrackingAchievement nextAchievement;
            PackageFullReason reason;
            var RewardItems = pa.CompleteAchievement(m_RequestPacket.AchievementId, out nextAchievement);
            if (RewardItems == null)
            {
                ErrorCode = (int)ErrorType.RequireNotMet;
                ErrorInfo = "You can not claim this achievement";
                return false;
            }
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            PBReceivedItems receivedItems;
            pp.GetItems(RewardItems, ReceiveItemMethodType.None, out receivedItems);
            m_ResponsePacket.ReceivedItems = receivedItems;
            if (nextAchievement != null)
            {
                m_ResponsePacket.NextTrackingAchievement = new PBTrackingAchievement()
                {
                    AchievementId = nextAchievement.Id,
                    ProgressCount = nextAchievement.Progress,
                };
            }
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);

            m_ResponsePacket.PlayerInfo = new PBPlayerInfo()
            {
                Id = p.MyPlayer.Id,
                Exp = p.MyPlayer.Exp,
                Level = p.MyPlayer.Level,
                Coin = p.MyPlayer.Coin,
                Money = p.MyPlayer.Money,
                Spirit = p.MyPlayer.Spirit
            };
            m_ResponsePacket.AchievementId = m_RequestPacket.AchievementId;
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}