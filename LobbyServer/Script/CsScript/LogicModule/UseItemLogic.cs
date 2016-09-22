using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.GameServer.LobbyServer
{
    public class UseItemLogic
    {
        private int m_UserId;
        private int m_HeroId;
        public UseItemLogic()
        {
        }
        public UseItemLogic SetUser(int userId)
        {
            m_UserId = userId;
            return this;
        }

        public UseItemLogic SetHero(int heroId)
        {
            m_HeroId = heroId;
            return this;
        }

        public void UseItem(int itemId, int count)
        {
            DTItem itemData = CacheSet.ItemTable.GetData(itemId);
            this.GetType().GetMethod("ProcessEffect" + itemData.FunctionId.ToString(), BindingFlags.Instance | BindingFlags.NonPublic).Invoke(this, new object[] { count, itemData.FunctionParams });
        }

        private void ProcessEffect1(int count, string funcParams)
        {
            int totalAddExp = count * int.Parse(funcParams);
            PlayerHeroLogic playerHero = new PlayerHeroLogic();
            playerHero.SetUser(m_UserId).SetHero(m_HeroId);
            playerHero.AddExp(totalAddExp);
        }

        private void ProcessEffect2(int count, string funcParams)
        {
            int totalAddEnergy = count * int.Parse(funcParams);
            PlayerLogic player = new PlayerLogic().SetUser(1);
            long nextRecoverTime;
            player.AddEnergy(totalAddEnergy, out nextRecoverTime);
        }
    }
}
