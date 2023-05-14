using System;

public partial class Pool
{
    /// <summary>
    /// An <see cref="IDisposable"/> to allow pooled objects to be acquired and released within <c>using</c>
    /// statements instead of needing to manually release everything.
    /// </summary>
    internal readonly struct Disposable<T> : IDisposable
    {
        /// <summary>
        /// The object acquired from the <see cref="Disposable{T}"/>.
        /// </summary>
        public readonly T Item;

        /// <summary>
        /// Called by IDisposable.Dispose.
        /// </summary>
        public readonly Action<T> OnRelease;

        /// <summary>
        /// 释放
        /// </summary>
        public Disposable(T item, Action<T> onRelease)
        {
            Item = item;
            OnRelease = onRelease;
        }

        public void Dispose()
        {
            OnRelease?.Invoke(Item);
        }
    }
}