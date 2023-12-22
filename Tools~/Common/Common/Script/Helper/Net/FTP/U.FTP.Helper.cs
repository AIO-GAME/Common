/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-17
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

public partial class AHelper
{
    public partial class FTP
    {
        private static string FixShortcuts(string remote) 
        {
            // 修复远程地址中的短划线和协议前缀
            return remote
                .Replace('\\', '/') // 将反斜杠替换为正斜杠
                .Replace("///", "/") // 将连续的两个斜杠替换为一个斜杠
                .Replace("//", "/") // 将连续的两个斜杠替换为一个斜杠
                .Replace("ftp:/", "ftp://") // 将 "ftp:/ "替换为 "ftp://"
                .Trim('/', '\\', ' '); // 去除字符串开头和结尾的斜杠和空格
        }

        private static List<FtpWebRequest> Pool = new List<FtpWebRequest>();

        /// <summary>
        /// 创建一个FTP请求
        /// </summary>
        /// <param name="remote">远程地址，格式为ftp://xxx</param>
        /// <param name="user">FTP登录用户名</param>
        /// <param name="pass">FTP登录密码</param>
        /// <param name="method">请求的方法，可以是"GET"、"PUT"、"POST"等</param>
        /// <param name="timeout">请求的超时时间，单位为秒，默认超时时间为TIMEOUT</param>
        /// <param name="cancellationToken">
        /// 用于取消请求的令牌，如果请求被取消，则会抛出<see cref="OperationCanceledException"/>
        /// </param>
        /// <returns>创建的FTP请求</returns>
        private static FtpWebRequest CreateRequestFile(string remote, string user, string pass, string method,
            int timeout = Net.TIMEOUT, CancellationToken cancellationToken = default)
        {
            // 如果远程地址没有以"ftp://"开头，则在前面添加上
            if (!remote.StartsWith("ftp://")) remote = string.Concat("ftp://", remote);

            // 创建一个FTP请求
            var request = (FtpWebRequest)WebRequest.Create(new Uri(FixShortcuts(remote)));

            // 设置请求的凭据为给定的用户名和密码
            request.Credentials = new NetworkCredential(user, pass);

            // 设置请求的超时时间为给定的超时时间
            request.Timeout = timeout;

            // 设置请求的方法为给定的方法
            request.Method = method;

            // 不使用代理服务器
            request.Proxy = null;

            // 使用二进制模式进行传输
            request.UseBinary = true;

            // 不保持Alive连接
            request.KeepAlive = false;

            // 使用被动模式
            request.UsePassive = true;

            // 不启用SSL
            request.EnableSsl = false;

            // 注册取消请求的回调
            if (cancellationToken == default) cancellationToken = CancellationToken.None;
            cancellationToken.Register(request.Abort);

            // 返回创建的FTP请求
            return request;
        }

        /// <summary>
        /// 创建一个FTP请求
        /// </summary>
        /// <param name="remote">远程地址，格式为ftp://xxx</param>
        /// <param name="user">FTP登录用户名</param>
        /// <param name="pass">FTP登录密码</param>
        /// <param name="method">请求的方法，可以是"GET"、"PUT"、"POST"等</param>
        /// <param name="timeout">请求的超时时间，单位为秒，默认超时时间为TIMEOUT</param>
        /// <param name="cancellationToken">
        /// 用于取消请求的令牌，如果请求被取消，则会抛出<see cref="OperationCanceledException"/>
        /// </param>
        /// <returns>创建的FTP请求</returns>
        private static FtpWebRequest CreateRequestDir(string remote, string user, string pass, string method,
            int timeout = Net.TIMEOUT
            , CancellationToken cancellationToken = default
        )
        {
            // 如果远程地址没有以"ftp://"开头，则在前面添加上
            if (!remote.StartsWith("ftp://")) remote = string.Concat("ftp://", remote);

            // 创建一个FTP请求
            var request = (FtpWebRequest)WebRequest.Create(new Uri(string.Concat(FixShortcuts(remote), '/')));

            // 设置请求的凭据为给定的用户名和密码
            request.Credentials = new NetworkCredential(user, pass);

            // 设置请求的超时时间为给定的超时时间
            request.Timeout = timeout;

            // 设置请求的方法为给定的方法
            request.Method = method;

            // 不使用代理服务器
            request.Proxy = null;

            // 使用二进制模式进行传输
            request.UseBinary = true;

            // 不保持Alive连接
            request.KeepAlive = false;

            // 使用被动模式
            request.UsePassive = true;

            // 不启用SSL
            request.EnableSsl = false;

            // 注册取消请求的回调
            if (cancellationToken == default) cancellationToken = CancellationToken.None;
            cancellationToken.Register(request.Abort);

            // 返回创建的FTP请求
            return request;
        }
    }
}