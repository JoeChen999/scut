using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.CommonLibrary
{
    public class DataTableFactory<T> where T : MemoryEntity, new()
    {
        private MemoryCacheStruct<T> m_Table;

        public DataTableFactory()
        {
            m_Table = new MemoryCacheStruct<T>();
        }

        public int Count
        {
            get
            {
                return m_Table.Count;
            }
        }

        public T GetData(int key)
        {
            T data;
            m_Table.TryGet(key.ToString(), out data);
            return data;
        }

        public T GetData(Predicate<T> match)
        {
            return m_Table.Find(match);
        }

        public List<T> GetAllData()
        {
            return m_Table.FindAll();
        }

        public List<T> GetAllData(Predicate<T> match)
        {
            return m_Table.FindAll(match);
        }

        public void TryAdd(string Id, T data)
        {
            m_Table.TryAdd(Id, data);
        }
    }
}
