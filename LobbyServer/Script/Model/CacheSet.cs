using Genesis.GameServer.CommonLibrary;
using ZyGames.Framework.Cache.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public static class CacheSet
    {
        //share Cache
        public static ShareCacheStruct<Gears> GearCache = new ShareCacheStruct<Gears>();
        public static ShareCacheStruct<Souls> SoulCache = new ShareCacheStruct<Souls>();
        public static ShareCacheStruct<HeroTeam> HeroTeamCache = new ShareCacheStruct<HeroTeam>();   
        public static ShareCacheStruct<Player> PlayerCache = new ShareCacheStruct<Player>();      
        public static ShareCacheStruct<RoomServer> RoomServerCache = new ShareCacheStruct<RoomServer>();
        public static ShareCacheStruct<FoundryRoom> FoundryRoomCache = new ShareCacheStruct<FoundryRoom>();
        public static ShareCacheStruct<ArenaRank> ArenaRankCache = new ShareCacheStruct<ArenaRank>();
        public static ShareCacheStruct<PlayerSinglePvp> PlayerSinglePvpCache = new ShareCacheStruct<PlayerSinglePvp>();
        //personal cache
        public static PersonalCacheStruct<PlayerPackage> playerPackageCache = new PersonalCacheStruct<PlayerPackage>();
        public static PersonalCacheStruct<PlayerInstanceDrop> InstanceDropCache = new PersonalCacheStruct<PlayerInstanceDrop>();
        public static PersonalCacheStruct<PlayerHeros> PlayerHeroCache = new PersonalCacheStruct<PlayerHeros>();
        public static PersonalCacheStruct<InstanceProgress> InstanceProgressCache = new PersonalCacheStruct<InstanceProgress>();
        public static PersonalCacheStruct<PlayerChess> PlayerChessCache = new PersonalCacheStruct<PlayerChess>();
        public static PersonalCacheStruct<PlayerMeridian> PlayerMeridianCache = new PersonalCacheStruct<PlayerMeridian>();
        public static PersonalCacheStruct<PlayerEpigraph> PlayerEpigraphCache = new PersonalCacheStruct<PlayerEpigraph>();
        public static PersonalCacheStruct<PlayerChat> PlayerChatCache = new PersonalCacheStruct<PlayerChat>();
        public static PersonalCacheStruct<PlayerFriends> PlayerFriendsCache = new PersonalCacheStruct<PlayerFriends>();
        public static PersonalCacheStruct<PlayerMail> PlayerMailCache = new PersonalCacheStruct<PlayerMail>();
        public static PersonalCacheStruct<PlayerFoundry> PlayerFoundryCache = new PersonalCacheStruct<PlayerFoundry>();
        public static PersonalCacheStruct<PlayerCoinChance> PlayerCoinChanceCache = new PersonalCacheStruct<PlayerCoinChance>();
        public static PersonalCacheStruct<PlayerMoneyChance> PlayerMoneyChanceCache = new PersonalCacheStruct<PlayerMoneyChance>();
        public static PersonalCacheStruct<PlayerStoryInstance> PlayerStoryInstanceCache = new PersonalCacheStruct<PlayerStoryInstance>();
        public static PersonalCacheStruct<PlayerArena> PlayerArenaCache = new PersonalCacheStruct<PlayerArena>();
        public static PersonalCacheStruct<PlayerDefaultShop> PlayerDefaultShopCache = new PersonalCacheStruct<PlayerDefaultShop>();
        public static PersonalCacheStruct<PlayerNearbyPosition> PlayerNearbyPositionCache = new PersonalCacheStruct<PlayerNearbyPosition>();
        public static PersonalCacheStruct<PlayerCosmosCrack> PlayerCosmosCrackCache = new PersonalCacheStruct<PlayerCosmosCrack>();
        public static PersonalCacheStruct<PlayerVipShop> PlayerVipShopCache = new PersonalCacheStruct<PlayerVipShop>();
        public static PersonalCacheStruct<PlayerAchievement> PlayerAchievementCache = new PersonalCacheStruct<PlayerAchievement>();
        public static PersonalCacheStruct<PlayerDailyQuest> PlayerDailyQuestCache = new PersonalCacheStruct<PlayerDailyQuest>();
        public static PersonalCacheStruct<PlayerSevenDayCheckIn> PlayerSevenDayCheckInCache = new PersonalCacheStruct<PlayerSevenDayCheckIn>();
        //memory cache
        public static MemoryCacheStruct<RoomList> RoomListCache = new MemoryCacheStruct<RoomList>();
        public static MemoryCacheStruct<PVPMatchQueue> PVPMathcQueueCache = new MemoryCacheStruct<PVPMatchQueue>();
        //DataTables
        public static DataTableFactory<DTInstance> InstanceTable = new DataTableFactory<DTInstance>();
        public static DataTableFactory<DTDrop> DropTable = new DataTableFactory<DTDrop>();
        public static DataTableFactory<DTGear> GearTable = new DataTableFactory<DTGear>();
        public static DataTableFactory<DTHero> HeroTable = new DataTableFactory<DTHero>();
        public static DataTableFactory<DTHeroBase> HeroBaseTable = new DataTableFactory<DTHeroBase>();
        public static DataTableFactory<DTItem> ItemTable = new DataTableFactory<DTItem>();
        public static DataTableFactory<DTHeroConsciousnessBase> HeroConsciousnessBaseTable = new DataTableFactory<DTHeroConsciousnessBase>();
        public static DataTableFactory<DTHeroElevationBase> HeroElevationBaseTable = new DataTableFactory<DTHeroElevationBase>();
        public static DataTableFactory<DTChance> ChanceTable = new DataTableFactory<DTChance>();
        public static DataTableFactory<DTChanceCost> ChanceCostTable = new DataTableFactory<DTChanceCost>();
        public static DataTableFactory<DTGearLevelUp> GearLevelUpTable = new DataTableFactory<DTGearLevelUp>();
        public static DataTableFactory<DTPlayer> PlayerTable = new DataTableFactory<DTPlayer>();
        public static DataTableFactory<DTSoul> SoulTable = new DataTableFactory<DTSoul>();
        public static DataTableFactory<DTMeridian> MeridianTable = new DataTableFactory<DTMeridian>();
        public static DataTableFactory<DTEpigraph> EpigraphTable = new DataTableFactory<DTEpigraph>();
        public static DataTableFactory<DTInstanceStory> InstanceStoryTable = new DataTableFactory<DTInstanceStory>();
        public static DataTableFactory<DTRandomShop> RandomShopTable = new DataTableFactory<DTRandomShop>();
        public static DataTableFactory<DTSkillLevelUp> SkillLevelUpTable = new DataTableFactory<DTSkillLevelUp>();
        public static DataTableFactory<DTVipShop> VipShopTable = new DataTableFactory<DTVipShop>();
        public static DataTableFactory<DTActivity> ActivityTable = new DataTableFactory<DTActivity>();
        public static DataTableFactory<DTInstanceCosmosCrack> InstanceCosmosCrackTable = new DataTableFactory<DTInstanceCosmosCrack>();
        public static DataTableFactory<DTMightLevelParam> MightLevelParamTable = new DataTableFactory<DTMightLevelParam>();
        public static DataTableFactory<DTAchievement> AchievementTable = new DataTableFactory<DTAchievement>();
        public static DataTableFactory<DTDailyQuest> DailyQuestTable = new DataTableFactory<DTDailyQuest>();
        public static DataTableFactory<DTPvpTitle> PvpTitleTable = new DataTableFactory<DTPvpTitle>();
        public static DataTableFactory<DTOperationActivity> OperationActivityTable = new DataTableFactory<DTOperationActivity>();
        public static DataTableFactory<DTOperationActivitySevenDay> OperationActivitySevenDayTable = new DataTableFactory<DTOperationActivitySevenDay>();
    }
}
