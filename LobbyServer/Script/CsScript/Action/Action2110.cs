﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// You may need to modify 'TakeAction' method.
// Don't modify the rest unless you know what you're doing.
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;

namespace Genesis.GameServer.LobbyServer
{
    public class Action2110 : AuthorizeAction
    {
        private CLGetFoundryReward m_RequestPacket;
        private LCGetFoundryReward m_ResponsePacket;
        private int m_UserId;

        public Action2110(ActionGetter actionGetter)
            : base((short)2110, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetFoundryReward();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetFoundryReward>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerFoundryLogic pf = new PlayerFoundryLogic();
            pf.SetUser(m_UserId);

            int[] drops = GameConsts.Foundry.FoundryRewardDropPackageId[m_RequestPacket.Level];
            RandomDropLogic random = RandomDropLogic.GetInstance();
            CacheDictionary<int, int> dropDict = new CacheDictionary<int, int>();
            foreach (int dropId in drops)
            {
                DTDrop dataRow = CacheSet.DropTable.GetData(dropId);
                random.GetDropDict(dataRow, dropDict);
            }
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            if (!pp.CheckPackageSlot(dropDict))
            {
                ErrorCode = (int)ErrorType.PackageSlotFull;
                ErrorInfo = "package is full";
                return false;
            }
            if (!pf.GetReward(m_RequestPacket.Level))
            {
                ErrorCode = (int)ErrorType.RequireNotMet;
                ErrorInfo = "You can not get this reward";
                return false;
            }
            PBReceivedItems receivedItems;
            pp.GetItems(dropDict, ReceiveItemMethodType.GearFoundry, out receivedItems);
            m_ResponsePacket.ReceivedItems = receivedItems;
            m_ResponsePacket.RewardFlags.AddRange(pf.MyFoundry.CanReceiveRewards);
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}