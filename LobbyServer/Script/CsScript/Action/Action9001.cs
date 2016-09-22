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
    public class Action9001 : AuthorizeAction
    {
        private RLCreateNewRoom m_RequestPacket;
        private LRCreateNewRoom m_ResponsePacket;
        private int m_UserId;

        public Action9001(ActionGetter actionGetter)
            : base((short)9001, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LRCreateNewRoom();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<RLCreateNewRoom>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            // TODO: Add code here.

            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
