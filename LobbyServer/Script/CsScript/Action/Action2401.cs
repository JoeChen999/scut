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
    public class Action2401 : AuthorizeAction
    {
        private CLEnterCosmosCrackInstance m_RequestPacket;
        private LCEnterCosmosCrackInstance m_ResponsePacket;
        private int m_UserId;

        public Action2401(ActionGetter actionGetter)
            : base((short)2401, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCEnterCosmosCrackInstance();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLEnterCosmosCrackInstance>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerCosmosCrackLogic pcc = new PlayerCosmosCrackLogic().SetUser(m_UserId);
            if (!pcc.EnterInstance(m_RequestPacket.InstanceType))
            {
                ErrorCode = (int)ErrorType.RequireNotMet;
                ErrorInfo = "You have not enough RoundCount";
                return false;
            }
            m_ResponsePacket.InstanceType = m_RequestPacket.InstanceType;
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            m_ResponsePacket.NpcLevel = Math.Max(p.MyPlayer.Level + pcc.GetNPCAndPlayerDeltaLevel(m_RequestPacket.InstanceType), 1);
            PlayerInstanceLogic pi = new PlayerInstanceLogic();
            pi.SetUser(m_UserId);
            var dropInfo = pi.EnterInstance(m_RequestPacket.InstanceType);
            if (dropInfo == null)
            {
                ErrorCode = (int)ErrorType.PackageSlotFull;
                ErrorInfo = "Package slot full";
                return false;
            }
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
