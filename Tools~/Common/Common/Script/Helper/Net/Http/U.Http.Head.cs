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
            /// 请求获取特定的资源的响应消息报告
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Head(string   remoteUrl,
                                      Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, HEAD, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求获取特定的资源的响应消息报告
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Head(string   remoteUrl,       byte[] data,
                                      Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, HEAD, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求获取特定的资源的响应消息报告
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Head(string   remoteUrl,       string data,
                                      Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, HEAD, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求获取特定的资源的响应消息报告
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> HeadAsync(string   remoteUrl,
                                                 Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, HEAD, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求获取特定的资源的响应消息报告
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> HeadAsync(string   remoteUrl,       byte[] data,
                                                 Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, HEAD, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求获取特定的资源的响应消息报告
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> HeadAsync(string   remoteUrl,       string data,
                                                 Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, HEAD, data, encoding, timeout, cookie);
            }
        }
    }
}