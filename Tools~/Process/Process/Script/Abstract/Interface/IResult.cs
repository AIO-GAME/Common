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
        IResult Finish();

        /// <summary>
        /// 完成
        /// </summary>
        IResult Finish(in string inputs);

        /// <summary>
        /// 执行文件
        /// </summary>
        ProcessStartInfo Info { get; }

        /// <summary>
        /// 输出回调
        /// </summary>
        void ReceivedOutput(object sender, DataReceivedEventArgs e);

        /// <summary>
        /// 错误回调
        /// </summary>
        void ReceivedError(object sender, DataReceivedEventArgs e);

        /// <summary>
        /// 退出回调
        /// </summary>
        void ReceivedExited(object sender, EventArgs e);

        /// <summary>
        /// 释放回调
        /// </summary>
        void ReceivedDisposed(object sender, EventArgs e);

        /// <summary>
        /// 获取关联进程的基本优先级
        /// </summary>
        int BasePriority { get; }

        /// <summary>
        /// 获取关联进程的终端服务会话标识符。
        /// </summary>
        int SessionId { get; }

        /// <summary>
        /// 获取关联进程的唯一标识符。
        /// </summary>
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
        IResult Next { get; set; }

        /// <summary>
        /// 上一个
        /// </summary>
        IResult Last { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        IResult Link(in IResult result);

        /// <summary>
        /// 输出信息
        /// </summary>
        StringBuilder StdOut { get; }

        /// <summary>
        /// 错误信息
        /// </summary>
        StringBuilder StdError { get; }

        /// <summary>
        /// 退出信息
        /// </summary>
        StringBuilder StdExited { get; }

        /// <summary>
        /// 释放信息
        /// </summary>
        StringBuilder StdDisposed { get; }

        /// <summary>
        /// 全部信息
        /// </summary>
        StringBuilder StdALL { get; }

        /// <summary>
        /// 退出码
        /// </summary>
        int ExitCode { get; }

        /// <summary>
        /// 任务开始时间
        /// </summary>
        DateTime StartTime { get; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        DateTime ExitTime { get; }

        /// <summary>
        /// 获取此进程的总的处理器时间。
        /// </summary>
        TimeSpan TotalProcessorTime { get; }

        /// <summary>
        /// 获取此进程的用户处理器时间。
        /// </summary>
        TimeSpan UserProcessorTime { get; }

        /// <summary>
        /// 获取此进程的特权处理器时间。
        /// </summary>
        TimeSpan PrivilegedProcessorTime { get; }
    }
}