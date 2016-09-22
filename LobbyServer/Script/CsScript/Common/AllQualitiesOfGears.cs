using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    public static class AllQualitiesOfGears
    {
        private static Dictionary<GearQuality, List<DTGear>> m_AllQualitiesOfGearsCache = new Dictionary<GearQuality, List<DTGear>>()
        {
            { GearQuality.White, new List<DTGear>() },
            { GearQuality.Green, new List<DTGear>() },
            { GearQuality.Blue, new List<DTGear>() },
            { GearQuality.Purple, new List<DTGear>() },
            { GearQuality.Orange, new List<DTGear>() }
        };

        public static void LoadGears()
        {
            var allGears = CacheSet.GearTable.GetAllData();
            foreach(var gear in allGears)
            {
                m_AllQualitiesOfGearsCache[(GearQuality)gear.Quality].Add(gear);
            }
        }

        public static List<DTGear> GetSpecifiedQualityGears(GearQuality quality)
        {
            return m_AllQualitiesOfGearsCache[quality];
        }
    }
}
