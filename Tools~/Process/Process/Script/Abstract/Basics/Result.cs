/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Diagnostics;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 执行结果
    /// </summary>
    public class Result : IResultInternal
    {
        internal const string FORMAT_ALL = @"- ExitCode  : {0}
- FileName  : {1}
- Arguments : {2}
- WorkDic   : {3}
- Messages  : {4}
- OutPut    : {5}
- Error     : {6}
- Exited    : {7}
- Disposed  : {8}";

        internal const string NULL = "NULL";

        /// <summary>
        /// Process
        /// </summary>
        protected Process Pr { get; }

        internal Result(Process process)
        {
            Pr = process;
            StdOut = new StringBuilder();
            StdError = new StringBuilder();
            StdALL = new StringBuilder();
            StdExited = new StringBuilder();
            StdDisposed = new StringBuilder();
        }

        /// <inheritdoc/>
        public virtual IResult Finish()
        {
            StdALL.AppendFormat(FORMAT_ALL,
                ExitCode,
                Pr.StartInfo.FileName,
                Pr.StartInfo.Arguments,
                Pr.StartInfo.WorkingDirectory,
                NULL,
                StdOut,
                StdError,
                StdExited,
                StdDisposed);
            return this;
        }

        /// <inheritdoc/>
        public virtual IResult Finish(in string messages)
        {
            if (string.IsNullOrEmpty(messages)) return Finish();
            StdALL.AppendFormat(FORMAT_ALL,
                ExitCode,
                Pr.StartInfo.FileName,
                Pr.StartInfo.Arguments,
                Pr.StartInfo.WorkingDirectory,
                messages.Substring(0, messages.Length - 1),
                StdOut,
                StdError,
                StdExited,
                StdDisposed);
            return this;
        }

        /// <inheritdoc/>
        public IResult Link(in IResult result)
        {
            if (Next == null)
            {
                Next = result;
                result.Last = this;
            }
            else Next.Link(result);
            return this;
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Pr?.Dispose();
        }

        #region 接口

        /// <inheritdoc/>
        public IResult Next { get; set; }

        /// <inheritdoc/>
        public IResult Last { get; set; }

        /// <inheritdoc/>
        public StringBuilder StdOut { get; protected set; }

        /// <inheritdoc/>
        public StringBuilder StdError { get; protected set; }

        /// <inheritdoc/>
        public StringBuilder StdALL { get; protected set; }

        /// <inheritdoc/>
        public StringBuilder StdExited { get; protected set; }

        /// <inheritdoc/>
        public StringBuilder StdDisposed { get; protected set; }

        /// <inheritdoc/>
        public virtual int ExitCode => Pr.ExitCode;

        /// <inheritdoc/>
        public virtual DateTime StartTime => Pr.StartTime;

        /// <inheritdoc/>
        public virtual DateTime ExitTime => Pr.ExitTime;

        /// <inheritdoc/>
        public virtual TimeSpan UserProcessorTime => Pr.UserProcessorTime;

        /// <inheritdoc/>
        public virtual TimeSpan TotalProcessorTime => Pr.TotalProcessorTime;

        /// <inheritdoc/>
        public virtual TimeSpan PrivilegedProcessorTime => Pr.PrivilegedProcessorTime;

        /// <inheritdoc/>
        public virtual int BasePriority => Pr.BasePriority;

        /// <inheritdoc/>
        public virtual int SessionId => Pr.SessionId;

        /// <inheritdoc/>
        public virtual int Id => Pr.Id;

        /// <inheritdoc/>
        public virtual ProcessStartInfo Info => Pr.StartInfo;

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
    }
}