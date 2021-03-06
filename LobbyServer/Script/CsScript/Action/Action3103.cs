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
    public class Action3103 : AuthorizeAction
    {
        private CLGetFriendList m_RequestPacket;
        private LCGetFriendList m_ResponsePacket;
        private int m_UserId;

        public Action3103(ActionGetter actionGetter)
            : base((short)3103, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetFriendList();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetFriendList>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerFriendsLogic pf = new PlayerFriendsLogic();
            pf.SetUser(m_UserId);
            PlayerLogic p = new PlayerLogic();
            foreach (var player in pf.GetFriends())
            {
                p.SetUser(player.Key);
                m_ResponsePacket.Players.Add(new PBPlayerInfo()
                {
                    Id = player.Key,
                    Name = p.MyPlayer.Name,
                    Level = p.MyPlayer.Level,
                    VipLevel = p.MyPlayer.VIPLevel,
                    LastLoginInTicks = p.MyPlayer.LastLoginTime,
                    IsOnline = p.MyPlayer.Status == PlayerStatus.Online
                });

                m_ResponsePacket.CanGiveEnergy.Add(player.Value.CanSendEnergy);
                m_ResponsePacket.CanReceiveEnergy.Add(player.Value.CanReceiveEnergy);
            }
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
