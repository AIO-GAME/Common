#region

using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

#endregion

namespace AIO
{
    /// <summary>
    ///     Socket 扩展
    /// </summary>
    public static class ExtendSocket
    {
        /// <summary>
        ///     转化为IP地址
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