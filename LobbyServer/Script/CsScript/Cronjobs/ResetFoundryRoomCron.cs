using System;
using System.Collections.Generic;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.LobbyServer
{
    public class ResetFoundryRoomCron : ICronjob
    {
        public List<DateTime> ExecuteTimes
        {
            get
            {
                return new List<DateTime>()
                {
                    new DateTime(2015, 1, 1, 20, 0, 0),
                };
            }
        }      

        public ResetFoundryRoomCron()
        {
        }

        public void Execute()
        {
            var roomList = CacheSet.FoundryRoomCache.FindAll();
            PlayerFoundryLogic pf = new PlayerFoundryLogic();
            foreach(var room in roomList)
            {
                room.Progress = 0;
                room.Level = 0;
                foreach (var player in room.Players)
                {
                    pf.SetUser(player);
                    pf.ResetUser();
                    var target = GameSession.Get(player);
                    if (target != null)
                    {
                        LCResetGearFoundryInfo package = new LCResetGearFoundryInfo();
                        package.NextFoundryTimeInTicks = pf.MyFoundry.NextFoundryTime;
                        package.Progress = new PBGearFoundryProgressInfo() { CurrentLevel = room.Level, CurrentProgress = room.Progress };
                        package.RewardFlags.AddRange(pf.MyFoundry.CanReceiveRewards);
                        byte[] buffer = CustomActionDispatcher.GeneratePackageStream(package.PacketActionId, ProtoBufUtils.Serialize(package));
                        target.SendAsync(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }
}
