#region

using System.IO;
using System.Runtime.CompilerServices;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 清空当前文件夹 | 不删除目标文件夹
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void ClearDir(in string folder) { ClearDir(new DirectoryInfo(folder)); }

            /// <summary>
            /// 清空当前文件夹 | 不删除目标文件夹
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void ClearDir(in DirectoryInfo folder)
            {
                if (!folder.Exists) return;                                           // 如果参数为空，则视为已成功清空
                foreach (var item in GetFilesInfo(folder.FullName)) DeleteFile(item); // 删除当前文件夹下的文件
                foreach (var item in GetDirsInfo(folder.FullName)) ClearDir(item);    // 删除当前文件夹下的子文件夹
            }
        }

        #endregion
    }
}