using Genesis.GameServer.CommonLibrary;
using Genesis.GameServer.LobbyServer;
using NUnit.Framework;
using System.IO;

namespace UnitTest
{
    [TestFixture]
    public class PlayerHeroLogicTest
    {
        private PlayerHeroLogic m_PlayerHeroLogic;
        [SetUp]
        public void SetUp()
        {
            DataTableLoader.LoadDataTableFile(new FileInfo(TestConsts.DataTableDir + "Hero.txt"), typeof(DTHero));
            DataTableLoader.LoadDataTableFile(new FileInfo(TestConsts.DataTableDir + "HeroBase.txt"), typeof(DTHeroBase));
            m_PlayerHeroLogic = new PlayerHeroLogic();
            PlayerHeros ph = new PlayerHeros()
            {
                UserId = 1,
            };
            Hero h1 = new Hero()
            {
                HeroType = 1,
                HeroLv = 1,
                HeroExp = 0,
                HeroStarLevel = 1,
                ConsciousnessLevel = 0,
                ElevationLevel = 0
            };
            h1.SkillLevels.AddRange(new int[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 1 });
            m_PlayerHeroLogic.MyHeros = ph;
            ph.Heros.Add(1, h1);
        }

        [Test]
        public void AddNewHeroTest()
        {
            m_PlayerHeroLogic.AddNewHero(2);
            m_PlayerHeroLogic.SetHero(2);
            Hero heroInfo = m_PlayerHeroLogic.GetHeroInfo();
            Assert.AreEqual(2, heroInfo.HeroType);
            Assert.AreEqual(1, heroInfo.HeroLv);
            Assert.AreEqual(0, heroInfo.HeroExp);
            Assert.AreEqual(1, heroInfo.HeroStarLevel);
            Assert.AreEqual(0, heroInfo.ElevationLevel);
            Assert.AreEqual(0, heroInfo.ConsciousnessLevel);
            var item = m_PlayerHeroLogic.AddNewHero(2);
            Assert.IsNotNull(item);
        }

        [Test]
        public void AddHeroExp()
        {
            m_PlayerHeroLogic.SetHero(1);
            m_PlayerHeroLogic.AddExp(749, 2);

            Assert.AreEqual(749, m_PlayerHeroLogic.GetHeroInfo().HeroExp);
            Assert.AreEqual(1, m_PlayerHeroLogic.GetHeroInfo().HeroLv);

            m_PlayerHeroLogic.AddExp(2, 2);
            Assert.AreEqual(1, m_PlayerHeroLogic.GetHeroInfo().HeroExp);
            Assert.AreEqual(2, m_PlayerHeroLogic.GetHeroInfo().HeroLv);

            m_PlayerHeroLogic.AddExp(999999, 2);
            Assert.AreEqual(2, m_PlayerHeroLogic.GetHeroInfo().HeroLv);
        }
    }
}
