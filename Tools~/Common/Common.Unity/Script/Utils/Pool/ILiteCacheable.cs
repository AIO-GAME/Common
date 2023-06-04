namespace AIO.Unity
{
    /// <summary>
    /// 快捷缓存
    /// </summary>
    public interface ILiteCacheable
    {
        /// <summary>
        /// 缓存状态
        /// </summary>
        ELiteCacheableState CacheState { get; }

        /// <summary>
        /// 还存等待
        /// </summary>
        bool CacheIsIdle { get; }

        /// <summary>
        /// 缓存正在运行
        /// </summary>
        bool CacheIsRunning { get; }

        /// <summary>
        /// 重置
        /// </summary>
        void Reset();

        /// <summary>
        /// 回收
        /// </summary>
        void Recycle();
    }
}