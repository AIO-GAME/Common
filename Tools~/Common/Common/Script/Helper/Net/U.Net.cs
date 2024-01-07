namespace AIO
{
    public partial class AHelper
    {
        /// <summary>
        /// 网络 工具类
        /// </summary>
        public partial class Net
        {
            /// <summary>
            /// 容量缓存 : 1M
            /// </summary>
            internal const int BUFFER_SIZE = 1024 * 1024;

            internal static readonly byte[] CODE = new byte[] { 1, 3, 9, 3, 1, 3, 9, 3, 1 };

            internal const ushort TIMEOUT = 3000;

            private Net()
            {
            }
        }
    }
}