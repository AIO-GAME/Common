using System;
using System.Collections.Generic;
using System.IO;

namespace AIO
{
    class MyClass<T> where T : class
    {
    }

    class Program
    {
        private static void Main(string[] args)
        {
            Test();
            Console.Read();
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        private class PAttribute : Attribute
        {
        }

        private static void LoadAsset<T1, T2, T3, T4, T5, T6>(
            T1 obj,
            params T2[] objs
        )
            where T1 : Delegate
            where T2 : class
            where T3 : new()
            where T4 : struct, IComparable
            where T5 : MyClass<T2>
            where T6 : Enum
        {
        }


        static void Test()
        {
            // var methodInfo = typeof(Program).GetMethod("LoadAsset",
            //     System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            //
            // var str = methodInfo.ToStrAlias();
            // // Console.WriteLine(typeof(MyClass<>).ToStrAlias());
            // Console.WriteLine("井井井井井井井井井井井井井井井井井井井井");
            // Console.WriteLine(str);

            var temp = PoolStringBuilder.Alloc();
            Console.WriteLine(temp.GetHashCode());
            temp.Append("Hello World!");
            Console.WriteLine(temp.GetHashCode());

            // Console.WriteLine(temp.ToString());
            PoolStringBuilder.Recycle(temp);
            //
            // var temp2 = PoolStringBuilder.Alloc();
            // temp2.Append("Hello World!");
            // Console.WriteLine(temp2.ToString());
            // PoolStringBuilder.Recycle(temp2);
        }
    }
}