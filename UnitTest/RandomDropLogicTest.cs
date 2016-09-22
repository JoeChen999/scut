using Genesis.GameServer.LobbyServer;
using NUnit.Framework;
using ZyGames.Framework.Cache.Generic;
using Genesis.GameServer.CommonLibrary;

namespace UnitTest
{
    [TestFixture]
    public class RandomDropLogicTester
    {
        private RandomDropLogic m_RandomDrop;
        private DTDrop m_Drop;

        [SetUp]
        public void SetUp()
        {
            m_RandomDrop = RandomDropLogic.GetInstance();
            m_Drop = new DTDrop();
            m_Drop.Id = 1;
            m_Drop.RepeatCount = 100;
            DropItem di = new DropItem();
            di.ItemId = 100;
            di.ItemCount = 1;
            di.ItemWeight = 10000;
            m_Drop.DropList.Add(di);
            DropItem di1 = new DropItem();
            di1.ItemId = 101;
            di1.ItemCount = 2;
            di1.ItemWeight = 20000;
            m_Drop.DropList.Add(di1);
            DropItem di2 = new DropItem();
            di2.ItemId = 102;
            di2.ItemCount = 3;
            di2.ItemWeight = 30000;
            m_Drop.DropList.Add(di2);
            DropItem di3 = new DropItem();
            di3.ItemId = 103;
            di3.ItemCount = 4;
            di3.ItemWeight = 40000;
            m_Drop.DropList.Add(di3);
        }

        [Test]
        public void GetDropDictTest()
        {
            CacheDictionary<int, int> dropDict = new CacheDictionary<int, int>();
            m_RandomDrop.GetDropDict(m_Drop, dropDict);
            Assert.IsNotEmpty(dropDict);
            Assert.IsNotNull(dropDict[101]);
            Assert.IsNotNull(dropDict[102]);
            Assert.IsNotNull(dropDict[103]);
            Assert.IsNotNull(dropDict[100]);
        }
    }
}