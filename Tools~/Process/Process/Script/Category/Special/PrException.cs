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
    /// 进程执行结果(异常)
    /// </summary>
    public sealed class ResultException : IResultInternal
    {
        #region Interface

        /// <inheritdoc/>
        public int ExitCode => -1;

        /// <inheritdoc/>
        public StringBuilder StdOut { get; private set; }

        /// <inheritdoc/>
        public StringBuilder StdError { get; private set; }

        /// <inheritdoc/>
        public StringBuilder StdALL { get; private set; }

        /// <inheritdoc/>
        public StringBuilder StdExited { get; private set; }

        /// <inheritdoc/>
        public StringBuilder StdDisposed { get; private set; }

        /// <inheritdoc/>
        public DateTime StartTime => DateTime.MinValue;

        /// <inheritdoc/>
        public DateTime ExitTime => DateTime.MinValue;

        /// <inheritdoc/>
        public TimeSpan UserProcessorTime => TimeSpan.Zero;

        /// <inheritdoc/>
        public TimeSpan TotalProcessorTime => TimeSpan.Zero;

        /// <inheritdoc/>
        public int BasePriority => -1;

        /// <inheritdoc/>
        public int SessionId => -1;

        /// <inheritdoc/>
        public int Id => -1;

        /// <inheritdoc/>
        public ProcessStartInfo Info { get; private set; }

        /// <inheritdoc/>
        public TimeSpan PrivilegedProcessorTime => TimeSpan.Zero;

        /// <inheritdoc/>
        public IResult Last { get; set; }

        /// <inheritdoc/>
        public IResult Next { get; set; }

        /// <inheritdoc/>
        public IResult Link(in IResult presult)
        {
            if (Next == null)
            {
                Next = presult;
                presult.Last = this;
            }
            else Next.Link(presult);

            return this;
        }

        /// <inheritdoc/>
        public IResult Finish()
        {
            StdALL.AppendFormat(FORMAT_ALL, ExitCode, Info.FileName, Info.Arguments, Info.WorkingDirectory, Result.NULL, Exception);
            return this;
        }

        /// <inheritdoc/>
        public IResult Finish(in string inputs)
        {
            if (string.IsNullOrEmpty(inputs)) return Finish();
            StdALL.AppendFormat(FORMAT_ALL, ExitCode, Info.FileName, Info.Arguments, Info.WorkingDirectory, inputs.Substring(0, inputs.Length - 1), Exception);
            return this;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            StdALL = StdExited = StdDisposed = StdOut = StdError = null;
            Next?.Dispose();
        }

        /// <inheritdoc/>
        public void ReceivedOutput(object sender, DataReceivedEventArgs e)
        {
            if (e != null) StdOut.AppendLine(e.Data);
        }

        /// <inheritdoc/>
        public void ReceivedError(object sender, DataReceivedEventArgs e)
        {
            if (e != null) StdError.AppendLine(e.Data);
        }

        /// <inheritdoc/>
        public void ReceivedExited(object sender, EventArgs e)
        {
            if (e != null) StdExited.AppendLine(e.ToString());
        }

        /// <inheritdoc/>
        public void ReceivedDisposed(object sender, EventArgs e)
        {
            if (e != null) StdDisposed.AppendLine(e.ToString());
        }

        #endregion

        private const string FORMAT_ALL = @"- ExitCode  : {0}
- FileName  : {1}
- Arguments : {2}
- WorkDir   : {3}
- Messages  : {4}
- Exception : {5}";

        private Exception Exception { get; }

        /// <summary>
        /// 进程执行结果(异常)
        /// </summary>
        public ResultException(in ProcessStartInfo info, in Exception exception) : this(exception)
        {
            Info = info;
        }

        /// <summary>
        /// 进程执行结果(异常)
        /// </summary>
        public ResultException(in Exception exception)
        {
            Exception = exception;
            StdError = new StringBuilder(Exception.ToString());
            StdOut = new StringBuilder();
            StdALL = new StringBuilder();
            StdDisposed = new StringBuilder();
            StdExited = new StringBuilder();
        }
    }

    /// <summary>
    /// 进程执行器(异常)
    /// </summary>
    public sealed class ExecutorException : Executor
    {
        private Exception Exception { get; }

        /// <summary>
        /// 进程执行器(异常)
        /// </summary>
        public ExecutorException(in ProcessStartInfo info, in Exception exception) : base(info, true)
        {
            Exception = exception;
        }

        /// <inheritdoc/>
        public override IResult Sync()
        {
            IsRunning = true;
            var result = new ResultException(Pr.StartInfo, Exception);
            IsFinish = true;
            IsRunning = false;
            result.Finish(inputs.ToString());
            if (EnableOutput) result.Debug();
            if (Next != null) return result.Link(Next.Sync());
            return result;
        }
    }

    /// <summary>
    /// 进程构造器(异常)
    /// </summary>
    public sealed class PrException : PrCourse
    {
        private Exception Exception { get; }

        /// <summary>
        /// 进程构造器(异常)
        /// </summary>
        public PrException(in Exception exception)
        {
            Exception = exception;
            Info = new ProcessStartInfo();
        }

        /// <inheritdoc/>
        public override IExecutor Execute()
        {
            return new ExecutorException(Info, Exception);
        }
    }

    /// <summary>
    /// 进程构造器(异常)
    /// </summary>
    public sealed class PrException<T> : PrCourse where T : Exception, new()
    {
        private T Exception { get; }

        /// <summary>
        /// 进程构造器(异常)
        /// </summary>
        public PrException(in string message) : this(CreateException<T>(message))
        {
            Info = new ProcessStartInfo();
        }

        /// <summary>
        /// 进程构造器(异常)
        /// </summary>
        public PrException(T exception)
        {
            Exception = exception;
        }

        /// <inheritdoc/>
        public override IExecutor Execute()
        {
            return new ExecutorException(Info, Exception);
        }

        private static TException CreateException<TException>(string message) where TException : Exception
        {
            var constructor = typeof(TException).GetConstructor(new[] { typeof(string) });

            if (constructor == null)
            {
                throw new ArgumentException($"Type {typeof(TException).FullName} does not have a constructor that takes a single string argument.");
            }

            return (TException)constructor.Invoke(new object[] { message });
        }
    }
}