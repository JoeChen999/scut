using System;
using ZyGames.Framework.Common.Serialization;

namespace Genesis.GameServer.CommonLibrary
{
    public class RemoteCommunication
    {
        public static T ParseRemotePackage<T>(byte[] message)
        {
            byte[] a = message;
            byte[] b = new byte[a.Length - 4];
            Array.Copy(a, 4, b, 0, a.Length - 4);
            return ProtoBufUtils.Deserialize<T>(b);
        }
    }
}
