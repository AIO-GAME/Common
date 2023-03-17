/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace AIO
{
    /// <summary> 获取属性 </summary>
    public static partial class IOUtils
    {
        /// <summary>
        /// 获取当前所有文件夹中所有文件信息
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜查模式</param>
        /// <returns>所有文件信息数组</returns>
        public static FileInfo[] GetFilesInfo(string value, Func<FileInfo, bool> filtration, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new FileInfo[] { };
            var list = new List<FileInfo>();
            foreach (var item in new DirectoryInfo(value).GetFiles(pattern, option))
                if (filtration(item))
                    list.Add(item);
            return list.ToArray();
        }

        /// <summary>
        /// 获取当前所有文件夹中所有文件信息
        /// </summary>
        public static FileInfo[] GetFilesInfo(string value, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new FileInfo[] { };
            return new DirectoryInfo(value).GetFiles(pattern, option);
        }

        /// <summary>
        /// 获取该文件夹下所有文件 绝对路径
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹 绝对路径</returns>
        public static string[] GetFiles(string value, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new string[] { };
            var list = new List<string>();
            foreach (var item in GetFilesInfo(value, pattern, option))
                list.Add(item.FullName.Replace('\\', '/'));
            return list.ToArray();
        }

        /// <summary>
        /// 获取该文件夹下所有文件 绝对路径
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹 绝对路径</returns>
        public static string[] GetFiles(string value, Func<FileInfo, bool> filtration, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new string[] { };
            var list = new List<string>();
            foreach (var item in GetFilesInfo(value, filtration, pattern, option))
                list.Add(item.FullName.Replace('\\', '/'));
            return list.ToArray();
        }

        /// <summary>
        /// 获取该文件夹下所有文件 相对路径
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹 相对路径</returns>
        public static string[] GetFilesRelative(string value, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new string[] { };
            var list = new List<string>();
            var len = value.Length;
            foreach (var item in GetFilesInfo(value, pattern, option))
                list.Add(item.FullName.Substring(len).Replace('\\', '/'));
            return list.ToArray();
        }

        /// <summary>
        /// 获取该文件夹下所有文件 相对路径
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹 相对路径</returns>
        public static string[] GetFilesRelative(string value, Func<FileInfo, bool> filtration, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new string[] { };
            var list = new List<string>();
            var len = value.Length;
            foreach (var item in GetFilesInfo(value, filtration, pattern, option))
                list.Add(item.FullName.Substring(len).Replace('\\', '/'));
            return list.ToArray();
        }

        /// <summary>
        /// 获取该文件夹下所有文件名称
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件名称</returns>
        public static string[] GetFilesName(string value, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new string[] { };
            var list = new List<string>();
            foreach (var item in GetFilesInfo(value, pattern, option))
                list.Add(item.Name);
            return list.ToArray();
        }

        /// <summary>
        /// 获取该文件夹下所有文件名称
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件名称</returns>
        public static string[] GetFilesName(string value, Func<FileInfo, bool> filtration, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new string[] { };
            var list = new List<string>();
            foreach (var item in GetFilesInfo(value, filtration, pattern, option))
                list.Add(item.Name);
            return list.ToArray();
        }

        /// <summary>
        /// 获取文件夹数组
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹名称</returns>
        public static DirectoryInfo[] GetFlodersInfo(string value, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new DirectoryInfo[] { };
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
        public static DirectoryInfo[] GetFlodersInfo(string value, Func<DirectoryInfo, bool> filtration, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new DirectoryInfo[] { };
            var list = new List<DirectoryInfo>();
            foreach (var item in new DirectoryInfo(value).GetDirectories(pattern, option))
                if (!filtration(item))
                    list.Add(item);
            return list.ToArray();
        }

        /// <summary>
        /// 获取文件夹数组
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹名称</returns>
        public static string[] GetFloders(string value, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new string[] { };
            var list = new List<string>();
            foreach (var item in GetFlodersInfo(value, pattern, option))
                list.Add(item.FullName.Replace('\\', '/'));
            return list.ToArray();
        }

        /// <summary>
        /// 获取文件夹数组
        /// </summary>
        /// <param name="value">路径</param>
        /// <param name="filtration">过滤函数 Ture:过滤 False:不过滤</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <returns>所有文件夹名称</returns>
        public static string[] GetFloders(string value, Func<DirectoryInfo, bool> filtration, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new string[] { };
            var list = new List<string>();
            foreach (var item in GetFlodersInfo(value, filtration, pattern, option))
                list.Add(item.FullName.Replace('\\', '/'));
            return list.ToArray();
        }

        /// <summary>
        /// 获取该文件夹下所有文件夹名 不含子文件夹 不包含自己
        /// </summary>
        public static string[] GetFlodersName(string value, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new string[] { };
            var list = new List<string>();
            foreach (var item in GetFlodersInfo(value, pattern, option))
                list.Add(item.Name);
            return list.ToArray();
        }

        /// <summary>
        /// 获取该文件夹下所有文件夹名 不含子文件夹 不包含自己
        /// </summary>
        public static string[] GetFlodersName(string value, Func<DirectoryInfo, bool> filtration, string pattern = "*", SearchOption option = SearchOption.TopDirectoryOnly)
        {
            if (!DirExists(value)) return new string[] { };
            var list = new List<string>();
            foreach (var item in GetFlodersInfo(value, filtration, pattern, option))
                list.Add(item.Name);
            return list.ToArray();
        }

        /// <summary>
        /// 返回文件大小 默认单位KB
        /// </summary>
        public static double GetFileSize(string Path, float Unit = 1024f)
        {
            if (!FileExists(Path)) return 0;
            return (new FileInfo(Path).Length / Unit);
        }

        /// <summary>
        /// 返回文件字节长度
        /// </summary>
        /// <param name="Path">文件相对路径</param>
        public static long GetFileLength(string Path)
        {
            if (!FileExists(Path)) return 0;
            return (new FileInfo(Path).Length);
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
        /// 获取当前所有文件夹中所有文件信息
        /// </summary>
        /// <param name="value">文件夹路径</param>
        public static FileInfo GetFileInfo(string value)
        {
            if (!FileExists(value)) return null;
            return new FileInfo(value);
        }

        /// <summary>
        /// 获取文件的哈希值
        /// </summary>
        public static string GetFileHash(string Path)
        {
            if (!FileExists(Path)) return ($"获取文件的哈希值 参数错误 <{Path}>, 不存在");
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
        public static string GetFileMD5(string Path, long bufferSize = 1024 * 16)
        {
            if (!FileExists(Path)) return ($"获取文件的哈希值 参数错误 <{Path}>, 不存在");
            return GetMD5ByHashAlgorithm(File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.Read), bufferSize);
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
        public static DateTime GetLastWriteTimeUtc(string Path)
        {
            if (!FileExists(Path)) return DateTime.MinValue;
            return File.GetLastWriteTimeUtc(Path);
        }

        /// <summary>
        /// 获取创建文件时间
        /// </summary>
        public static DateTime GetCreationTimeUtc(string Path)
        {
            if (!FileExists(Path)) return DateTime.MinValue;
            return File.GetCreationTimeUtc(Path);
        }

        /// <summary>
        /// 获取文件属性
        /// </summary>
        public static FileAttributes GetAttributes(string Path)
        {
            if (!FileExists(Path)) return 0;
            return File.GetAttributes(Path);
        }
    }
}