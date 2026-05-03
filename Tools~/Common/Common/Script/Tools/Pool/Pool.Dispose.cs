#region

using System;

#endregion

namespace AIO
{
    public partial class Pool
    {
        /// <summary>
        /// An <see cref="IDisposable"/> to allow pooled objects to be acquired and released within <c>using</c>
        /// statements instead of needing to manually release everything.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public interface IUsing<out T> : IDisposable
        {
            /// <summary>
            /// The object acquired from the <see cref="IUsing{T}"/>.
            /// </summary>
            T Item { get; }
        }

        /// <summary>
        /// An <see cref="IDisposable"/> to allow pooled objects to be acquired and released within <c>using</c>
        /// statements instead of needing to manually release everything.
        /// </summary>
        internal struct Disposable<T> : IUsing<T>
        {
            /// <summary>
            /// The object acquired from the <see cref="Disposable{T}"/>.
            /// </summary>
            public T Item { get; private set; }

            /// <summary>
            /// Called by IDisposable.Dispose.
            /// </summary>
            private Action<T> _release;

            /// <summary>
            /// 释放
            /// </summary>
            public Disposable(T item, Action<T> release)
            {
                Item = item;
                _release = release;
            }

            public Disposable(T item)
            {
                Item = item;
                _release = null;
            }

            public void Dispose()
            {
                if (_release == null)
                {
                    Item = default;
                    return;
                }

                _release.Invoke(Item);
                _release = null;
            }
        }
    }
}