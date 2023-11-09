/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AIO
{
    /// <summary>
    /// 命令 钉钉API
    /// </summary>
    public partial class PrDingding
    {
        private static PrDingding CreateInstance()
        {
            return new PrDingding();
        }

        /// <summary>
        /// 消息
        /// </summary>
        public static Msg CreateMSG(MsgType type = MsgType.empty)
        {
            var data = new Msg
            {
                Type = type,
                TBText = new Hashtable(),
                TBAt = new Hashtable()
            };
            return data;
        }

        /// <summary>
        /// 消息
        /// </summary>
        public class Msg
        {
            /// <summary>
            /// 消息类型
            /// </summary>
            [JsonProperty(PropertyName = "msgtype")] [JsonConverter(typeof(StringEnumConverter))]
            public MsgType Type = MsgType.empty;

            /// <summary>
            /// 文本类型
            /// </summary>
            [JsonProperty(PropertyName = "text")] public Hashtable TBText;

            /// <summary>
            /// At类型
            /// </summary>
            [JsonProperty(PropertyName = "at")] public Hashtable TBAt;

            /// <summary>
            /// At类型
            /// </summary>
            [JsonProperty(PropertyName = "link")] public Hashtable TBLink;

            /// <summary>
            /// 设置 被@人的手机号。
            /// </summary>
            /// <param name="array">列表</param>
            public void SetAtMobile(params string[] array)
            {
                if (array is null || array.Length == 0) return;
                if (TBAt is null) TBAt = new Hashtable();
                TBAt["atMobiles"] = array;
            }

            /// <summary>
            /// 设置 被@人的用户userid。
            /// </summary>
            /// <param name="array">列表</param>
            public void SetAtUserId(params string[] array)
            {
                if (array is null || array.Length == 0) return;
                if (TBAt is null) TBAt = new Hashtable();
                TBAt["atUserIds"] = array;
            }

            /// <summary>
            /// 设置 是否@所有人。
            /// </summary>
            /// <param name="atAll">是否@所有人。</param>
            public void SetAtAll(bool atAll)
            {
                if (TBAt is null) TBAt = new Hashtable();
                TBAt["isAtAll"] = atAll;
            }

            /// <summary>
            /// 设置 Text类型
            /// </summary>
            /// <param name="content">内容</param>
            public void ToText(string content)
            {
                if (TBText is null) TBText = new Hashtable();
                Type = MsgType.text;
                TBText["content"] = content;
            }

            /// <summary>
            /// 设置 Link类型
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="content">内容</param>
            /// <param name="messageUrl">图片地址</param>
            /// <param name="picUrl">消息链接</param>
            public void ToLink(string title, string content, string messageUrl, string picUrl = null)
            {
                if (TBLink is null) TBLink = new Hashtable();
                Type = MsgType.link;
                TBLink["title"] = title;
                TBLink["text"] = content;
                TBLink["messageUrl"] = messageUrl;
                TBLink["picUrl"] = picUrl;
            }

            /// <summary>
            /// 设置 Markdown类型
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="content">内容</param>
            public void ToMarkdown(string title, string content)
            {
                if (TBLink is null) TBLink = new Hashtable();
                Type = MsgType.markdown;
                TBLink["title"] = title;
                TBLink["text"] = content;
            }

            public void ToActionCard(
                string title, string content, string btnOrientation,
                string singleTitle, string singleURL)
            {
                if (TBLink is null) TBLink = new Hashtable();
                Type = MsgType.actionCard;
                TBLink["title"] = title;
                TBLink["text"] = content;
                TBLink["singleTitle"] = singleTitle;
                TBLink["singleURL"] = singleURL;
                TBLink["btnOrientation"] = singleURL;
            }
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        public enum MsgType
        {
            /// <summary>
            /// 空
            /// </summary>
            empty = 0,

            /// <summary>
            /// 文本
            /// </summary>
            text = 1,

            /// <summary>
            /// 链接
            /// </summary>
            link = 2,

            /// <summary>
            /// markdown
            /// </summary>
            markdown = 3,

            /// <summary>
            /// actionCard
            /// </summary>
            actionCard = 4,
        }

        #region Link

        private static JsonSerializerSettings JsonSetting
        {
            get
            {
                if (_JsonSetting is null)
                    _JsonSetting = new JsonSerializerSettings
                    {
                        Culture = CultureInfo.CurrentCulture,
                        Context = new StreamingContext(StreamingContextStates.All),
                        Formatting = Formatting.None,
                        NullValueHandling = NullValueHandling.Ignore,
                        DateParseHandling = DateParseHandling.DateTime,
                        DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                        CheckAdditionalContent = false,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
                    };
                return _JsonSetting;
            }
        }

        private static JsonSerializerSettings _JsonSetting;

        /// <summary>
        /// 获取签名后路径
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <param name="secret">签名</param>
        /// <returns>签名后路径</returns>
        private static string GetSign(string remote, string secret)
        {
            if (string.IsNullOrEmpty(secret)) return remote;

            var timestamp = AHelper.TimeStamp.NowMillisecond;
            var stringToSign = string.Concat(timestamp, '\n', secret);
            var signData = AHelper.Encrypt.HmacSHA256(secret, stringToSign, Encoding.UTF8);
            var sign = AHelper.Base64.Serialize(signData);
            return string.Concat(remote, "&timestamp=", timestamp, "&sign=", sign);
        }

        #endregion

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <param name="data">数据</param>
        /// <param name="secret">签名密钥</param>
        /// <returns><see cref="IExecutor"/>执行器</returns>
        public static IExecutor SendData(string remote, Msg data, string secret = null)
        {
            var option = new PrCurl.Option();
            option.include = true;
            option.AddAuthorization();
            option.AddContentType();
            option.AddCharset();
            return PrCurl.Post(GetSign(remote, secret), AHelper.Json.Serialize(data, JsonSetting), option);
        }
    }
}