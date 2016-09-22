﻿//----------------------------------------------------------------------------------------------------
// This code was auto generated by tools.
// You may need to modify 'TakeAction' method.
// Don't modify the rest unless you know what you're doing.
//----------------------------------------------------------------------------------------------------

using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;

namespace Genesis.GameServer.LobbyServer
{
    public class Action1000 : BaseStruct
    {
        private CLLoginServer m_RequestPacket;
        private LCLoginServer m_ResponsePacket;
        private PlayerLogic m_Player;

        public Action1000(ActionGetter actionGetter)
            : base((short)1000, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCLoginServer();
        }

        public override bool GetUrlElement()
        {
            m_RequestPacket = ProtoBufUtils.Deserialize<CLLoginServer>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            m_ResponsePacket.Authorized = false;
            GameSession session = actionGetter.GetSession();
            if (string.IsNullOrEmpty(m_RequestPacket.AccountName))
            {
                ErrorCode = (int)ErrorType.EmptyInput;
                ErrorInfo = "empty account name";
                return false;
            }
            if (CheckAuthorize())
            {
                m_ResponsePacket.Authorized = true;
            }
            else
            {
                session.Close();
                ErrorCode = (int)ErrorType.WrongInput;
                ErrorInfo = "Wrong password or account name";
                return false;
            }

            if (m_Player.MyPlayer == null)
            {
                m_ResponsePacket.NewAccount = true;
                m_Player.AddPlayer();
            }
            else if (string.IsNullOrEmpty(m_Player.MyPlayer.Name))
            {
                //CacheSet.PlayerCache.Delete(m_Player.MyPlayer);
                m_ResponsePacket.NewAccount = true;
                //m_Player.AddPlayer();
            }
            else
            {
                m_ResponsePacket.NewAccount = false;
            }
            
            m_Player.OnLine(session);
            return true;
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }

        private bool CheckAuthorize()
        {
            if (ConfigUtils.GetSetting("Environment") == "Development")
            {
                m_Player = new PlayerLogic();
                m_Player.SetUser(m_RequestPacket.AccountName);
                return true;
            }
            else
            {
                //TODO
                return false;
            }
        }
    }
}