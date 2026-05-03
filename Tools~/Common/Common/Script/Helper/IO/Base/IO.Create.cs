#region

using System.IO;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

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
                if (info.Exists) // 判断文件夹是否存在 判断是否需要清空文件夹
                {
                    if (!clear) return;
                    ClearDir(info);
                }
                else info.Create();
            }
        }

        #endregion
    }
}