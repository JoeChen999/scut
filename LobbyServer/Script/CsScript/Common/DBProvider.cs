using System.Collections.Generic;
using System.Data;
using ZyGames.Framework.Data;

namespace Genesis.GameServer.LobbyServer
{
    public class DBProvider
    {
        private static Dictionary<string, DbBaseProvider> m_DBs = new Dictionary<string,DbBaseProvider>();

        private DbBaseProvider m_DB;
        public DBProvider(string connKey)
        {
            if (!m_DBs.ContainsKey(connKey))
            {
                m_DBs[connKey] = DbConnectionProvider.CreateDbProvider(connKey);
            }
            m_DB = m_DBs[connKey];
        }

        public List<Dictionary<string, object>> Select(string tableName, string columns, string condition = "", string orderby = "")
        {
            List<Dictionary<string, object>> retList = new List<Dictionary<string, object>>();
            string[] fieldList = columns.Split(',');
            var command = m_DB.CreateCommandStruct(tableName, CommandMode.Inquiry);
            command.Columns = columns;
            command.Filter.Condition = condition;
            command.OrderBy = orderby;
            command.Parser();
            using (var dr = m_DB.ExecuteReader(CommandType.Text, command.Sql, command.Parameters))
            {
                while (dr.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    foreach (string field in fieldList)
                    {
                        row[field] = dr[field];
                    }
                    retList.Add(row);
                }
            }
            return retList;
        }

        public int Delete(string tableName, string condition)
        {
            var command = m_DB.CreateCommandStruct(tableName, CommandMode.Delete);
            command.Filter.Condition = condition;
            command.Parser();
            return m_DB.ExecuteQuery(CommandType.Text, command.Sql, command.Parameters);
        }

        public int InsertOrUpdate(string tableName, Dictionary<string, object> parameters, string condition)
        {
            var command = m_DB.CreateCommandStruct(tableName, CommandMode.ModifyInsert);
            return InsertOrUpdate(command, parameters, condition);
        }

        public int Insert(string tableName, Dictionary<string, object> parameters, string condition = "")
        {
            var command = m_DB.CreateCommandStruct(tableName, CommandMode.Insert);
            return InsertOrUpdate(command, parameters, condition);
        }

        public int Update(string tableName, Dictionary<string, object> parameters, string condition)
        {
            var command = m_DB.CreateCommandStruct(tableName, CommandMode.Modify);
            return InsertOrUpdate(command, parameters, condition);
        }

        private int InsertOrUpdate(CommandStruct command, Dictionary<string, object> parameters, string condition)
        {
            foreach(var param in parameters)
            {
                command.AddParameter(param.Key, param.Value);
            }
            command.Filter.Condition = condition;
            command.Parser();
            return m_DB.ExecuteQuery(CommandType.Text, command.Sql, command.Parameters);
        }
    }
}
