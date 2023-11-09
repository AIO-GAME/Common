/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// Socket 扩展
    /// </summary>
    public static partial class ExtendSocket
    {
        /// <summary>
        /// 转化为IP地址
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToConverseIP(this Socket socket)
        {
            if (!(socket.RemoteEndPoint is IPEndPoint endPoint))
                throw new ArgumentException("The socket is not connected to a remote endpoint.");
            return endPoint.Address.ToString();
        }
    }
}