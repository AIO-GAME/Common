/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AIO
{
    /// <summary>
    /// 文件读写操作工具集
    /// </summary>
    public static partial class IOUtils
    {
        /// <summary> 空字节数组 </summary>
        public static readonly byte[] EMPTY_BYTES = { };

        /// <summary>
        /// 判断当前路径是否存在 不管是以文件夹 还是 文件的形式存在
        /// </summary>
        /// <returns>True:存在 False:不存在</returns>
        public static bool Exists(string path)
        {
            if (FileExists(path)) return true;
            if (DirExists(path)) return true;
            return false;
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <returns>True:存在 False:不存在</returns>
        public static bool FileExists(in string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        /// <returns>True:存在 False:不存在</returns>
        public static bool DirExists(in string path)
        {
            return Directory.Exists(path);
        }

        #region Delete

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="path">文件相对路径</param>
        public static void DeleteFile(in string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="path">文件相对路径</param>
        public static void DeleteFile(in FileInfo path)
        {
            if (path.Exists)
                path.Delete();
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="paths">文件相对路径</param>
        public static void DeleteFiles(in IEnumerable<string> paths)
        {
            foreach (var item in paths)
            {
                if (File.Exists(item))
                    File.Delete(item);
            }
        }

        /// <summary>
        /// 删除指定文件夹下 指定类型文件
        /// </summary>
        /// <param name="folder">文件夹路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">查询模式</param>
        public static async Task DeleteFilesAsync(string folder, string pattern = "*", SearchOption option = SearchOption.AllDirectories)
        {
            var files = GetFilesInfo(folder, pattern, option);
            await Task.Run(() => { Parallel.ForEach(files, file => { file.Delete(); }); });
        }

        /// <summary>
        /// 删除指定文件夹下 指定类型文件
        /// </summary>
        /// <param name="folder">文件夹路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">查询模式</param>
        public static void DeleteFiles(string folder, string pattern = "*", SearchOption option = SearchOption.AllDirectories)
        {
            var files = GetFilesInfo(folder, pattern, option);
            foreach (var file in files)
                if (file.Exists)
                    file.Delete();
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        public static async Task DeleteFolderAsync(string folder, SearchOption option = SearchOption.AllDirectories, bool isall = false)
        {
            await DeleteFolderAsync(new DirectoryInfo(folder), option, isall);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        public static void DeleteFolder(string folder, SearchOption option = SearchOption.AllDirectories, bool isall = false)
        {
            DeleteFolder(new DirectoryInfo(folder), option, isall);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        public static async Task DeleteFolderAsync(DirectoryInfo folder, SearchOption option = SearchOption.AllDirectories, bool isall = false)
        {
            if (folder.Exists)
            {
                if (isall)
                {
                    await Task.Run(() =>
                    {
                        var files = folder.GetFiles("*", option);
                        Parallel.ForEach(files, file => { file.Delete(); });
                    });
                }

                folder.Delete(isall);
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        public static void DeleteFolder(DirectoryInfo folder, SearchOption option = SearchOption.AllDirectories, bool isall = false)
        {
            if (folder.Exists)
            {
                if (isall)
                {
                    foreach (var file in folder.GetFiles("*", option))
                        if (file.Exists)
                            file.Delete();
                }

                folder.Delete(isall);
            }
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        public static async Task DeleteFlodersAsync(IEnumerable<string> folders, SearchOption option = SearchOption.AllDirectories, bool isall = false)
        {
            foreach (var folder in folders)
                await DeleteFolderAsync(new DirectoryInfo(folder), option, isall);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        public static void DeleteFloders(IEnumerable<string> folders, SearchOption option = SearchOption.AllDirectories, bool isall = false)
        {
            foreach (var folder in folders)
                DeleteFolder(new DirectoryInfo(folder), option, isall);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        public static void DeleteFloders(IEnumerable<DirectoryInfo> folders, SearchOption option = SearchOption.AllDirectories, bool isall = false)
        {
            foreach (var folder in folders)
                DeleteFolder(folder, option, isall);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        public static async Task DeleteFlodersAsync(IEnumerable<DirectoryInfo> folders, SearchOption option = SearchOption.AllDirectories, bool isall = false)
        {
            foreach (var folder in folders)
                await DeleteFolderAsync(folder, option, isall);
        }

        #endregion

        #region Create

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="folder">文件夹路径</param>
        /// <param name="clear">清除</param>
        public static void CreateFloder(string folder, bool clear = false)
        {
            var info = new DirectoryInfo(folder);
            // 判断文件夹是否存在 判断是否需要清空文件夹
            if (info.Exists)
            {
                if (clear)
                {
                    info.Delete(true);
                    info.Create();
                }
            }
            else info.Create();
        }

        #endregion

        #region Clear

        /// <summary>
        /// 清空当前文件夹
        /// </summary>
        public static void ClearFloder(string floder)
        {
            // 如果参数为空，则视为已成功清空
            if (!DirExists(floder)) return;
            // 删除当前文件夹下的文件
            foreach (var item in GetFilesInfo(floder, "*", SearchOption.TopDirectoryOnly)) DeleteFile(item);
            // 删除当前文件夹下的子文件夹
            foreach (var item in GetFlodersInfo(floder, "*", SearchOption.TopDirectoryOnly)) DeleteFolder(item, SearchOption.AllDirectories, true);
        }

        /// <summary>
        /// 清空当前文件夹
        /// </summary>
        public static void ClearFloder(DirectoryInfo floder)
        {
            // 如果参数为空，则视为已成功清空
            if (!floder.Exists) return;
            // 删除当前文件夹下的文件
            foreach (var item in GetFilesInfo(floder.FullName, "*", SearchOption.TopDirectoryOnly)) DeleteFile(item);
            // 删除当前文件夹下的子文件夹
            foreach (var item in GetFlodersInfo(floder.FullName, "*", SearchOption.TopDirectoryOnly)) ClearFloder(item);
        }

        #endregion
    }
}