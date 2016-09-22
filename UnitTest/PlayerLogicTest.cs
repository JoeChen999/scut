using Genesis.GameServer.LobbyServer;
using NUnit.Framework;
using System;
using ZyGames.Framework.Cache.Generic;


namespace LobbyServerTester
{
    [TestFixture]
    public class PlayerLogicTest
    {
        private PlayerLogic m_Player;

        [SetUp]
        public void SetUp()
        {
            m_Player = new PlayerLogic();
            Player p = new Player();
            p.Id = 1;
            p.AccountName = "testUser";
            p.Coin = 100;
            p.Energy = 18;
            p.Exp = 10;
            p.Level = 2;
            p.Money = 100;
            p.Name = "testUser";
            p.PortraitType = 1;
            p.Status = PlayerStatus.Online;
            p.LastEnergyRecoverTime = DateTime.UtcNow.Ticks;

            m_Player.MyPlayer = p;
        }

        [Test]
        public void GetNewEnergyTest()
        {
            long nextRecoverTime;
           m_Player.MyPlayer.LastEnergyRecoverTime -= (new TimeSpan(0, 10, 0)).Ticks;
            m_Player.MyPlayer.Energy = 19;
            int newEnergy = m_Player.GetNewEnergy(out nextRecoverTime);
            Assert.AreEqual(20, newEnergy);
            m_Player.MyPlayer.LastEnergyRecoverTime -= (new TimeSpan(0, 20, 0)).Ticks;
            m_Player.MyPlayer.Energy = 17;
            newEnergy = m_Player.GetNewEnergy(out nextRecoverTime);
            Assert.AreEqual(19, newEnergy);
            m_Player.MyPlayer.LastEnergyRecoverTime -= (new TimeSpan(0, 10, 0)).Ticks;
            m_Player.MyPlayer.Energy = 20;
            newEnergy = m_Player.GetNewEnergy(out nextRecoverTime);
            Assert.AreEqual(21, newEnergy);
        }
    }
}
