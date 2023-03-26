using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class Utils
{
    public partial class IO
    {
        #region DeleteFile

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件相对路径</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFile(in string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件相对路径</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFile(in FileInfo path)
        {
            if (path.Exists) path.Delete();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="list">文件相对路径</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFile(in IEnumerable<string> list)
        {
            Parallel.ForEach(list, file => { DeleteFile(file); });
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="list">文件相对路径</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFile(in IEnumerable<FileInfo> list)
        {
            Parallel.ForEach(list, file => { DeleteFile(file); });
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="list">文件相对路径</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFile(params FileInfo[] list)
        {
            Parallel.ForEach(list, file => { DeleteFile(file); });
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="list">文件相对路径</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFile(params string[] list)
        {
            Parallel.ForEach(list, file => { DeleteFile(file); });
        }

        /// <summary>
        /// 删除指定文件夹下 指定类型文件
        /// </summary>
        /// <param name="folder">文件夹路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">查询模式</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFile(
            in string folder,
            in string pattern,
            in SearchOption option = SearchOption.AllDirectories)
        {
            Parallel.ForEach(GetFilesInfo(folder, pattern, option), file => { DeleteFile(file); });
        }

        #endregion

        /// <summary>
        /// 删除文件夹
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFolder(
            in DirectoryInfo folder,
            in SearchOption option = SearchOption.AllDirectories,
            in bool isAll = false)
        {
            if (!folder.Exists) return;
            if (isAll) Parallel.ForEach(folder.GetFiles("*", option), file => { DeleteFile(file); });
            folder.Delete(isAll);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFolder(
            in string folder,
            in SearchOption option = SearchOption.AllDirectories,
            in bool isAll = false)
        {
            DeleteFolder(new DirectoryInfo(folder), option, isAll);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFolder(
            in IEnumerable<string> folders,
            SearchOption option = SearchOption.AllDirectories,
            bool isAll = false)
        {
            Parallel.ForEach(folders, folder => { DeleteFolder(new DirectoryInfo(folder), option, isAll); });
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DeleteFolder(
            in IEnumerable<DirectoryInfo> folders,
            SearchOption option = SearchOption.AllDirectories,
            bool isAll = false)
        {
            Parallel.ForEach(folders, folder => { DeleteFolder(folder, option, isAll); });
        }
    }
}
