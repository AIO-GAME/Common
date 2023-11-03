using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AIO;

public partial class AHelper
{
    public partial class Net
    {
        public partial class HTTP
        {
            /// <summary>
            /// 返回服务器正对特定资源所支持的HTTP请求方法
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Options(string remoteUrl,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, OPTIONS, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 返回服务器正对特定资源所支持的HTTP请求方法
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Options(string remoteUrl, byte[] data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, OPTIONS, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 返回服务器正对特定资源所支持的HTTP请求方法
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Options(string remoteUrl, string data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, OPTIONS, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 返回服务器正对特定资源所支持的HTTP请求方法
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> OptionsAsync(string remoteUrl,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, OPTIONS, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 返回服务器正对特定资源所支持的HTTP请求方法
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> OptionsAsync(string remoteUrl, byte[] data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, OPTIONS, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 返回服务器正对特定资源所支持的HTTP请求方法
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> OptionsAsync(string remoteUrl, string data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, OPTIONS, data, encoding, timeout, cookie);
            }
        }
    }
}