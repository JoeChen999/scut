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
    public class Action2107 : AuthorizeAction
    {
        private CLLeaveGearFoundryTeam m_RequestPacket;
        private LCLeaveGearFoundryTeam m_ResponsePacket;
        private int m_UserId;

        public Action2107(ActionGetter actionGetter)
            : base((short)2107, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCLeaveGearFoundryTeam();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLLeaveGearFoundryTeam>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerFoundryLogic pf = new PlayerFoundryLogic();
            pf.SetUser(m_UserId);
            int roomId = pf.MyFoundry.CurrentRoomId;
            pf.QuitRoom();
            pf.PushRoomPlayerChangedNotify(roomId);
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
