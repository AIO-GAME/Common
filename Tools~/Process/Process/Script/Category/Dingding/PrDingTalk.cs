/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
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
    public partial class PrDingTalk
    {
        private static PrDingTalk CreateInstance()
        {
            return new PrDingTalk();
        }

        /// <summary>
        /// 消息
        /// </summary>
        public static Msg CreateData()
        {
            return new Msg();
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
            private MsgType Type = MsgType.empty;

            /// <summary>
            /// 文本类型
            /// </summary>
            [JsonProperty(PropertyName = "text")] private Hashtable TBText;

            /// <summary>
            /// At 类型
            /// </summary>
            [JsonProperty(PropertyName = "at")] private Hashtable TBAt;

            /// <summary>
            /// link 类型
            /// </summary>
            [JsonProperty(PropertyName = "link")] private Hashtable TBLink;

            /// <summary>
            /// link 类型
            /// </summary>
            [JsonProperty(PropertyName = "markdown")] private Hashtable TBMarkdown;

            /// <summary>
            /// actionCard 类型
            /// </summary>
            [JsonProperty(PropertyName = "actionCard")]
            private Hashtable TBActionCard;

            /// <summary>
            /// feedCard 类型
            /// </summary>
            [JsonProperty(PropertyName = "feedCard")]
            private Hashtable TBFeedCard;

            /// <summary>
            /// 转换为Json
            /// </summary>
            /// <param name="msg">钉钉 msg</param>
            /// <returns>json string</returns>
            public static implicit operator string(Msg msg)
            {
                return AHelper.Json.Serialize(msg, JsonSetting);
            }

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
                Type = MsgType.text;
                TBText = new Hashtable
                {
                    ["content"] = content
                };
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
                Type = MsgType.link;
                TBLink = new Hashtable
                {
                    ["title"] = title,
                    ["text"] = content,
                    ["messageUrl"] = messageUrl,
                    ["picUrl"] = picUrl
                };
            }

            /// <summary>
            /// 设置 Markdown类型
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="content">内容</param>
            public void ToMarkdown(string title, string content)
            {
                Type = MsgType.markdown;
                TBMarkdown = new Hashtable
                {
                    ["title"] = title,
                    ["text"] = content
                };
            }

            /// <summary>
            /// 消息类型，此时固定为：actionCard
            /// </summary>
            /// <param name="title">首屏会话透出的展示内容</param>
            /// <param name="content">markdown格式的消息</param>
            /// <param name="singleTitle">单个按钮的标题</param>
            /// <param name="singleURL">点击消息跳转的URL</param>
            public void ToActionCard(
                string title, string content,
                string singleTitle, string singleURL)
            {
                Type = MsgType.actionCard;
                TBActionCard = new Hashtable
                {
                    ["title"] = title,
                    ["text"] = content,
                    ["singleURL"] = singleURL,
                    ["singleTitle"] = singleTitle
                };
            }


            /// <summary>
            /// 消息类型，此时固定为：actionCard
            /// </summary>
            /// <param name="title">首屏会话透出的展示内容</param>
            /// <param name="content">markdown格式的消息</param>
            /// <param name="buttons">按钮 ket:按钮标题 value:点击按钮触发的URL</param>
            /// <param name="btnOrientation">0：按钮竖直排列 other：按钮横向排列</param>
            public void ToActionCard(
                string title, string content, IDictionary<string, string> buttons, int btnOrientation = 0)
            {
                Type = MsgType.actionCard;
                TBActionCard = new Hashtable
                {
                    ["title"] = title,
                    ["text"] = content,
                    ["btns"] = new List<Hashtable>(),
                    ["btnOrientation"] = (btnOrientation == 0 ? 0 : 1).ToString()
                };

                foreach (var item in buttons)
                {
                    ((List<Hashtable>)TBActionCard["btns"]).Add(new Hashtable
                    {
                        ["title"] = item.Key,
                        ["actionURL"] = item.Value
                    });
                }
            }


            /// <summary>
            /// 消息类型，此时固定为：feedCard
            /// </summary>
            /// <param name="links">
            /// item1[title]:单条信息文本
            /// item1[messageURL]:点击单条信息到跳转链接
            /// item1[picURL]:单条信息后面图片的URL
            /// </param>
            public void ToFeedCard(IEnumerable<Tuple<string, string, string>> links)
            {
                Type = MsgType.feedCard;
                TBFeedCard = new Hashtable
                {
                    ["links"] = new List<Hashtable>(),
                };

                foreach (var item in links)
                {
                    ((List<Hashtable>)TBFeedCard["links"]).Add(new Hashtable
                    {
                        ["title"] = item.Item1,
                        ["messageURL"] = item.Item2,
                        ["picURL"] = item.Item3
                    });
                }
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

            /// <summary>
            /// feedCard
            /// </summary>
            feedCard = 5,
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
            if (string.IsNullOrEmpty(secret))
                return string.Concat("https://oapi.dingtalk.com/robot/send?access_token=", remote);

            var timestamp = AHelper.TimeStamp.NowMillisecond;
            var stringToSign = string.Concat(timestamp, '\n', secret);
            var signData = AHelper.Encrypt.HmacSHA256(secret, stringToSign, Encoding.UTF8);
            var sign = AHelper.Base64.Serialize(signData);
            return string.Concat(
                "https://oapi.dingtalk.com/robot/send?access_token=", remote,
                "&timestamp=", timestamp,
                "&sign=", sign);
        }

        #endregion
    }
}