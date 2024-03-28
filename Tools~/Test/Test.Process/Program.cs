using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AIO
{
    class MyClass<T> where T : class, new()
    {
    }

    class MyClass1<T> where T : struct
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


        private static T1 LoadAsset<T1, T2, T3, T4, T5, T6>(
            in Action<T1, T2> obj,
            params T2[] objs
        )
            where T2 : class
            where T3 : class, new()
            where T4 : struct, IComparable
            where T5 : MyClass<T3>
            where T6 : Enum
        {
            throw new NotImplementedException();
        }

        private static T1 LoadAsset1<T1>()
        {
            throw new NotImplementedException();
        }

        static void Test()
        {
            var methodInfo = typeof(Program).GetMethod(nameof(LoadAsset),
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            Console.WriteLine(methodInfo.ToDetails());

            var methodInfo1 = typeof(Program).GetMethod(nameof(LoadAsset1),
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            Console.WriteLine(methodInfo1.ToDetails());
            Console.WriteLine(typeof(MyClass<>).ToDetails());
            Console.WriteLine(typeof(MyClass1<>).ToDetails());
        }
    }
}