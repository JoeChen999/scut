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
    public class Action2300 : AuthorizeAction
    {
        private CLGetArenaInfo m_RequestPacket;
        private LCGetArenaInfo m_ResponsePacket;
        private int m_UserId;

        public Action2300(ActionGetter actionGetter)
            : base((short)2300, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetArenaInfo();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetArenaInfo>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerArenaLogic pa = new PlayerArenaLogic();
            ArenaRankLogic ar = new ArenaRankLogic();
            pa.SetUser(m_UserId);
            m_ResponsePacket.ArenaTokenCount = pa.MyArena.ArenaTokenCount;
            m_ResponsePacket.ChallengeCount = pa.MyArena.ChallengeCount;
            m_ResponsePacket.ClaimedFlags.AddRange(pa.MyArena.ClaimedLivenessRewardFlag);
            m_ResponsePacket.MyRank = ar.GetPlayerRank(m_UserId);
            m_ResponsePacket.WinCount = pa.MyArena.WinCount;
            var matchPlayers = ar.GetMatchPlayers(m_UserId);
            PlayerLogic p = new PlayerLogic();
            foreach (var player in matchPlayers)
            {
                PBArenaPlayerAndTeamInfo enemy = new PBArenaPlayerAndTeamInfo();
                enemy.Rank = player.RankId;
                p.SetUser(player.PlayerId);
                enemy.PlayerInfo = new PBPlayerInfo()
                {
                    Id = player.PlayerId,
                    Name = p.MyPlayer.Name,
                    Level = p.MyPlayer.Level,
                    PortraitType = p.MyPlayer.PortraitType
                };
                m_ResponsePacket.Enemies.Add(enemy);
            }
            return true;
        }


        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
