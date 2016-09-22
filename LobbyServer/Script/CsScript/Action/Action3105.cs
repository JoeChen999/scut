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
    public class Action3105 : AuthorizeAction
    {
        private CLGetPendingFriendRequests m_RequestPacket;
        private LCGetPendingFriendRequests m_ResponsePacket;
        private int m_UserId;

        public Action3105(ActionGetter actionGetter)
            : base((short)3105, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetPendingFriendRequests();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetPendingFriendRequests>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerFriendsLogic pf = new PlayerFriendsLogic();
            pf.SetUser(m_UserId);
            var invitations = pf.GetInvitations();
            PlayerLogic p = new PlayerLogic();
            foreach (int id in invitations)
            {
                p.SetUser(id);
                m_ResponsePacket.Players.Add(new PBPlayerInfo()
                {
                    Id = p.MyPlayer.Id,
                    Name = p.MyPlayer.Name,
                    VipLevel = p.MyPlayer.VIPLevel,
                    Level = p.MyPlayer.Level
                });
            }
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}