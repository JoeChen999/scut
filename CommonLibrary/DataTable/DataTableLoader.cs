using System;
using System.IO;
using System.Reflection;
using ZyGames.Framework.Script;

namespace Genesis.GameServer.CommonLibrary
{
    public class DataTableLoader
    {
        private static readonly string[] ColumnSplit = new string[] { "\t" };
        private static string s_ServerType;

        public static void LoadDataTables(string serverType)
        {
            s_ServerType = serverType;
            DirectoryInfo dtFolder = new DirectoryInfo("DataTables");
            foreach (FileInfo dtFile in dtFolder.GetFiles())
            {
                LoadDataTableFile(dtFile);
            }
        }

        public static void LoadDataTableFile(FileInfo dtFile)
        {
            FileStream fp = dtFile.Open(FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fp);
            string type = Path.GetFileNameWithoutExtension(dtFile.Name);
            Type dtType = ScriptEngines.RuntimeScope.ModelAssembly.GetType("Genesis.GameServer."+ s_ServerType +"Server.DT" + type);
            if (dtType == null)
            {
                fp.Close();
                return;
            }
            Console.WriteLine("Loading" + dtFile.ToString());
            while (!sr.EndOfStream)
            {
                string dtLine = sr.ReadLine();
                if (dtLine.IndexOf("#") >= 0 || string.IsNullOrEmpty(dtLine))
                {
                    continue;
                }
                IDataTable dataTable = Activator.CreateInstance(dtType) as IDataTable;
                dataTable.ParseRow(dtLine.Split(ColumnSplit, StringSplitOptions.None));
            }
            fp.Close();
        }

        public static void LoadDataTableFile(FileInfo dtFile, Type dtType)
        {
            FileStream fp = dtFile.Open(FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fp);
            while (!sr.EndOfStream)
            {
                string dtLine = sr.ReadLine();
                if (dtLine.IndexOf("#") >= 0 || string.IsNullOrEmpty(dtLine))
                {
                    continue;
                }
                IDataTable dataTable = Activator.CreateInstance(dtType) as IDataTable;
                dataTable.ParseRow(dtLine.Split(ColumnSplit, StringSplitOptions.None));
            }
            fp.Close();
        }
    }
}
