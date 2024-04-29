#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 快捷缓存
    /// </summary>
    public abstract class CacheableLite : ILiteCacheable
    {
        /// <summary>
        /// 检出回调
        /// </summary>
        protected Action<CacheableLite> actCheckIn;

        #region ILiteCacheable Members

        /// <inheritdoc />
        public ELiteCacheableState CacheState { get; private set; }

        /// <inheritdoc />
        public bool CacheIsIdle => CacheState == ELiteCacheableState.Idle;

        /// <inheritdoc />
        public bool CacheIsRunning => CacheState == ELiteCacheableState.Running;

        /// <inheritdoc />
        public void Reset()
        {
            DoReset();
        }

        /// <inheritdoc />
        public void Recycle()
        {
            if (actCheckIn != null) actCheckIn.Invoke(this);
            else Dispose();
        }

        #endregion

        /// <summary>
        /// 重置
        /// </summary>
        protected virtual void DoReset() { }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="disposing">是否立即处理</param>
        protected virtual void DoDispose(in bool disposing) { }

        internal void SetCacher(Action<CacheableLite> value)
        {
            actCheckIn = value;
        }

        internal void SetState(ELiteCacheableState value)
        {
            CacheState = value;
        }

        internal void Dispose()
        {
            DoDispose(true);
            GC.SuppressFinalize(this);
        }
    }
}