using System;
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
            /// 预留给能够将连接改为管道方式的代理服务器
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Connect(string remoteUrl,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, CONNECT, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 预留给能够将连接改为管道方式的代理服务器
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Connect(string remoteUrl, byte[] data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, CONNECT, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 预留给能够将连接改为管道方式的代理服务器
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Connect(string remoteUrl, string data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequest(remoteUrl, CONNECT, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 预留给能够将连接改为管道方式的代理服务器
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> ConnectAsync(string remoteUrl,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, CONNECT, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 预留给能够将连接改为管道方式的代理服务器
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> ConnectAsync(string remoteUrl, byte[] data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, CONNECT, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 预留给能够将连接改为管道方式的代理服务器
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="NetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> ConnectAsync(string remoteUrl, string data,
                Encoding encoding = null, ushort timeout = TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, CONNECT, data, encoding, timeout, cookie);
            }
        }
    }
}