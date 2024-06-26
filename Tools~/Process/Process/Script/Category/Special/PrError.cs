﻿/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;
using System.Diagnostics;

#endregion

namespace AIO
{
    /// <summary>
    /// 进程执行器(错误)
    /// </summary>
    public sealed class ExecutorError : Executor
    {
        /// <summary>
        /// 进程执行器(错误)
        /// </summary>
        public ExecutorError(in ProcessStartInfo info) : base(info) { }

        /// <summary>
        /// 进程执行器(错误)
        /// </summary>
        public ExecutorError(in ProcessStartInfo info, in bool enableOutput = true) : base(info, enableOutput) { }


        /// <inheritdoc/>
        public override IResult Sync()
        {
            var result = new ResultError(Pr);
            result.Finish(inputs.ToString());
            if (EnableOutput) result.Debug();
            if (Next != null) return result.Link(Next.Sync());
            return result;
        }
    }

    /// <summary>
    /// 进程执行结果(错误)
    /// </summary>
    public sealed class ResultError : Result
    {
        /// <summary>
        /// 进程执行结果(错误)
        /// </summary>
        public ResultError(Process process) : base(process) { }

        /// <inheritdoc/>
        public override int ExitCode => -1;

        /// <inheritdoc/>
        public override DateTime StartTime => DateTime.MinValue;

        /// <inheritdoc/>
        public override DateTime ExitTime => DateTime.MinValue;

        /// <inheritdoc/>
        public override TimeSpan UserProcessorTime => TimeSpan.Zero;

        /// <inheritdoc/>
        public override TimeSpan TotalProcessorTime => TimeSpan.Zero;

        /// <inheritdoc/>
        public override int BasePriority => 0;

        /// <inheritdoc/>
        public override int SessionId => 0;

        /// <inheritdoc/>
        public override int Id => 0;

        /// <inheritdoc/>
        public override TimeSpan PrivilegedProcessorTime => TimeSpan.Zero;
    }

    /// <summary>
    /// 进程执行构建(错误)
    /// </summary>
    public sealed class PrError : PrCourse
    {
        /// <summary>
        /// 执行
        /// </summary>
        public override IExecutor Execute()
        {
            return new ExecutorError(Info, EnableOutput);
        }
    }
}