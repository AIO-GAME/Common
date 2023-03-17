/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{

    using System;
    using System.IO;

    public static partial class StringExtend
    {
        #region Path

        /// <summary>
        /// 获取文件名 HasExtension = false 没有扩展名
        /// </summary>
        public static string PathGetFileName(this string FilePath, bool HasExtension = true)
        {
            if (HasExtension) return Path.GetFileName(@FilePath);
            else return Path.GetFileNameWithoutExtension(@FilePath);
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        public static string PathCombine(this string str1, string str2)
        {
            if (str1.IsNullOrEmpty()) return "";
            if (str2.IsNullOrEmpty()) return str1;
            return Path.Combine(str1, str2).Replace('\\', '/');              //.Replace('/', '\\')
        }

        /// <summary>
        /// 获取路径
        /// </summary>
        public static string PathCombine(this string str1, params string[] str2)
        {
            var a = new string[str2.Length + 1];
            a[0] = str1;
            Array.Copy(str2, 0, a, 1, str2.Length);
            return Path.Combine(a).Replace('\\', '/');                         //.Replace('/', '\\')
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        public static string PathGetExtension(this string FilePath)
        {
            return Path.GetExtension(@FilePath);
        }

        /// <summary>
        /// 修改文件扩展名
        /// </summary>
        public static string PathChangeExtension(this string FilePath, string Extension)
        {
            return Path.ChangeExtension(@FilePath, Extension);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string PathNormalize(this string path)
        {
            if (string.IsNullOrEmpty(path)) return path;
            return path.Replace('\\', '/').ToLower();
        }

        /// <summary>
        /// 获取根目录 I:\
        /// </summary>
        public static string PathGetRoot(this string FilePath)
        {
            return Path.GetPathRoot(@FilePath);
        }

        /// <summary>
        /// 获取上一级目录
        /// </summary>
        public static string PathGetLastFloder(this string FilePath)
        {
            return Path.GetDirectoryName(@FilePath);
        }

        #endregion
    }
}
