using System;
using System.Runtime.CompilerServices;
using Object = UnityEngine.Object;

namespace AIO
{
    public static class IHandleExtensions
    {
        /// <summary>
        /// 安全转换
        /// </summary>
        /// <returns> 转换后的对象 </returns>
        public static Object SafeCast(this IOperation<Object> operation)
        {
            return operation.Result;
        }

        /// <summary>
        /// 安全转换
        /// </summary>
        /// <returns> 转换后的对象 </returns>
        public static TObject SafeCast<TObject>(this IOperation<TObject> operation)
        where TObject : Object
        {
            return operation.Result;
        }
    }

    /// <summary>
    /// 异步处理器
    /// </summary>
    public interface IOperation : IOperationBase
    {
        /// <summary>
        /// 结果
        /// </summary>
        object Result { get; }

        /// <summary>
        /// 完成回调
        /// </summary>
        event Action<object> Completed;

        /// <summary>
        /// 获取异步等待器
        /// </summary>
        TaskAwaiter<object> GetAwaiter();

        /// <summary>
        /// 执行
        /// </summary>
        object Invoke();
    }

    /// <summary>
    /// 异步处理器
    /// </summary>
    public interface IOperation<TObject> : IOperation
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
        /// 获取异步等待器
        /// </summary>
        new TaskAwaiter<TObject> GetAwaiter();

        /// <summary>
        /// 执行
        /// </summary>
        new TObject Invoke();
    }
}