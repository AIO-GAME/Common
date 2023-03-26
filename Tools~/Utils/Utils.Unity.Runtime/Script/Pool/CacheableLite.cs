namespace AIO
{
    using System;

    public class CacheableLite : ILiteCacheable
    {
        public LiteCacheableState CacheState { get; private set; }

        public bool CacheIsIdle => CacheState == LiteCacheableState.Idle;

        public bool CacheIsRunning => CacheState == LiteCacheableState.Running;

        protected Action<CacheableLite> actCheckIn;

        protected virtual void DoReset() { }

        protected virtual void DoDispose(bool disposing) { }

        internal void SetCacher(Action<CacheableLite> value)
        {
            actCheckIn = value;
        }

        internal void SetState(LiteCacheableState value)
        {
            CacheState = value;
        }

        internal void Dispose()
        {
            DoDispose(true);
            GC.SuppressFinalize(this);
        }

        public void Reset()
        {
            DoReset();
        }

        public void Recycle()
        {
            if (actCheckIn != null) actCheckIn.Invoke(this);
            else Dispose();
        }
    }
}
