#region

using System.Collections;
using System.Runtime.CompilerServices;

#endregion

namespace AIO
{
    /// <summary>
    ///    异步任务
    /// </summary>
    public interface ITask : IEnumerator
    {
        /// <summary>
        ///     获取异步等待器
        /// </summary>
        TaskAwaiter GetAwaiter();
    }

    /// <summary>
    ///   异步任务
    /// </summary>
    public interface ITask<TResult> : IEnumerator
    {
        /// <summary>
        ///     获取异步等待器
        /// </summary>
        TaskAwaiter<TResult> GetAwaiter();
    }

    /// <summary>
    ///   异步任务
    /// </summary>
    public interface ITaskObject : IEnumerator
    {
        /// <summary>
        ///     获取异步等待器
        /// </summary>
        TaskAwaiter<object> GetAwaiter();
    }

    /// <summary>
    ///   异步任务
    /// </summary>
    public interface ITaskObjectArray : IEnumerator
    {
        /// <summary>
        ///     获取异步等待器
        /// </summary>
        TaskAwaiter<object[]> GetAwaiter();
    }

    /// <summary>
    ///   异步任务
    /// </summary>
    public interface ITaskArray<TResult> : IEnumerator
    {
        /// <summary>
        ///     获取异步等待器
        /// </summary>
        TaskAwaiter<TResult[]> GetAwaiter();
    }
}