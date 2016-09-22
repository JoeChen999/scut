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
    public class Action1016 : AuthorizeAction
    {
        private CLChangeSoul m_RequestPacket;
        private LCChangeSoul m_ResponsePacket;
        private int m_UserId;

        public Action1016(ActionGetter actionGetter)
            : base((short)1016, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCChangeSoul();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLChangeSoul>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerHeroLogic hero = new PlayerHeroLogic();
            hero.SetUser(m_UserId).SetHero(m_RequestPacket.HeroId);
            SoulLogic soul = new SoulLogic();
            PlayerPackageLogic package = new PlayerPackageLogic();
            package.SetUser(m_UserId);
            if (m_RequestPacket.HasPutOnSoulId)
            {
                soul.SetSoul(m_RequestPacket.PutOnSoulId);
                if (soul.MySoul == null)
                {
                    ErrorCode = (int)ErrorType.CannotOpenChance;
                    ErrorInfo = "wrong soulID";
                    return false;
                }
                int eid = CacheSet.SoulTable.GetData(soul.MySoul.TypeId).Type;
                if (!package.DeductSoul(m_RequestPacket.PutOnSoulId))
                {
                    ErrorCode = (int)ErrorType.RequireNotMet;
                    ErrorInfo = "wrong soulID";
                    return false;
                }
                if (!hero.DressSoul(eid, m_RequestPacket.PutOnSoulId))
                {
                    ErrorCode = (int)ErrorType.CannotOpenChance;
                    ErrorInfo = "wrong soulID";
                    return false;
                }
                m_ResponsePacket.PutOnSoulId = m_RequestPacket.PutOnSoulId;
                m_ResponsePacket.HeroId = m_RequestPacket.HeroId;
            }
            else if(m_RequestPacket.HasTakeOffSoulId)
            {
                soul.SetSoul(m_RequestPacket.TakeOffSoulId);
                if (soul.MySoul == null)
                {
                    ErrorCode = (int)ErrorType.CannotOpenChance;
                    ErrorInfo = "wrong soulID";
                    return false;
                }
                int eid = CacheSet.SoulTable.GetData(soul.MySoul.TypeId).Type;
                if (!hero.UndressSoul(eid, m_RequestPacket.TakeOffSoulId))
                {
                    ErrorCode = (int)ErrorType.CannotOpenChance;
                    ErrorInfo = "wrong soulID";
                    return false;
                }
                package.AddSoul(m_RequestPacket.TakeOffSoulId, soul.MySoul.TypeId);
                m_ResponsePacket.HeroId = m_RequestPacket.HeroId;
                m_ResponsePacket.TakeOffSoulId = m_RequestPacket.TakeOffSoulId;
            }
            else
            {
                //TODO
            }
            if(m_RequestPacket.HeroId > 0)
            {
                hero.RefreshMight();
                var heroInfo = hero.GetHeroInfo();
                m_ResponsePacket.HeroInfo = new PBLobbyHeroInfo()
                {
                    Type = heroInfo.HeroType,
                    Might = heroInfo.Might,
                };
            }
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}