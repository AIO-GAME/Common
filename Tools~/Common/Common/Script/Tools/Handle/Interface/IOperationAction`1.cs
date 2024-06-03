using System;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// IOperationFunc
    /// </summary>
    public interface IOperationAction<T> : IOperationBase, INotifyCompletion, ITask<T>
    {
        /// <summary>
        /// 结果
        /// </summary>
        T Result { get; }

        /// <summary>
        /// 回调函数
        /// </summary>
        event Action<T> Completed;

        /// <summary>
        /// 执行
        /// </summary>
        T Invoke();
    }
}