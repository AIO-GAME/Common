namespace AIO
{
    public interface ILiteCacheable
    {
        LiteCacheableState CacheState { get; }

        bool CacheIsIdle { get; }

        bool CacheIsRunning { get; }

        void Reset();

        void Recycle();
    }
}
