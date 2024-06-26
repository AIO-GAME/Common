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
    /// 进程构造器 使用原生方式执行
    /// </summary>
    public sealed class PrNative : PrCourse
    {
        /// <inheritdoc/>
        public override IExecutor Execute()
        {
            return new ExecutorNative(Info);
        }

        #region Nested type: ExecutorNative

        private sealed class ExecutorNative : Executor
        {
            public ExecutorNative(ProcessStartInfo info) : base(info) { }

            public override IResult Sync()
            {
                IResultInternal result;
                Process process = null;
                try
                {
                    process = Process.Start(Pr.StartInfo);
                    process.WaitForExit();
                    result = new Result(process);
                }
                catch (Exception ex)
                {
                    if (process != null)
                    {
                        process.Kill();
                        result = new ResultException(process.StartInfo, ex);
                    }
                    else
                    {
                        result = new ResultException(Pr.StartInfo, ex);
                    }
                }

                result.Finish(inputs.ToString());
                if (EnableOutput) result.Debug();
                if (Next != null) return result.Link(Next.Sync());
                return result;
            }
        }

        #endregion
    }
}