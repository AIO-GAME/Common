/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace AIO.Core.Runtime
{
    /// <summary>
    /// 线程池管理类
    /// </summary>
    public static class ThreadSystem
    {
        #region ThreadPool

        /// <summary>
        /// 获取线程池 存活 工作线程数量 完成线程数量
        /// </summary>
        public static void GetAvailableThreads(out int workerThreads, out int completionPortThreads)
        {
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
        }

        /// <summary>
        /// 获取线程池 最大 工作线程数量 完成线程数量
        /// </summary>
        public static void GetMaxThreads(out int workerThreads, out int completionPortThreads)
        {
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
        }

        /// <summary>
        /// 获取线程池 最小 工作线程数量 完成线程数量
        /// </summary>
        public static void GetMinThreads(out int workerThreads, out int completionPortThreads)
        {
            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
        }

        /// <summary>
        /// 设置线程池 最小 工作线程数量 完成线程数量
        /// </summary>
        public static void SetMinThreads(int workerThreads, int completionPortThreads)
        {
            ThreadPool.SetMinThreads(workerThreads, completionPortThreads);
        }

        /// <summary>
        /// 设置线程池 最大 工作线程数量 完成线程数量
        /// </summary>
        public static void SetMaxThreads(int workerThreads, int completionPortThreads)
        {
            ThreadPool.SetMinThreads(workerThreads, completionPortThreads);
        }

        /// <summary>
        /// 插入线程队列 将方法排入队列以便执行，并指定包含该方法所用数据的对象。此方法在有线程池线程变得可用时执行。
        /// </summary>
        public static bool ThreadPoolAdd(WaitCallback action)
        {
            return ThreadPool.QueueUserWorkItem(action);
        }

        /// <summary>
        /// 插入线程队列 将方法排入队列以便执行，并指定包含该方法所用数据的对象。此方法在有线程池线程变得可用时执行。
        /// </summary>
        public static bool ThreadPoolAdd(WaitCallback action, object state)
        {
            return ThreadPool.QueueUserWorkItem(action, state);
        }

        /// <summary>
        /// 线程池添加一个可以定时执行的方法
        /// </summary>
        [SecuritySafeCritical]
        public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
        {
            return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce);
        }

        /// <summary>
        /// 线程池添加一个可以定时执行的方法
        /// </summary>
        [SecuritySafeCritical]
        public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
        {
            return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, timeout, executeOnlyOnce);
        }

        /// <summary>
        /// 线程池添加一个可以定时执行的方法
        /// </summary>
        [SecuritySafeCritical]
        public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
        {
            return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce);
        }

        /// <summary>
        /// 线程池添加一个可以定时执行的方法
        /// </summary>
        [SecuritySafeCritical]
        public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
        {
            return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce);
        }

        [SecurityCritical]
        public static bool UnsafeQueueUserWorkItem(WaitCallback callBack, object state)
        {
            return ThreadPool.UnsafeQueueUserWorkItem(callBack, state);
        }

        [SecurityCritical]
        public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
        {
            return ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, callBack, state, timeout, executeOnlyOnce);
        }

        [SecurityCritical]
        public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
        {
            return ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce);
        }

        [SecurityCritical]
        public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
        {
            return ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce);
        }

        [SecurityCritical]
        public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
        {
            return ThreadPool.UnsafeRegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce);
        }

        #endregion

        #region Task

        public static Task TaskAdd(Action action, CancellationToken cancellationToken)
        {
            return new Task(action, cancellationToken);
        }

        public static Task TaskCreate(Action action, TaskCreationOptions creationOptions)
        {
            return new Task(action, creationOptions);
        }

        public static Task TaskCreate(Action<object> action, object state)
        {
            return new Task(action, state);
        }

        public static Task TaskCreate(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
        {
            return new Task(action, cancellationToken, creationOptions);
        }

        public static Task TaskCreate(Action<object> action, object state, CancellationToken cancellationToken)
        {
            return new Task(action, state, cancellationToken);
        }

        public static Task TaskCreate(Action<object> action, object state, TaskCreationOptions creationOptions)
        {
            return new Task(action, state, creationOptions);
        }

        public static Task TaskCreate(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
        {
            return new Task(action, state, cancellationToken, creationOptions);
        }

        public static Task TaskCreate(Action action)
        {
            return new Task(action);
        }

        #endregion

        #region Parallel

        /// <summary>
        /// Parallel 是对Task的进一步封装，但会阻塞主线程，主线程会参与业务逻辑处理
        /// </summary>
        public static void ParallelAdd(Action action)
        {
            //分配线程执行业务逻辑,  Invoke可传多个业务处理，内部会自动分配线程处理
            Parallel.Invoke(action);
        }

        #endregion
    }
}