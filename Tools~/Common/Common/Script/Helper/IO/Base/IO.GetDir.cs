/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-18
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AIO
{
    public partial class AHelper
    {
        public partial class IO
        {
            /// <summary>
            /// 获取最新的文件夹
            /// </summary>
            /// <param name="directoryInfos">文件夹列表</param>
            /// <returns><see cref="System.IO.DirectoryInfo"/>文件夹信息</returns>
            public static DirectoryInfo GetLastWriteTimeUtc(IEnumerable<DirectoryInfo> directoryInfos)
            {
                DirectoryInfo last = null;
                foreach (var directoryInfo in directoryInfos)
                {
                    if (last is null)
                    {
                        last = directoryInfo;
                        continue;
                    }

                    if (last.LastWriteTimeUtc < directoryInfo.LastWriteTimeUtc)
                        last = directoryInfo;
                }

                return last;
            }

            /// <summary>
            /// 获取文件夹数组
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹名称</returns>
            public static IEnumerable<DirectoryInfo> GetDirsInfo(
                string path,
                string pattern = "*",
                SearchOption option = SearchOption.TopDirectoryOnly)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                return !Directory.Exists(path)
                    ? Array.Empty<DirectoryInfo>()
                    : new DirectoryInfo(path).GetDirectories(pattern, option);
            }

            /// <summary>
            /// 获取文件夹数组
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹名称</returns>
            public static IEnumerable<DirectoryInfo> GetDirsInfo(
                string path,
                Func<DirectoryInfo, bool> filtration,
                string pattern = "*",
                SearchOption option = SearchOption.TopDirectoryOnly)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                return !Directory.Exists(path)
                    ? Array.Empty<DirectoryInfo>()
                    : new DirectoryInfo(path).GetDirectories(pattern, option).Where(filtration);
            }

            /// <summary>
            /// 获取文件夹数组
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹名称</returns>
            public static IEnumerable<string> GetDirs(
                string path,
                string pattern = "*",
                SearchOption option = SearchOption.TopDirectoryOnly)
            {
                return GetDirsInfo(path, pattern, option).Select(item => item.FullName.Replace('\\', '/'));
            }

            /// <summary>
            /// 获取文件夹数组
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹名称</returns>
            public static IEnumerable<string> GetDirs(
                in string path,
                in Func<DirectoryInfo, bool> filtration,
                in string pattern = "*",
                in SearchOption option = SearchOption.TopDirectoryOnly)
            {
                return GetDirsInfo(path, filtration, pattern, option).Select(item => item.FullName.Replace('\\', '/'));
            }

            /// <summary>
            /// 获取该文件夹下所有文件夹名 不含子文件夹 不包含自己
            /// </summary>
            public static IEnumerable<string> GetDirsName(
                in string path,
                in string pattern = "*",
                in SearchOption option = SearchOption.TopDirectoryOnly)
            {
                return GetDirsInfo(path, pattern, option).Select(item => item.Name);
            }

            /// <summary>
            /// 获取该文件夹下所有文件夹名 不含子文件夹 不包含自己
            /// </summary>
            public static IEnumerable<string> GetDirsName(
                in string path,
                in Func<DirectoryInfo, bool> filtration,
                in string pattern = "*",
                in SearchOption option = SearchOption.TopDirectoryOnly)
            {
                return GetDirsInfo(path, filtration, pattern, option).Select(item => item.Name);
            }

            /// <summary>
            /// 返回文件夹字节长度
            /// </summary>
            /// <param name="path">文件相对路径</param>
            public static long GetDirLength(string path)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                return !ExistsDir(path)
                    ? 0
                    : new DirectoryInfo(path).GetFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
            }

            /// <summary>
            /// 获取最后写入时间
            /// </summary>
            public static DateTime GetDirLastWriteTimeUtc(string path)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                return !ExistsDir(path) ? DateTime.MinValue : Directory.GetLastWriteTimeUtc(path);
            }

            /// <summary>
            /// 获取创建文件夹时间
            /// </summary>
            public static DateTime GetDirCreationTimeUtc(string path)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                return !ExistsDir(path) ? DateTime.MinValue : Directory.GetCreationTimeUtc(path);
            }
        }
    }
}