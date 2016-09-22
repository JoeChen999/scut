using System;
using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public abstract class BILog
    {
        public BITypes Type { get; protected set; }

        public int UserId { get; set; }

        public string RemoteAdress { get; set; }
    }

    public enum BITypes
    {
        Payment = 1,
        Purchase = 2,
    }

    public class BILogger
    {
        private DBProvider db;
        
        public BILogger()
        {
            db = new DBProvider("BI");
        }

        public void AddBILog(BILog logInfo)
        {
            switch (logInfo.Type)
            {
                case BITypes.Payment:
                    PaymentBILog paymentLog = logInfo as PaymentBILog;
                    AddPaymentLog(paymentLog);
                    break;
                case BITypes.Purchase:
                    break;
            }
        }

        private void AddPaymentLog(PaymentBILog logInfo)
        {
            string tableName = BITypes.Payment.ToString();
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                { "UserId", logInfo.UserId },
                { "RemoteAdress", logInfo.RemoteAdress },
                { "CashCount", logInfo.CashCount },
                { "GameTokenCount", logInfo.GameTokenCount },
                { "RewardTokenCount", logInfo.RewardTokenCount },
                { "AttachInfo", logInfo.AttachInfo}
            };
            db.Insert(tableName, values);
        }
    }
}
