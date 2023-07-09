/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-06
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnityEngine
{
    using Unitx = Unit;

    /// <summary>
    /// 定时器 时间调度器
    /// </summary>
    public static partial class TimerSystem
    {
        public static class Settings
        {
            /// <summary>
            /// 开启 循环任务线程
            /// </summary>
            public static bool EnableLoopThread { get; set; } = true;
        }

        /// <summary>
        /// 计时器 精确时间刻度器
        /// </summary>
        [ContextStatic] private static Stopwatch Watch;

        /// <summary>
        /// 多层级定时器 主 有添加接口 有消耗 只有一个
        /// </summary>
        [ContextStatic] private static List<ITimerOperator> MainList;

        /// <summary>
        /// 多层级定时器 Task 副 没有添加接口 只有消耗 有多个
        /// </summary>
        [ContextStatic] private static List<ITimerContainer> TaskList;

        /// <summary>
        /// 无限循环定时器容器
        /// </summary>
        [ContextStatic] private static ITimerContainer LoopContainer;

        /// <summary>
        /// 当前容器列表剩余数量
        /// </summary>
        [ContextStatic] private static volatile int RemainNum;

        /// <summary>
        /// 更新刷新List时间
        /// </summary>
        [ContextStatic] internal static long UPDATELISTTIME;

        /// <summary>
        /// 容量
        /// </summary>
        [ContextStatic] private static volatile int Capacity;

        /// <summary>
        /// 更新刷新List时间
        /// </summary>
        [ContextStatic] private static long UpdateCacheTime;

        /// <summary>
        /// 线程句柄
        /// </summary>
        [ContextStatic] private static Task ThreadHandle;

        [ContextStatic] private static CancellationToken TaskHandleToken;

        [ContextStatic] private static CancellationTokenSource TaskHandleTokenSource;

        /// <summary>
        /// 计时器单位回调
        /// </summary>
        public static event TimerUnitsTask TimingUnitsEvent;

        internal static List<(long, long, long)> TimingUnits { get; private set; }

        private static Dictionary<long, ITimerExecutor> TimerExecutors;

        /// <summary>
        /// 当前计时器计算单位 ms
        /// </summary>
        public static long Unit { get; private set; }

        /// <summary>
        /// 计数器 代表当前程序运行时间 单位ms
        /// </summary>
        public static long Counter { get; private set; }

        /// <summary>
        /// 开关
        /// </summary>
        internal static bool SWITCH { get; private set; }

        /// <summary>
        /// 获取定时器信息
        /// </summary>
        public new static void ToString()
        {
            var builder = new StringBuilder();
            builder.Append("-----------<主定时器 层器>-----------").AppendLine();
            lock (MainList)
            {
                foreach (var item in MainList)
                {
                    builder.Append(item).AppendLine().AppendLine();
                }
            }

            builder.Append("-----------<无限循环 层级>-----------").AppendLine();
            lock (LoopContainer)
            {
                builder.Append(LoopContainer).AppendLine();
            }

            builder.Append("-----------<辅助执行 层级>-----------").AppendLine();
            lock (TaskList)
            {
                foreach (var item in TaskList)
                {
                    builder.Append(item).AppendLine();
                }
            }

            Print.Log(builder.ToString());
        }
    }
}