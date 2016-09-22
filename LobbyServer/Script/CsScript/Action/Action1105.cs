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
    public class Action1105 : AuthorizeAction
    {
        private CLPurchaseInVipShop m_RequestPacket;
        private LCPurchaseInVipShop m_ResponsePacket;
        private int m_UserId;

        public Action1105(ActionGetter actionGetter)
            : base((short)1105, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCPurchaseInVipShop();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLPurchaseInVipShop>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerVipShopLogic pds = new PlayerVipShopLogic();
            pds.SetUser(m_UserId);
            if (!pds.PurchaseItem(m_RequestPacket.ShopItemIndex, m_RequestPacket.TypeId, m_RequestPacket.Count))
            {
                ErrorCode = (int)ErrorType.RequireNotMet;
                ErrorInfo = "Item info is not correct";
                return false;
            }
            Dictionary<int, int> itemDict = new Dictionary<int, int>();
            itemDict.Add(m_RequestPacket.TypeId, m_RequestPacket.Count);
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            PBReceivedItems receivedItems;
            pp.GetItems(itemDict, ReceiveItemMethodType.None, out receivedItems);
            m_ResponsePacket.ReceivedItems = receivedItems;
            m_ResponsePacket.OpenedGoodInfo.Add(new PBItemInfo() { Type = m_RequestPacket.TypeId, Count = m_RequestPacket.Count });
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            m_ResponsePacket.PlayerInfo = new PBPlayerInfo()
            {
                Id = m_UserId,
                Money = p.MyPlayer.Money,
                Coin = p.MyPlayer.Coin,
                Spirit = p.MyPlayer.Spirit
            };
            m_ResponsePacket.ShopItemIndex = m_RequestPacket.ShopItemIndex;
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
