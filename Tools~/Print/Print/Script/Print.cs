#region

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

#endregion

namespace AIO.Internal
{
    /// <summary>
    /// 打印输出
    /// </summary>
    public class Print
    {
        /// <summary>
        /// 日志
        /// </summary>
        internal const int LOG = 1;

        /// <summary>
        /// 警告
        /// </summary>
        internal const int WARNING = 2;

        /// <summary>
        /// 错误
        /// </summary>
        internal const int ERROR = 4;

        /// <summary>
        /// 异常
        /// </summary>
        internal const int EXCEPTION = 8;

        /// <summary>
        /// 宏定义
        /// </summary>
        internal const string MACRO_DEFINITION = "DEBUG";

        /// <summary>
        /// 输出开关
        /// </summary>
        internal static bool IsNotOut { get; private set; }

        /// <summary>
        /// 类型
        /// </summary>
        internal static int CurOutLevel { get; private set; } = (int)EPrint.ALL;

        /// <summary>
        /// 显示日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Level(in EPrint type)
        {
            CurOutLevel = type.GetHashCode();
        }

        /// <summary>
        /// 显示日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Show()
        {
            IsNotOut = false;
        }

        /// <summary>
        /// 显示日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Show(in EPrint type)
        {
            IsNotOut    = false;
            CurOutLevel = type.GetHashCode();
        }

        /// <summary>
        /// 关闭日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Close()
        {
            IsNotOut = true;
        }

        /// <summary>
        /// 判断状态
        /// </summary>
        internal static bool NoStatus(in int status)
        {
            if (CurOutLevel < 0) return true;
            return (CurOutLevel & status) != status;
        }


        internal static IEnumerable<char> IEnumerable(in IEnumerable objs)
        {
            var str = new StringBuilder();
            var count = 0;
            foreach (var item in objs)
                str.Append(count++).
                    Append(':').
                    Append(item is null ? "null" : item.ToString()).
                    Append("\r\n");

            var message = $"Count: {count}\r\n";
            if (str.Length > 0)
            {
                str.Insert(0, message);
                message = str.Remove(str.Length - 2, 2).ToString();
            }

            return message;
        }

        internal static IEnumerable<char> ICollection(in ICollection objs)
        {
            var r = $"Count: {objs.Count}\r\n";
            if (objs.Count > 0)
            {
                var index = 1;
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs)
                    str.Append(index++).
                        Append(':').
                        Append(item is null ? "null" : item.ToString()).
                        Append("\r\n");

                r = str.Remove(str.Length - 2, 2).ToString();
            }

            return r;
        }

        internal static IEnumerable<char> IList(in IList objs)
        {
            var r = $"Count: {objs.Count}\r\n";
            if (objs.Count > 0)
            {
                var index = 1;
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs)
                    str.Append(index++).
                        Append(':').
                        Append(item is null ? "null" : item.ToString()).
                        Append("\r\n");

                r = str.Remove(str.Length - 2, 2).ToString();
            }

            return r;
        }

        internal static IEnumerable<char> IDictionary(in IDictionary objs)
        {
            var r = $"Count: {objs.Count}\r\n";
            if (objs.Count > 0)
            {
                var index = 1;
                var str = new StringBuilder(r).Append("\r\n");
                foreach (DictionaryEntry dic in objs)
                    str.Append(index++).
                        Append(':').
                        Append(dic.Key is null ? "null" : dic.Key.ToString()).
                        Append(':').
                        Append(dic.Value is null ? "null" : dic.Value.ToString()).
                        Append("\r\n");

                r = str.Remove(str.Length - 2, 2).ToString();
            }

            return r;
        }
    }
}