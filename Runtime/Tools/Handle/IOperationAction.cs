using System;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 异步处理器
    /// </summary>
    public interface IOperationAction : IOperationBase, INotifyCompletion
    {
        /// <summary>
        /// 回调函数
        /// </summary>
        event Action Completed;

        /// <summary>
        /// 获取异步等待器
        /// </summary>
        TaskAwaiter GetAwaiter();

        /// <summary>
        /// 执行
        /// </summary>
        void Invoke();
    }
}