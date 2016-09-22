using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;

namespace Genesis.GameServer.LobbyServer
{
    public class AnnouncementLogic
    {
        public static void PushReceiveGearAnnouncement(int playerId, ReceiveItemMethodType pathType, int gearType)
        {
            if(pathType == ReceiveItemMethodType.None)
            {
                return;
            }
            if(CacheSet.GearTable.GetData(gearType).Quality < (int)GearQuality.Orange)
            {
                return;
            }
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            packet.AnnouncementId = (int)AnnounceType.ReceiveGear;
            packet.Sender = GetPlayerInfo(playerId);
            packet.Params.Add("`ReceiveItemMethod," + ((int)pathType).ToString() + "`");
            packet.Params.Add("`GearName," + gearType.ToString() + "`");
            PushAnnouncement(packet);
        }

        public static void PushLevelUpAnnouncement(int playerId, int level)
        {
            if(level != 80)
            {
                return;
            }
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            packet.AnnouncementId = (int)AnnounceType.LevelUp;
            packet.Sender = GetPlayerInfo(playerId);
            packet.Params.Add("`Number," + level.ToString() + "`");
            PushAnnouncement(packet);
        }

        public static void PushVipLevelAnnouncement(int playerId, int level)
        {
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            if (level < 6)
            {
                packet.AnnouncementId = (int)AnnounceType.VipLevelUpLow;
            }
            else
            {
                packet.AnnouncementId = (int)AnnounceType.VipLevelUpHigh;
            }
            packet.Sender = GetPlayerInfo(playerId);
            packet.Params.Add("`Number," + level.ToString() + "`");
            PushAnnouncement(packet);
        }

        public static void PushReceiveHeroAnnouncement(int playerId, ReceiveItemMethodType pathType, int heroType)
        {
            if (pathType == ReceiveItemMethodType.None)
            {
                return;
            }
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            packet.AnnouncementId = (int)AnnounceType.ReceiveHero;
            packet.Sender = GetPlayerInfo(playerId);
            packet.Params.Add("`ReceiveItemMethod," + ((int)pathType).ToString() + "`");
            packet.Params.Add("`HeroName," + heroType.ToString() + "`");
            PushAnnouncement(packet);
        }

        public static void PushHeroStarLevelUpAnnouncement(int playerId, int starLevel, int heroType)
        {
            if(starLevel < 5)
            {
                return;
            }
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            packet.AnnouncementId = (int)AnnounceType.HeroStarLevelUp;
            packet.Sender = GetPlayerInfo(playerId);
            packet.Params.Add("`Number," + starLevel.ToString() + "`");
            packet.Params.Add("`HeroName," + heroType.ToString() + "`");
            PushAnnouncement(packet);
        }

        public static void PushHeroMightAnnouncement(int playerId, int oldMight, int might, int heroType)
        {
            if ((oldMight < 10000 && might > 10000) || (oldMight < 50000 && might > 50000))
            {
                LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
                packet.AnnouncementId = (int)AnnounceType.HeroMightLow;
                packet.Sender = GetPlayerInfo(playerId);
                packet.Params.Add("`HeroName," + heroType.ToString() + "`");
                packet.Params.Add("`Number," + might.ToString() + "`");
                PushAnnouncement(packet);
            }
            if ((oldMight < 100000 && might > 100000) || (oldMight < 150000 && might > 150000))
            {
                LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
                packet.AnnouncementId = (int)AnnounceType.HeroMightHigh;
                packet.Sender = GetPlayerInfo(playerId);
                packet.Params.Add("`HeroName," + heroType.ToString() + "`");
                packet.Params.Add("`Number," + might.ToString() + "`");
                PushAnnouncement(packet);
            }
            else
            {
                return;
            }
        }

        public static void PushGearStrengthenAnnouncement(int playerId, int gearStrengthenLevel, int GearType)
        {
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            if (gearStrengthenLevel < 2)
            {
                return;
            }
            else if(gearStrengthenLevel < 5)
            {
                packet.AnnouncementId = (int)AnnounceType.GearStrengthenLow;
            }
            else
            {
                packet.AnnouncementId = (int)AnnounceType.GearStrengthenHigh;
            }
            packet.Sender = GetPlayerInfo(playerId);
            packet.Params.Add("`GearName," + GearType.ToString() + "`");
            packet.Params.Add("`Number," + gearStrengthenLevel.ToString() + "`");
            PushAnnouncement(packet);
        }

        public static void PushHeroElevationAnnouncement(int playerId, int heroElevationLevel, int heroType)
        {
            if (heroElevationLevel % 5 != 0)
            {
                return;
            }
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            packet.AnnouncementId = (int)AnnounceType.HeroElevation;
            packet.Sender = GetPlayerInfo(playerId);
            packet.Params.Add("`HeroName," + heroType.ToString() + "`");
            packet.Params.Add("`Number," + heroElevationLevel.ToString() + "`");
            PushAnnouncement(packet);
        }

        public static void PushHeroConsciousnessAnnouncement(int playerId, int heroConsciousnessLevel, int heroType)
        {
            if (heroConsciousnessLevel % 10 != 0)
            {
                return;
            }
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            if (heroConsciousnessLevel <= 40)
            {
                packet.AnnouncementId = (int)AnnounceType.HeroConsicousnesslow;
            }
            else
            {
                packet.AnnouncementId = (int)AnnounceType.HeroConsicousnessHigh;
            }
            packet.Sender = GetPlayerInfo(playerId);
            packet.Params.Add("`HeroName," + heroType.ToString() + "`");
            packet.Params.Add("`Number," + heroConsciousnessLevel.ToString() + "`");
            PushAnnouncement(packet);
        }

        public static void PushOpenTenCoinChanceAnnouncement(int playerId)
        {
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            packet.AnnouncementId = (int)AnnounceType.OpenCoinChance;
            packet.Sender = GetPlayerInfo(playerId);
            PushAnnouncement(packet);
        }

        public static void PushOpenTenMoneyChanceAnnouncement(int playerId)
        {
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            packet.AnnouncementId = (int)AnnounceType.OpenMoneyChance;
            packet.Sender = GetPlayerInfo(playerId);
            PushAnnouncement(packet);
        }

        public static void PushSystemNotice(string message)
        {
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            packet.AnnouncementId = (int)AnnounceType.SystemAnnouncement;
            packet.Params.Add(message);
            PushAnnouncement(packet);
        }

        public static void PushAdvertisement(string message)
        {
            LCPushSystemAnnouncement packet = new LCPushSystemAnnouncement();
            packet.AnnouncementId = (int)AnnounceType.Advertisement;
            packet.Params.Add(message);
            PushAnnouncement(packet);
        }

        private static PBPlayerInfo GetPlayerInfo(int playerId)
        {
            PlayerLogic p = new PlayerLogic();
            p.SetUser(playerId);
            var ret = new PBPlayerInfo()
            {
                Id = playerId,
                Name = p.MyPlayer.Name,
                VipLevel = p.MyPlayer.VIPLevel,
                Level = p.MyPlayer.Level,
            };
            return ret;
        }

        private static void PushAnnouncement(LCPushSystemAnnouncement packet)
        {
            byte[] data = ProtoBufUtils.Serialize(packet);
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream(4006, data);
            foreach (var session in GameSession.GetOnlineAll())
            {
                if (session.Connected)
                {
                    session.SendAsync(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
