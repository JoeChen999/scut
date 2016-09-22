using System.Collections.Generic;

namespace Genesis.GameServer.LobbyServer
{
    public partial class PlayerDailyQuestLogic
    {
        private void UpdateCommonQuest(TrackingDailyQuest beingUpdatedQuest, object[] param)
        {
            beingUpdatedQuest.Progress += (int)param[0];
            PushProgressModified(new List<TrackingDailyQuest>() { beingUpdatedQuest });
        }
    }
}
