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
            public static DirectoryInfo GetLastWriteTimeUtc(ICollection<DirectoryInfo> directoryInfos)
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
            /// <param name="value">路径</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹名称</returns>
            public static IEnumerable<DirectoryInfo> GetDirsInfo(
                in string value,
                in string pattern = "*",
                in SearchOption option = SearchOption.TopDirectoryOnly)
            {
                return !Directory.Exists(value)
                    ? Array.Empty<DirectoryInfo>()
                    : new DirectoryInfo(value).GetDirectories(pattern, option);
            }

            /// <summary>
            /// 获取文件夹数组
            /// </summary>
            /// <param name="value">路径</param>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹名称</returns>
            public static IEnumerable<DirectoryInfo> GetDirsInfo(
                in string value,
                in Func<DirectoryInfo, bool> filtration,
                in string pattern = "*",
                in SearchOption option = SearchOption.TopDirectoryOnly)
            {
                return !Directory.Exists(value)
                    ? Array.Empty<DirectoryInfo>()
                    : new DirectoryInfo(value).GetDirectories(pattern, option).Where(filtration);
            }

            /// <summary>
            /// 获取文件夹数组
            /// </summary>
            /// <param name="value">路径</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹名称</returns>
            public static IEnumerable<string> GetDirs(
                in string value,
                in string pattern = "*",
                in SearchOption option = SearchOption.TopDirectoryOnly)
            {
                return GetDirsInfo(value, pattern, option).Select(item => item.FullName);
            }

            /// <summary>
            /// 获取文件夹数组
            /// </summary>
            /// <param name="value">路径</param>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹名称</returns>
            public static IEnumerable<string> GetDirs(
                in string value,
                in Func<DirectoryInfo, bool> filtration,
                in string pattern = "*",
                in SearchOption option = SearchOption.TopDirectoryOnly)
            {
                return GetDirsInfo(value, filtration, pattern, option).Select(item => item.FullName);
            }

            /// <summary>
            /// 获取该文件夹下所有文件夹名 不含子文件夹 不包含自己
            /// </summary>
            public static IEnumerable<string> GetDirsName(
                in string value,
                in string pattern = "*",
                in SearchOption option = SearchOption.TopDirectoryOnly)
            {
                return GetDirsInfo(value, pattern, option).Select(item => item.Name);
            }

            /// <summary>
            /// 获取该文件夹下所有文件夹名 不含子文件夹 不包含自己
            /// </summary>
            public static IEnumerable<string> GetDirsName(
                in string value,
                in Func<DirectoryInfo, bool> filtration,
                in string pattern = "*",
                in SearchOption option = SearchOption.TopDirectoryOnly)
            {
                return GetDirsInfo(value, filtration, pattern, option).Select(item => item.Name);
            }

            /// <summary>
            /// 返回文件夹字节长度
            /// </summary>
            /// <param name="Path">文件相对路径</param>
            public static long GetDirLength(in string Path)
            {
                return !ExistsDir(Path)
                    ? 0
                    : new DirectoryInfo(Path).GetFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
            }

            /// <summary>
            /// 获取最后写入时间
            /// </summary>
            public static DateTime GetDirLastWriteTimeUtc(string Path)
            {
                return !ExistsDir(Path) ? DateTime.MinValue : Directory.GetLastWriteTimeUtc(Path);
            }

            /// <summary>
            /// 获取创建文件夹时间
            /// </summary>
            public static DateTime GetDirCreationTimeUtc(string Path)
            {
                return !ExistsDir(Path) ? DateTime.MinValue : Directory.GetCreationTimeUtc(Path);
            }
        }
    }
}