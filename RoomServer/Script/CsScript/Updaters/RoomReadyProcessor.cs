namespace Genesis.GameServer.RoomServer
{
    public class RoomReadyProcessor : BaseActionProcessor
    {
        public RoomReadyProcessor(Room room)
            : base(room)
        {
        }

        public override ActionType ActionType
        {
            get { return ActionType.CRRoomReady; }
        }

        public override void Process()
        {
            m_Room.Players[m_UserId].State = RoomPlayerState.Playing;
        }

        public override void PushResult()
        {
        }
    }
}
