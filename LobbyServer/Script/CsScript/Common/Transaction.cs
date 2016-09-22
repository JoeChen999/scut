using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    public class Transaction
    {
        private Dictionary<ISqlEntity, byte[]> m_Entities;
        private static ICacheSerializer m_Serializer = new ProtobufCacheSerializer();
        public Transaction()
        {
            m_Entities = new Dictionary<ISqlEntity, byte[]>();
        }

        public void DumpEntity(ISqlEntity entity)
        {
            if (m_Entities.ContainsKey(entity))
            {
                return;
            }
            else
            {
                m_Entities.Add(entity, m_Serializer.Serialize(entity));
            }
        }

        public void RollBack()
        {
            foreach (var kv in m_Entities)
            {
                ISqlEntity dumpedEntity = m_Serializer.Deserialize(kv.Value, kv.Key.GetType()) as ISqlEntity;
                foreach(var prop in kv.Key.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
                {
                    prop.SetValue(kv.Key, prop.GetValue(dumpedEntity));
                }
            }
        }

        public void Commit()
        {
            m_Entities = new Dictionary<ISqlEntity, byte[]>();
        }
    }
}
