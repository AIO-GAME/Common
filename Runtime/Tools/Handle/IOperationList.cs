using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 异步处理器
    /// </summary>
    public interface IOperationList : IEnumerable, IOperationBase
    {
        /// <summary>
        /// 结果数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 索引器
        /// </summary>
        object this[int index] { get; }

        /// <summary>
        /// 获取结果
        /// </summary>
        object[] Invoke();

        /// <summary>
        /// 结果
        /// </summary>
        object[] Result { get; }

        /// <summary>
        /// 完成回调
        /// </summary>
        event Action<object[]> Completed;

        /// <summary>
        /// 获取异步等待器
        /// </summary>
        TaskAwaiter<object[]> GetAwaiter();

        /// <summary>
        /// 获取异步等待器
        /// </summary>
        new IEnumerator<object> GetEnumerator();
    }

    /// <summary>
    /// 异步处理器
    /// </summary>
    public interface IOperationList<TObject> : IOperationList
    {
        /// <summary>
        /// 获取异步等待器
        /// </summary>
        new IEnumerator<TObject> GetEnumerator();

        /// <summary>
        /// 结果
        /// </summary>
        new TObject[] Result { get; }

        /// <summary>
        /// 完成回调
        /// </summary>
        new event Action<TObject[]> Completed;

        /// <summary>
        /// 获取异步等待器
        /// </summary>
        new TaskAwaiter<TObject[]> GetAwaiter();

        /// <summary>
        /// 执行
        /// </summary>
        new TObject[] Invoke();

        /// <summary>
        /// 索引器
        /// </summary>
        new TObject this[int index] { get; }
    }
}