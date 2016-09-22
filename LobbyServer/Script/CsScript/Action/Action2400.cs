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
    public class Action2400 : AuthorizeAction
    {
        private CLGetCosmosCrackInfo m_RequestPacket;
        private LCGetCosmosCrackInfo m_ResponsePacket;
        private int m_UserId;

        public Action2400(ActionGetter actionGetter)
            : base((short)2400, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetCosmosCrackInfo();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetCosmosCrackInfo>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerCosmosCrackLogic pcc = new PlayerCosmosCrackLogic().SetUser(m_UserId);
            var playerCosmosCrack = pcc.GetCosmosCrackInstanceInfo();
            m_ResponsePacket.UsedRoundCount = playerCosmosCrack.PassedRoundCount;
            foreach(var instance in playerCosmosCrack.ChosenInstance)
            {
                PBCosmosCrackInstanceInfo instanceInfo = new PBCosmosCrackInstanceInfo()
                {
                    InstanceType = instance.Key,
                    RewardLevel = instance.Value.RewardLevel
                };
                foreach(var item in instance.Value.RewardItem)
                {
                    instanceInfo.Rewards.Add(new PBItemInfo()
                    {
                        Type = item.Key,
                        Count = item.Value
                    });
                }
                m_ResponsePacket.InstanceInfos.Add(instanceInfo);
            }
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
