using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AIO.Internal;

namespace AIO
{
    /// <summary>
    /// 打印
    /// </summary>
    public partial class CPrint : Print
    {
        ///// <summary>
        ///// 日志
        ///// </summary>
        //[Conditional(MACRO_DEFINITION)]
        //public static void Log(object arg1, object arg2, object arg3, object arg4, object arg5)
        //{
        //    if (IsNotOut || NoStatus(LOG)) return;
        //    Console.WriteLine(new object[] { arg1, arg2, arg3, arg4, arg5 });
        //}

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Log<T>(in T obj, in EFormat format)
        {
            if (IsNotOut || NoStatus(LOG)) return;
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
        [Conditional(MACRO_DEFINITION)]
        public static void Log(in object obj)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Console.WriteLine(obj);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Log(in IDictionary objs)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            if (objs == null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            Console.WriteLine(IDictionary(objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Log(in IList objs)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            if (objs == null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            Console.WriteLine(IList(objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Log(in ICollection objs)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            if (objs == null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            Console.WriteLine(ICollection(objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void Log(in IEnumerable objs)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            if (objs == null)
            {
                Console.WriteLine("{0} is null", nameof(objs));
                return;
            }

            Console.WriteLine(IEnumerable(objs));
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void LogFormat<T1>(in string format, in T1 arg1)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Console.WriteLine(format, arg1);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void LogFormat<T1>(in string format, in T1 arg1, in T1 arg2)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Console.WriteLine(format, arg1, arg2);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void LogFormat<T1>(in string format, in T1 arg1, in T1 arg2, in T1 arg3)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Console.WriteLine(format, arg1, arg2, arg3);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void LogFormat<T1, T2>(in string format, in T1 arg1, in T2 arg2)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Console.WriteLine(format, arg1, arg2);
        }

        /// <summary>
        /// 日志
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void LogFormat<T1, T2, T3>(in string format, in T1 arg1, in T2 arg2, in T3 arg3)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Console.WriteLine(format, arg1, arg2, arg3);
        }

        /// <summary>
        /// 使用指定的格式信息，将指定的对象数组（后跟当前行终止符）的文本表示形式写入标准输出流。
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void LogFormat<T>(in string format, params T[] args)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Console.WriteLine(format, args);
        }

        /// <summary>
        /// 使用指定的格式信息，将指定的对象数组（后跟当前行终止符）的文本表示形式写入标准输出流。
        /// </summary>
        [Conditional(MACRO_DEFINITION)]
        public static void LogFormat(in string format, params object[] args)
        {
            if (IsNotOut || NoStatus(LOG)) return;
            Console.WriteLine(format, args);
        }
    }
}