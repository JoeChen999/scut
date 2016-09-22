namespace Genesis.GameServer.RoomServer
{
    public class Constants
    {
        public const int WaitingConnectTimeLimit = 30;
        public const int WaitingStartTime = 10;
        public const int MaxProcessCountPerUpdate = 5;
        public const int RoomPlayingTime = 303;
        public const int RoomExpireTime = 600;

        public class Hero
        {
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
            public const float ReducedHeroSwitchCDRate = .5f;
        }
    }
}
