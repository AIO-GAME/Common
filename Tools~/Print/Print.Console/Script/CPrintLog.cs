using System;
using System.Collections;
using System.Diagnostics;

namespace AIO
{
    /// <summary>
    /// 打印
    /// </summary>
    public partial class CPrint : Print
    {
        /// <summary>
        /// 日志
        /// </summary>

        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log<T>(params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Console.WriteLine(IList(objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log(params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Console.WriteLine(IList(objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log<T>(in T obj, EFormat format)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            switch (format)
            {
                case EFormat.Json:
                    Console.WriteLine(Json.Serialize(obj));
                    return;
                case EFormat.Array:
                    if (obj is IEnumerable enumerable)
                    {
                        Log(enumerable);
                        return;
                    }
                    break;
            }
            Console.WriteLine(obj);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log(in object obj)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Console.WriteLine(obj);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Log<T>(in T objs) where T : IEnumerable
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            if (objs == null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            if (objs is IDictionary dictionary)
            {
                Console.WriteLine(IDictionary(dictionary));
                return;
            }

            if (objs is IList list)
            {
                Console.WriteLine(IList(list));
                return;
            }

            if (objs is ICollection collection)
            {
                Console.WriteLine(ICollection(collection));
                return;
            }

            Console.WriteLine(IEnumerable(objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogFormat<T1>(in string format, in T1 arg1)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Console.WriteLine(format, arg1);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogFormat<T1>(in string format, in T1 arg1, in T1 arg2)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Console.WriteLine(format, arg1, arg2);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogFormat<T1>(in string format, in T1 arg1, in T1 arg2, in T1 arg3)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Console.WriteLine(format, arg1, arg2, arg3);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogFormat<T1, T2>(in string format, in T1 arg1, in T2 arg2)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Console.WriteLine(format, arg1, arg2);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogFormat<T1, T2, T3>(in string format, in T1 arg1, in T2 arg2, in T3 arg3)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Console.WriteLine(format, arg1, arg2, arg3);
        }

        /// <summary>
        /// 使用指定的格式信息，将指定的对象数组（后跟当前行终止符）的文本表示形式写入标准输出流。
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogFormat<T>(in string format, params T[] args)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Console.WriteLine(format, args);
        }

        /// <summary>
        /// 使用指定的格式信息，将指定的对象数组（后跟当前行终止符）的文本表示形式写入标准输出流。
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void LogFormat(in string format, params object[] args)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.LOG)) return;
            Console.WriteLine(format, args);
        }
    }
}