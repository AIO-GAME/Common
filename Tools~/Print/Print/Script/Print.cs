using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 打印输出
    /// </summary>
    public partial class Print
    {
        /// <summary>
        /// 输出开关
        /// </summary>
        internal static bool IsNotOut { get; private set; } = false;

        /// <summary>
        /// 类型
        /// </summary>
        internal static int CurOutLevel { get; private set; } = (int)EPrint.ALL;

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
            IsNotOut = false;
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
        /// 宏定义
        /// </summary>
        internal const string MACRO_DEFINITION = "DEBUG";

        /// <summary>
        /// 判断状态
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool NoStatus(in int status)
        {
            if (CurOutLevel < 0) return true;
            return (CurOutLevel & status) != status;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<char> IEnumerable<T>(in T objs) where T : IEnumerable
        {
            var str = new StringBuilder();
            var count = 0;
            foreach (var item in objs)
            {
                str.Append(string.Format("{0}:{1}", count++, item)).Append("\r\n");
            }

            var message = string.Format("Count: {0}\r\n", count);
            if (str.Length > 0)
            {
                str.Insert(0, message);
                message = str.Remove(message.Length - 2, 2).ToString();
            }
            return message;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<char> ICollection<T>(in T objs) where T : ICollection
        {
            var r = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var index = 1;
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs)
                    str.Append(index++).Append(':').Append(item).Append("\r\n");
                r = str.Remove(r.Length - 2, 2).ToString();
            }
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<char> IList<T>(in T objs) where T : IList
        {
            var r = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var index = 1;
                var str = new StringBuilder(r).Append("\r\n");
                foreach (var item in objs)
                    str.Append(index++).Append(':').Append(item).Append("\r\n");
                r = str.Remove(r.Length - 2, 2).ToString();
            }
            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IEnumerable<char> IDictionary<T>(in T objs) where T : IDictionary
        {
            var r = string.Format("Count: {0}\r\n", objs.Count);
            if (objs.Count > 0)
            {
                var index = 1;
                var str = new StringBuilder(r).Append("\r\n");
                foreach (DictionaryEntry dic in objs)
                {
                    if (dic.Value != null)
                    {
                        str.Append($"[{index++}]{dic.Key}:{dic.Value}\r\n");
                    }
                    else str.Append($"[{index++}]{dic.Key}:NULL\r\n");
                }

                r = str.Remove(r.Length - 2, 2).ToString();
            }
            return r;
        }

    }
}