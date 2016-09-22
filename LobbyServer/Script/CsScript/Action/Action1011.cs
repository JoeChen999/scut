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
    public class Action1011 : AuthorizeAction
    {
        private CLHeroElevationLevelUp m_RequestPacket;
        private LCHeroElevationLevelUp m_ResponsePacket;
        private int m_UserId;

        public Action1011(ActionGetter actionGetter)
            : base((short)1011, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCHeroElevationLevelUp();
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLHeroElevationLevelUp>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerHeroLogic playerHero = new PlayerHeroLogic();
            playerHero.SetUser(m_UserId).SetHero(m_RequestPacket.HeroType);
            if(!playerHero.DataCheck())
            {
                ErrorCode = (int)ErrorType.CannotOpenChance;
                ErrorInfo = "wrong HeroId";
                return false;
            }

            PlayerPackageLogic package = new PlayerPackageLogic();
            package.SetUser(m_UserId);
            Hero myHero = playerHero.GetHeroInfo();
            DTHeroElevationBase heroElevationData = CacheSet.HeroElevationBaseTable.GetData(myHero.ElevationLevel);
            if (heroElevationData.LevelUpItemId == -1)
            {
                ErrorCode = (int)ErrorType.RequireNotMet;
                ErrorInfo = "reached max level";
                return false;
            }

            ItemListItem item = new ItemListItem()
            {
                Id = heroElevationData.LevelUpItemId,
                Count = heroElevationData.LevelUpItemCount
            };
            if (!package.DeductInventory(item))
            {
                ErrorCode = (int)ErrorType.RequireNotMet;
                ErrorInfo = "not enough piece";
                return false;
            }
            for (int i = 0; i < GameConsts.Hero.ElevationLevelUpGearCount; i++)
            {
                if (heroElevationData.LevelUpGearType[i] == -1 || heroElevationData.LevelUpGearMinQuality[i] == -1)
                {
                    break;
                }
                int gearTypeId = 0;
                if(package.MyPackage.Gears.ContainsKey(m_RequestPacket.GearId[i]))
                {
                    gearTypeId = package.MyPackage.Gears[m_RequestPacket.GearId[i]];
                }
                else
                {
                    ErrorCode = (int)ErrorType.RequireNotMet;
                    ErrorInfo = "do not have this gear";
                    return false;
                }
                DTGear gear = CacheSet.GearTable.GetData(gearTypeId);
                if (gear.Type == heroElevationData.LevelUpGearType[i] && gear.Quality >= heroElevationData.LevelUpGearMinQuality[i]) 
                {
                    package.DeductGear(m_RequestPacket.GearId[i]);
                }
                else
                {
                    ErrorCode = (int)ErrorType.CannotOpenChance;
                    ErrorInfo = "gear"+ i.ToString() +" do not match this slot";
                    return false;
                }
            }
            playerHero.ElevationLevelUp();
            var heroInfo = playerHero.GetHeroInfo();
            m_ResponsePacket.LobbyHeroInfo = new PBLobbyHeroInfo()
            {
                Type = heroInfo.HeroType,
                ElevationLevel = heroInfo.ElevationLevel,
                Might = heroInfo.Might,
            };
            m_ResponsePacket.RemovedGears.AddRange(m_RequestPacket.GearId);
            m_ResponsePacket.ItemInfo = new PBItemInfo() { Type = item.Id, Count = package.MyPackage.Inventories[item.Id] };
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
