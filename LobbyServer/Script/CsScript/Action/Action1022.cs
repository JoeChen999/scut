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
    public class Action1022 : BaseStruct
    {
        private CLGetGameConfigs m_RequestPacket;
        private LCGetGameConfigs m_ResponsePacket;
        private int m_UserId;

        public Action1022(ActionGetter actionGetter)
            : base((short)1022, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetGameConfigs();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetGameConfigs>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            List<int> a = new List<int>();
            a.IndexOf(1);
            foreach (var config in GameConfigs.Configs)
            {
                m_ResponsePacket.Keys.Add(config.Key);
                m_ResponsePacket.Values.Add(config.Value);
            }
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
