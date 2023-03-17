/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Diagnostics;

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
        public ExecutorError(in ProcessStartInfo info) : base(info)
        {
        }

        /// <inheritdoc/>
        public override IResult Sync()
        {
            var result = new ResultError(Pr);
            result.Finish(inputs.ToString()).Debug();
            if (Next != null) return result.Link(Next.Sync());
            return result;
        }
    }

    /// <summary>
    /// 进程执行结果(错误)
    /// </summary>
    public sealed class ResultError : Result
    {
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

        /// <summary>
        /// 进程执行结果(错误)
        /// </summary>
        public ResultError(Process process) : base(process)
        {

        }
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
            return new ExecutorError(Info);
        }
    }
}
