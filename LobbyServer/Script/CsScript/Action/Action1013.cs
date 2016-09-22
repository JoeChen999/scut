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
    public class Action1013 : AuthorizeAction
    {
        private CLGearLevelUp m_RequestPacket;
        private LCGearLevelUp m_ResponsePacket;
        private int m_UserId;

        public Action1013(ActionGetter actionGetter)
            : base((short)1013, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGearLevelUp();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGearLevelUp>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            // TODO: Add code here.
            GearLogic gearlogic = new GearLogic();
            gearlogic.SetGear(m_RequestPacket.GearId);
            PlayerLogic player = new PlayerLogic();
            player.SetUser(m_UserId);
            if (gearlogic.MyGear.Level >= player.MyPlayer.Level)
            {
                ErrorCode = (int)ErrorType.CannotOpenChance;
                ErrorInfo = "Can not upgrade,gear reached max level.";
                return false;
            }
            int targetLevel;
            if (m_RequestPacket.IsUpToMax)
            {
                targetLevel = player.MyPlayer.Level;
            }
            else
            {
                targetLevel = gearlogic.MyGear.Level + 1;
            }
            int totalcost = gearlogic.GetLevelUpCost(targetLevel);
            if(!player.DeductCoin(totalcost))
            {
                ErrorCode = (int)ErrorType.RequireNotMet;
                ErrorInfo = "Can not upgrade, coin not enough.";
                return false;
            }
            gearlogic.GearLevelUp(targetLevel, m_UserId);
            if(m_RequestPacket.HeroType > 0)
            {
                PlayerHeroLogic ph = new PlayerHeroLogic();
                ph.SetUser(m_UserId).SetHero(m_RequestPacket.HeroType);
                ph.RefreshMight();
                var heroInfo = ph.GetHeroInfo();
                m_ResponsePacket.HeroInfo = new PBLobbyHeroInfo()
                {
                    Type = heroInfo.HeroType,
                    Might = heroInfo.Might
                };
            }
            m_ResponsePacket.LevelUpedGear = new PBGearInfo()
            {
                Id = m_RequestPacket.GearId,
                Level = gearlogic.MyGear.Level
            };
            m_ResponsePacket.PlayerInfo = new PBPlayerInfo()
            {
                Id = m_UserId,
                Coin = player.MyPlayer.Coin
            };
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
