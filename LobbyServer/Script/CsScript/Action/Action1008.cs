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
    public class Action1008 : AuthorizeAction
    {
        private CLInstanceProgress m_RequestPacket;
        private LCInstanceProgress m_ResponsePacket;
        private int m_UserId;

        public Action1008(ActionGetter actionGetter)
            : base((short)1008, actionGetter)
        {
            m_UserId = 0;
            m_RequestPacket = null;
            m_ResponsePacket = new LCInstanceProgress();
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLInstanceProgress>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            InstanceProgressLogic instanceProgress = new InstanceProgressLogic();
            instanceProgress.SetUser(m_UserId);
            foreach (var instance in instanceProgress.GetInstanceProgress())
            {
                m_ResponsePacket.InstanceProgress.Add(new PBInstance() { Id = instance.Key, StarCount = instance.Value });
            }
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
