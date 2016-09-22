using Genesis.GameServer.LobbyServer;
using NUnit.Framework;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;


namespace UnitTest
{
    [TestFixture]
    public class HeroTeamLogicTester
    {
        private HeroTeamLogic m_heroTeam;

        [SetUp]
        public void SetUp()
        {
            m_heroTeam = new HeroTeamLogic();
            HeroTeam ht = new HeroTeam();
            ht.PlayerId = 1;
            ht.Team = new CacheList<int> { 0, 0, 0 };
            m_heroTeam.MyHeroTeam = ht;
        }

        [Test]
        public void GetTeamTest()
        {
            CacheList<int> result = m_heroTeam.GetTeam();
            Assert.IsNotNull(result[0]);
            Assert.IsNotNull(result[1]);
            Assert.IsNotNull(result[2]);
        }
    }
}
