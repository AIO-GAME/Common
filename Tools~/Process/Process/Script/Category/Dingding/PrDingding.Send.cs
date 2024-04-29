/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-09
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class PrDingTalk
    {
        private static PrCurl.Option GetOption()
        {
            var option = new PrCurl.Option();
            option.include = true;
            option.AddAuthorization();
            option.AddContentType();
            option.AddCharset();
            return option;
        }

        private static IExecutor Post(string remote, string secret, string data)
        {
            return PrCurl.Post(GetSign(remote, secret), data, GetOption());
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <param name="secret">签名密钥</param>
        /// <param name="content">内容</param>
        /// <returns><see cref="IExecutor"/>执行器</returns>
        public static IExecutor SendText(string content, string remote, string secret = null)
        {
            var data = new Msg();
            data.ToText(content);
            return Post(remote, secret, data);
        }

        /// <summary>
        /// 发送 Link 消息
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <param name="secret">签名密钥</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="messageUrl">图片地址</param>
        /// <param name="picUrl">消息链接</param>
        public static IExecutor SendLink(
            string title,  string content, string messageUrl, string picUrl,
            string remote, string secret = null)
        {
            var data = new Msg();
            data.ToLink(title, content, messageUrl, picUrl);
            return Post(remote, secret, data);
        }

        /// <summary>
        /// 发送 Markdown 消息
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <param name="secret">签名密钥</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        public static IExecutor SendMarkdown(
            string title,  string content,
            string remote, string secret = null)
        {
            var data = new Msg();
            data.ToMarkdown(title, content);
            return Post(remote, secret, data);
        }

        /// <summary>
        /// 发送 ActionCard 消息
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <param name="secret">签名密钥</param>
        /// <param name="title">首屏会话透出的展示内容</param>
        /// <param name="content">markdown格式的消息</param>
        /// <param name="singleTitle">单个按钮的标题</param>
        /// <param name="singleURL">点击消息跳转的URL</param>
        public static IExecutor SendActionCard(
            string title,  string content, string singleTitle, string singleURL,
            string remote, string secret = null)
        {
            var data = new Msg();
            data.ToActionCard(title, content, singleTitle, singleURL);
            return Post(remote, secret, data);
        }

        /// <summary>
        /// 发送 ActionCard 消息
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <param name="secret">签名密钥</param>
        /// <param name="title">首屏会话透出的展示内容</param>
        /// <param name="content">markdown格式的消息</param>
        /// <param name="buttons">按钮 ket:按钮标题 value:点击按钮触发的URL</param>
        /// <param name="btnOrientation">0：按钮竖直排列 other：按钮横向排列</param>
        public static IExecutor SendActionCard(
            string title,  string content, IDictionary<string, string> buttons, int btnOrientation,
            string remote, string secret = null)
        {
            var data = new Msg();
            data.ToActionCard(title, content, buttons, btnOrientation);
            return Post(remote, secret, data);
        }

        /// <summary>
        /// 发送 ActionCard 消息
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <param name="secret">签名密钥</param>
        /// <param name="links">
        /// item1[title]:单条信息文本
        /// item1[messageURL]:点击单条信息到跳转链接
        /// item1[picURL]:单条信息后面图片的URL
        /// </param>
        public static IExecutor SendFeedCard(IEnumerable<Tuple<string, string, string>> links,
                                             string                                     remote, string secret = null)
        {
            var data = new Msg();
            data.ToFeedCard(links);
            return Post(remote, secret, data);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <param name="data">数据</param>
        /// <param name="secret">签名密钥</param>
        /// <returns><see cref="IExecutor"/>
        /// 执行器
        /// 错误码(error-code)   错误码描述(errmsg)   解决方案(solution)
        /// 400013              群已被解散           请向其他群发消息
        /// 400102              access-token不存在  请确认access-token拼写是否正确
        /// 400103              机器人已停用         请联系管理员启用机器人
        /// 400106              不支持的消息类型      请使用文档中支持的消息类型
        /// 400107              机器人不存在         请确认机器人是否在群中
        /// 410101              发送速度太快而限流    请降低发送速度
        /// 430102              含有不安全的外链      请确认发送的内容合法
        /// 430103              含有不合适的文本      请确认发送的内容合法
        /// 430104              含有不合适的图片      请确认发送的内容合法
        /// 430105              含有不合适的内容      请确认发送的内容合法
        /// </returns>
        public static IExecutor SendData(Msg    data,
                                         string remote, string secret = null)
        {
            return Post(remote, secret, data);
        }
    }
}