using System;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 异步处理器
    /// </summary>
    public static class IOperationExtensions
    {
        /// <summary>
        /// 安全转换
        /// </summary>
        /// <returns> 转换后的对象 </returns>
        public static object SafeCast(this IOperation operation)
        {
            return operation.Result;
        }

        /// <summary>
        /// 安全转换
        /// </summary>
        /// <returns> 转换后的对象 </returns>
        public static TObject SafeCast<TObject>(this IOperation<TObject> operation)
        {
            return operation.Result;
        }
    }

    /// <summary>
    /// 异步处理器
    /// </summary>
    public interface IOperation : IOperationBase, ITaskObject
    {
        /// <summary>
        /// 结果
        /// </summary>
        object Result { get; }

        /// <summary>
        /// 执行
        /// </summary>
        object Invoke();

        /// <summary>
        /// 完成回调
        /// </summary>
        event Action<object> Completed;
    }


    /// <summary>
    /// 异步处理器
    /// </summary>
    public interface IOperation<TObject> : IOperation, ITask<TObject>
    {
        /// <summary>
        /// 结果
        /// </summary>
        new TObject Result { get; }

        /// <summary>
        /// 完成回调
        /// </summary>
        new event Action<TObject> Completed;

        /// <summary>
        /// 执行
        /// </summary>
        new TObject Invoke();

        /// <summary>
        /// 获取异步等待器
        /// </summary>
        new TaskAwaiter<TObject> GetAwaiter();
    }
}