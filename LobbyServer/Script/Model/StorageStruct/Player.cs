using ProtoBuf;
using System;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.LobbyServer
{
    [Serializable, ProtoContract]
    //[EntityTable(AccessLevel.ReadWrite, CacheType.Entity, false)]
    [EntityTable(CacheType.Entity, "Game", "Player")]

    public class Player : ShareEntity
    {
        public Player()
        {
            
        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public string Name
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int Coin
        {
            get;
            set;
        }


        [ProtoMember(4), EntityField]
        public int Money
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int Level
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int Exp
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public string AccountName
        {
            get;
            set;
        }

        [ProtoMember(8), EntityField]
        public PlayerStatus Status
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public int PortraitType
        {
            get;
            set;
        }

        [ProtoMember(10), EntityField]
        public bool IsFemale
        {
            get;
            set;
        }

        [ProtoMember(11), EntityField]
        public int Energy
        {
            get;
            set;
        }

        [ProtoMember(12), EntityField]
        public long LastEnergyRecoverTime
        {
            get;
            set;
        }

        [ProtoMember(13), EntityField]
        public int VIPLevel
        {
            get;
            set;
        }

        [ProtoMember(14), EntityField]
        public int VIPExp
        {
            get;
            set;
        }

        [ProtoMember(15), EntityField]
        public int StarEnergy
        {
            get;
            set;
        }

        [ProtoMember(16), EntityField]
        public long LastLoginTime
        {
            get;
            set;
        }

        [ProtoMember(17), EntityField]
        public int ArenaToken
        {
            get;
            set;
        }

        [ProtoMember(18), EntityField]
        public int Spirit
        {
            get;
            set;
        }

        [ProtoMember(19), EntityField]
        public int DragonStripeToken
        {
            get;
            set;
        }

        [ProtoMember(20), EntityField]
        public int UUID
        {
            get;
            set;
        }

        [ProtoMember(21), EntityField]
        public int Might
        {
            get;
            set;
        }

        [ProtoMember(22), EntityField]
        public long CreateTime
        {
            get;
            set;
        }

        [ProtoMember(23), EntityField]
        public bool IsRobot
        {
            get;
            set;
        }

        [ProtoMember(24), EntityField]
        public int PvpToken
        {
            get;
            set;
        }

        protected override int GetIdentityId()
        {
            return Id;
        }
    }


}
