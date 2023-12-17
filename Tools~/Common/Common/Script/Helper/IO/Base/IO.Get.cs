/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

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
        /// 获取最新的文件
        /// </summary>
        /// <param name="directoryInfos">文件夹列表</param>
        /// <returns><see cref="System.IO.FileInfo"/>文件夹信息</returns>
        public static FileInfo GetLastWriteTimeUtc(ICollection<FileInfo> directoryInfos)
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
            return directories.Select(directory => Path.Combine(directory, fileName)).FirstOrDefault(File.Exists);
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
        /// 获取指定路径相对于给定目录的相对路径。
        /// </summary>
        /// <param name="path">要获取其相对路径的文件或文件夹的路径。</param>
        /// <param name="directory">相对路径将基于此目录计算的目标目录。</param>
        /// <returns>相对路径字符串。</returns>
        /// <exception cref="ArgumentNullException">当 path 或 directory 为 null 时，抛出此异常。</exception>
        /// <exception cref="UriFormatException">使用 URI 库时，如果 path 或 directory 不是有效的 URI 字符串，则抛出此异常。</exception>
        public static string GetRelativePath(string path, string directory)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(directory)) throw new ArgumentNullException(nameof(directory));

            if (!directory.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                directory += Path.DirectorySeparatorChar;
            }

            try
            {
                // Optimization: Try a simple substring if possible
                path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                directory = directory.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

                if (path.StartsWith(directory, StringComparison.Ordinal))
                {
                    return path.Substring(directory.Length);
                }

                // Otherwise, use the URI library

                var pathUri = new Uri(path);
                var folderUri = new Uri(directory);

                return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString()
                    .Replace('/', Path.DirectorySeparatorChar));
            }
            catch (UriFormatException ufex)
            {
                throw new UriFormatException(
                    $"Failed to get relative path.\nPath: {path}\nDirectory:{directory}\n{ufex}");
            }
        }

        /// <summary>
        /// 获取当前所有文件夹中所有文件信息
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜查模式</param>
        /// <returns>所有文件信息数组</returns>
        public static IEnumerable<FileInfo> GetFilesInfo(
            in string value,
            in Func<FileInfo, bool> filtration,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return Array.Empty<FileInfo>();
            return new DirectoryInfo(value).GetFiles(pattern, option).Where(filtration);
        }

        /// <summary>
        /// 获取当前所有文件夹中所有文件信息
        /// </summary>
        public static IEnumerable<FileInfo> GetFilesInfo(
            in string value,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return Array.Empty<FileInfo>();
            return new DirectoryInfo(value).GetFiles(pattern, option);
        }

        /// <summary>
        /// 获取当前所有文件夹中所有文件信息
        /// </summary>
        public static IEnumerable<FileInfo> GetFilesInfo(
            in DirectoryInfo value,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!value.Exists) return Array.Empty<FileInfo>();
            return value.GetFiles(pattern, option);
        }

        /// <summary>
        /// 获取该文件夹下所有文件 绝对路径
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹 绝对路径</returns>
        public static IEnumerable<string> GetFiles(
            in string value,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            return GetFilesInfo(value, pattern, option).Select(item => item.FullName);
        }

        /// <summary>
        /// 获取该文件夹下所有文件 绝对路径
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹 绝对路径</returns>
        public static IEnumerable<string> GetFiles(
            in string value,
            in Func<FileInfo, bool> filtration,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            return GetFilesInfo(value, filtration, pattern, option).Select(item => item.FullName);
        }

        /// <summary>
        /// 获取该文件夹下所有文件 相对路径
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹 相对路径</returns>
        public static IEnumerable<string> GetFilesRelative(
            in string value,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            var len = value.Length;
            return GetFilesInfo(value, pattern, option).Select(item => item.FullName.Substring(len));
        }

        /// <summary>
        /// 获取该文件夹下所有文件 相对路径
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹 相对路径</returns>
        public static IEnumerable<string> GetFilesRelative(
            in string value,
            in Func<FileInfo, bool> filtration,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            var len = value.Length;
            return GetFilesInfo(value, filtration, pattern, option).Select(item => item.FullName.Substring(len));
        }

        /// <summary>
        /// 获取该文件夹下所有文件名称
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件名称</returns>
        public static IEnumerable<string> GetFilesName(
            in string value,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            return GetFilesInfo(value, pattern, option).Select(item => item.Name);
        }

        /// <summary>
        /// 获取该文件夹下所有文件名称
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件名称</returns>
        public static IEnumerable<string> GetFilesName(
            in string value,
            in Func<FileInfo, bool> filtration,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            return GetFilesInfo(value, filtration, pattern, option).Select(item => item.Name);
        }

        /// <summary>
        /// 获取文件夹数组
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹名称</returns>
        public static IEnumerable<DirectoryInfo> GetFoldersInfo(
            in string value,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return Array.Empty<DirectoryInfo>();
            return new DirectoryInfo(value).GetDirectories(pattern, option);
        }

        /// <summary>
        /// 获取文件夹数组
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹名称</returns>
        public static IEnumerable<DirectoryInfo> GetFoldersInfo(
            in string value,
            in Func<DirectoryInfo, bool> filtration,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!Directory.Exists(value)) return Array.Empty<DirectoryInfo>();
            return new DirectoryInfo(value).GetDirectories(pattern, option).Where(filtration);
        }

        /// <summary>
        /// 获取文件夹数组
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹名称</returns>
        public static IEnumerable<string> GetFolders(
            in string value,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            return GetFoldersInfo(value, pattern, option).Select(item => item.FullName);
        }

        /// <summary>
        /// 获取文件夹数组
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹名称</returns>
        public static IEnumerable<string> GetFolders(
            in string value,
            in Func<DirectoryInfo, bool> filtration,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            return GetFoldersInfo(value, filtration, pattern, option).Select(item => item.FullName);
        }

        /// <summary>
        /// 获取该文件夹下所有文件夹名 不含子文件夹 不包含自己
        /// </summary>
        public static IEnumerable<string> GetFoldersName(
            in string value,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            return GetFoldersInfo(value, pattern, option).Select(item => item.Name);
        }

        /// <summary>
        /// 获取该文件夹下所有文件夹名 不含子文件夹 不包含自己
        /// </summary>
        public static IEnumerable<string> GetFoldersName(
            in string value,
            in Func<DirectoryInfo, bool> filtration,
            in string pattern = "*",
            in SearchOption option = SearchOption.TopDirectoryOnly)
        {
            return GetFoldersInfo(value, filtration, pattern, option).Select(item => item.Name);
        }

        /// <summary>
        /// 返回文件大小 默认单位KB
        /// </summary>
        public static double GetFileSize(in string path, in float unit = 1024f)
        {
            if (!ExistsFile(path)) return 0;
            return (new FileInfo(path).Length / unit);
        }

        /// <summary>
        /// 返回文件字节长度
        /// </summary>
        /// <param name="Path">文件相对路径</param>
        public static long GetFileLength(in string Path)
        {
            if (!ExistsFile(Path)) return 0;
            return new FileInfo(Path).Length;
        }

        /// <summary>
        /// 返回文件夹字节长度
        /// </summary>
        /// <param name="Path">文件相对路径</param>
        public static long GetFolderLength(in string Path)
        {
            if (!ExistsFolder(Path)) return 0;
            return new DirectoryInfo(Path).GetFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
        }

        /// <summary>
        /// 返回文件名，不含路径 默认带文件名后缀
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="extension">是否有后缀</param>
        /// <returns>文件名</returns>
        public static string GetFileName(string file, bool extension = true)
        {
            if (extension) return Path.GetFileName(file);
            return Path.GetFileNameWithoutExtension(file);
        }

        /// <summary>
        /// 返回文件名，不含路径 默认带文件名后缀
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>文件名</returns>
        public static string GetFileExtension(string file)
        {
            return Path.GetExtension(file);
        }

        /// <summary>
        /// 获取当前所有文件夹中所有文件信息
        /// </summary>
        /// <param name="value">文件夹路径</param>
        public static FileInfo GetFileInfo(string value)
        {
            if (!ExistsFile(value)) return null;
            return new FileInfo(value);
        }

        /// <summary>
        /// 获取文件的哈希值
        /// </summary>
        public static string GetFileHash(string Path)
        {
            if (!ExistsFile(Path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{Path}>, 不存在");
            using (var sha1 = SHA1.Create())
            {
                byte[] hashByte;
                using (var stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    hashByte = sha1.ComputeHash(stream);
                    stream.Close();
                    stream.Dispose();
                }

                sha1.Dispose();
                return BitConverter.ToString(hashByte).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 获取文件的MD5值
        /// </summary>
        public static string GetFileMD5(string path, long bufferSize = 1024 * 16)
        {
            if (!ExistsFile(path)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{path}>, 不存在");
            return GetMD5ByHashAlgorithm(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read),
                bufferSize);
        }

        /// <summary>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5
        /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算MD5值
        /// 可自定义缓冲区大小，计算速度较快
        /// </summary>
        internal static string GetMD5ByHashAlgorithm(Stream stream, long bufferSize = 1024 * 16)
        {
            var buffer = new byte[bufferSize];
            using (var inputStream = stream)
            {
                var hashAlgorithm = new MD5CryptoServiceProvider();
                int readLength; //每次读取长度
                var output = new byte[bufferSize];
                while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    //计算MD5
                    hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
                }

                //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
                var md5 = BitConverter.ToString(hashAlgorithm.Hash);
                hashAlgorithm.Clear();
                inputStream.Close();
                hashAlgorithm.Dispose();
                inputStream.Dispose();
                md5 = md5.Replace("-", "").ToLower();
                return md5;
            }
        }

        /// <summary>
        /// 获取最后写入时间
        /// </summary>
        public static DateTime GetFileLastWriteTimeUtc(string Path)
        {
            return !ExistsFile(Path) ? DateTime.MinValue : File.GetLastWriteTimeUtc(Path);
        }

        /// <summary>
        /// 获取最后写入时间
        /// </summary>
        public static DateTime GetFolderLastWriteTimeUtc(string Path)
        {
            return !ExistsFolder(Path) ? DateTime.MinValue : Directory.GetLastWriteTimeUtc(Path);
        }

        /// <summary>
        /// 获取创建文件夹时间
        /// </summary>
        public static DateTime GetFolderCreationTimeUtc(string Path)
        {
            return !ExistsFolder(Path) ? DateTime.MinValue : Directory.GetCreationTimeUtc(Path);
        }

        /// <summary>
        /// 获取创建文件时间
        /// </summary>
        public static DateTime GetFileCreationTimeUtc(string Path)
        {
            return !ExistsFile(Path) ? DateTime.MinValue : File.GetCreationTimeUtc(Path);
        }

        /// <summary>
        /// 获取文件属性
        /// </summary>
        public static FileAttributes GetFileAttributes(string Path)
        {
            return !ExistsFile(Path) ? 0 : File.GetAttributes(Path);
        }

        /// <summary>
        /// 获取文件SHA1
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">
        ///    <paramref name="filePath" /> 不存在
        /// </exception>
        public static string GetFileSHA1(string filePath)
        {
            if (!ExistsFile(filePath)) throw new FileNotFoundException($"获取文件的哈希值 参数错误 <{filePath}>, 不存在");
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using var crypt = SHA1.Create(); // 判断MD5 是否一致
                {
                    return BitConverter.ToString(crypt.ComputeHash(fileStream)).Replace("-", "").ToLower();
                }
            }
        }
    }
}