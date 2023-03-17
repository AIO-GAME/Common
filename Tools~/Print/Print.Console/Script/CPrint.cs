using System;
using System.Diagnostics;

namespace AIO
{
    /// <summary>
    /// 输出
    /// </summary>
    public static partial class CPrint
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        [Conditional(Print.MACRO_DEFINITION)]
        public static void JsonResolver<T>(T obj)
        {
            if (Print.IsNotOut || Print.NoStatus(Print.Log)) return;
            Console.WriteLine(Json.Serialize(obj));
        }
    }
}