using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace AIO
{
    public static partial class StringExtend
    {
        /// <summary>
        /// 获取文件名 HasExtension = false 没有扩展名
        /// </summary>
        public static string PathGetFileName(this string filePath, in bool HasExtension = true)
        {
            if (HasExtension) return Path.GetFileName(filePath);
            return Path.GetFileNameWithoutExtension(filePath);
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        public static string PathCombine(this string str1, params string[] str2)
        {
            var a = new string[str2.Length + 1];
            a[0] = str1;
            Array.ConstrainedCopy(str2, 0, a, 1, str2.Length);
            return Path.Combine(a).Replace('\\', '/');
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        public static string PathCombine(this string str1, string str2)
        {
            return Path.Combine(str1, str2).Replace('\\', '/');
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        public static string PathCombine(this string str1, string str2, string str3)
        {
            return Path.Combine(str1, str2, str3).Replace('\\', '/');
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        public static string PathCombine(this string str1, string str2, string str3, string str4)
        {
            return Path.Combine(str1, str2, str3, str4).Replace('\\', '/');
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        public static string PathGetExtension(this string filePath)
        {
            return Path.GetExtension(filePath);
        }

        /// <summary>
        /// 修改文件扩展名
        /// </summary>
        public static string PathChangeExtension(this string filePath, string extension)
        {
            return Path.ChangeExtension(filePath, extension);
        }

        /// <summary>
        /// 一个新的规范化字符串，其文本值与此字符串相同，但其二进制表示形式符合范式
        /// </summary>
        public static string PathNormalize(this string path)
        {
            if (string.IsNullOrEmpty(path)) return path;
            return path.Replace('\\', '/').Normalize();
        }

        /// <summary>
        /// 获取根目录 I:\
        /// </summary>
        public static string PathGetRoot(this string filePath)
        {
            return Path.GetPathRoot(filePath);
        }

        /// <summary>
        /// 获取上一级目录
        /// </summary>
        public static string PathGetLastFloder(this string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }
    }
}