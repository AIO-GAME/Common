namespace AIO
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    using UnityEngine.Profiling;

    /// <summary>
    /// 调试工具类
    /// </summary>
    public static class ProfilingUtils
    {
        static ProfilingUtils()
        {
            CurrentSegment = RootSegment = new ProfiledSegment(null, "Root");
        }

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object @lock = new object();

        /// <summary>
        /// 根节点采样片段
        /// </summary>
        public static ProfiledSegment RootSegment { get; private set; }

        /// <summary>
        /// 当前调试片段
        /// </summary>
        public static ProfiledSegment CurrentSegment { get; set; }

        /// <summary>
        /// 清除采样片段
        /// </summary>
        [Conditional("ENABLE_PROFILER")]
        public static void Clear()
        {
            CurrentSegment = RootSegment = new ProfiledSegment(null, "Root");
        }

        /// <summary>
        /// 获取采样区域
        /// </summary>
        public static ProfilingScope SampleBlock(in string name)
        {
            return new ProfilingScope(name);
        }

        /// <summary>
        /// 开始采样
        /// </summary>
        [Conditional("ENABLE_PROFILER")]
        public static void BeginSample(in string name)
        {
            Monitor.Enter(@lock);

            if (!CurrentSegment.Children.Contains(name))
            {
                CurrentSegment.Children.Add(new ProfiledSegment(CurrentSegment, name));
            }

            CurrentSegment = CurrentSegment.Children[name];
            CurrentSegment.Calls++;
            CurrentSegment.Stopwatch.Start();
            Profiler.BeginSample(name);
        }

        /// <summary>
        /// 结束采样
        /// </summary>
        [Conditional("ENABLE_PROFILER")]
        public static void EndSample()
        {
            CurrentSegment.Stopwatch.Stop();

            if (CurrentSegment.Parent != null)
            {
                CurrentSegment = CurrentSegment.Parent;
            }

            Profiler.EndSample();

            Monitor.Exit(@lock);
        }
    }
}
