/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// 函数扩展
    /// </summary>
    public static partial class ActionExtend
    {
        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        public static long GetTotalTime(this Action action)
        {
            var watch = new Stopwatch();
            watch.Start();
            var now = watch.ElapsedMilliseconds;
            action.Invoke();
            return watch.ElapsedMilliseconds - now;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        public static long GetTotalTime<T1>(this Action<T1> action, T1 arg1)
        {
            var watch = new Stopwatch();
            watch.Start();
            var now = watch.ElapsedMilliseconds;
            action.Invoke(arg1);
            return watch.ElapsedMilliseconds - now;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        public static long GetTotalTime<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            var watch = new Stopwatch();
            watch.Start();
            var now = watch.ElapsedMilliseconds;
            action.Invoke(arg1, arg2);
            return watch.ElapsedMilliseconds - now;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        public static long GetTotalTime<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
        {
            var watch = new Stopwatch();
            watch.Start();
            var now = watch.ElapsedMilliseconds;
            action.Invoke(arg1, arg2, arg3);
            return watch.ElapsedMilliseconds - now;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        public static long GetTotalTime<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var watch = new Stopwatch();
            watch.Start();
            var now = watch.ElapsedMilliseconds;
            action.Invoke(arg1, arg2, arg3, arg4);
            return watch.ElapsedMilliseconds - now;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        public static long GetTotalTime<T1>(this Action<T1[]> action, params T1[] arg1)
        {
            var watch = new Stopwatch();
            watch.Start();
            var now = watch.ElapsedMilliseconds;
            action.Invoke(arg1);
            return watch.ElapsedMilliseconds - now;
        }
    }
}
