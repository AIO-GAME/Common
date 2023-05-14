/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 函数扩展
    /// </summary>
    public static partial class ActionExtend
    {
        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetTotalTime(this Action action)
        {
            var watch = Stopwatch.StartNew();
            action();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetTotalTime<T1>(this Action<T1> action, in T1 arg1)
        {
            var watch = Stopwatch.StartNew();
            action.Invoke(arg1);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetTotalTime<T1>(this Action<T1, T1> action, in T1 arg1, in T1 arg2)
        {
            var watch = Stopwatch.StartNew();
            action.Invoke(arg1, arg2);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetTotalTime<T1>(this Action<T1, T1, T1> action, in T1 arg1, in T1 arg2, in T1 arg3)
        {
            var watch = Stopwatch.StartNew();
            action.Invoke(arg1, arg2, arg3);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetTotalTime<T1>(this Action<T1, T1, T1, T1> action, in T1 arg1, in T1 arg2, in T1 arg3, in T1 arg4)
        {
            var watch = Stopwatch.StartNew();
            action.Invoke(arg1, arg2, arg3, arg4);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetTotalTime<T1, T2>(this Action<T1, T2> action, in T1 arg1, in T2 arg2)
        {
            var watch = Stopwatch.StartNew();
            action.Invoke(arg1, arg2);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetTotalTime<T1, T2, T3>(this Action<T1, T2, T3> action, in T1 arg1, in T2 arg2, in T3 arg3)
        {
            var watch = Stopwatch.StartNew();
            action.Invoke(arg1, arg2, arg3);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetTotalTime<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
        {
            var watch = Stopwatch.StartNew();
            action.Invoke(arg1, arg2, arg3, arg4);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// 获取当前函数执行总时间
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long GetTotalTime<T1>(this Action<T1[]> action, params T1[] arg1)
        {
            var watch = Stopwatch.StartNew();
            action.Invoke(arg1);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}