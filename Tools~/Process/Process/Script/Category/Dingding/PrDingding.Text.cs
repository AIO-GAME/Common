/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-09
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Collections;

namespace AIO
{
    public partial class PrDingding
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <param name="content">内容</param>
        /// <param name="secret">签名密钥</param>
        /// <returns><see cref="IExecutor"/>执行器</returns>
        public static IExecutor SendText(string remote, string content, string secret = null)
        {
            var option = new PrCurl.Option();
            option.include = true;
            option.AddAuthorization();
            option.AddContentType();
            option.AddCharset();
            var data = new Msg
            {
                Type = MsgType.text,
                TBText = new Hashtable { ["content"] = content }
            };
            return PrCurl.Post(GetSign(remote, secret), AHelper.Json.Serialize(data, JsonSetting), option);
        }
    }
}