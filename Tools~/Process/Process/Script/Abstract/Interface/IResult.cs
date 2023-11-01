/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Diagnostics;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 执行结果 内部类
    /// </summary>
    public interface IResultInternal : IResult
    {
        /// <summary>
        /// 完成
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        IResult Finish();

        /// <summary>
        /// 完成
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        IResult Finish(in string inputs);

        /// <summary>
        /// 执行文件
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        ProcessStartInfo Info { get; }

        /// <summary>
        /// 输出回调
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        void ReceivedOutput(object sender, DataReceivedEventArgs e);

        /// <summary>
        /// 错误回调
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        void ReceivedError(object sender, DataReceivedEventArgs e);

        /// <summary>
        /// 退出回调
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        void ReceivedExited(object sender, EventArgs e);

        /// <summary>
        /// 释放回调
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        void ReceivedDisposed(object sender, EventArgs e);

        /// <summary>
        /// 获取关联进程的基本优先级
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        int BasePriority { get; }

        /// <summary>
        /// 获取关联进程的终端服务会话标识符。
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        int SessionId { get; }

        /// <summary>
        /// 获取关联进程的唯一标识符。
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        int Id { get; }
    }

    /// <summary>
    /// 执行结果
    /// </summary>
    public interface IResult : IDisposable
    {
        /// <summary>
        /// 下一个
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        IResult Next { get; set; }

        /// <summary>
        /// 上一个
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        IResult Last { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        IResult Link(in IResult result);

        /// <summary>
        /// 输出信息
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        StringBuilder StdOut { get; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        StringBuilder StdError { get; }

        /// <summary>
        /// 退出信息
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        StringBuilder StdExited { get; }

        /// <summary>
        /// 释放信息
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        StringBuilder StdDisposed { get; }

        /// <summary>
        /// 全部信息
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        StringBuilder StdALL { get; }

        /// <summary>
        /// 退出码
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        int ExitCode { get; }

        /// <summary>
        /// 任务开始时间
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        DateTime StartTime { get; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        DateTime ExitTime { get; }

        /// <summary>
        /// 获取此进程的总的处理器时间。
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        TimeSpan TotalProcessorTime { get; }

        /// <summary>
        /// 获取此进程的用户处理器时间。
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        TimeSpan UserProcessorTime { get; }

        /// <summary>
        /// 获取此进程的特权处理器时间。
        /// </summary>
        [DebuggerHidden, DebuggerNonUserCode]
        TimeSpan PrivilegedProcessorTime { get; }
    }
}