/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-05-21               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using System;
using System.Collections.Generic;
using System.Text;

namespace AIO
{
    /// <summary>
    /// PrSvn[WIN][MAC][LINUX]
    /// </summary>
    public sealed partial class PrSvn : PrCourse
    {
        /// <summary>
        /// 命令
        /// </summary>
        public const string CMD = "svn";

        /// <summary>
        /// 运行
        /// </summary>
        public override IExecutor Execute()
        {
            try
            {
                SetFileName(CMD);
                SetInCreateNoWindow(true);
                SetInUseShellExecute(false);
                SetRedirectError(true);
                SetRedirectOutput(true);

                if (string.IsNullOrEmpty(Info.WorkingDirectory)) Info.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

                encoding = encoding ?? Encoding.UTF8;

#if (NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP3_0 || NETCOREAPP3_1)
                if (p.StartInfo.RedirectStandardInput) p.StartInfo.StandardInputEncoding = encoding;
#endif

                if (Info.RedirectStandardOutput) Info.StandardOutputEncoding = encoding;
                if (Info.RedirectStandardError) Info.StandardErrorEncoding = encoding;
            }
            catch (Exception ex)
            {
                return new ExecutorException(Info, ex);
            }

            return new Executor(Info);
        }

        private static IPrCourse Create()
        {
            return Activator.CreateInstance<PrSvn>().SetInEncoding(Encoding.UTF8);
        }

        #region Create

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="work">工作路径:NoNull</param>                      
        /// <param name="format">Format:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in string work, in string format, params object[] args)
        {
            return Create().SetWorkingDir(work).SetInArgs(format, args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="work">工作路径:NoNull</param>                   
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in string work, in StringBuilder args)
        {
            return Create().SetWorkingDir(work).SetInArgs(args.ToString()).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="work">工作路径:NoNull</param>             
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in string work, in string args)
        {
            return Create().SetWorkingDir(work).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="work">工作路径:NoNull</param>                          
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in string work, in ICollection<string> args)
        {
            return Create().SetWorkingDir(work).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="work">工作路径:NoNull</param>            
        /// <returns>结果执行器</returns>
        public static IExecutor Create(in string work)
        {
            return Create().SetWorkingDir(work).Execute();
        }

        #endregion
    }
}