
namespace Genesis.GameServer.LobbyServer
{
    public class PaymentBILog : BILog
    {
        public PaymentBILog()
        {
            base.Type = BITypes.Payment;
        }

        public int CashCount { get; set; }

        public int GameTokenCount { get; set; }

        public int RewardTokenCount { get; set; }

        public string AttachInfo { get; set; }
    }
}
