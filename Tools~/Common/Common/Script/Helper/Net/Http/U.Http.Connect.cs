﻿#region

using System;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: HTTP

        public partial class Http
        {
            /// <summary>
            /// 预留给能够将连接改为管道方式的代理服务器
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Connect(string   remoteUrl,
                                         Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
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
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Connect(string   remoteUrl,       byte[] data,
                                         Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
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
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Connect(string   remoteUrl,       string data,
                                         Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
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
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> ConnectAsync(string   remoteUrl,
                                                    Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
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
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> ConnectAsync(string   remoteUrl,       byte[] data,
                                                    Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
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
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> ConnectAsync(string   remoteUrl,       string data,
                                                    Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, CONNECT, data, encoding, timeout, cookie);
            }
        }

        #endregion
    }
}