using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using ZyGames.Framework.Cache.Generic;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Com.Rank;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Redis;

namespace Genesis.GameServer.LobbyServer
{
    public class PVPLogic
    {
        public static int SeasonId = 0;
        public static int Year = 0;

        public static bool IsOpen(int userId)
        {
            TimeSpan startTime = TimeSpan.Parse(GameConfigs.GetString("Pvp_Start_Time_Everyday", "4:00:00"));
            TimeSpan endTime = TimeSpan.Parse(GameConfigs.GetString("Pvp_End_Time_Everyday", "16:00:00"));
            DateTime todayStartTime = DateTime.Today.Add(startTime);
            DateTime todayEndTime = DateTime.Today.Add(endTime);
            DateTime now = DateTime.UtcNow;
            if (now < todayStartTime || now > todayEndTime)
            {
                return false;
            }
            PlayerLogic p = new PlayerLogic().SetUser(userId);
            if (p.MyPlayer.Level < GameConfigs.GetInt("Pvp_Require_Player_Level", 30))
            {
                return false;
            }
            return true;
        }

        public static int GetSeasonId()
        {
            if(SeasonId != 0 && Year != 0)
            {
                return SeasonId;
            }
            int season = RedisConnectionPool.GetClient().Get<int>(GameConsts.Pvp.PvpSeasonCountKey);
            int year = RedisConnectionPool.GetClient().Get<int>(GameConsts.Pvp.PvpSeasonOfYearKey);
            if(season == 0 || year == 0)
            {
                SeasonId = 1;
                Year = DateTime.UtcNow.Year;
                RedisConnectionPool.GetClient().Set(GameConsts.Pvp.PvpSeasonCountKey, SeasonId);
                RedisConnectionPool.GetClient().Set(GameConsts.Pvp.PvpSeasonOfYearKey, Year);
            }
            return season;
        }

        public static bool StartSingleMatch(int userId)
        {
            bool success = false;
            LWStartSinglePvpMatching package = new LWStartSinglePvpMatching();
            package.RoomPlayerInfo = GetPlayerRoomData(userId);
            WorldServerSender.Send("LWStartSinglePvpMatchingHandler", package, delegate (RemotePackage callback) 
            {
                var res = RemoteCommunication.ParseRemotePackage<WLStartSinglePvpMatching>(callback.Message as byte[]);
                success = res.Success;
            });
            return success;
        }

        public static bool StopSingleMatch(int userId)
        {
            bool success = false;
            LWCancelSinglePvpMatching package = new LWCancelSinglePvpMatching();
            package.PlayerId = userId;
            WorldServerSender.Send("LWCancelSinglePvpMatchingHandler", package, delegate (RemotePackage callback)
            {
                var res = RemoteCommunication.ParseRemotePackage<WLCancelSinglePvpMatching>(callback.Message as byte[]);
                success = res.Success;
            });
            return success;
        }

