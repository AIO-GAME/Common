using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AIO
{
    public partial class AHelper
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
            public static async Task DeleteDirAsync(
                string directory,
                SearchOption option = SearchOption.AllDirectories,
                bool isAll = false)
            {
                await DeleteDirAsync(new DirectoryInfo(directory), option, isAll);
            }

            /// <summary>
            /// 删除文件夹
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static async Task DeleteDirAsync(
                DirectoryInfo director,
                SearchOption option = SearchOption.AllDirectories,
                bool isAll = false)
            {
                if (!director.Exists) return;
                if (isAll)
                {
                    await Task.Run(() =>
                        Parallel.ForEach(director.GetFiles("*", option), file => { DeleteFile(file); }));
                }

                director.Delete(isAll);
            }

            /// <summary>
            /// 删除文件夹
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static async Task DeleteDirAsync(
                IEnumerable<string> directors,
                SearchOption option = SearchOption.AllDirectories,
                bool isAll = false)
            {
                await Task.Run(() =>
                    Parallel.ForEach(directors, folder => { DeleteDir(new DirectoryInfo(folder), option, isAll); }));
            }

            /// <summary>
            /// 删除文件夹
            /// </summary>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static async Task DeleteDirAsync(
                IEnumerable<DirectoryInfo> directors,
                SearchOption option = SearchOption.AllDirectories,
                bool isAll = false)
            {
                await Task.Run(() =>
                    Parallel.ForEach(directors, folder => { DeleteDir(folder, option, isAll); }));
            }
        }
    }
}