﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// You may need to modify 'TakeAction' method.
// Don't modify the rest unless you know what you're doing.
//----------------------------------------------------------------------------------------------------

using System;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;

namespace Genesis.GameServer.LobbyServer
{
    public class Action3108 : AuthorizeAction
    {
        private CLRespondToFriendRequest m_RequestPacket;
        private LCRespondToFriendRequest m_ResponsePacket;
        private int m_UserId;

        public Action3108(ActionGetter actionGetter)
            : base((short)3108, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCRespondToFriendRequest();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLRespondToFriendRequest>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerFriendsLogic pf = new PlayerFriendsLogic();
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            PBPlayerInfo myInfo = new PBPlayerInfo()
            {
                Id = m_UserId,
                Name = p.MyPlayer.Name,
                Level = p.MyPlayer.Level,
                VipLevel = p.MyPlayer.VIPLevel,
                LastLoginInTicks = p.MyPlayer.LastLoginTime
            };
            p.SetUser(m_RequestPacket.PlayerId);
            PBPlayerInfo friendInfo = new PBPlayerInfo()
            {
                Id = m_RequestPacket.PlayerId,
                Name = p.MyPlayer.Name,
                Level = p.MyPlayer.Level,
                VipLevel = p.MyPlayer.VIPLevel,
                LastLoginInTicks = p.MyPlayer.LastLoginTime
            };
            LCReceiveRespondToFriendRequest SendPackage = new LCReceiveRespondToFriendRequest()
            {
                Accept = m_RequestPacket.Accept,
                Player = myInfo,
            };
            if (m_RequestPacket.Accept == true)
            {
                if (!pf.AddFriend(m_UserId, m_RequestPacket.PlayerId))
                {
                    ErrorCode = (int)ErrorType.RequireNotMet;
                    ErrorInfo = "Can't add this friend";
                    return false;
                }
                pf.SetUser(m_UserId);
                m_ResponsePacket.CanGiveEnergy = pf.MyFriends.Friends[m_RequestPacket.PlayerId].CanSendEnergy;
                m_ResponsePacket.CanReceiveEnergy = pf.MyFriends.Friends[m_RequestPacket.PlayerId].CanReceiveEnergy;
                pf.SetUser(m_RequestPacket.PlayerId);
                SendPackage.CanGiveEnergy = pf.MyFriends.Friends[m_UserId].CanSendEnergy;
                SendPackage.CanReceiveEnergy = pf.MyFriends.Friends[m_UserId].CanReceiveEnergy;
                pf.RemoveInvitation(m_UserId);
            }
            var target = GameSession.Get(m_RequestPacket.PlayerId);
            if (target != null)
            {
                byte[] buffer = CustomActionDispatcher.GeneratePackageStream(3111, ProtoBufUtils.Serialize(SendPackage));
                target.SendAsync(buffer, 0, buffer.Length);
            }
            pf.SetUser(m_UserId);
            pf.RemoveInvitation(m_RequestPacket.PlayerId);
            m_ResponsePacket.Accept = m_RequestPacket.Accept;
            m_ResponsePacket.Player = friendInfo;
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}