        public static void ExecuteMatch(object state)
        {
            var queues = CacheSet.PVPMathcQueueCache.FindAll();
            PlayerSinglePvpLogic psp = new PlayerSinglePvpLogic();
            foreach (var queue in queues)
            {
                if (queue.InQueueGroups.Count <= 1)
                {
                    continue;
                }
                if (queue.MatchedGroupCount > 0)
                {
                    ClearMatchedGroups(queue);
                }
                var groups = new List<PVPGroup>();
                groups.AddRange(queue.InQueueGroups);
                groups.Sort();
                for (int i = 0; i < groups.Count; i++)
                {
                    if (i + 1 >= groups.Count)
                    {
                        break;
                    }
                    bool success = true;
                    LRCreateNewRoom userData = new LRCreateNewRoom();
                    foreach (int playerId in groups[i].Players)
                    {
                        if (!GameSession.Get(playerId).Connected)
                        {
                            queue.InQueueGroups.RemoveAt(i);
                            success = false;
                            break;
                        }
                        PBRoomPlayerInfo playerRoomData = GetPlayerRoomData(playerId);
                        userData.RoomPlayerInfo.Add(playerRoomData);
                    }
                    if (!success)
                    {
                        continue;
                    }
                    foreach (int playerId in groups[++i].Players)
                    {
                        if (!GameSession.Get(playerId).Connected)
                        {
                            queue.InQueueGroups.RemoveAt(i);
                            success = false;
                            break;
                        }
                        PBRoomPlayerInfo playerRoomData = GetPlayerRoomData(playerId);
                        userData.RoomPlayerInfo.Add(playerRoomData);
                    }
                    if (!success)
                    {
                        continue;
                    }

                    userData.Token = GetToken();

                    var room = RoomServerManager.GetLowestLoadedRoomServer();
                    TraceLog.Write("room" + room.Id.ToString() + "::" + room.IP);
                    RoomServerSender.Send(room.Id, "LRCreateNewRoomHandler", userData, delegate (RemotePackage callback)
                    {
                        var res = RemoteCommunication.ParseRemotePackage<RLCreateNewRoom>(callback.Message as byte[]);
                        LCPushPvpMatchSuccess package = new LCPushPvpMatchSuccess() { RoomId = res.RoomId, RoomServerHost = room.Host, RoomServerPort = room.Port, Token = userData.Token, InstanceId = res.InstanceId };
                        var rank = RankingFactory.Get<SinglePvpRankUser>("SinglePvpRanking");

                        foreach (int playerId in groups[queue.MatchedGroupCount].Players)
                        {
                            foreach (var rp in userData.RoomPlayerInfo)
                            {
                                if (rp.PlayerInfo.Id != playerId)
                                {
                                    package.EnemyInfo = rp;

                                }
                            }
                            byte[] buffer = CustomActionDispatcher.GeneratePackageStream(package.PacketActionId, ProtoBufUtils.Serialize(package));

                            try
                            {
                                GameSession.Get(playerId).SendAsync(buffer, 0, buffer.Length);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        foreach (int playerId in groups[queue.MatchedGroupCount + 1].Players)
                        {
                            foreach (var rp in userData.RoomPlayerInfo)
                            {
                                if (rp.PlayerInfo.Id != playerId)
                                {
                                    package.EnemyInfo = rp;

                                }
                            }
                            byte[] buffer = CustomActionDispatcher.GeneratePackageStream(package.PacketActionId, ProtoBufUtils.Serialize(package));
                            try
                            {
                                GameSession.Get(playerId).SendAsync(buffer, 0, buffer.Length);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        queue.MatchedGroupCount += 2;
                    });
                }
                ClearMatchedGroups(queue);
            }
        }

        private static void ClearMatchedGroups(PVPMatchQueue queue)
        {
            if (queue.MatchedGroupCount <= 0)
            {
                return;
            }
            queue.InQueueGroups.RemoveRange(0, queue.MatchedGroupCount);
            queue.MatchedGroupCount = 0;
        }

        private static string GetToken()
        {
            byte[] bytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
            return Convert.ToBase64String(bytes);
        }

        private static PBRoomPlayerInfo GetPlayerRoomData(int playerId)
        {
            PBRoomPlayerInfo retData = new PBRoomPlayerInfo();
            PlayerLogic p = new PlayerLogic();
            p.SetUser(playerId);
            retData.PlayerInfo = new PBPlayerInfo()
            {
                Id = playerId,
                Name = p.MyPlayer.Name,
                Level = p.MyPlayer.Level,
                VipLevel = p.MyPlayer.VIPLevel,
                PortraitType = p.MyPlayer.PortraitType,
                Might = p.MyPlayer.Might,
            };
            HeroTeamLogic heroTeam = new HeroTeamLogic();
            heroTeam.SetUser(playerId);
            CacheList<int> team = heroTeam.GetTeam();
            PlayerHeroLogic ph = new PlayerHeroLogic().SetUser(playerId);
            foreach (int heroType in team)
            {
                if (heroType == 0)
                {
                    continue;
                }
                ph.SetHero(heroType);
                var heroInfo = ph.GetHeroInfo();
                PBLobbyHeroInfo lobbyHeroInfo = new PBLobbyHeroInfo();
                lobbyHeroInfo.Type = heroInfo.HeroType;
                lobbyHeroInfo.Level = heroInfo.HeroLv;
                lobbyHeroInfo.Exp = heroInfo.HeroExp;
                lobbyHeroInfo.StarLevel = heroInfo.HeroStarLevel;
                lobbyHeroInfo.ConsciousnessLevel = heroInfo.ConsciousnessLevel;
                lobbyHeroInfo.ElevationLevel = heroInfo.ElevationLevel;
                lobbyHeroInfo.SkillLevels.AddRange(heroInfo.SkillLevels);
                foreach (var gear in heroInfo.Gears)
                {
                    if (gear.Value == 0)
                    {
                        continue;
                    }
                    PBGearInfo gearInfo = new PBGearInfo();
                    gearInfo.Id = gear.Value;
                    Gears gearData = CacheSet.GearCache.FindKey(gear.Value);
                    gearInfo.Level = gearData.Level;
                    gearInfo.Type = gearData.TypeId;
                    gearInfo.StrengthenLevel = gearData.StrengthenLevel;
                    lobbyHeroInfo.GearInfo.Add(gearInfo);
                }
                foreach (var soul in heroInfo.Souls)
                {
                    if (soul.Value == 0)
                    {
                        continue;
                    }
                    PBSoulInfo soulInfo = new PBSoulInfo();
                    soulInfo.Id = soul.Value;
                    Souls soulData = CacheSet.SoulCache.FindKey(soul.Value);
                    soulInfo.Type = soulData.TypeId;
                    lobbyHeroInfo.SoulInfo.Add(soulInfo);
                }
                PBRoomHeroInfo roomHeroInfo = new PBRoomHeroInfo()
                {
                    LobbyHeroInfo = lobbyHeroInfo,
                };
                retData.RoomHeroInfo.Add(roomHeroInfo);
            }
            retData.LobbyServerId = int.Parse(ConfigUtils.GetSetting("Server.Id"));
            PlayerSinglePvpLogic psp = new PlayerSinglePvpLogic();
            psp.SetUser(playerId);
            retData.Score = psp.MyPvp.SinglePvpScore;
            return retData;
        }
    }
}
