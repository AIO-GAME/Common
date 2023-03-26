using System.Threading;

public static partial class UtilsEngine
{
    /// <summary>
    /// 提供在特定线程上下文（SynchronizationContext）中异步执行回调函数或操作的静态方法。
    /// </summary>
    public static class ThreadX
    {
        /// <summary>
        /// 当前线程的同步上下文。
        /// </summary>
        private readonly static SynchronizationContext syncContext = SynchronizationContext.Current;

        /// <summary>
        /// 将传入的回调函数和状态对象封装成一个操作，并通过同步上下文的 Post 方法异步执行。
        /// </summary>
        /// <param name="d">要执行的回调函数。</param>
        /// <param name="state">传递给回调函数的状态对象。</param>
        public static void SyncPost(SendOrPostCallback d, object state)
        {
            syncContext.Post(d, state);
        }

        /// <summary>
        /// 将传入的 Action 对象封装成一个操作，并在执行时捕获异常进行处理后，通过同步上下文的 Post 方法异步执行。
        /// </summary>
        /// <param name="act">要执行的 Action 对象。</param>
        public static void SyncPost(System.Action act)
        {
            syncContext.Post((obj) =>
            {
                try
                {
                    act.Invoke();
                }
                catch (System.Exception e)
                {
                    throw e;
                }
            }, null);
        }

        /// <summary>
        /// 将传入的回调函数和状态对象封装成一个操作，并通过 SyncPost 方法异步执行，实际上就是 SyncPost 方法的一个别名。
        /// </summary>
        /// <param name="d">要执行的回调函数。</param>
        /// <param name="state">传递给回调函数的状态对象。</param>
        public static void SynchronizationPost(SendOrPostCallback d, object state)
        {
            SyncPost(d, state);
        }

        /// <summary>
        /// 将传入的 Action 对象封装成一个操作，并通过 SyncPost 方法异步执行，实际上也是 SyncPost 方法的一个别名。
        /// </summary>
        /// <param name="act">要执行的 Action 对象。</param>
        public static void SynchronizationPost(System.Action act)
        {
            SyncPost(act);
        }
    }
}
