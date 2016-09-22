using Genesis.GameServer.CommonLibrary;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.RoomServer
{

    public class GameConfigs
    {
        private static CacheDictionary<string, string> m_Configs = new CacheDictionary<string, string>();

        public static CacheDictionary<string, string> Configs
        {
            get
            {
                return m_Configs;
            }
        }

        public static string GetString(string name, string defaultValue = "")
        {
            if (m_Configs.Count <= 0)
            {
                Reload();
            }
            if (m_Configs.ContainsKey(name))
            {
                return m_Configs[name];
            }
            else
            {
                return defaultValue;
            }
        }

        public static int GetInt(string name, int defaultValue = 0)
        {
            if (m_Configs.Count <= 0)
            {
                Reload();
            }
            string s = GetString(name);
            if (s == string.Empty || s == "")
            {
                return defaultValue;
            }
            int retval;
            if (!int.TryParse(s, out retval))
            {
                return defaultValue;
            }
            return retval;
        }

        public static float GetFloat(string name, float defaultValue = 0f)
        {
            if (m_Configs.Count <= 0)
            {
                Reload();
            }
            string s = GetString(name);
            if (s == string.Empty || s == "")
            {
                return defaultValue;
            }
            float retval;
            if (!float.TryParse(s, out retval))
            {
                return defaultValue;
            }
            return retval;
        }

        public static void Reload()
        {
            Dictionary<string, string> allDatas = GetAllConfigs();
            m_Configs.ModifyLocked(() =>
            {
                foreach (var data in allDatas)
                {
                    m_Configs[data.Key] = data.Value;
                }
            });
        }

        private static Dictionary<string, string> GetAllConfigs()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            RequestParam param = new RequestParam();
            LobbyServerSender.Send("RLGetGameConfigsHandler", param, callback => {
                var res = RemoteCommunication.ParseRemotePackage<LRGetGameConfigs>(callback.Message as byte[]);
                for(int i = 0; i < res.Keys.Count; i++)
                {
                    ret.Add(res.Keys[i], res.Values[i]);
                }
            });
            return ret;
        }
    }
}
