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
    public class Action3100 : AuthorizeAction
    {
        private CLGetNearbyPlayers m_RequestPacket;
        private LCGetNearbyPlayers m_ResponsePacket;
        private int m_UserId;

        public Action3100(ActionGetter actionGetter)
            : base((short)3100, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetNearbyPlayers();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetNearbyPlayers>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerFriendsLogic pf = new PlayerFriendsLogic();
            pf.SetUser(m_UserId);
            var nearbyPlayers = pf.GetOnlinePlayers();
            var friends = pf.GetFriends();
            HeroTeamLogic ht = new HeroTeamLogic();
            PlayerHeroLogic ph = new PlayerHeroLogic();
            NearbyPlayerLogic np = new NearbyPlayerLogic();
            foreach (var player in nearbyPlayers)
            {
                ht.SetUser(player.Id);
                ph.SetUser(player.Id).SetHero(ht.GetTeam()[0]);
                Hero hero = ph.GetHeroInfo();
                if(hero == null)
                {
                    continue;
                }
                m_ResponsePacket.Heroes.Add(new PBLobbyHeroInfo()
                {
                    Type = hero.HeroType,
                    Level = hero.HeroLv,
                    StarLevel = hero.HeroStarLevel,
                    ConsciousnessLevel = hero.ConsciousnessLevel,
                    ElevationLevel = hero.ElevationLevel
                });
                np.SetUser(player.Id);
                m_ResponsePacket.Players.Add(new PBPlayerInfo()
                {
                    Id = player.Id,
                    Name = player.Name,
                    Level = player.Level,
                    VipLevel = player.VIPLevel,
                    PortraitType = player.PortraitType,
                    LastLoginInTicks = player.LastLoginTime,
                    PositionX = np.NearbyPlayers.MyPositionX,
                    PositionY = np.NearbyPlayers.MyPositionY,
                    DisplayId = player.UUID
                });
                if (friends.ContainsKey(player.Id))
                {
                    m_ResponsePacket.IsMyFriend.Add(true);
                }
                else
                {
                    m_ResponsePacket.IsMyFriend.Add(false);
                }
            }
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
