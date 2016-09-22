﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// You may need to modify 'TakeAction' method.
// Don't modify the rest unless you know what you're doing.
//----------------------------------------------------------------------------------------------------

using System;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;

namespace Genesis.GameServer.LobbyServer
{
    public class Action1302 : AuthorizeAction
    {
        private CLGetSinglePvpInfo m_RequestPacket;
        private LCGetSinglePvpInfo m_ResponsePacket;
        private int m_UserId;

        public Action1302(ActionGetter actionGetter)
            : base((short)1302, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetSinglePvpInfo();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetSinglePvpInfo>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerSinglePvpLogic psp = new PlayerSinglePvpLogic();
            psp.SetUser(m_UserId);
            var rank = RankingFactory.Get<SinglePvpRankUser>("SinglePvpRanking").Find(u => u.UserId == m_UserId);
            if(rank == null)
            {
                m_ResponsePacket.Score = 0;
                m_ResponsePacket.Rank = -1;
            }
            else
            {
                m_ResponsePacket.Score = rank.Score;
                m_ResponsePacket.Rank = rank.RankId;
            }
            m_ResponsePacket.ChallengeCount = psp.MyPvp.RemainingCount;
            m_ResponsePacket.Season = PVPLogic.GetSeasonId();
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}