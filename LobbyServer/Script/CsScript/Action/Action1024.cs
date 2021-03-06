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
    public class Action1024 : AuthorizeAction
    {
        private CLUpgradeEpigraph m_RequestPacket;
        private LCUpgradeEpigraph m_ResponsePacket;
        private int m_UserId;

        public Action1024(ActionGetter actionGetter)
            : base((short)1024, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCUpgradeEpigraph();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLUpgradeEpigraph>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            int pieceId = pp.UpgradeEpigraph(m_RequestPacket.Id);
            if (pieceId == -1)
            {
                ErrorCode = (int)ErrorType.RequireNotMet;
                ErrorInfo = "You can not upgrade this epigraph";
                return false;
            }
            m_ResponsePacket.EpigraphInfo = new PBEpigraphInfo()
            {
                Type = m_RequestPacket.Id,
                Level = pp.MyPackage.Epigraphs[m_RequestPacket.Id]
            };
            m_ResponsePacket.ItemInfo = new PBItemInfo()
            {
                Type = pieceId,
                Count = pp.MyPackage.Inventories[pieceId]
            };
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
