﻿#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 获取最新的文件
            /// </summary>
            /// <param name="directoryInfos">文件夹列表</param>
            /// <returns><see cref="System.IO.FileInfo"/>文件夹信息</returns>
            public static FileInfo GetLastWriteTimeUtc(IEnumerable<FileInfo> directoryInfos)
            {
                FileInfo last = null;
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
            /// 在给定的文件夹路径列表中查找指定文件名的文件，并返回第一个存在的文件完整路径。
            /// </summary>
            /// <param name="fileName">要查找的文件名。</param>
            /// <param name="directories">包含所有可能包含该文件的文件夹路径的 IEnumerable 类型实例。</param>
            /// <returns>如果找到该文件，则返回完整路径，否则返回 null。</returns>
            public static string TryPathsForFile(string fileName, in IEnumerable<string> directories)
            {
                return directories.Select(directory => Path.Combine(directory, fileName).Replace('\\', '/')).FirstOrDefault(File.Exists);
            }

            /// <summary>
            /// 在给定的文件夹路径列表中查找指定文件名的文件，并返回第一个存在的文件完整路径。
            /// </summary>
            /// <param name="fileName">要查找的文件名。</param>
            /// <param name="directories">包含所有可能包含该文件的文件夹路径的可变参数数组。</param>
            /// <returns>如果找到该文件，则返回完整路径，否则返回 null。</returns>
            public static string TryPathsForFile(in string fileName, params string[] directories)
            {
                return TryPathsForFile(fileName, (IEnumerable<string>)directories);
            }

            /// <summary>
            /// 获取当前所有文件夹中所有文件信息
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜查模式</param>
            /// <returns>所有文件信息数组</returns>
            public static IEnumerable<FileInfo> GetFilesInfo(
                string               path,
                Func<FileInfo, bool> filtration,
                string               pattern = "*",
                SearchOption         option  = SearchOption.TopDirectoryOnly)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                return !Directory.Exists(path)
                    ? Array.Empty<FileInfo>()
                    : new DirectoryInfo(path).GetFiles(pattern, option).Where(filtration);
            }

            /// <summary>
            /// 获取当前所有文件夹中所有文件信息
            /// </summary>
            public static IEnumerable<FileInfo> GetFilesInfo(
                string       path,
                string       pattern = "*",
                SearchOption option  = SearchOption.TopDirectoryOnly)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                return !Directory.Exists(path)
                    ? Array.Empty<FileInfo>()
                    : new DirectoryInfo(path).GetFiles(pattern, option);
            }

            /// <summary>
            /// 获取当前所有文件夹中所有文件信息
            /// </summary>
            public static IEnumerable<FileInfo> GetFilesInfo(
                in DirectoryInfo value,
                in string        pattern = "*",
                in SearchOption  option  = SearchOption.TopDirectoryOnly)
            {
                return !value.Exists ? Array.Empty<FileInfo>() : value.GetFiles(pattern, option);
            }

            /// <summary>
            /// 获取该文件夹下所有文件 绝对路径
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹 绝对路径</returns>
            public static IEnumerable<string> GetFiles(
                in string       path,
                in string       pattern = "*",
                in SearchOption option  = SearchOption.TopDirectoryOnly)
            {
                return GetFilesInfo(path, pattern, option).Select(item => item.FullName);
            }

            /// <summary>
            /// 获取该文件夹下所有文件 绝对路径
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹 绝对路径</returns>
            public static IEnumerable<string> GetFiles(
                in string               path,
                in Func<FileInfo, bool> filtration,
                in string               pattern = "*",
                in SearchOption         option  = SearchOption.TopDirectoryOnly)
            {
                return GetFilesInfo(path, filtration, pattern, option).Select(item => item.FullName);
            }

            /// <summary>
            /// 获取该文件夹下所有文件 相对路径
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹 相对路径</returns>
            public static IEnumerable<string> GetFilesRelative(
                in string       path,
                in string       pattern = "*",
                in SearchOption option  = SearchOption.TopDirectoryOnly)
            {
                var len = path.Length + 1;
                return GetFilesInfo(path, pattern, option).Select(item => item.FullName.Substring(len).Replace('\\', '/'));
            }

            /// <summary>
            /// 获取该文件夹下所有文件 相对路径
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件夹 相对路径</returns>
            public static IEnumerable<string> GetFilesRelative(
                in string               path,
                in Func<FileInfo, bool> filtration,
                in string               pattern = "*",
                in SearchOption         option  = SearchOption.TopDirectoryOnly)
            {
                var len = path.Length + 1;
                return GetFilesInfo(path, filtration, pattern, option).Select(item => item.FullName.Substring(len).Replace('\\', '/'));
            }

            /// <summary>
            /// 获取该文件夹下所有文件名称
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件名称</returns>
            public static IEnumerable<string> GetFilesName(
                in string       path,
                in string       pattern = "*",
                in SearchOption option  = SearchOption.TopDirectoryOnly)
            {
                return GetFilesInfo(path, pattern, option).Select(item => item.Name);
            }

            /// <summary>
            /// 获取该文件夹下所有文件名称
            /// </summary>
            /// <param name="path">路径</param>
            /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">搜索模式</param>
            /// <returns>所有文件名称</returns>
            public static IEnumerable<string> GetFilesName(
                in string               path,
                in Func<FileInfo, bool> filtration,
                in string               pattern = "*",
                in SearchOption         option  = SearchOption.TopDirectoryOnly)
            {
                return GetFilesInfo(path, filtration, pattern, option).Select(item => item.Name);
            }

            /// <summary>
            /// 返回文件大小 默认单位KB
            /// </summary>
            public static double GetFileSize(string path, in float unit = 1024f)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) return 0;
                return new FileInfo(path).Length / unit;
            }

            /// <summary>
            /// 返回文件字节长度
            /// </summary>
            /// <param name="path">文件相对路径</param>
            public static long GetFileLength(string path)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                return !ExistsFile(path) ? 0 : new FileInfo(path).Length;
            }

            /// <summary>
            /// 返回文件名，不含路径 默认带文件名后缀
            /// </summary>
            /// <param name="path">文件路径</param>
            /// <param name="extension">是否有后缀</param>
            /// <returns>文件名</returns>
            public static string GetFileName(string path, bool extension = true)
            {
                return extension ? Path.GetFileName(path) : Path.GetFileNameWithoutExtension(path);
            }

            /// <summary>
            /// 返回文件名，不含路径 默认带文件名后缀
            /// </summary>
            /// <param name="file">文件路径</param>
            /// <returns>文件名</returns>
            public static string GetFileExtension(string file) { return Path.GetExtension(file); }

            /// <summary>
            /// 获取当前所有文件夹中所有文件信息
            /// </summary>
            /// <param name="value">文件夹路径</param>
            public static FileInfo GetFileInfo(string value) { return new FileInfo(value); }

            /// <summary>
            /// 获取最后写入时间
            /// </summary>
            public static DateTime GetFileLastWriteTimeUtc(string path)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                return !ExistsFile(path) ? DateTime.MinValue : File.GetLastWriteTimeUtc(path);
            }

            /// <summary>
            /// 获取创建文件时间
            /// </summary>
            public static DateTime GetFileCreationTimeUtc(string path)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                return !ExistsFile(path) ? DateTime.MinValue : File.GetCreationTimeUtc(path);
            }

            /// <summary>
            /// 获取文件属性
            /// </summary>
            public static FileAttributes GetFileAttributes(string path)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                return !ExistsFile(path) ? 0 : File.GetAttributes(path);
            }

            /// <summary>
            /// 获取文件的MD5值
            /// </summary>
            public static string GetFileMD5(string path, int bufferSize = StreamExtend.BUFFER_SIZE)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{path}>, 不存在");
                using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                return stream.GetMD5(bufferSize);
            }

            /// <summary>
            /// 获取文件的MD5值
            /// </summary>
            public static async Task<string> GetFileMD5Async(string path, int bufferSize = StreamExtend.BUFFER_SIZE)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{path}>, 不存在");
                using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                return await stream.GetMD5Async(bufferSize);
            }

            /// <summary>
            /// 获取文件SHA1
            /// </summary>
            /// <param name="path">文件路径</param>
            /// <returns></returns>
            /// <exception cref="FileNotFoundException">
            ///    <paramref name="path" /> 不存在
            /// </exception>
            public static string GetFileSHA1(string path)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{path}>, 不存在");
                using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                return fileStream.GetSHA1();
            }

            /// <summary>
            /// 获取文件SHA1
            /// </summary>
            /// <param name="path">文件路径</param>
            /// <param name="bufferSize">容器大小</param>
            /// <returns></returns>
            /// <exception cref="FileNotFoundException">
            ///    <paramref name="path" /> 不存在
            /// </exception>
            public static async Task<string> GetFileSHA1Async(string path, int bufferSize = StreamExtend.BUFFER_SIZE)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{path}>, 不存在");
                using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                return await fileStream.GetSHA1Async(bufferSize);
            }

            /// <summary>
            /// 获取文件SHA256
            /// </summary>
            /// <param name="path">文件路径</param>
            /// <returns></returns>
            /// <exception cref="FileNotFoundException">
            ///    <paramref name="path" /> 不存在
            /// </exception>
            public static string GetFileSHA256(string path)
            {
                if (!ExistsFile(path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{path}>, 不存在");
                using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                return fileStream.GetSHA256();
            }

            /// <summary>
            /// 获取文件SHA256
            /// </summary>
            /// <param name="path">文件路径</param>
            /// <param name="bufferSize">容器大小</param>
            /// <returns></returns>
            /// <exception cref="FileNotFoundException">
            ///    <paramref name="path" /> 不存在
            /// </exception>
            public static async Task<string> GetFileSHA256Async(string path, int bufferSize = StreamExtend.BUFFER_SIZE)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{path}>, 不存在");
                using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                return await fileStream.GetSHA256Async(bufferSize);
            }

            /// <summary>
            /// 获取文件SHA384
            /// </summary>
            /// <param name="path">文件路径</param>
            /// <returns></returns>
            /// <exception cref="FileNotFoundException">
            ///    <paramref name="path" /> 不存在
            /// </exception>
            public static string GetFileSHA384(string path)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{path}>, 不存在");
                using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                return fileStream.GetSHA384();
            }

            /// <summary>
            /// 获取文件SHA384
            /// </summary>
            /// <param name="path">文件路径</param>
            /// <param name="bufferSize">容器大小</param>
            /// <returns></returns>
            /// <exception cref="FileNotFoundException">
            ///    <paramref name="path" /> 不存在
            /// </exception>
            public static async Task<string> GetFileSHA384Async(string path, int bufferSize = StreamExtend.BUFFER_SIZE)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{path}>, 不存在");
                using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                return await fileStream.GetSHA384Async(bufferSize);
            }

            /// <summary>
            /// 获取文件SHA512
            /// </summary>
            /// <param name="path">文件路径</param>
            /// <returns></returns>
            /// <exception cref="FileNotFoundException">
            ///    <paramref name="path" /> 不存在
            /// </exception>
            public static string GetFileSHA512(string path)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{path}>, 不存在");
                using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                return fileStream.GetSHA512();
            }

            /// <summary>
            /// 获取文件SHA512
            /// </summary>
            /// <param name="path">文件路径</param>
            /// <param name="bufferSize">容器大小</param>
            /// <returns></returns>
            /// <exception cref="FileNotFoundException">
            ///    <paramref name="path" /> 不存在
            /// </exception>
            public static async Task<string> GetFileSHA512Async(string path, int bufferSize = StreamExtend.BUFFER_SIZE)
            {
                path = path.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!ExistsFile(path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{path}>, 不存在");
                using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                return await fileStream.GetSHA512Async(bufferSize);
            }
        }

        #endregion
    }
}