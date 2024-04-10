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
using System.Text;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    /// <summary>
    /// 执行器
    /// </summary>
    public class Executor : IExecutorInternal
    {
        /// <summary>
        /// 回调
        /// </summary>
        private Action<object, string> Progress;

        /// <summary>
        /// 执行器
        /// </summary>
        public Executor(in ProcessStartInfo info, in bool enableOutput = true)
        {
            IsFinish = IsRunning = false;
            Info     = info;
            Pr       = new Process();
            if (info != null) Pr.StartInfo = info;
            inputs       = new StringBuilder();
            EnableOutput = enableOutput;
        }

        /// <summary>
        /// 执行的进程
        /// </summary>
        protected Process Pr { get; }

        /// <summary>
        /// 输出信息
        /// </summary>
        protected StringBuilder inputs { get; private set; }

        /// <summary>
        /// 回调
        /// </summary>
        protected Action<IResult> CallBack { get; private set; }

        #region IExecutorInternal Members

        /// <inheritdoc/>
        public ProcessStartInfo Info { get; }

        /// <inheritdoc/>
        public bool EnableOutput { get; set; }

        /// <inheritdoc/>
        public IExecutor Next { get; set; }

        /// <inheritdoc/>
        public bool IsRunning { get; protected set; }

        /// <inheritdoc/>
        public bool IsFinish { get; protected set; }

        /// <inheritdoc/>
        public TaskAwaiter<IResult> GetAwaiter()
        {
            return Task.Factory.StartNew(Sync).GetAwaiter();
        }

        /// <inheritdoc/>
        public virtual IExecutor Link(in IExecutor next)
        {
            if (IsRunning) throw new Exception("Call before the Run function executes");

            if (Next == null) Next = next;
            else Next.Link(next);

            return this;
        }

        /// <inheritdoc/>
        public virtual IResult Sync()
        {
            if (IsFinish) throw new Exception("The current task has been completed");

            IsRunning = true;
            IResultInternal result = new Result(Pr);

            try
            {
                Pr.Disposed += result.ReceivedDisposed;
                Pr.Exited   += result.ReceivedExited;

                if (Pr.StartInfo.RedirectStandardOutput)
                {
                    Pr.OutputDataReceived += result.ReceivedOutput;
                    if (Progress != null) Pr.OutputDataReceived += ReceiveProgress;
                }

                if (Pr.StartInfo.RedirectStandardError) Pr.ErrorDataReceived += result.ReceivedError;

                Pr.Refresh();
                Pr.Start();
                Pr.PriorityBoostEnabled = true; // 启用优先级增强

                if (Pr.StartInfo.RedirectStandardOutput) Pr.BeginOutputReadLine();
                if (Pr.StartInfo.RedirectStandardError) Pr.BeginErrorReadLine();

                if (Pr.StartInfo.RedirectStandardInput)
                {
                    Pr.StandardInput.AutoFlush = true; // 自动重定向
                    foreach (var input in inputs.ToString().Split('\n'))
                    {
                        ReceiveProgress(null, input);
                        Pr.StandardInput.WriteLine(input.TrimEnd());
                    }

                    Pr.StandardInput.Flush();
                }

                Pr.WaitForExit();

                if (Pr.StartInfo.RedirectStandardOutput)
                {
                    Pr.CancelOutputRead();
                    Pr.OutputDataReceived -= result.ReceivedOutput;
                }

                if (Pr.StartInfo.RedirectStandardError)
                {
                    Pr.CancelErrorRead();
                    Pr.ErrorDataReceived -= result.ReceivedError;
                }

                if (Pr.StartInfo.RedirectStandardInput) Pr.StandardInput.Close();

                Pr.Disposed -= result.ReceivedDisposed;
                Pr.Exited   -= result.ReceivedExited;
            }
            catch (Exception ex)
            {
                result = new ResultException(Pr.StartInfo, ex);
            }

            result.Finish(inputs.ToString());
            CallBack?.Invoke(result);
            if (EnableOutput) result.Debug();
            if (Next != null) result.Link(Next.Sync());
            return result;
        }

        /// <inheritdoc/>
        public Task<IResult> Async()
        {
            return Task.Factory.StartNew(Sync);
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Pr?.Close();
            Pr?.Dispose();
        }

        /// <inheritdoc/>
        public virtual IExecutor Input(in string message)
        {
            if (IsRunning) throw new Exception("Call before the Run function executes");
            if (string.IsNullOrEmpty(message)) return this;
            inputs.AppendLine(message);
            return this;
        }

        /// <inheritdoc/>
        public virtual IExecutor Input(in ICollection<string> messages)
        {
            if (IsRunning) throw new Exception("Call before the Run function executes");
            if (messages == null || messages.Count == 0) return this;
            foreach (var item in messages)
            {
                if (string.IsNullOrEmpty(item)) continue;
                inputs.AppendLine(item);
            }

            return this;
        }

        /// <inheritdoc/>
        public virtual IExecutor Input(params string[] messages)
        {
            if (IsRunning) throw new Exception("Call before the Run function executes");
            if (messages == null || messages.Length == 0) return this;
            foreach (var item in messages)
            {
                if (string.IsNullOrEmpty(item)) continue;
                inputs.AppendLine(item.TrimEnd());
            }

            return this;
        }

        /// <inheritdoc/>
        public IExecutor OnComplete(in Action<IResult> action)
        {
            CallBack = action;
            return this;
        }

        /// <inheritdoc/>
        public IExecutor OnProgress(in Action<object, string> action)
        {
            Progress = action;
            return this;
        }

        #endregion

        /// <summary>
        /// 回调
        /// </summary>
        protected void ReceiveProgress(object sender, DataReceivedEventArgs e)
        {
            Progress?.Invoke(sender, e.Data);
        }

        /// <summary>
        /// 回调
        /// </summary>
        protected void ReceiveProgress(object sender, string e)
        {
            Progress?.Invoke(sender, e);
        }
    }
}