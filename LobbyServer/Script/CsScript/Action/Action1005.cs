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
    public class Action1005 : AuthorizeAction
    {
        private CLLeaveInstance m_RequestPacket;
        private LCLeaveInstance m_ResponsePacket;
        private int m_UserId;

        public Action1005(ActionGetter actionGetter)
            : base((short)1005, actionGetter)
        {
            m_UserId = 0;
            m_RequestPacket = null;
            m_ResponsePacket = new LCLeaveInstance();
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLLeaveInstance>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerInstanceLogic playerinstance = new PlayerInstanceLogic();
            playerinstance.SetUser(m_UserId);
            if(playerinstance.MyInstance == null)
            {
                ErrorCode = (int)ErrorType.PlayerNotInInstance;
                ErrorInfo = "You have not enter instance";
                return false;
            }
            GetInstanceReward(playerinstance);
            playerinstance.LeaveInstance();
            if (m_RequestPacket.Win)
            {
                InstanceProgressLogic instanceProgress = new InstanceProgressLogic();
                instanceProgress.SetUser(m_UserId);
                instanceProgress.InstanceCompleted(playerinstance.MyInstance.InstanceId, m_RequestPacket.StarCount);
            }
            m_ResponsePacket.Win = m_RequestPacket.Win;
            m_ResponsePacket.StarCount = m_RequestPacket.StarCount;
            return true;
        }

        private void GetInstanceReward(PlayerInstanceLogic instance)
        {
            int instanceId = instance.MyInstance.InstanceId;
            PlayerLogic player = new PlayerLogic();
            player.SetUser(m_UserId);
            long nextRecoverTime;
            if (!m_RequestPacket.Win)
            {
                int feedbackEnergy = PlayerInstanceLogic.GetFeedBackEnergy(instanceId);
                player.AddEnergy(feedbackEnergy, out nextRecoverTime);
                m_ResponsePacket.PlayerInfo = new PBPlayerInfo();
                m_ResponsePacket.PlayerInfo.Energy = player.MyPlayer.Energy;
                m_ResponsePacket.PlayerInfo.NextEnergyRecoveryTime = nextRecoverTime;
                return;
            }
            DTInstance instanceData = PlayerInstanceLogic.GetInstanceData(instanceId);
            player.MyPlayer.Coin += instanceData.Coin;
            player.AddExp(instanceData.PlayerExp);
            m_ResponsePacket.PlayerInfo = new PBPlayerInfo();
            m_ResponsePacket.PlayerInfo.Id = player.MyPlayer.Id;
            m_ResponsePacket.PlayerInfo.Coin = player.MyPlayer.Coin;
            m_ResponsePacket.PlayerInfo.Exp = player.MyPlayer.Exp;
            m_ResponsePacket.PlayerInfo.Level = player.MyPlayer.Level;
            HeroTeamLogic heroTeam = new HeroTeamLogic();
            heroTeam.SetUser(m_UserId);
            PlayerHeroLogic playerHero = new PlayerHeroLogic();
            playerHero.SetUser(m_UserId);
            foreach (int heroId in heroTeam.MyHeroTeam.Team)
            {
                if (heroId == 0)
                {
                    continue;
                }
                playerHero.SetHero(heroId).AddExp(instanceData.HeroExp);
                m_ResponsePacket.LobbyHeroInfo.Add(new PBLobbyHeroInfo()
                {
                    Type = heroId,
                    Exp = playerHero.MyHeros.Heros[heroId].HeroExp,
                    Level = playerHero.MyHeros.Heros[heroId].HeroLv,
                    Might = playerHero.MyHeros.Heros[heroId].Might,
                });
            }
            m_ResponsePacket.InstanceType = instance.MyInstance.InstanceId;
            CacheDictionary<int, int> dropDict = instance.GetDropList();
            PlayerPackageLogic package = new PlayerPackageLogic();
            package.SetUser(m_UserId);
            PBReceivedItems receivedItems;
            package.GetItems(dropDict, ReceiveItemMethodType.CompleteInstance, out receivedItems);
            m_ResponsePacket.ReceivedItems = receivedItems;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
