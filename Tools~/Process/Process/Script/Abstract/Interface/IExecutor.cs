/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    /// <summary>
    /// 内部执行器
    /// </summary>
    public interface IExecutorInternal : IExecutor
    {
        /// <summary>
        /// 表示当前执行器是否正在执行。
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        bool IsRunning { get; }

        /// <summary>
        /// 表示当前执行器是否已经完成。
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        bool IsFinish { get; }

        /// <summary>
        /// 表示下一个执行器
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        IExecutor Next { get; set; }
    }

    /// <summary>
    /// 执行器
    /// </summary>
    public interface IExecutor : IPrCourseRead
    {
        /// <summary>
        /// 提供一个等待异步任务完成的对象
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        TaskAwaiter<IResult> GetAwaiter();

        /// <summary>
        /// 设置下一个
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        IExecutor Link(in IExecutor executor);

        /// <summary>
        /// 同步运行
        /// </summary>
        [SecurityCritical, DebuggerHidden, DebuggerNonUserCode]
        IResult Sync();

        /// <summary>
        /// 异步运行
        /// </summary>
        [SecurityCritical, DebuggerHidden, DebuggerNonUserCode]
        Task<IResult> Async();

        /// <summary>
        /// 将输入信息添加到当前执行器的输入信息队列中
        /// </summary>
        /// <param name="message">消息</param>
        [DebuggerHidden, DebuggerNonUserCode]
        IExecutor Input(in string message);

        /// <summary>
        /// 将输入信息添加到当前执行器的输入信息队列中
        /// </summary>
        /// <param name="messages">消息</param>
        [DebuggerHidden, DebuggerNonUserCode]
        IExecutor Input(in ICollection<string> messages);

        /// <summary>
        /// 将输入信息添加到当前执行器的输入信息队列中
        /// </summary>
        /// <param name="messages">消息</param>
        [DebuggerHidden, DebuggerNonUserCode]
        IExecutor Input(params string[] messages);

        /// <summary>
        /// 设置执行完毕后的回调方法
        /// </summary>
        /// <param name="action">回调</param>
        [DebuggerHidden, DebuggerNonUserCode]
        IExecutor OnComplete(in Action<IResult> action);

        /// <summary>
        /// 进度执行回调
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        IExecutor OnProgress(in Action<object, string> action);
    }
}