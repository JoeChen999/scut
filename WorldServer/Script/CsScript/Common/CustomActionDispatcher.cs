using Genesis.GameServer.CommonLibrary;
using System;
using System.Net;
using System.Text;
using System.Web;
using ZyGames.Framework.Common.Log;
using ZyGames.Framework.Common.Serialization;
using ZyGames.Framework.Game.Contract;
using ZyGames.Framework.Game.Service;
using ZyGames.Framework.RPC.IO;
using ZyGames.Framework.RPC.Sockets;

namespace Genesis.GameServer.WorldServer
{
    public class CustomActionDispatcher : IActionDispatcher
    {
        public bool TryDecodePackage(ConnectionEventArgs e, out RequestPackage package)
        {
            byte[] content = null;
            CLPacketHead head = ReadMessageHead(e.Data, out content);
            if (head == null)
            {
                var packageReader = new PackageReader(e.Data, Encoding.UTF8);
                if (TryBuildPackage(packageReader, out package))
                {
                    package.OpCode = e.Meaage.OpCode;
                    package.CommandMessage = e.Socket.IsWebSocket && e.Meaage.OpCode == OpCode.Text
                        ? e.Meaage.Message
                        : null;
                    return true;
                }
                package = null;
                return false;
            }

            package = new RequestPackage(head.MsgId, head.SessionId, head.ActionId, head.UserId) { Message = content };
            return true;
        }

        public bool TryDecodePackage(HttpListenerRequest request, out RequestPackage package, out int statusCode)
        {
            package = null;
            statusCode = 0;
            return false;
        }

        public bool TryDecodePackage(HttpListenerContext context, out RequestPackage package)
        {
            package = null;
            return false;
        }

        public bool TryDecodePackage(HttpContext context, out RequestPackage package)
        {
            package = null;
            return false;
        }

        private CLPacketHead ReadMessageHead(byte[] data, out byte[] content)
        {
            CLPacketHead headPack = null;
            content = new byte[0];
            try
            {
                int pos = 0;
                byte[] headLenBytes = new byte[4];
                Buffer.BlockCopy(data, pos, headLenBytes, 0, headLenBytes.Length);
                pos += headLenBytes.Length;
                int headSize = BitConverter.ToInt32(headLenBytes, 0);
                if (headSize < data.Length)
                {
                    byte[] headBytes = new byte[headSize];
                    Buffer.BlockCopy(data, pos, headBytes, 0, headBytes.Length);
                    pos += headBytes.Length;
                    headPack = ProtoBufUtils.Deserialize<CLPacketHead>(headBytes);

                    if (data.Length > pos)
                    {
                        int len = data.Length - pos;
                        content = new byte[len];
                        Buffer.BlockCopy(data, pos, content, 0, content.Length);

                    }
                }
                else
                {
                    TraceLog.ReleaseWriteFatal("Can not parse packet head.");
                }
            }
            catch (Exception ex)
            {
                TraceLog.WriteError(ex.Message);
            }

            return headPack;
        }

        public ActionGetter GetActionGetter(RequestPackage package, GameSession session)
        {
            return new ActionGetter(package, session);
        }

        protected virtual bool TryBuildPackage(PackageReader packageReader, out RequestPackage package)
        {
            package = null;
            Guid proxySid;
            packageReader.TryGetParam("ssid", out proxySid);
            int actionid;
            if (!packageReader.TryGetParam("actionid", out actionid))
            {
                return false;
            }
            int msgid;
            if (!packageReader.TryGetParam("msgid", out msgid))
            {
                return false;
            }
            int userId;
            packageReader.TryGetParam("uid", out userId);
            string sessionId;
            string proxyId;
            packageReader.TryGetParam("sid", out sessionId);
            packageReader.TryGetParam("proxyId", out proxyId);

            package = new RequestPackage(msgid, sessionId, actionid, userId)
            {
                ProxySid = proxySid,
                ProxyId = proxyId,
                IsProxyRequest = packageReader.ContainsKey("isproxy"),
                RouteName = packageReader.RouteName,
                Params = packageReader.Params,
                Message = packageReader.InputStream,
                OriginalParam = packageReader.RawParam
            };
            return true;
        }

        public void ResponseError(BaseGameResponse response, ActionGetter actionGetter, int errorCode, string errorInfo)
        {
            LCPacketHead packetHead = new LCPacketHead()
            {
                MsgId = actionGetter.GetMsgId(),
                ActionId = actionGetter.GetActionId(),
                ErrorCode = errorCode,
                ErrorInfo = errorInfo
            };

            byte[] headBytes = ProtoBufUtils.Serialize(packetHead);
            byte[] buffer = BufferUtils.AppendHeadBytes(headBytes);
            response.BinaryWrite(buffer);
            //actionGetter.GetSession().SendAsync(buffer, 0, buffer.Length);
        }

        public static void ResponseOK(BaseGameResponse response, ActionGetter actionGetter, byte[] data)
        {
            LCPacketHead packetHead = new LCPacketHead()
            {
                MsgId = actionGetter.GetMsgId(),
                ActionId = actionGetter.GetActionId(),
                ErrorCode = 0,
                ErrorInfo = string.Empty
            };

            byte[] headBytes = ProtoBufUtils.Serialize(packetHead);
            byte[] buffer = BufferUtils.MergeBytes(BufferUtils.AppendHeadBytes(headBytes), data);
            //response.BinaryWrite(buffer);
            actionGetter.GetSession().SendAsync(buffer, 0, buffer.Length);
        }

        public static byte[] GeneratePackageStream(int actionId, byte[] data)
        {
            LCPacketHead packetHead = new LCPacketHead()
            {
                MsgId = 0,
                ActionId = actionId,
                ErrorCode = 0,
                ErrorInfo = string.Empty
            };

            byte[] headBytes = ProtoBufUtils.Serialize(packetHead);
            byte[] buffer = BufferUtils.MergeBytes(BufferUtils.AppendHeadBytes(headBytes), data);
            return buffer;
        }
    }
}
