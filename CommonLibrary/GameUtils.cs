using System;
using System.Collections.Generic;

namespace Genesis.GameServer.CommonLibrary
{
    public static class GameUtils
    {
        public static IDictionary<int, int> MergeItemDict(IDictionary<int, int> source, IDictionary<int, int> target)
        {
            foreach (var kv in target)
            {
                if (source.ContainsKey(kv.Key))
                {
                    source[kv.Key] += kv.Value;
                }
                else
                {
                    source.Add(kv.Key, kv.Value);
                }
            }
            return source;
        }

        public static IDictionary<int, int> MergeItem(IDictionary<int, int> source, int targetType, int targetCount)
        {
            if (source.ContainsKey(targetType))
            {
                source[targetType] += targetCount;
            }
            else
            {
                source.Add(targetType, targetCount);
            }
            return source;
        }

        public static int[] RandomChoose(int startIndex, int endIndex, int count)
        {
            List<int> characters = new List<int>();
            for (int i = startIndex; i <= endIndex; i++)
            {
                characters.Add(i);
            }
            return RandomChoose(characters, count);
        }

        public static T[] RandomChoose<T>(IList<T> characters, int count)
        {
            Random r = new Random();
            T[] res = new T[count];
            for (int i = 0; i < count; i++)
            {
                int index = r.Next(i, characters.Count);
                T temp = characters[i];
                res[i] = characters[index];
                characters[i] = characters[index];
                characters[index] = temp;
            }
            return res;
        }

        public static bool NeedRefresh(long lastRefreshTime, int NeedRefreshHour)
        {
            if (DateTime.UtcNow.Ticks - lastRefreshTime > TimeSpan.TicksPerDay)
            {
                return true;
            }
            long refreshTime;
            if (DateTime.UtcNow.Hour >= NeedRefreshHour)
            {
                refreshTime = DateTime.UtcNow.AddHours(NeedRefreshHour - DateTime.UtcNow.Hour).Ticks;
            }
            else
            {
                refreshTime = DateTime.UtcNow.AddHours(NeedRefreshHour - DateTime.UtcNow.Hour).AddDays(-1).Ticks;
            }
            if (lastRefreshTime < refreshTime)
            {
                return true;
            }
            return false;
        }

        public static void PrintBuddha()
        {
            Console.WriteLine("                  _oo0oo_\n                 o8888888o\n                 88\" . \"88\n                 (| -_- |)\n                 0\\  =  /0  \n               ___/'___'\\___\n             .' \\\\|     |// '.\n            / \\\\|||  :  |||// \\ \n           / _||||| -:- |||||- \\ \n          |   | \\\\\\  -  /// |   |\n          | \\_|  ''\\---/''  |_/ |\n          \\  .-\\__  '_'  ___/-. /\n        ___'. .'  /--.--\\  `. .'___\n     .\"\" '<  `.___\\_<|>_/___.' >' \"\".\n    | | :  `- \\`.;`\\ _ /`;.`/ -`  : | | \n    \\  \\ `_.   \\_ __\\ /__ _/    .-' / /\n=====`-.____`.___ \\_____/___.-`____.-'=====\n                  `=---='\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n            佛祖保佑    永无BUG");
        }
    }
}
