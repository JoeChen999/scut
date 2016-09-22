using Genesis.GameServer.CommonLibrary;
using ZyGames.Framework.Cache.Generic;

namespace Genesis.GameServer.RoomServer
{
    public static class CacheSet
    {
        public static MemoryCacheStruct<Room> RoomCache = new MemoryCacheStruct<Room>();

        public static DataTableFactory<DTInstance> InstanceTable = new DataTableFactory<DTInstance>();
        public static DataTableFactory<DTDrop> DropTable = new DataTableFactory<DTDrop>();
        public static DataTableFactory<DTGear> GearTable = new DataTableFactory<DTGear>();
        public static DataTableFactory<DTHero> HeroTable = new DataTableFactory<DTHero>();
        public static DataTableFactory<DTHeroBase> HeroBaseTable = new DataTableFactory<DTHeroBase>();
        public static DataTableFactory<DTItem> ItemTable = new DataTableFactory<DTItem>();
        public static DataTableFactory<DTHeroConsciousnessBase> HeroConsciousnessBaseTable = new DataTableFactory<DTHeroConsciousnessBase>();
        public static DataTableFactory<DTHeroElevationBase> HeroElevationBaseTable = new DataTableFactory<DTHeroElevationBase>();
        public static DataTableFactory<DTGearLevelUp> GearLevelUpTable = new DataTableFactory<DTGearLevelUp>();
        public static DataTableFactory<DTPlayer> PlayerTable = new DataTableFactory<DTPlayer>();
        public static DataTableFactory<DTSoul> SoulTable = new DataTableFactory<DTSoul>();
        public static DataTableFactory<DTMeridian> MeridianTable = new DataTableFactory<DTMeridian>();
        public static DataTableFactory<DTEpigraph> EpigraphTable = new DataTableFactory<DTEpigraph>();
        public static DataTableFactory<DTSkillLevelUp> SkillLevelUpTable = new DataTableFactory<DTSkillLevelUp>();
        public static DataTableFactory<DTSinglePvpInstance> SinglePvpInstanceTable = new DataTableFactory<DTSinglePvpInstance>();
    }
}
