using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using UnityEngine;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.WorldServer
{
    public class PVPLogic
    {
        public static bool StartSingleMatch(PBRoomPlayerInfo player)
        {
            var p = CacheSet.PvpPlayerCache.FindKey(player.PlayerInfo.Id, player.LobbyServerId);
            if (p == null)
            {
                PvpPlayer pp = new PvpPlayer()
                {
                    Id = player.PlayerInfo.Id,
                    ServerId = player.LobbyServerId,
                    Score = player.Score,
                    Name = player.PlayerInfo.Name,
                };
                CacheSet.PvpPlayerCache.Add(pp);
            }
            else
            {
                p.Name = player.PlayerInfo.Name;
                p.Score = player.Score;
                p.PortraitType = player.PlayerInfo.PortraitType;
            }
            int typeId = (int)PVPType.Single;
            PVPMatchQueue queue;
            if (!CacheSet.PVPMathcQueueCache.TryGet(typeId.ToString(), out queue))
            {
                queue = new PVPMatchQueue();
                queue.QueueType = typeId;
                queue.MatchedGroupCount = 0;
                CacheSet.PVPMathcQueueCache.AddOrUpdate(typeId.ToString(), queue);
            }
            if (queue.InQueueGroups.Find(g => g.Player.PlayerInfo.Id == player.PlayerInfo.Id) != null)
            {
                return false;
            }
            PVPGroup group = new PVPGroup();
            group.Player = player;
            queue.InQueueGroups.Add(group);
            return true;
        }

        public static bool StopSingleMatch(int userId)
        {
            int typeId = (int)PVPType.Single;
            PVPMatchQueue queue;
            if (!CacheSet.PVPMathcQueueCache.TryGet(typeId.ToString(), out queue))
            {
                return false;
            }
            if (queue.MatchedGroupCount > 0)
            {
                return false;
            }
            queue.InQueueGroups.RemoveAll(g => g.Player.PlayerInfo.Id == userId);
            return true;
        }

        public static void ExecuteMatch(object state)
        {
            var queues = CacheSet.PVPMathcQueueCache.FindAll();
            //PlayerSinglePvpLogic psp = new PlayerSinglePvpLogic();
            foreach (var queue in queues)
            {
                queue.MatchedGroupCount = 0;
                if (queue.InQueueGroups.Count <= 1)
                {
                    continue;
                }
                var groups = new List<PVPGroup>();
                groups.AddRange(queue.InQueueGroups);
                groups.Sort();
                for (int i = 0; i < groups.Count; i++)
                {
                    while ((groups[i + 1].Player.Score - groups[i].Player.Score) > 200 && i + 1 < groups.Count)
                    {
                        queue.MatchedGroupCount += 1;
                        i++;
                    }
                    if (i + 1 >= groups.Count)
                    {
                        break;
                    }
                    WRCreateNewRoom userData = new WRCreateNewRoom();
                    userData.RoomPlayerInfo.Add(groups[i].Player);
                    userData.RoomPlayerInfo.Add(groups[i + 1].Player);
                    userData.Token = GetToken();
                    var room = RoomServerManager.GetLowestLoadedRoomServer();
                    RoomServerSender.Send(room.Id, "LRCreateNewRoomHandler", userData, delegate (RemotePackage callback)
                    {
                        //TraceLog.ReleaseWrite("Player:{0} and Player:{1} match success", groups[i].Player.PlayerInfo.Id.ToString(), groups[i + 1].Player.PlayerInfo.Id.ToString());
                        var res = RemoteCommunication.ParseRemotePackage<RWCreateNewRoom>(callback.Message as byte[]);
                        WLMatchSuccess package = new WLMatchSuccess() {
                            RoomId = res.RoomId,
                            InstanceId = res.InstanceId,
                            Token = userData.Token,
                            RoomServerHost = room.Host,
                            RoomServerPort = room.Port,
                        };
                        package.EnemyInfo = groups[queue.MatchedGroupCount + 1].Player;
                        package.PlayerId = groups[queue.MatchedGroupCount].Player.PlayerInfo.Id;
                        LobbyServerSender.Send(groups[queue.MatchedGroupCount].Player.LobbyServerId, "WLMatchSuccessHandler", package, delegate(RemotePackage cb) { });
                        package.EnemyInfo = groups[queue.MatchedGroupCount].Player;
                        package.PlayerId = groups[queue.MatchedGroupCount + 1].Player.PlayerInfo.Id;
                        LobbyServerSender.Send(groups[queue.MatchedGroupCount + 1].Player.LobbyServerId, "WLMatchSuccessHandler", package, delegate (RemotePackage cb) { });
                        queue.InQueueGroups.Remove(groups[queue.MatchedGroupCount]);
                        queue.InQueueGroups.Remove(groups[queue.MatchedGroupCount + 1]);
                    });
                    i++;
                }
            }
        }

        public static int GetDeltaScore(int playerScore, int enemyScore, int result)
        {
            double power = (enemyScore - playerScore) / 400.0;
            double ratio = 1 / (1 + Math.Pow(10, power));
            int deltaScore = 0;
            switch (result)
            {
                case 1:
                    deltaScore = (int)Math.Round(GameConsts.BasePvpDeltaScore * (1 - ratio));
                    break;
                case 2:
                    deltaScore = (int)Math.Round(GameConsts.BasePvpDeltaScore * (0 - ratio));
                    break;
                case 3:
                default:
                    break;
            }
            return deltaScore;
        }

        private static string GetToken()
        {
            byte[] bytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
            return Convert.ToBase64String(bytes);
        }
    }
}
