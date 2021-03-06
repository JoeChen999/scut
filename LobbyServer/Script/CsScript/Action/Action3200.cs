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
    public class Action3200 : AuthorizeAction
    {
        private CLGetEmailList m_RequestPacket;
        private LCGetEmailList m_ResponsePacket;
        private int m_UserId;

        public Action3200(ActionGetter actionGetter)
            : base((short)3200, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetEmailList();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetEmailList>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerMailLogic pm = new PlayerMailLogic();
            pm.SetUser(m_UserId);
            var mails = pm.GetMails();
            foreach (var mail in mails)
            {
                m_ResponsePacket.EmailList.Add(new PBEmailInfo()
                {
                    Id = mail.Key,
                    Message = mail.Value.Message,
                    ItemType = mail.Value.AttachedId,
                    ItemCount = mail.Value.AttachedCount,
                    ExpireTime = mail.Value.ExpireTime,
                    SendTime = mail.Value.StartTime,
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
