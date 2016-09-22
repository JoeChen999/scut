
using System.Collections.Generic;
namespace Genesis.GameServer.LobbyServer
{
    public static class GameConsts
    {
        public const int DropItemTotalWeight = 100000;
        public const long TicksPerSecond = 10000000;
        public const int PlayerInitialCoin = 10000;
        public const int PlayerInitialMoney = 100;
        public const int InstanceCostEnergy = 6;
        public const int EnergyRecoverMinutes = 10;
        public const int UnlockNextMeridianStarCount = 27;

        public const int MaxRoomCount = 100;
        

        public const int MinGearId = 110000;
        public const int MaxGearId = 169999;
        public const int MinSoulId = 170000;
        public const int MaxSoulId = 179999;
        public const int MinEpigraphId = 180000;
        public const int MaxEpigraphId = 189999;

        public const int MaxLevel = 100;
        public const int MaxEpigraphSlot = 8;
        public const int MaxEpigraphLevel = 5;

        public static class Gear
        {
            public const int MaxStrengthenLevel = 5;
            public static readonly Dictionary<int, int[]> StrengthenSuccessRate = new Dictionary<int, int[]>()
            {
                {3, new int[5]{100, 90, 80, 70, 60} },
                {4, new int[5]{80, 70, 60, 50, 40} },
                {5, new int[5]{60, 50, 40, 30, 20} }
            };
        }

        public static class Package
        {
            public const int MaxPackageSlotCount = 200;
            public const int MaxItemCount = 9999;
        }

        public static class PlayerChess
        {
            public const int ChessBoardSize = 100;
            public const int DailyPlayCount = 15;
            public const int EmptyGrayFieldCount = 16;
            public const int RewardGrayFieldCount = 64;
            public const int EmptyFieldCount = 5;
            public const int RedFieldCount = 5;
            public const int YellowFieldCount = 5;
            public const int GreenFieldCount = 5;
            public const int RedFieldMinCount = 8;
            public const int RedFieldMaxCount = 11;
            public const int YellowFieldMinCount = 5;
            public const int YellowFieldMaxCount = 8;
            public const int GreenFieldMinCount = 2;
            public const int GreenFieldMaxCount = 5;
            public const int ChessBoardWidth = 10;
            public const int MidRewardDropId = 2001;
            public const int TopRewardDropId = 2002;
            public const int MidRewardRate = 80;
            public const int TopRewardRate = 5;
        }

        public static class Hero
        {
            public const int MaxConsciousnessLevel = 80;
            public const int MaxStarLevel = 5;
            public const int ElevationLevelUpGearCount = 4;
            public const int MaxHeroTeamCount = 3;
            public const int MaxSoulSlot = 6;
            public const float OppPhysicalDfsReduceRateMaxVal = .4f;
            public const float OppMagicDfsReduceRateMaxVal = .4f;
            public const float PhysicalAtkHPAbsorbRateMaxVal = .3f;
            public const float MagicAtkHPAbsorbRateMaxVal = .3f;
            public const float PhysicalAtkReflectRateMaxVal = .3f;
            public const float MagicAtkReflectRateMaxVal = .3f;
            public const float DamageReductionRateMaxVal = .5f;
            public const float CriticalHitProbMaxVal = .3f;
            public const float CriticalHitRateMaxVal = 2f;
            public const float AntiCriticalHitProbMaxVal = .3f;
            public const float ReducedSkillCoolDownRateMaxVal = .3f;
        }

        public static class Foundry
        {
            public const int MaxFoundryLevel = 3;
            public const int MaxFoundryPlayerCount = 5;
            public static readonly List<int[]> FoundryRewardDropPackageId = new List<int[]>()
            {
                new int[3]{40,40,40},
                new int[3]{40,40,40},
                new int[3]{40,40,40}
            };
        }

        public static class Social
        {
            public const int RecommendationListCount = 5;
            public const int MaxSearchResultCount = 50;
            public const int MaxOnlineCount = 50;
            public const int MaxFriendCount = 50;
            public const int RefreshTime = 20;
            public const int MaxReceiveCount = 5;
            public const int MaxSendCount = 5;
        }

        public static class Chance
        {
            public const int MinCoinChancePackId = 1;
            public const int MaxCoinChancePackId = 10;
            public const int MinMoneyChancePackId = 11;
            public const int MaxMoneyChancePackId = 20;
            public const int MaxFreeCountForCoinChance = 5;
            public const int FreeCoinChanceCDSeconds = 5;
            public const int FreeMoneyChanceCDSeconds = 85600;
            public const int CoinChanceCount = 10;
            public const int MoneyChanceCount = 10;
        }

        public static class Arena
        {
            public const int RobotCount = 50;
            public const int MatchPlayerCount = 5;
            public const int BattleReportCount = 5;
            public const int DailyChallengeCount = 5;
            public const int SearchRangeBelow200 = 50;
            public const int SearchRangeBetween201To1000 = 200;
            public const int SearchRangeBeyond1000 = 500;
        }

        public static class Shop
        {
            public static readonly Dictionary<int, int> ShopItemCount = new Dictionary<int, int>()
            {
                {(int)GiftItemType.Money, 2},
                {(int)GiftItemType.Coin, 2},
                {(int)GiftItemType.Spirit, 2}
            };
        }

        public static class Pvp
        {
            public const int PvpPlayCount = 5;
            public const string PvpSeasonCountKey = "Pvp_Season_Count";
            public const string PvpSeasonOfYearKey = "Pvp_Year_Of_Season";
        }

        public static class DailyQuest
        {
            public const int RefreshTime = 20;
        }
    }
}
