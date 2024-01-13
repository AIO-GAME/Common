using System.IO;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class AHelper
    {
        public partial class IO
        {
            /// <summary>
            /// 创建文件夹
            /// </summary>
            /// <param name="directory">文件夹路径</param>
            /// <param name="clear">清除</param>
            public static void CreateDir(in string directory, in bool clear = false)
            {
                var info = new DirectoryInfo(directory);
                // 判断文件夹是否存在 判断是否需要清空文件夹
                if (info.Exists)
                {
                    if (!clear) return;
                    info.Delete(true);
                    info.Create();
                }
                else info.Create();
            }
        }
    }
}