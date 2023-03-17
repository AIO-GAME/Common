/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System.Net.Sockets;

    /// <summary>
    /// Socket 扩展
    /// </summary>
    public static partial class SocketExtend
    {
        /// <summary>
        /// 转化为IP地址
        /// </summary>
        public static string ToConverIP(this Socket socket)
        {
            return socket.RemoteEndPoint.ToString();
        }
    }
}
