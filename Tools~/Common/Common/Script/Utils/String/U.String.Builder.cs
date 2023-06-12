/*|==========|*|
|*|Author:   |*| -> SAM
|*|Date:     |*| -> 2023-06-06
|*|==========|*/

using System;
using System.Text;

public partial class Utils
{
    public partial class String
    {
        [ThreadStatic] private static StringBuilder CacheBuilder;

        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="format">格式化</param>
        /// <param name="arg0">参数</param>
        /// <returns>字符串</returns>
        /// <exception cref="ArgumentNullException">参数异常</exception>
        public static string Format(string format, object arg0)
        {
            if (string.IsNullOrEmpty(format))
                throw new ArgumentNullException();

            CacheBuilder.Length = 0;
            CacheBuilder.AppendFormat(format, arg0);
            return CacheBuilder.ToString();
        }

        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="format">格式化</param>
        /// <param name="arg0">参数</param>
        /// <param name="arg1">参数</param>
        /// <returns>字符串</returns>
        /// <exception cref="ArgumentNullException">参数异常</exception>
        public static string Format(string format, object arg0, object arg1)
        {
            if (string.IsNullOrEmpty(format))
                throw new ArgumentNullException();

            CacheBuilder.Length = 0;
            CacheBuilder.AppendFormat(format, arg0, arg1);
            return CacheBuilder.ToString();
        }

        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="format">格式化</param>
        /// <param name="arg0">参数</param>
        /// <param name="arg1">参数</param>
        /// <param name="arg2">参数</param>
        /// <returns>字符串</returns>
        /// <exception cref="ArgumentNullException">参数异常</exception>
        public static string Format(string format, object arg0, object arg1, object arg2)
        {
            if (string.IsNullOrEmpty(format))
                throw new ArgumentNullException();

            CacheBuilder.Length = 0;
            CacheBuilder.AppendFormat(format, arg0, arg1, arg2);
            return CacheBuilder.ToString();
        }

        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="format">格式化</param>
        /// <param name="args">参数</param>
        /// <returns>字符串</returns>
        /// <exception cref="ArgumentNullException">参数异常</exception>
        public static string Format(string format, params object[] args)
        {
            if (string.IsNullOrEmpty(format))
                throw new ArgumentNullException();

            if (args == null)
                throw new ArgumentNullException();

            CacheBuilder.Length = 0;
            CacheBuilder.AppendFormat(format, args);
            return CacheBuilder.ToString();
        }

        /// <summary>
        /// 移除第一个字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字符串</returns>
        public static string RemoveFirstChar(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return str.Substring(1);
        }

        /// <summary>
        /// 移除最后一个字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字符串</returns>
        public static string RemoveLastChar(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 移除扩展名
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>字符串</returns>
        public static string RemoveExtension(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            var index = str.LastIndexOf(".", StringComparison.CurrentCulture);
            if (index == -1) return str;
            return str.Remove(index); //"assets/config/test.unity3d" --> "assets/config/test"
        }
    }
}