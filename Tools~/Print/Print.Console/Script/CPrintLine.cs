using System;
using System.Diagnostics;
using System.Linq;

namespace AIO
{
    public partial class CPrint
    {
        /// <summary>
        /// 
        /// </summary>

        [Conditional(Print.MACRO_DEFINITION)]
        public static void Line<T>(params T[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Console.WriteLine(string.Join(" ", objs.Select(arg => arg)));
        }

        /// <summary>
        /// 
        /// </summary>

        [Conditional(Print.MACRO_DEFINITION)]
        public static void Line(params object[] objs)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Console.WriteLine(string.Join(" ", objs.Select(arg => arg)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void Line<T>(T obj)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Console.WriteLine(obj);
        }
    }
}