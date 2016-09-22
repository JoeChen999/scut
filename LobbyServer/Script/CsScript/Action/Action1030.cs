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
    public class Action1030 : AuthorizeAction
    {
        private CLGetHeroesInfo m_RequestPacket;
        private LCGetHeroesInfo m_ResponsePacket;
        private int m_UserId;

        public Action1030(ActionGetter actionGetter)
            : base((short)1030, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetHeroesInfo();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetHeroesInfo>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            IList<int> heroes;
            if (m_RequestPacket.HeroTypes.Count == 0) 
            {
                HeroTeamLogic ht = new HeroTeamLogic();
                ht.SetUser(m_UserId);
                heroes = ht.GetTeam();
            }
            else
            {
                heroes = m_RequestPacket.HeroTypes;
            }
            PlayerHeroLogic ph = new PlayerHeroLogic();
            ph.SetUser(m_RequestPacket.PlayerId);
            GearLogic gear = new GearLogic();
            SoulLogic soul = new SoulLogic();
            foreach (int heroId in heroes)
            {
                if (heroId == 0)
                {
                    continue;
                }
                ph.SetHero(heroId);
                Hero heroInfo = ph.GetHeroInfo();
                PBLobbyHeroInfo pbhero = new PBLobbyHeroInfo()
                {
                    Type = heroInfo.HeroType,
                    Level = heroInfo.HeroLv,
                    ConsciousnessLevel = heroInfo.ConsciousnessLevel,
                    ElevationLevel = heroInfo.ElevationLevel,
                    StarLevel = heroInfo.HeroStarLevel,
                };
                foreach (var equipedGear in heroInfo.Gears)
                {
                    gear.SetGear(equipedGear.Value);
                    PBGearInfo heroGear = new PBGearInfo()
                    {
                        Id = gear.MyGear.Id,
                        Type = gear.MyGear.TypeId,
                        Level = gear.MyGear.Level,
                        StrengthenLevel = gear.MyGear.StrengthenLevel
                    };
                    pbhero.GearInfo.Add(heroGear);
                }
                foreach (var equipedSoul in heroInfo.Souls)
                {
                    soul.SetSoul(equipedSoul.Value);
                    PBSoulInfo heroSoul = new PBSoulInfo()
                    {
                        Id = soul.MySoul.Id,
                        Type = soul.MySoul.TypeId
                    };
                    pbhero.SoulInfo.Add(heroSoul);
                }
                m_ResponsePacket.Heroes.Add(pbhero);
            }
            m_ResponsePacket.PlayerId = m_RequestPacket.PlayerId;
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
