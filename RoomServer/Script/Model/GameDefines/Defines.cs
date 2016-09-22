using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.GameServer.RoomServer
{
    public enum GearType
    {
        Weapon = 1,
        Helmet,
        Armor,
        Shoes,
        Ring,
        Necklace
    }

    public enum ErrorType
    {
        WrongToken = 1001,
        WrongUserId = 1002,
    }

    public enum RoomPlayerState
    {
        Winned = 1,
        Failed = 2,
        Draw = 3,
        WaitingConnect = 4,
        Playing = 5,
        Exited = 6,
    }

    public enum SoulEffect
    {
        /// <summary>
        /// 血量上限。
        /// </summary>
        MaxHP = 1,

        /// <summary>
        /// 物理攻击。
        /// </summary>
        PhysicalAttack = 2,

        /// <summary>
        /// 法术攻击。
        /// </summary>
        MagicAttack = 3,

        /// <summary>
        /// 物理防御。
        /// </summary>
        PhysicalDefense = 4,

        /// <summary>
        /// 法术防御。
        /// </summary>
        MagicDefense = 5,

        /// <summary>
        /// 物理攻击加成。
        /// </summary>
        PhysicalAtkIncreaseRate = 6,

        /// <summary>
        /// 法术攻击加成。
        /// </summary>
        MagicAtkIncreaseRate = 7,

        /// <summary>
        /// 物理防御加成。
        /// </summary>
        PhysicalDfsIncreaseRate = 8,

        /// <summary>
        /// 法术防御加成。
        /// </summary>
        MagicDfsIncreaseRate = 9,

        /// <summary>
        /// 血量上限加成。
        /// </summary>
        MaxHPIncreaseRate = 10,

        /// <summary>
        /// 怒气上涨速率。
        /// </summary>
        AngerIncreaseRate = 11,

        /// <summary>
        /// 真实伤害。
        /// </summary>
        AdditionalDamage = 12,

        /// <summary>
        /// 伤害减免百分比。
        /// </summary>
        DamageReductionRate = 13,

        /// <summary>
        /// 冷却时间缩减百分比。
        /// </summary>
        ReducedSkillCoolDownRate = 14,

        /// <summary>
        /// 暴击率。
        /// </summary>
        CriticalHitProb = 15,

        /// <summary>
        /// 暴击伤害。
        /// </summary>
        CriticalHitRate = 16,

        /// <summary>
        /// 物理反击。
        /// </summary>
        PhysicalAtkReflectRate = 17,

        /// <summary>
        /// 法术反击。
        /// </summary>
        MagicAtkReflectRate = 18,

        /// <summary>
        /// 物理穿透。
        /// </summary>
        OppPhysicalDfsReduceRate = 19,

        /// <summary>
        /// 法术穿透。
        /// </summary>
        OppMagicDfsReduceRate = 20,

        /// <summary>
        /// 免爆率。
        /// </summary>
        AntiCriticalHitProb = 21,

        /// <summary>
        /// 物理吸血。
        /// </summary>
        PhysicalAtkHPAbsorbRate = 22,

        /// <summary>
        /// 法术吸血。
        /// </summary>
        MagicAtkHPAbsorbRate = 23,

        /// <summary>
        /// 待战回血。
        /// </summary>
        RecoverHP = 24,

        /// <summary>
        /// 待战时间缩减。
        /// </summary>
        ReducedHeroSwitchCDRate = 25,

        /// <summary>
        /// 移动速度。
        /// </summary>
        Speed = 26,
    }

    public enum ActionType
    {
        RCPushBattleResult = 4003,
        RCPushEntityMove = 4100,
        RCPushEntityPerformSkillStart = 4101,
        RCPushEntityPerformSkillEnd = 4102,
        RCPushEntityImpact = 4103,
        RCPushEntitySwitchHero = 4104,
        RCPushEntityPerformSkillFF = 4106,
        RCPushEntitySkillRushing = 4107,
        RCPushEntityAddBuff = 4108,
        RCPushEntityRemoveBuff = 4109,
        RCPushEntityHeroDie = 4110,
        GetRoomInfo = 5001,
        GiveUpBattle = 5003,
        CRRoomReady = 5004,
        CREntityMove = 5100,
        CREntityPerformSkillStart = 5101,
        CREntityPerformSkillEnd = 5102,
        CREntityImpact = 5103,
        EntitySwitchHero = 5104,
        CREntityPerformSkillFF = 5106,
        CREntitySKillRushing = 5107,
        CREntityAddBuff = 5108,
        CREntityRemoveBuff = 5109,
        RCRequestResult = 5099,
    }

    public enum CampType
    {
        Player = 0,
        Enemy = 1,
        Neutral = 2,
        Player2 = 3,
        Enemy2 = 4,
        Neutral2 = 5,
        PlayerFriend = 6,
    }


    public enum InstanceSuccessReason
    {
        Unknown = 0,
        AbandonedByOpponent,
        TimeOut,
        HasBeatenOpponent,
    }

    public enum InstanceFailureReason
    {
        Unknown = 0,
        AbandonedByUser,
        TimeOut,
        HasBeenBeaten,
        ClaimedByAI,
    }
}
