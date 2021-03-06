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
    public class Action2100 : AuthorizeAction
    {
        private CLGetGearFoundryData m_RequestPacket;
        private LCGetGearFoundryData m_ResponsePacket;
        private int m_UserId;

        public Action2100(ActionGetter actionGetter)
            : base((short)2100, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetGearFoundryData();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetGearFoundryData>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerFoundryLogic pf = new PlayerFoundryLogic();
            pf.SetUser(m_UserId);
            var playerFoundryInfo = pf.GetPlayerFoundryInfo();
            m_ResponsePacket.Data = pf.GetAllFoundryData();
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
