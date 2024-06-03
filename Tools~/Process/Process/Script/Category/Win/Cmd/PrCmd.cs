/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// CMD执行器
    /// </summary>
    public sealed partial class PrCmd : PrWin
    {
        /// <summary>
        /// HELP=>
        /// CMD [/A | /U] [/Q] [/D] [/E:ON | /E:OFF] [/F:ON | /F:OFF] [/V:ON | /V:OFF] [[/S][/C | /K] string]
        /// /C 执行字符串指定的命令然后终止
        /// /K 执行字符串指定的命令但保留
        /// /S 修改 /C 或 /K 之后的字符串处理(见下)
        /// /Q 关闭回显
        /// /D 禁止从注册表执行 AutoRun 命令(见下)
        /// /A 使向管道或文件的内部命令输出成为 ANSI
        /// /U 使向管道或文件的内部命令输出成为 Unicode
        /// 
        /// /T:fg  设置前台/背景颜色(详细信息见 COLOR /?)
        /// /E:ON  启用命令扩展(见下)
        /// /E:OFF 禁用命令扩展(见下)
        /// /F:ON  启用文件和目录名完成字符(见下)
        /// /F:OFF 禁用文件和目录名完成字符(见下)
        /// /V:ON  使用 ! 作为分隔符启用延迟的环境变量
        ///        扩展。例如，/V:ON 会允许 !var! 在执行时扩展变量 var。var 语法会在输入时扩展变量，这与在一个 FOR 循环内不同。
        /// /V:OFF 禁用延迟的环境扩展。        
        /// </summary>
        public const string CMD = "cmd";

        /// <summary>
        /// CMD默认启动参数
        /// </summary>
        public const string CMD_ARGS = "/U /D /Q /V:ON /F:ON /E:ON /C ";

        /// <inheritdoc/>
        public override IExecutor Execute()
        {
            try
            {
                SetFileName(CMD);
                SetInCreateNoWindow(true);
                SetInUseShellExecute(false);
                SetRedirectError(true);
                SetRedirectOutput(true);
                SetRedirectInput(true);

                if (string.IsNullOrEmpty(Info.WorkingDirectory))
                    Info.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

                encoding = encoding ?? Encoding.Default;

#if (NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP3_0 || NETCOREAPP3_1)
                if (p.StartInfo.RedirectStandardInput) p.StartInfo.StandardInputEncoding = encoding;
#endif
                if (Info.RedirectStandardOutput) Info.StandardOutputEncoding = encoding;
                if (Info.RedirectStandardError) Info.StandardErrorEncoding   = encoding;
            }
            catch (Exception ex)
            {
                return new ExecutorException(Info, ex);
            }

            return new ExecutorCmd(Info);
        }

        #region Nested type: ExecutorCmd

        private sealed class ExecutorCmd : Executor
        {
            public ExecutorCmd(in ProcessStartInfo info) : base(info) { }

            public override IResult Sync()
            {
                IResultInternal result = new Result(Pr);
                try
                {
                    Pr.Disposed += result.ReceivedDisposed;
                    Pr.Exited   += result.ReceivedExited;

                    if (Pr.StartInfo.RedirectStandardOutput)
                    {
                        Pr.OutputDataReceived += result.ReceivedOutput;
                        Pr.OutputDataReceived += ReceiveProgress;
                    }

                    if (Pr.StartInfo.RedirectStandardError)
                    {
                        Pr.ErrorDataReceived += result.ReceivedError;
                        Pr.ErrorDataReceived += ReceiveProgress;
                    }

                    var hasArgumentsMLine = false;
                    var inputStr = inputs.ToString();
                    if (inputs.Length > 0)
                    {
                        hasArgumentsMLine = inputStr.Split('\n').Length > 2;
                        if (hasArgumentsMLine)
                            Pr.StartInfo.Arguments = "/U /D /Q /V:ON /F:ON /E:ON";
                        else
                            Pr.StartInfo.Arguments = string.Concat(Pr.StartInfo.Arguments, " \" ",
                                                                   inputs.ToString().Substring(0, inputs.Length - 1), " \"");
                    }

                    Pr.Refresh();
                    Pr.Start();
                    Pr.PriorityBoostEnabled = true; // 启用优先级增强

                    if (hasArgumentsMLine && Pr.StartInfo.RedirectStandardInput)
                    {
                        Pr.StandardInput.WriteLine("@echo off");
                        foreach (var input in inputStr.Split('\n'))
                        {
                            ReceiveProgress(null, input);
                            Pr.StandardInput.WriteLine(input.TrimEnd());
                        }

                        Pr.StandardInput.WriteLine("@exit");
                    }

                    if (Pr.StartInfo.RedirectStandardOutput) Pr.BeginOutputReadLine();
                    if (Pr.StartInfo.RedirectStandardError) Pr.BeginErrorReadLine();

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
        }

        #endregion

        #region Create

        /// <summary>
        /// 创建构造器
        /// </summary>
        /// <returns>结果执行器</returns>
        public static IExecutor Create()
        {
            return Activator.CreateInstance<PrCmd>().SetInArgs(CMD_ARGS).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="format">Format:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        public new static IExecutor Create(in (string, string) cmd_work, in string format, params object[] args)
        {
            return Activator.CreateInstance<PrCmd>().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2).SetInArgs(format, args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        public new static IExecutor Create(in (string, string) cmd_work, in StringBuilder args)
        {
            return Activator.CreateInstance<PrCmd>().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2).SetInArgs(args.ToString()).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        public new static IExecutor Create(in (string, string) cmd_work, in string args)
        {
            return Activator.CreateInstance<PrCmd>().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        public new static IExecutor Create(in (string, string) cmd_work, in ICollection<string> args)
        {
            return Activator.CreateInstance<PrCmd>().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>        
        /// <returns>结果执行器</returns>
        public new static IExecutor Create(in (string, string) cmd_work)
        {
            return Activator.CreateInstance<PrCmd>().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="format">Format:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        public new static IExecutor Create(in string cmd, in string format, params object[] args)
        {
            return Activator.CreateInstance<PrCmd>().SetFileName(cmd).SetInArgs(format, args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        public new static IExecutor Create(in string cmd, in ICollection<string> args)
        {
            return Activator.CreateInstance<PrCmd>().SetFileName(cmd).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        public new static IExecutor Create(in string cmd, in string args)
        {
            return Activator.CreateInstance<PrCmd>().SetFileName(cmd).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        public new static IExecutor Create(in string cmd, in StringBuilder args)
        {
            return Activator.CreateInstance<PrCmd>().SetFileName(cmd).SetInArgs(args.ToString()).Execute();
        }

        #endregion
    }
}