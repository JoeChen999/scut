using Genesis.GameServer.CommonLibrary;
using ProtoBuf;
using System;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Model;

namespace Genesis.GameServer.RoomServer
{
    [Serializable, ProtoContract]
    public class DTSkillLevelUp : MemoryEntity, IDataTable
    {
        private MemoryCacheStruct<DTSkillLevelUp> DTSkillLevelUpCache = new MemoryCacheStruct<DTSkillLevelUp>();

        public DTSkillLevelUp()
        {

        }

        [ProtoMember(1), EntityField(true)]
        public int Id
        {
            get;
            set;
        }

        [ProtoMember(2), EntityField]
        public int HeroType
        {
            get;
            set;
        }

        [ProtoMember(3), EntityField]
        public int SkillIndex
        {
            get;
            set;
        }

        [ProtoMember(4), EntityField]
        public int SkillLevel
        {
            get;
            set;
        }

        [ProtoMember(5), EntityField]
        public int CoinCost
        {
            get;
            set;
        }

        [ProtoMember(6), EntityField]
        public int CostItemId
        {
            get;
            set;
        }

        [ProtoMember(7), EntityField]
        public int CostItemCount
        {
            get;
            set;
        }

        [ProtoMember(9), EntityField]
        public int RequiresHeroLevel
        {
            get;
            set;
        }

        public void ParseRow(string[] rowData)
        {
            int index = 0;
            index++;
            Id = int.Parse(rowData[index++]);
            index++;
            HeroType = int.Parse(rowData[index++]);
            SkillIndex = int.Parse(rowData[index++]);
            SkillLevel = int.Parse(rowData[index++]);
            CoinCost = int.Parse(rowData[index++]);
            CostItemId = int.Parse(rowData[index++]);
            CostItemCount = int.Parse(rowData[index++]);
            RequiresHeroLevel = int.Parse(rowData[index++]);

            DTSkillLevelUpCache.TryAdd(Id.ToString(), this);
        }
    }
}