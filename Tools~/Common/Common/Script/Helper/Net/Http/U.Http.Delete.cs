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
            /// 请求删除指定的资源
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Delete(string   remoteUrl,
                                        Encoding encoding = null,
                                        ushort   timeout  = Net.TIMEOUT,
                                        string   cookie   = null)
            {
                return AutoCommonRequest(remoteUrl, DELETE, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求删除指定的资源
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Delete(string   remoteUrl, byte[] data,
                                        Encoding encoding = null,
                                        ushort   timeout  = Net.TIMEOUT,
                                        string   cookie   = null)
            {
                return AutoCommonRequest(remoteUrl, DELETE, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求删除指定的资源
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static string Delete(string   remoteUrl, string data,
                                        Encoding encoding = null,
                                        ushort   timeout  = Net.TIMEOUT,
                                        string   cookie   = null)
            {
                return AutoCommonRequest(remoteUrl, DELETE, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求删除指定的资源
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> DeleteAsync(string   remoteUrl,
                                                   Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, DELETE, Array.Empty<byte>(), encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求删除指定的资源
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> DeleteAsync(string   remoteUrl,       byte[] data,
                                                   Encoding encoding = null, ushort timeout = Net.TIMEOUT, string cookie = null)
            {
                return AutoCommonRequestAsync(remoteUrl, DELETE, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求删除指定的资源
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="encoding">编码</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> DeleteAsync(
                string   remoteUrl,
                string   data,
                Encoding encoding,
                ushort   timeout = Net.TIMEOUT,
                string   cookie  = null)
            {
                return AutoCommonRequestAsync(remoteUrl, DELETE, data, encoding, timeout, cookie);
            }

            /// <summary>
            /// 请求删除指定的资源
            /// </summary>
            /// <param name="remoteUrl">远端地址</param>
            /// <param name="data">数据</param>
            /// <param name="timeout">超时时间</param>
            /// <param name="cookie">cookie</param>
            /// <exception cref="AExpNetGetResponseStream">异常</exception>
            /// <returns>内容</returns>
            public static Task<string> DeleteAsync(
                string remoteUrl,
                string data,
                ushort timeout = Net.TIMEOUT,
                string cookie  = null)
            {
                return AutoCommonRequestAsync(remoteUrl, DELETE, data, timeout, cookie);
            }
        }

        #endregion
    }
}