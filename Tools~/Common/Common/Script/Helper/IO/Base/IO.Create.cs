using System.IO;
using System.Runtime.CompilerServices;

public partial class AHelper
{
    public partial class IO
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="folder">文件夹路径</param>
        /// <param name="clear">清除</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CreateFolder(in string folder, in bool clear = false)
        {
            var info = new DirectoryInfo(folder);
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
