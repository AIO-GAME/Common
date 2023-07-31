/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AIO
{
    /// <summary>
    /// 进程执行结果(空)
    /// </summary>
    public sealed class ResultEmpty : Result
    {
        /// <inheritdoc/>
        public override int ExitCode => 0;

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

        /// <summary>
        /// 进程执行结果(空)
        /// </summary>
        public ResultEmpty() : base(null)
        {
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
        }

        /// <inheritdoc/>
        public override IResult Finish()
        {
            return this;
        }

        /// <inheritdoc/>
        public override IResult Finish(in string messages)
        {
            return this;
        }

        /// <inheritdoc/>
        public override ProcessStartInfo Info => null;

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Empty";
        }
    }

    /// <summary>
    /// 进程执行器(空)
    /// </summary>
    public sealed class ExecutorEmpty : Executor
    {
        /// <summary>
        /// 进程执行器(空)
        /// </summary>
        public ExecutorEmpty(bool enableOutput = true) : base(null, enableOutput)
        {
        }

        /// <inheritdoc/>
        public override IResult Sync()
        {
            var result = new ResultEmpty();
            result.Finish(inputs.ToString()).Debug();
            if (Next != null) return result.Link(Next.Sync());
            return result;
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
        }

        /// <inheritdoc/>
        public override IExecutor Input(in string message)
        {
            return this;
        }

        /// <inheritdoc/>
        public override IExecutor Input(in ICollection<string> messages)
        {
            return this;
        }

        /// <inheritdoc/>
        public override IExecutor Input(params string[] messages)
        {
            return this;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Empty";
        }
    }

    /// <summary>
    /// 进程构造器(空)
    /// </summary>
    public sealed partial class PrEmpty : PrCourse
    {
        /// <inheritdoc/>
        public override void Dispose()
        {
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Empty";
        }

        /// <inheritdoc/>
        public override IExecutor Execute()
        {
            return new ExecutorEmpty(EnableOutput);
        }
    }
}