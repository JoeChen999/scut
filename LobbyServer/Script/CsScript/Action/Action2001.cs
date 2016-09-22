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
    public class Action2001 : AuthorizeAction
    {
        private CLGetChessBoard m_RequestPacket;
        private LCGetChessBoard m_ResponsePacket;
        private int m_UserId;

        public Action2001(ActionGetter actionGetter)
            : base((short)2001, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCGetChessBoard();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLGetChessBoard>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerChessLogic playerChess = new PlayerChessLogic();
            playerChess.SetUser(m_UserId);
            if (m_RequestPacket.IsReset)
            {
                if (!playerChess.Reset())
                {
                    ErrorCode = (int)ErrorType.RequireNotMet;
                    ErrorInfo = "have not token";
                    return false;
                }
            }
            if (playerChess.MyChess.LastResetTime < DateTime.Today)
            {
                playerChess.ResetToken();
            }
            SetResponseData(playerChess.MyChess);
            return true;
        }

        private void SetResponseData(PlayerChess playerChess)
        {
            m_ResponsePacket.GotCoin = playerChess.GotCoin;
            m_ResponsePacket.GotMoney = playerChess.GotMoney;
            m_ResponsePacket.GotStarEnergy = playerChess.GotStarEnergy;
            foreach (var item in playerChess.GotItems)
            {
                m_ResponsePacket.GotItems.Add(new PBItemInfo() { Type = item.Key, Count = item.Value });
            }
            GearLogic gears = new GearLogic();
            foreach (int gearId in playerChess.GotGears)
            {
                gears.SetGear(gearId);
                m_ResponsePacket.GotGears.Add(new PBGearInfo() { Id = gears.MyGear.Id, Level = gears.MyGear.Level, Type = gears.MyGear.TypeId });
            }
            SoulLogic souls = new SoulLogic();
            foreach (int soulId in playerChess.GotSouls)
            {
                souls.SetSoul(soulId);
                m_ResponsePacket.GotSouls.Add(new PBSoulInfo() { Id = souls.MySoul.Id, Type = souls.MySoul.TypeId });
            }
            foreach (int epigraphId in playerChess.GotEpigraphs)
            {
                m_ResponsePacket.GotEpigraphs.Add(new PBEpigraphInfo() { Level = 1, Type = epigraphId });
            }
            foreach (var heroHP in playerChess.HP)
            {
                m_ResponsePacket.HeroStatus.Add(new PBLobbyHeroStatus() { Type = heroHP.Key, CurHP = heroHP.Value });
            }
            m_ResponsePacket.PlayCount = playerChess.Count;
            m_ResponsePacket.Anger = playerChess.Anger;
            m_ResponsePacket.TokenCount = playerChess.Token;
            m_ResponsePacket.Width = GameConsts.PlayerChess.ChessBoardWidth;
            PlayerLogic player = new PlayerLogic();
            foreach (var chessField in playerChess.ChessBoard)
            {
                PBChessField cf = new PBChessField();
                cf.Color = chessField.Color == ChessFieldColor.Empty || chessField.Color == ChessFieldColor.EmptyGray || chessField.Color == ChessFieldColor.RewardGray ? 
                    (int)ChessFieldColor.EmptyGray : (int)chessField.Color;
                if (chessField.Color == ChessFieldColor.Empty || chessField.Color == ChessFieldColor.EmptyGray || chessField.Color == ChessFieldColor.RewardGray)
                {
                    var curField = chessField as RewardChessField;
                    cf.IsOpened = curField.IsOpened;
                    if (!cf.IsOpened)
                    {
                        cf.IsFree = curField.IsFree;
                        if (cf.IsFree)
                        {
                            cf.Parent = curField.ParentId;
                        }
                    }
                }
                else
                {
                    var curField = chessField as BattleChessField;
                    player.SetUser(curField.EnemyPlayerId);
                    cf.IsOpened = curField.IsOpened;
                    if (cf.IsOpened)
                    {
                        cf.FreeCount = curField.Count;
                        cf.Children.AddRange(curField.ChildrenId);
                    }
                }
                m_ResponsePacket.ChessBoard.Add(cf);
            }
            if (playerChess.MyTeam != null)
            {
                m_ResponsePacket.HeroTeam.AddRange(playerChess.MyTeam);
            }
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}