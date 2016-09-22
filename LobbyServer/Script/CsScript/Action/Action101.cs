using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Context;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.Model;
using ZyGames.Framework.Script;

namespace Genesis.GameServer.LobbyServer
{
    public class Action101 : BaseStruct
    {
        private CLServerCommand m_RequestPacket;
        private LCServerCommand m_ResponsePacket;

        public Action101(ActionGetter actionGetter)
            : base((short)101, actionGetter)
        {
            m_RequestPacket = new CLServerCommand();
            m_ResponsePacket = new LCServerCommand();
        }

        public override bool GetUrlElement()
        {
            m_RequestPacket = ProtoBufUtils.Deserialize<CLServerCommand>((byte[])actionGetter.GetMessage());
            return true;
        }

        public override bool TakeAction()
        {
            switch (m_RequestPacket.Type)
            {
                case 1:
                    DataTableLoader.LoadDataTables("Lobby");
                    m_ResponsePacket.Success = true;
                    break;
                case 2:
                    FlushKey(m_RequestPacket.Params);
                    break;
                case 3:
                    GameConfigs.Reload();
                    m_ResponsePacket.Success = true;
                    break;
                case 4:
                    GetOnlinePlayerCount();
                    break;
                case 5:
                    ScriptEngines.Initialize();
                    break;
                default:
                    return false;
            }
            
            return true;
        }

        private void FlushKey(string param)
        {
            Type modelType = ScriptEngines.RuntimeScope.ModelAssembly.GetType("Genesis.GameServer.LobbyServer." + param);
            Type cacheType;
            if (modelType.BaseType == typeof(BaseEntity))
            {
                cacheType = typeof(PersonalCacheStruct<>);
                
            }
            else if(modelType.BaseType == typeof(ShareEntity))
            {
                cacheType = typeof(ShareCacheStruct<>);
            }
            else
            {
                return;
            }
            cacheType = cacheType.MakeGenericType(modelType);
            var cacheObject = Activator.CreateInstance(cacheType);
            var method = cacheType.GetMethod("UnLoad");
            method.Invoke(cacheObject, null);
        }

        private void GetOnlinePlayerCount()
        {
            LCServerCommand response = new LCServerCommand()
            {
                Success = true,
                Info = LobbyServerUtils.GetOnlinePlayerCount().ToString()
            };
            byte[] buffer = ProtoBufUtils.Serialize(response);
            actionGetter.GetSession().SendAsync(buffer, 0, buffer.Length);
        }

        public override void WriteResponse(BaseGameResponse response)
        {
            CustomActionDispatcher.ResponseOK(response, actionGetter, ProtoBufUtils.Serialize(m_ResponsePacket));
        }

        public override bool CheckAction()
        {
            string address = actionGetter.GetSession().RemoteAddress;
            TraceLog.WriteInfo("request from address:" + address);
            if (address.IndexOf("127.0.0.1") < 0 && address.IndexOf("192.168") < 0)
            {
                return false;
            }
            return true;
        }
    }
}

