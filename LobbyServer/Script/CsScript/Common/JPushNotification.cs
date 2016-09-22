using cn.jpush.api;
using cn.jpush.api.push.mode;
using cn.jpush.api.common;
using cn.jpush.api.common.resp;
using ZyGames.Framework.Common.Log;
using cn.jpush.api.push.notification;

namespace Genesis.GameServer.LobbyServer
{
    public enum JPushPlatForms
    {
        All,
        IOS,
        Android
    }
    public class JPushNotification
    {
        private const string APP_KEY = "10ff5a719faa7d3c70ec9d34";
        private const string MASTER_SECRET = "262d92c42a926a27858c8291";
        private const string TITTLE = "genesis";

        private static JPushNotification m_Instance;
        private JPushClient m_Client;

        private JPushNotification()
        {
            m_Client = new JPushClient(APP_KEY, MASTER_SECRET);
        }

        public static JPushNotification GetInstance()
        {
            if(m_Instance == null)
            {
                m_Instance = new JPushNotification();
            }
            return m_Instance;
        }

        public void Push(JPushPlatForms platform, string content, string alias = "")
        {
            PushPayload payload;
            switch (platform)
            {
                case JPushPlatForms.All:
                    payload = PushAllPlatform(content);
                    break;
                case JPushPlatForms.IOS:
                    payload = PushIOSPlatform(content);
                    break;
                case JPushPlatForms.Android:
                    payload = PushAndroidPlatform(content);
                    break;
                default:
                    return;
            }
            if(alias == "")
            {
                payload.audience = Audience.all();
            }
            else
            {
                payload.audience = Audience.s_alias(alias);
            }
            try
            {
                m_Client.SendPush(payload);
            }
            catch (APIRequestException e)
            {
                TraceLog.WriteError("Error response from JPush server.\nHTTP Status: {0}\nError Code: {1}\nError Message: {2}", e.Status, e.ErrorCode, e.ErrorMessage);
            }
            catch (APIConnectionException e)
            {
                TraceLog.WriteError(e.Message);
            }
        }

        private static PushPayload PushAllPlatform(string content)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            pushPayload.notification = new Notification().setAlert(content);
            return pushPayload;
        }

        private static PushPayload PushIOSPlatform(string content)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.ios();
            pushPayload.notification = new Notification().setAlert(content);
            pushPayload.notification.IosNotification = new IosNotification().AddExtra("from", "genesis");
            return pushPayload;
        }

        private static PushPayload PushAndroidPlatform(string content)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.android();
            pushPayload.notification = Notification.android(content, TITTLE);
            return pushPayload;
        }
    }
}
