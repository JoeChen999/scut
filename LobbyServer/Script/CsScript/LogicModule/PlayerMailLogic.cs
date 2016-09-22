using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.LobbyServer
{
    public class PlayerMailLogic
    {
        private int m_UserId;
        private PlayerMail m_Mails;
        public PlayerMailLogic()
        {

        }

        public void SetUser(int userId)
        {
            m_UserId = userId;
            m_Mails = CacheSet.PlayerMailCache.FindKey(m_UserId.ToString(), userId);
            if (m_Mails == null)
            {
                m_Mails = new PlayerMail();
                m_Mails.UserId = userId;
                m_Mails.ReceivedId = 0;
                CacheSet.PlayerMailCache.Add(m_Mails);
            }
        }

        public CacheDictionary<int, Mail> GetMails()
        {
            List<int> expiredIds = new List<int>();
            foreach (var mail in m_Mails.Mails)
            {
                if (mail.Value.ExpireTime < DateTime.UtcNow.Ticks)
                {
                    expiredIds.Add(mail.Key);
                }
            }
            foreach (var Id in expiredIds)
            {
                m_Mails.Mails.Remove(Id);
            }
            GetMailsFromDB();
            return m_Mails.Mails;
        }

        public int GetMailsFromDB()
        {
            DBProvider db = new DBProvider("Game");
            PlayerLogic p = new PlayerLogic();
            p.SetUser(m_UserId);
            string condition = String.Format("Id > {0} AND ToUser in ({1}) AND BeginTime < '{2}' AND ExpireTime > '{3}' AND BeginTime > '{4}'",
                m_Mails.ReceivedId, "-1,"+m_UserId, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), new DateTime(p.MyPlayer.CreateTime).ToString("yyyy-MM-dd HH:mm:ss"));
            var newMails = db.Select("mails", "Id,Message,AttachType,AttachCount,BeginTime,ExpireTime", condition);
            foreach (var mail in newMails)
            {
                Mail m = new Mail();
                m.Message = (string)mail["Message"];
                m.AttachedId = (int)(uint)mail["AttachType"];
                m.AttachedCount = (int)(uint)mail["AttachCount"];
                m.ExpireTime = ((DateTime)mail["ExpireTime"]).Ticks;
                m.StartTime = ((DateTime)mail["BeginTime"]).Ticks;
                m_Mails.Mails.Add((int)(uint)mail["Id"], m);
                m_Mails.ReceivedId = (int)(uint)mail["Id"];
            }
            return m_Mails.Mails.Count;
        }

        public ItemListItem OpenMail(int Id)
        {
            if (!m_Mails.Mails.ContainsKey(Id))
            {
                return null;
            }
            if (m_Mails.Mails[Id].ExpireTime < DateTime.UtcNow.Ticks)
            {
                return null;
            }
            PlayerPackageLogic pp = new PlayerPackageLogic();
            pp.SetUser(m_UserId);
            ItemListItem reward = new ItemListItem();
            reward.Id = m_Mails.Mails[Id].AttachedId;
            reward.Count = m_Mails.Mails[Id].AttachedCount;
            if(!pp.CheckPackageSlot(new Dictionary<int, int>() { { reward.Id, reward.Count } }))
            {
                return null;
            }
            m_Mails.Mails.Remove(Id);
            return reward;
        }

        public void AddNewMail(string message, int AttachId, int AttachCount)
        {
            if(CacheSet.ItemTable.GetData(AttachId) == null && CacheSet.GearTable.GetData(AttachId) == null && CacheSet.SoulTable.GetData(AttachId) == null && CacheSet.EpigraphTable.GetData(AttachId) == null)
            {
                return;
            }
            DBProvider db = new DBProvider("Game");
            Dictionary<string, object> values = new Dictionary<string,object>();
            values["Message"] = message;
            values["AttachType"] = AttachId;
            values["AttachCount"] = AttachCount;
            values["BeginTime"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime expireDay = DateTime.UtcNow.AddDays(GameConfigs.GetInt("Mail_Expire_Days", 7));
            values["ExpireTime"] = expireDay.ToString("yyyy-MM-dd HH:mm:ss");
            values["ToUser"] = m_UserId;
            db.Insert("mails", values);
        }

        public void SendNotification()
        {
            LCHaveNewEmail package = new LCHaveNewEmail();
            byte[] data = ProtoBufUtils.Serialize(package);
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream(3202, data);
            GameSession.Get(m_UserId).SendAsync(buffer, 0, buffer.Length);
        }
    }
}
