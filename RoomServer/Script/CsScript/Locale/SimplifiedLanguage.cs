using ZyGames.Framework.Game.Lang;

namespace Genesis.GameServer.RoomServer
{
    public class SimplifiedLanguage : Language
    {
        /// <summary>
        /// Sign error
        /// </summary>
        public new string SignError = "验证签名出错";

        /// <summary>
        /// validate error
        /// </summary>
        public new string ValidateError = "请求的参数无效";
        /// <summary>
        /// The system is busy
        /// </summary>
        public new string ServerBusy = "服务器繁忙";

        /// <summary>
        /// param error
        /// </summary>
        public new string UrlElement = "缺少请求参数-";

        /// <summary>
        /// 参数名:{0}不存在
        /// </summary>
        public new string UrlNoParam = "参数名:{0}是必须的";
        /// <summary>
        /// 参数名:{0}超出范围[{1}-{2}]
        /// </summary>
        public new string UrlParamOutRange = "参数:{0}超出范围[{1} - {2}]";

        /// <summary>
        /// 服务器正在维护
        /// </summary>
        public new string ServerMaintain = "服务器正在维护";

        /// <summary>
        /// 服务器正在重启中，请稍候...
        /// </summary>
        public new string ServerLoading = "服务器正在重启中，请稍候...";

        /// <summary>
        /// 请求超时
        /// </summary>
        public new string RequestTimeout = "请求超时";
        /// <summary>
        /// 您输入的账号或密码不正确
        /// </summary>
        public new string PasswordError = "您输入的账号或密码不正确";

        /// <summary>
        /// 加载数据失败
        /// </summary>
        public new string LoadDataError = "加载数据失败";

        /// <summary>
        /// 该账号已被封禁
        /// </summary>
        public new string AcountIsLocked = "该账号已被封禁";

        /// <summary>
        /// 您的账号未登录或已过期
        /// </summary>
        public new string AcountNoLogin = "您的账号未登录或已过期";

        /// <summary>
        /// 您的账号已在其它地方登录
        /// </summary>
        public new string AcountLogined = "您的账号已在其它地方登录";

        /// <summary>
        /// 充值失败
        /// </summary>
        public new string AppStorePayError = "充值失败";
        /// <summary>
        /// 获取受权失败
        /// </summary>
        public new string GetAccessFailure = "获取受权失败";
    }
}
