using System;
using System.Text;
using System.Threading.Tasks;

namespace AIO
{
    public partial class AHelper
    {
        public partial class HTTP
        {
            /// <summary>
            /// 回显服务器收到的请求
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Trace(string   remoteUrl,
                                       Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, TRACE, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 回显服务器收到的请求
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Trace(string   remoteUrl,       byte[] data,
                                       Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, TRACE, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 回显服务器收到的请求
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Trace(string   remoteUrl,       string data,
                                       Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, TRACE, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 回显服务器收到的请求
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> TraceAsync(string   remoteUrl,
                                                  Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, TRACE, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 回显服务器收到的请求
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> TraceAsync(string   remoteUrl,       byte[] data,
                                                  Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, TRACE, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 回显服务器收到的请求
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> TraceAsync(
                string   remoteUrl,
                string   data,
                Encoding encoding,
                ushort   timeout = Net.TIMEOUT,
                string   cookie  = null) =>
                AutoCommonRequestAsync(remoteUrl, TRACE, encoding.GetBytes(data), encoding, timeout, cookie);

            /// <summary>
            /// 回显服务器收到的请求
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> TraceAsync(
                string remoteUrl,
                string data,
                ushort timeout = Net.TIMEOUT,
                string cookie  = null) =>
                AutoCommonRequestAsync(remoteUrl, TRACE, Encoding.UTF8.GetBytes(data), Encoding.UTF8, timeout, cookie);
        }
    }
}