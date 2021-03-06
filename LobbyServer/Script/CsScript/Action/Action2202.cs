﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// You may need to modify 'TakeAction' method.
// Don't modify the rest unless you know what you're doing.
//----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract.Action;
using ZyGames.Framework.Game.Service;

namespace Genesis.GameServer.LobbyServer
{
    public class Action2202 : AuthorizeAction
    {
        private CLOpenAllChances m_RequestPacket;
        private LCOpenAllChances m_ResponsePacket;
        private int m_UserId;
        private Dictionary<int, int> m_DropItems = new Dictionary<int,int>();

        public Action2202(ActionGetter actionGetter)
            : base((short)2202, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCOpenAllChances();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLOpenAllChances>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerChanceLogic pc = new PlayerChanceLogic();
            pc.SetUserAndType(m_UserId, m_RequestPacket.ChanceType);
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            Dictionary<int, int> dropDict = new Dictionary<int, int>();
            if (!pp.CheckPackageSlot(dropDict))
            {
                ErrorCode = (int)ErrorType.PackageSlotFull;
                ErrorInfo = "package is full";
                return false;
            }
            var items = pc.OpenAllChance();
            if (items == null)
            {
                ErrorCode = (int)ErrorType.CannotOpenChance;
                ErrorInfo = "you can not open this";
                return false;
            }
            m_ResponsePacket.ChanceType = m_RequestPacket.ChanceType;
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            m_ResponsePacket.PlayerInfo = new PBPlayerInfo()
            {
                Id = p.MyPlayer.Id,
                Coin = p.MyPlayer.Coin,
                Money = p.MyPlayer.Money,
            };

            foreach (var iteminfo in items)
            {
                m_ResponsePacket.OpenedIndex.Add(iteminfo.Key);
                m_ResponsePacket.OpenedGoodInfo.Add(new PBItemInfo()
                {
                    Type = iteminfo.Value.ItemId,
                    Count = iteminfo.Value.ItemCount
                });
                GetItems(iteminfo.Value);
            }
           
            ReceiveItemMethodType type = m_RequestPacket.ChanceType == (int)ChanceType.Money ? ReceiveItemMethodType.CoinChance : ReceiveItemMethodType.MoneyChance;
            PBReceivedItems receivedItems;
            pp.GetItems(m_DropItems, type, out receivedItems);
            m_ResponsePacket.ReceivedItems = receivedItems;
            return true;
        }

        private void GetItems(DropItem item)
        {
            if (PlayerPackageLogic.IsItem(item.ItemId))
            {
                var itemData = CacheSet.ItemTable.GetData(item.ItemId);
                if(itemData == null)
                {
                    TraceLog.WriteError("Wrong Item ID in drop table : " + item.ItemId.ToString());
                }
                if (itemData.FunctionId == (int)ItemFunctions.AddHero)
                {
                    PlayerHeroLogic ph = new PlayerHeroLogic();
                    ph.SetUser(m_UserId);
                    int heroId = int.Parse(itemData.FunctionParams);
                    ItemListItem piece = ph.AddNewHero(heroId);
                    if (piece == null)
                    {
                        Hero newHero = ph.MyHeros.Heros[heroId];
                        m_ResponsePacket.LobbyHeroInfo.Add(new PBLobbyHeroInfo()
                        {
                            Type = newHero.HeroType,
                            Level = newHero.HeroLv,
                            Exp = newHero.HeroExp,
                            ElevationLevel = newHero.ElevationLevel,
                            ConsciousnessLevel = newHero.ConsciousnessLevel,
                            StarLevel = newHero.HeroStarLevel
                        });
                        return;
                    }
                    else
                    {
                        AddItem(piece.Id, piece.Count);
                        return;
                    }
                }
                AddItem(item.ItemId, item.ItemCount);
                return;
            }
            AddItem(item.ItemId, item.ItemCount);
            return;
        }

        private void AddItem(int Id, int Count)
        {
            if (m_DropItems.ContainsKey(Id))
            {
                m_DropItems[Id] += Count;
            }
            else
            {
                m_DropItems.Add(Id, Count);
            }
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
