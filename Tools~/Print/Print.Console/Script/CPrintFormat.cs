using System;
using System.Diagnostics;

namespace AIO
{
    public partial class CPrint
    {
        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Format<T1>(string format, T1 arg1)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Console.WriteLine(format, arg1);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Format<T1>(string format, T1 arg1, T1 arg2)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Console.WriteLine(format, arg1, arg2);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Format<T1>(string format, T1 arg1, T1 arg2, T1 arg3)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Console.WriteLine(format, arg1, arg2, arg3);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Format<T1, T2>(string format, T1 arg1, T2 arg2)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Console.WriteLine(format, arg1, arg2);
        }

        /// <summary>
        /// 
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Console.WriteLine(format, arg1, arg2, arg3);
        }

        /// <summary>
        /// 使用指定的格式信息，将指定的对象数组（后跟当前行终止符）的文本表示形式写入标准输出流。
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Format<T>(string format, params T[] args)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Console.WriteLine(format, args);
        }

        /// <summary>
        /// 使用指定的格式信息，将指定的对象数组（后跟当前行终止符）的文本表示形式写入标准输出流。
        /// </summary>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Format(string format, params object[] args)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Console.WriteLine(format, args);
        }
    }
}