namespace AIO
{
    using System;

    /// <summary>
    /// 调试区域
    /// </summary>
    public struct ProfilingScope : IDisposable
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="name">名称</param>
        public ProfilingScope(in string name)
        {
            ProfilingUtils.BeginSample(name);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            ProfilingUtils.EndSample();
        }
    }
}
