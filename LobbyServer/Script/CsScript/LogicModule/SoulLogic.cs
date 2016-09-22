
namespace Genesis.GameServer.LobbyServer
{
    public class SoulLogic
    {
        private Souls m_Soul;
        public SoulLogic()
        {
        }

        public void SetSoul(int soulId)
        {
            m_Soul = CacheSet.SoulCache.FindKey(soulId);
        }

        public Souls MySoul
        {
            get { return m_Soul; }
            set { m_Soul = value; }
        }

        public int AddNewSoul(int soulTypeId)
        {
            m_Soul = new Souls();
            m_Soul.Id = (int)CacheSet.SoulCache.GetNextNo();
            m_Soul.TypeId = soulTypeId;
            CacheSet.SoulCache.Add(m_Soul);
            return m_Soul.Id;
        }

        public bool RemoveSoul()
        {
            return CacheSet.SoulCache.Delete(m_Soul);
        }

        public void Upgrade(int newSoulType, PlayerPackageLogic pp)
        {
            m_Soul.TypeId = newSoulType;
            pp.UpgradeSoul(m_Soul.Id, m_Soul.TypeId);
        }
    }
}