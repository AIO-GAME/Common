using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class Utils
{
    public partial class IO
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="folder">文件夹路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">查询模式</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task DeleteFileAsync(
            string folder,
            string pattern,
            SearchOption option)
        {
            await Task.Run(() =>
                Parallel.ForEach(GetFilesInfo(folder, pattern, option), file => { DeleteFile(file); })
            );
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="list">文件列表</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task DeleteFileAsync(
            IEnumerable<string> list)
        {
            await Task.Run(() => Parallel.ForEach(list, file => { DeleteFile(file); }));
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="list">文件列表</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task DeleteFileAsync(
            IEnumerable<FileInfo> list)
        {
            await Task.Run(() => Parallel.ForEach(list, file => { DeleteFile(file); }));
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task DeleteFolderAsync(
            string folder,
            SearchOption option = SearchOption.AllDirectories,
            bool isAll = false)
        {
            await DeleteFolderAsync(new DirectoryInfo(folder), option, isAll);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task DeleteFolderAsync(
            DirectoryInfo folder,
            SearchOption option = SearchOption.AllDirectories,
            bool isAll = false)
        {
            if (!folder.Exists) return;
            if (isAll)
            {
                await Task.Run(() =>
                    Parallel.ForEach(folder.GetFiles("*", option), file => { DeleteFile(file); }));
            }

            folder.Delete(isAll);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task DeleteFolderAsync(
            IEnumerable<string> folders,
            SearchOption option = SearchOption.AllDirectories,
            bool isAll = false)
        {
            await Task.Run(() =>
                Parallel.ForEach(folders, folder => { DeleteFolder(new DirectoryInfo(folder), option, isAll); }));
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task DeleteFolderAsync(
            IEnumerable<DirectoryInfo> folders,
            SearchOption option = SearchOption.AllDirectories,
            bool isAll = false)
        {
            await Task.Run(() =>
                Parallel.ForEach(folders, folder => { DeleteFolder(folder, option, isAll); }));
        }
    }
}
