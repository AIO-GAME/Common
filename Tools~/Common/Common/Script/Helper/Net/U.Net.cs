using System.IO;
using System.Linq;

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: Net

        /// <summary>
        /// 网络 工具类
        /// </summary>
        public partial class Net
        {
            /// <summary>
            /// 容量缓存 : 1M
            /// </summary>
            internal const int BUFFER_SIZE = 1024 * 1024;

            internal const ushort TIMEOUT = 3000;

            internal static readonly byte[] CODE = { 1, 3, 9, 3, 1, 3, 9, 3, 1 };

            private Net()
            {
            }

            /// <summary>
            /// 验证下载文件是否下载完成
            /// </summary>
            /// <returns> 完成返回 true，未完成返回 false </returns>
            public static bool VerifyDownloadFileComplete(string filePath)
            {
                if (!File.Exists(filePath)) return false;
                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                if (fs.Length < CODE.Length) return true;

                var buffer = new byte[CODE.Length];
                var read = fs.Read(buffer, 0, buffer.Length);
                return CODE.Where((t, i) => buffer[i] != t).Any();
            }
        }

        #endregion
    }
}