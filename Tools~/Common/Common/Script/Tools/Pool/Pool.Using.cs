#region

using System;

#endregion

namespace AIO
{
    public partial class Pool
    {
        /// <summary>
        /// 引用释放
        /// </summary>
        public static IDisposable Using<T>(out T value)
        where T : class, IDisposable, new()
        {
            var disposable = new Disposable<T>(Activator.CreateInstance<T>(), release => release.Dispose());
            value = disposable.Item;
            return disposable;
        }

        /// <summary>
        /// 引用释放
        /// </summary>
        public static IDisposable Using<T>(out T value, Action<T> onRelease)
        where T : class, new()
        {
            var disposable = new Disposable<T>(Activator.CreateInstance<T>(), onRelease);
            value = disposable.Item;
            return disposable;
        }

        /// <summary>
        /// 引用释放
        /// </summary>
        public static IDisposable Using<T>(T value, Action<T> onRelease) => new Disposable<T>(value, onRelease);
    }
}