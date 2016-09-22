using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Redis;

namespace Genesis.GameServer.LobbyServer
{
    public static class GameConfigs
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
            if(m_Configs.Count <= 0)
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
            m_Configs.ModifyLocked(() =>{
                foreach (var data in allDatas)
                {
                    m_Configs[data.Key] = data.Value;
                }
            }); 
        }

        public static Dictionary<string, string> GetConfigs(int type)
        {
            Dictionary<string, string> ret = new Dictionary<string,string>();
            DBProvider db = new DBProvider("Game");
            var configs = db.Select("configs", "Name,Value", "Type=" + type.ToString() + " or Type=0");
            foreach (var config in configs)
            {
                ret.Add((string)config["Name"], (string)config["Value"]);
            }
            return ret;
        }

        public static Dictionary<string, string> GetAllConfigs()
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            DBProvider db = new DBProvider("Game");
            var configs = db.Select("configs", "Name,Value", "");
            foreach (var config in configs)
            {
                ret.Add((string)config["Name"], (string)config["Value"]);
            }
            return ret;
        }
    }
}
