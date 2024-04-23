using System;
using System.Reflection;

namespace AIO
{
    class MyClass<T>
    where T : class, new() { }

    class MyClass1<T>
    where T : struct { }

    class Program
    {
        private static void Main(string[] args)
        {
            Test();
            Console.Read();
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        private class PAttribute : Attribute { }

        public class TA
        {
            public int a;

            public TA(int b = 0) { a = b; }

            public sealed override string ToString() { return a.ToString(); }

            public sealed override int GetHashCode() { return a.GetHashCode(); }

            public sealed override bool Equals(object obj)
            {
                if (obj is TA v)
                {
                    return a == v.a;
                }

                return false;
            }
        }

        private static void LoadAsset() { Console.WriteLine("11111111111111111".GetHashCode()); }

        private static void LoadAsset1() { Console.WriteLine("222222222222222"); }

        static void Test()
        {
            // // 将 aIntPtr 指针替换为 bIntPtr 指针
            // unsafe
            // {
            //     var loadAssetMethod =
            //         typeof(Program).GetMethod(nameof(LoadAsset), BindingFlags.Static | BindingFlags.NonPublic);
            //     var loadAsset1Method =
            //         typeof(Program).GetMethod(nameof(LoadAsset1), BindingFlags.Static | BindingFlags.NonPublic);
            //
            //     // 获取函数地址
            //
            //     var aIntPtr = loadAssetMethod.GetMethodBody().GetILAsByteArray().Length;
            //     var bIntPtr = loadAsset1Method.GetMethodBody().GetILAsByteArray().Length;
            //     Console.WriteLine(aIntPtr);
            //     Console.WriteLine(bIntPtr);
            //
            //     LoadAsset();
            //     Console.WriteLine("---------------------------");
            //     LoadAsset1();
            //
            //     // loadAssetMethod.Invoke(null, null);
            //     // Console.WriteLine("---------------------------");
            //     // loadAsset1Method.Invoke(null, null);
            // }
            // var content = AHelper.HTTP.Get("https://ftpshare-hot.ingcreations.com/qc/Version/Android.json");
            // Console.WriteLine(content);
            // if (string.IsNullOrEmpty(content))
            // {
            //     Console.WriteLine("请求失败");
            // }


            // var index = 0;
            // var dis = new DisplayList<int>();
            // dis.Add(++index, "ACC", index);
            // dis.Add(++index, "ABB", index);
            // dis.Add(++index, "ABBB", index);
            // dis.Add(++index, "B", index);
            // dis.Add(++index, "C", index);
            // dis.Add(++index, "a", index);
            // dis.Add(++index, "b", index);
            // dis.Add(++index, "c", index);
            // dis.Add(++index, "*", index);
            // dis.Add(++index, "$", index);
            //
            // dis.Sort();
            // foreach (var variable in dis)
            // {
            //     Console.WriteLine(variable + " " + dis.GetDisplay(variable.Key));
            // }

            var index = 0;
            var dis   = new PageList<int>();
            for (var i = 0; i < 32; i++)
            {
                dis.Add(++index);
            }


            Console.WriteLine("--------------------");
            dis.Sort();
            foreach (var variable in dis)
            {
                Console.WriteLine(variable + " ");
            }
        }
    }
}