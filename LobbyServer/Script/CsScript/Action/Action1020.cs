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
    public class Action1020 : AuthorizeAction
    {
        private CLChangeEpigraph m_RequestPacket;
        private LCChangeEpigraph m_ResponsePacket;
        private int m_UserId;

        public Action1020(ActionGetter actionGetter)
            : base((short)1020, actionGetter)
        {
            m_RequestPacket = null;
            m_ResponsePacket = new LCChangeEpigraph();
            m_UserId = 0;
        }

        public override bool GetUrlElement()
        {
            m_UserId = actionGetter.GetSession().UserId;
            m_RequestPacket = ProtoBufUtils.Deserialize<CLChangeEpigraph>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            PlayerEpigraphLogic pe = new PlayerEpigraphLogic();
            pe.SetUser(m_UserId);
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            if (m_RequestPacket.DressedEpigraph != null)
            {
                int type = m_RequestPacket.DressedEpigraph.Type;
                int level = m_RequestPacket.DressedEpigraph.Level;
                if (!pp.DeductEpigraph(type, level))
                {
                    ErrorCode = (int)ErrorType.RequireNotMet;
                    ErrorInfo = "you do not have this epigraph";
                    return false;
                }
                pe.DressEpigraph(type, level, m_RequestPacket.Index);
                m_ResponsePacket.Index = m_RequestPacket.Index;
                m_ResponsePacket.DressedEpigraph = m_RequestPacket.DressedEpigraph;
                return true;
            }
            else if (m_RequestPacket.UndressedEpigraph != null)
            {
                int type = m_RequestPacket.UndressedEpigraph.Type;
                int level = m_RequestPacket.UndressedEpigraph.Level;
                if (!pe.UndressEpigraph(type, level, m_RequestPacket.Index))
                {
                    ErrorCode = (int)ErrorType.RequireNotMet;
                    ErrorInfo = "you do not have this epigraph";
                    return false;
                }
                pp.AddEpigraph(type, level);
                m_ResponsePacket.Index = m_RequestPacket.Index;
                m_ResponsePacket.UndressedEpigraph = m_RequestPacket.UndressedEpigraph;
                return true;
            }
            else
            {
                int type = m_RequestPacket.UndressedEpigraph.Type;
                int level = m_RequestPacket.UndressedEpigraph.Level;
                if (!pe.UndressEpigraph(type, level, m_RequestPacket.Index))
                {
                    ErrorCode = (int)ErrorType.RequireNotMet;
                    ErrorInfo = "you do not have this epigraph";
                    return false;
                }
                pe.DressEpigraph(type, level, m_RequestPacket.Index);
                m_ResponsePacket.Index = m_RequestPacket.Index;
                m_ResponsePacket.DressedEpigraph = m_RequestPacket.DressedEpigraph;
                m_ResponsePacket.UndressedEpigraph = m_RequestPacket.UndressedEpigraph;
                return true;
            }
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }
    }
}
