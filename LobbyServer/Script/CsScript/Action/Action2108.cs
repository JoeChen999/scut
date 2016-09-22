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
    public class Action2108 : AuthorizeAction
    {
        private CLPerformFoundry m_RequestPacket;
        private LCPerformFoundry m_ResponsePacket;
        private int m_UserId;

        public Action2108(ActionGetter actionGetter)
            : base((short)2108, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCPerformFoundry();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLPerformFoundry>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerFoundryLogic pf = new PlayerFoundryLogic();
            pf.SetUser(m_UserId);
            if (!pf.Foundry())
            {
                ErrorCode = (int)ErrorType.RequireNotMet;
                ErrorInfo = "You can not foundry now";
                return false;
            }
            m_ResponsePacket.NextFoundryTimeInTicks = pf.MyFoundry.NextFoundryTime;
            m_ResponsePacket.PerformerPlayerId = m_UserId;
            m_ResponsePacket.RewardFlags.AddRange(pf.MyFoundry.CanReceiveRewards);
            var room = CacheSet.FoundryRoomCache.FindKey(pf.MyFoundry.CurrentRoomId);
            m_ResponsePacket.Progress = new PBGearFoundryProgressInfo()
            {
                CurrentLevel = room.Level,
                CurrentProgress = room.Progress
            };
            pf.PushRoomProgressChangedNotify();
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
