using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private class MyClass
        {
            /// <summary>
            ///  领奖状态(0-待领奖 1-已领奖)
            /// </summary>
            public int DrawState;

            /// <summary>
            /// 任务状态(0-待完成 1-已完成)
            /// </summary>
            public int FinishState;

            public int TaskId;
        }

        static void Test()
        {
            var path = "E:\\Project\\AIO\\com.whoot.gf.security\\Runtime\\Encryptions\\MD5Encryption.cs";
            var testA    = File.OpenRead(path);
            CPrint.Run("A", () =>
            {
                Console.WriteLine(testA.GetMD5());
            });
            CPrint.Run("B", () =>
            {
                Console.WriteLine(testA.MD5String());
            });

            Console.WriteLine(PrWin.Certutil.MD5(path).Sync().ToString());
            return;


            var list = new List<MyClass>();
            for (var i = 1; i <= 3; i++) list.Add(new MyClass { DrawState = 0, FinishState = 1, TaskId = i });
            for (var i = 1; i <= 3; i++) list.Add(new MyClass { DrawState = 0, FinishState = 0, TaskId = 3 + i });
            for (var i = 1; i <= 3; i++) list.Add(new MyClass { DrawState = 1, FinishState = 1, TaskId = i + 6 });


            var sort = list.OrderBy(a => a.DrawState).ThenByDescending(a => a.FinishState).ThenBy(a => a.TaskId).ToList();
            foreach (var item in sort)
            {
                if (item.FinishState == 1 && item.DrawState == 0)
                {
                    Console.WriteLine("ID:" + item.TaskId + "可领奖");
                    continue;
                }

                if (item.FinishState == 0)
                {
                    Console.WriteLine("ID:" + item.TaskId + "未完成");
                    continue;
                }

                Console.WriteLine("ID:" + item.TaskId + "已领奖");
            }

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
            //
            var dis = new PageList<int>();
            dis.PageSize = 10;

            // for (var i = 10000; i >= 0; i--) dis.Add(AHelper.Random.RandInt32(0, 10000));
            // dis.PageSize = 3;
            // dis.Update();
            var c       = new ComparerA();
            var index   = 0;
            var curTime = DateTime.Now;
            // dis.Sort(c);
            // Console.WriteLine($"排序耗时 : {DateTime.Now - curTime} -> Array.Sort");
            // foreach (var variable in dis.CurrentPageValues)
            // {
            //     Console.WriteLine(variable + " ");
            // }
            //
            // dis.Clear();
            // for (var i = 10000; i >= 0; i--) dis.Add(AHelper.Random.RandInt32(0, 10000));
            // dis.PageSize = 3;
            // dis.Update();
            // Console.WriteLine("--------------------");
            // curTime = DateTime.Now;
            // dis.Sort((a, b) => a.CompareTo(b));
            // Console.WriteLine($"排序耗时 : {DateTime.Now - curTime} ->  Array.Sort");
            // foreach (var variable in dis.CurrentPageValues)
            // {
            //     Console.WriteLine(variable + " ");
            // }

            //
            // dis.Clear();
            // for (var i = 10000; i >= 0; i--) dis.Add(AHelper.Random.RandInt32(0, 10000));
            // dis.Update();
            // Console.WriteLine("--------------------");
            // curTime = DateTime.Now;
            // dis.Sort(c);
            // Console.WriteLine($"排序耗时 : {DateTime.Now - curTime} -> 堆排序");
            // var index = 0;
            // foreach (var variable in dis)
            // {
            //     if (index++ > dis.PageSize) break;
            //     Console.WriteLine(variable + " ");
            // }

            // dis.Clear();
            // dis.Add(new int[] { 124, 3334, 3675, 8329, 8329, 5341 });
            //
            // dis.Update();
            // Console.WriteLine("--------------------");
            // curTime = DateTime.Now;
            // dis.Sort(c, ESort.Select);
            // Console.WriteLine($"排序耗时 : {DateTime.Now - curTime} -> 快速排序");
            // index = 0;
            // foreach (var variable in dis)
            // {
            //     if (index++ > dis.PageSize) break;
            //     Console.WriteLine("                     " + variable);
            // }

            // dis.Clear();
            // for (var i = 10000; i >= 0; i--) dis.Add(AHelper.Random.RandInt32(0, 10000));
            // dis.PageSize = 3;
            // dis.Update();
            // Console.WriteLine("--------------------");
            // curTime = DateTime.Now;
            // dis.Sort(c, ESort.Quick);
            // Console.WriteLine($"排序耗时 : {DateTime.Now - curTime} -> 快速排序");
            // foreach (var variable in dis.CurrentPageValues)
            // {
            //     Console.WriteLine(variable + " ");
            // }
            //
            // dis.Clear();
            // for (var i = 10000; i >= 0; i--) dis.Add(AHelper.Random.RandInt32(0, 10000));
            // dis.PageSize = 3;
            // dis.Update();
            // Console.WriteLine("--------------------");
            // curTime = DateTime.Now;
            // dis.Sort(c, ESort.Merge);
            // Console.WriteLine($"排序耗时 : {DateTime.Now - curTime} -> 归并排序");
            // foreach (var variable in dis.CurrentPageValues)
            // {
            //     Console.WriteLine(variable + " ");
            // }
        }

        public class ComparerA : IComparer<int>
        {
            public int Compare(int x, int y) { return x.CompareTo(y); }
        }

        public class ComparerB : IComparer<int>
        {
            public int Compare(int x, int y) { return y.CompareTo(x); }
        }
    }
}