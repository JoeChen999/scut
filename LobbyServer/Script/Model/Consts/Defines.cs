using ProtoBuf;
using System;

namespace Genesis.GameServer.LobbyServer
{
    public enum RoomServerState
    {
        Normal,
        Exception,
    }

    public enum RoomState
    {
        Waiting,
        Ready,
        Fighting,
        Closed,
    }

    public enum GearType
    {
        Weapon = 1,
        Helmet,
        Armor,
        Shoes,
        Ring,
        Necklace
    }

    public enum GearQuality
    {
        White = 1,
        Green,
        Blue,
        Purple,
        Orange
    }

    public enum ErrorType
    {
        WrongInput = 1000,
        EmptyInput = 1001,
        DuplicateName = 1002,
        EnergyNotEnough = 1003,
        PlayerNotInInstance = 1004,
        CannotManuallyRefreshChance = 1005,
        CannotOpenChance = 1006,
        PlayerNotExist = 1007,
        ChatTooFrequency = 1008,
        CanNotCreateRoom = 1009,
        PlayerNotOnline = 1010,
        RequireNotMet = 1011,
        HaveBadWord = 1012,
        PackageSlotFull = 1013,
        ItemCountFull = 1014,
        ActivityNotOpenNow = 1015,
        CannotOpenEmail = 1015,
        CannotMakeFriendRequest = 2011,
        CannotStopMatching = 3001,
    }

    public enum GMCommandType
    {
        AddMoney = 1,
        AddCoin,
        AddItem,
        AddGear,
        AddHero,
        AddSoul,
        AddEnergy,
        HeroLevelUp,
        PlayerLevelUp,
        MeridianEnergyUp,
        AddEpigraphItem,
        AddSpirit,
        GetOnlinePlayerCount,
        AddArenaToken,
        AddPvpToken,
    }

    public enum ItemFunctions
    {
        AddHeroExp = 1,
        HeroPiece = 3,
        AddHero = 99,
    }

    public enum GiftItemType
    {
        Coin = 1,
        Money = 2,
        Energy = 3,
        MeridianEnergy = 4,
        Spirit = 5,
        DragonStripeToken = 6
    }

    public enum ChessFieldColor
    {
        Empty,
        EmptyGray,
        Red,
        Yellow,
        Green,
        RewardGray,
    }

    public enum ChatType
    {
        World = 1,
        Private,
        Alliance,
        Room
    }

    public enum ChanceType
    {
        Coin = 0,
        Money = 1,
    }

    public enum RankType
    {
        Arena = 1,
        Power = 2,
        Level = 3,
    }
    
    public enum RandomDropSetType
    {
        RandomWhiteGear = -101,
        RandomGreenGear = -102,
        RandomBlueGear = -103,
        RandomPurpleGear = -104,
        RandomOrangeGear = -105
    }

    public enum ActivityType
    {
        TurnOverChess = 1,
        GearFoundry = 2,
        OfflineArena = 3,
        CosmosCrack = 4,
    }

    public enum ActivityStatusType
    {
        Open = 0,
        Close = 1,
        Locked = 2,
        Hidden = 3
    }
    public enum ActivityRepeatType
    {
        NotRepeat = 0,
        daily = 1,
        Weekly = 200,
        Monthly = 300,
    }

    public enum PVPType
    {
        Single = 1,
        Triple = 3,
    }

    public enum SoulEffect
    {
        MaxHP = 1,
        PhysicalAttack,
        MagicAttack,
        PhysicalDefense,
        MagicDefense,
        CriticalHitProb,
        CriticalHitRate,
        PhysicalAtkHPAbsorbRate,
        MagicAtkHPAbsorbRate,
        AntiCriticalHitProb,
        OppPhysicalDfsReduceRate,
        OppMagicDfsReduceRate,
        PhysicalAtkReflectRate,
        MagicAtkReflectRate,
        RecoverHP,
    }

    public enum AchievementType
    {
        PlayerLevel = 1,
        HeroLevel,
        HeroCount,
        HeroStarLevel,
        HeroMight,
        HeroElevationLevel,
        HeroConsiousnessLevel,
        HeroSkillLevel,
        GearQuality,
        GearCount,
        GearStrengthenLevel,
        GearLevel,
        FriendCount,
        PvpWinCount,
        InstanceCompletedCount,
        OpenedMoneyChanceCount,
        CostedCoin,
        CostedMoney,
    }

    public enum DailyQuestType
    {
        CompleteInstance = 1,
        CleanOutInstance,
        CompleteOfflineArena,
        CompleteSinglePvp,
        HasMonthlyCard,
        WinTurnOverChessBattle,
        ClaimGearFoundryReward,
        CompleteCosmosCrack,
        GiftEnergyToFriend,
    }

    public enum AnnounceType
    {
        ReceiveGear = 1,
        LevelUp,
        VipLevelUpLow,
        VipLevelUpHigh,
        ReceiveHero,
        HeroStarLevelUp,
        HeroMightLow,
        HeroMightHigh,
        GearStrengthenLow,
        GearStrengthenHigh,
        HeroConsicousnesslow,
        HeroConsicousnessHigh,
        HeroElevation,
        OpenCoinChance,
        OpenMoneyChance,
        SystemAnnouncement,
        Advertisement,
    }

    public enum ReceiveItemMethodType
    {
        None = 0,
        CoinChance = 1,
        MoneyChance,
        GearFoundry,
        GearCompose,
        HeroPieceCompose,
        TurnOverChess,
        CompleteInstance,
        PvpTokenExchange,
    }

    public enum PlayerStatus
    {
        UnKnown = 0,
        Online = 1,
        Offline = 2,
    }

    public enum PackageFullReason
    {
        None = 0,
        SlotFull = 1,
        ItemFull = 2,
    }
}
