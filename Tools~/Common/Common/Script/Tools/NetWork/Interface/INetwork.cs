/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-10-07               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using System;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// INetwork
    /// </summary>
    public interface INetwork
    {
        /// <summary>
        /// 加密解密
        /// </summary>
        INetCryptography Cryptography { get; }

        /// <summary>
        /// 序列化
        /// </summary>
        INetSerialization ReqSerialization { get; }

        /// <summary>
        /// 流程控制
        /// </summary>
        INetGuard Guard { get; }

        /// <summary>
        /// 主机地址
        /// </summary>
        string Host { get; }

        /// <summary>
        /// 端口号
        /// </summary>
        int Port { get; }

        /// <summary>
        /// 是否使用SSL
        /// </summary>
        bool SSL { get; }

        /// <summary>
        /// 是否加密
        /// </summary>
        bool Encrypt { get; }

        /// <summary>
        /// 最大重试次数
        /// </summary>
        int MaxRetryCount { get; set; }

        /// <summary>
        /// 是否已连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="host">地址</param>
        /// <param name="port">端口</param>
        /// <param name="ssl">是否使用SSL</param>
        void Connect(string host, int port, bool ssl);

        /// <summary>
        /// 关闭连接
        /// </summary>
        void Disconnect();
    }
}