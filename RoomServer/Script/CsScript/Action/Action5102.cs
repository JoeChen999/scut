﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// You may need to modify 'TakeAction' method.
// Don't modify the rest unless you know what you're doing.
//----------------------------------------------------------------------------------------------------

using System;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;

namespace Genesis.GameServer.RoomServer
{
    public class Action5102 : AuthorizeAction
    {
        private CREntityPerformSkillEnd m_RequestPacket;
        private RCPushEntityPerformSkillEnd m_ResponsePacket;
        private int m_UserId;
        private int m_RoomId;

        public Action5102(ActionGetter actionGetter)
            : base((short)5102, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = null;
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RoomId = (actionGetter.GetSession().User as RoomSessionUser).RoomId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CREntityPerformSkillEnd>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            RoomManager rm = RoomManager.GetInstance(m_RoomId);
            if (rm != null)
            {
                rm.PushIntoMessageQueue(actionGetter.GetActionId(), actionGetter.GetSession(), m_RequestPacket);
            }
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            //CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
