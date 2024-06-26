﻿/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// WIN 平台命令
    /// </summary>
    public partial class PrWin : PrCourse
    {
        /// <inheritdoc/>
        public override IExecutor Execute()
        {
            try
            {
                SetInCreateNoWindow(true);
                SetInUseShellExecute(false);
                SetRedirectError(true);
                SetRedirectOutput(true);
                SetRedirectInput(true);

                if (string.IsNullOrEmpty(Info.WorkingDirectory)) Info.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

                encoding = encoding ?? Encoding.Default;

#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP3_0 || NETCOREAPP3_1
                if (Info.RedirectStandardInput) Info.StandardInputEncoding = encoding;
#endif
                if (Info.RedirectStandardOutput) Info.StandardOutputEncoding = encoding;
                if (Info.RedirectStandardError) Info.StandardErrorEncoding   = encoding;
            }
            catch (Exception ex)
            {
                return new ExecutorException(Info, ex);
            }

            return new Executor(Info, EnableOutput);
        }

        private static IPrCourse Create()
        {
            return Activator.CreateInstance<PrWin>().SetInEncoding(Encoding.UTF8);
        }

        #region Create

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="format">Format:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in (string, string) cmd_work, in string format, params object[] args)
        {
            return Create().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2).SetInArgs(format, args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in (string, string) cmd_work, in StringBuilder args)
        {
            return Create().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2).SetInArgs(args.ToString()).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in (string, string) cmd_work, in string args)
        {
            return Create().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in (string, string) cmd_work, in ICollection<string> args)
        {
            return Create().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>        
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in (string, string) cmd_work)
        {
            return Create().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="format">Format:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in string cmd, in string format, params object[] args)
        {
            return Create().SetFileName(cmd).SetInArgs(format, args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in string cmd, in ICollection<string> args)
        {
            return Create().SetFileName(cmd).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in string cmd, in string args)
        {
            return Create().SetFileName(cmd).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in string cmd, in StringBuilder args)
        {
            return Create().SetFileName(cmd).SetInArgs(args.ToString()).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in string cmd)
        {
            return Create().SetFileName(cmd).Execute();
        }

        #endregion
    }
}