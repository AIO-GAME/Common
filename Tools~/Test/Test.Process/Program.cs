using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        private static unsafe T1 LoadAsset<T1, T2, T3, T4, T5, T6>(
            in T1 obj,
            params T2[] objs
        )
            where T1 : Delegate
            where T2 : class
            where T3 : new()
            where T4 : struct, IComparable
            where T5 : MyClass<T2>
            where T6 : Enum
        {
            throw new NotImplementedException();
        }

        public class TA
        {
            public int a;

            public TA(int b = 0)
            {
                a = b;
            }

            public sealed override string ToString()
            {
                return a.ToString();
            }

            public sealed override int GetHashCode()
            {
                return a.GetHashCode();
            }

            public sealed override bool Equals(object obj)
            {
                if (obj is TA v)
                {
                    return a == v.a;
                }

                return false;
            }
        }

        static void Test()
        {
            // var methodInfo = typeof(Program).GetMethod("LoadAsset",
            //     System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            //
            // Console.WriteLine(typeof(MyClass<>).ToStrAlias());
            // Console.WriteLine(methodInfo.ToStrAlias());

            var a = new TA(1);
            var temp = new TA[] { new TA(1), new TA(2), new TA(3) };
            CPrint.Log(temp.MRemove(a).Exclude());
        }
    }
}