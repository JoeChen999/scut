using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ZyGames.Framework.Common.Configuration;

namespace Genesis.GameServer.LobbyServer
{
    public class PaymentVerifyLogic
    {
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public static string GetVerifyResult(string verifyCode)
        {
            HttpWebRequest request = null;
            string url = ConfigUtils.GetSetting("PaymentVerifyServer");
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "text/plain";
            request.UserAgent = DefaultUserAgent;
            request.Timeout = 1000;
            byte[] data = Encoding.UTF8.GetBytes(verifyCode);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            try
            {
                Stream resStream = request.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(resStream);
                return sr.ReadToEnd();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
