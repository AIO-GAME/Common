using System.IO;
using System.Runtime.CompilerServices;

public partial class AHelper
{
    public partial class IO
    {
        /// <summary>
        /// 清空当前文件夹
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearFolder(in string folder)
        {
            // 如果参数为空，则视为已成功清空
            if (!ExistsFolder(folder)) return;
            // 删除当前文件夹下的文件
            foreach (var item in GetFilesInfo(folder)) DeleteFile(item);
            // 删除当前文件夹下的子文件夹
            foreach (var item in GetFoldersInfo(folder))
                DeleteFolder(item, SearchOption.AllDirectories, true);
        }

        /// <summary>
        /// 清空当前文件夹
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ClearFolder(in DirectoryInfo folder)
        {
            // 如果参数为空，则视为已成功清空
            if (!folder.Exists) return;
            // 删除当前文件夹下的文件
            foreach (var item in GetFilesInfo(folder.FullName)) DeleteFile(item);
            // 删除当前文件夹下的子文件夹
            foreach (var item in GetFoldersInfo(folder.FullName)) ClearFolder(item);
        }
    }
}
