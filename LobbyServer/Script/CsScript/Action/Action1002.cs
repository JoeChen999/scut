﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// You may need to modify 'TakeAction' method.
// Don't modify the rest unless you know what you're doing.
//----------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;

namespace Genesis.GameServer.LobbyServer
{
    public class Action1002 : AuthorizeAction
    {
        private CLCreatePlayer m_RequestPacket;
        private LCCreatePlayer m_ResponsePacket;
        private int m_UserId;

        public Action1002(ActionGetter actionGetter)
            : base((short)1002, actionGetter)
        {
            m_UserId = 0;
            m_RequestPacket = null;
            m_ResponsePacket = new LCCreatePlayer();
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLCreatePlayer>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            if (m_RequestPacket.Name.Length < 1 || m_RequestPacket.Name.Length > 30)
            {
                ErrorCode = (int)ErrorType.WrongInput;
                ErrorInfo = "wrong length";
                return false;
            }
            if (PlayerLogic.FindUserByName(m_RequestPacket.Name) != null)
            {
                ErrorCode = (int)ErrorType.DuplicateName;
                ErrorInfo = "Duplicate name";
                return false;
            }
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            p.MyPlayer.ModifyLocked(() =>
            {
                p.MyPlayer.Name = m_RequestPacket.Name;
                p.MyPlayer.PortraitType = m_RequestPacket.PortraitType;
                p.MyPlayer.IsFemale = m_RequestPacket.IsFemale;
            });
            PlayerHeroLogic ph = new PlayerHeroLogic();
            ph.SetUser(m_UserId);
            ph.AddNewHero(m_RequestPacket.FirstHeroType);
            HeroTeamLogic ht = new HeroTeamLogic();
            ht.SetUser(m_UserId);
            List<int> heroTeam = new List<int>(){m_RequestPacket.FirstHeroType};
            ht.AssignHero(heroTeam);
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}