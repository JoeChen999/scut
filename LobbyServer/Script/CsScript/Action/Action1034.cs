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
    public class Action1034 : AuthorizeAction
    {
        private CLPlayerMove m_RequestPacket;
        private LCPlayerMove m_ResponsePacket;
        private int m_UserId;

        public Action1034(ActionGetter actionGetter)
            : base((short)1034, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCPlayerMove();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLPlayerMove>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            NearbyPlayerLogic np = new NearbyPlayerLogic();
            np.SetUser(m_UserId);
            np.Move(m_RequestPacket.LobbyPositionX, m_RequestPacket.LobbyPositionY);
            m_ResponsePacket.LobbyPositionY = m_RequestPacket.LobbyPositionY;
            m_ResponsePacket.LobbyPositionX = m_RequestPacket.LobbyPositionX;
            m_ResponsePacket.PlayerId = m_UserId;
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
