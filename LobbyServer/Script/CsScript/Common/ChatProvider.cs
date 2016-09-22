using Genesis.GameServer.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Message;
using ZyGames.Framework.RPC.IO;

namespace Genesis.GameServer.LobbyServer
{
    public static class ChatProvider
    {
        public static SensitiveWordService WordServer = new SensitiveWordService();
        public static bool SendWorld(Player sender, string message)
        {
            int time = GameConfigs.GetInt("World_Chat_CoolDown_Seconds", 30);
            PlayerChat pc = CacheSet.PlayerChatCache.FindKey(sender.Id.ToString(), sender.Id);
            if (DateTime.UtcNow.Ticks - pc.LastWorldChatTime < time * GameConsts.TicksPerSecond)
            {
                return false;
            }
            byte[] buffer = GenPackage((int)ChatType.World, sender, message);
            var allOLUsers = GameSession.GetOnlineAll();
            foreach (var user in allOLUsers)
            {
                user.SendAsync(buffer, 0, buffer.Length);
            }
            pc.LastWorldChatTime = DateTime.UtcNow.Ticks;
            return true;
        }

        public static bool SendPrivate(int receiverId, Player sender, string message)
        {
            GameSession receiver = GameSession.Get(receiverId);
            if (receiver == null || receiver.Connected == false)
            {
                return false;
            }
            byte[] buffer = GenPackage((int)ChatType.Private, sender, message);
            receiver.SendAsync(buffer, 0, buffer.Length);
            return true;
        }

        public static bool ProcessMessage(int userId, string message, out string newMessage)
        {
            newMessage = "";
            PlayerChat pc = CacheSet.PlayerChatCache.FindKey(userId.ToString(), userId);
            if (pc == null)
            {
                pc = new PlayerChat();
                pc.UserId = userId;
                pc.LastWorldChatTime = 0;
                pc.Content = "";
                CacheSet.PlayerChatCache.Add(pc);
            }
            int sizeLimit = GameConfigs.GetInt("World_Chat_Max_Length") == 0 ? 60 : GameConfigs.GetInt("World_Chat_Max_Length");
            if (message.Length > sizeLimit)
            {
                return false;
            }
            message = message.Trim();
            if(string.IsNullOrEmpty(message))
            {
                return false;
            }
            newMessage = WordServer.Filter(message);
            return true;
        }

        private static byte[] GenPackage(int type, Player sender, string message)
        {
            LCGetChat chatPackage = new LCGetChat();
            chatPackage.Type = type;
            chatPackage.Massage = message;
            chatPackage.Sender = new PBPlayerInfo()
            {
                Id = sender.Id,
                Name = sender.Name,
                VipLevel = sender.VIPLevel,
                Level = sender.Level
            };
            chatPackage.Time = DateTime.UtcNow.Ticks;
            byte[] data = ProtoBufUtils.Serialize(chatPackage);
            byte[] buffer = CustomActionDispatcher.GeneratePackageStream(3002, data);
            return buffer;
        }
    }
}
