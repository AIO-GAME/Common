/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AIO
{
    public partial class PrCourse
    {
        /// <summary>
        /// 结当前进程
        /// </summary>
        public static void Kill()
        {
            Process.GetCurrentProcess().Kill();
        }

        #region Create

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">执行器类型</typeparam>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="format">Format:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        protected static IExecutor Create<T>(in (string, string) cmd_work, in string format, params object[] args)
            where T : class, IPrCourse, new()
        {
            return Activator.CreateInstance<T>().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2)
                .SetInArgs(format, args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">执行器类型</typeparam>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        protected static IExecutor Create<T>(in (string, string) cmd_work, in StringBuilder args)
            where T : class, IPrCourse, new()
        {
            return Activator.CreateInstance<T>().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2)
                .SetInArgs(args.ToString()).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">执行器类型</typeparam>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        protected static IExecutor Create<T>(in (string, string) cmd_work, in string args)
            where T : class, IPrCourse, new()
        {
            return Activator.CreateInstance<T>().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2)
                .SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">执行器类型</typeparam>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>                      
        /// <param name="args">参数</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        protected static IExecutor Create<T>(in (string, string) cmd_work, in ICollection<string> args)
            where T : class, IPrCourse, new()
        {
            return Activator.CreateInstance<T>().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2)
                .SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">执行器类型</typeparam>
        /// <param name="cmd_work">[Item1=CMD路径:NoNull][Item2=工作路径:NoNull]</param>        
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        protected static IExecutor Create<T>(in (string, string) cmd_work) where T : class, IPrCourse, new()
        {
            return Activator.CreateInstance<T>().SetFileName(cmd_work.Item1).SetWorkingDir(cmd_work.Item2).Execute();
        }


        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">执行器类型</typeparam>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="format">Format:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        protected static IExecutor Create<T>(in string cmd, in string format, params object[] args)
            where T : class, IPrCourse, new()
        {
            return Activator.CreateInstance<T>().SetFileName(cmd).SetInArgs(format, args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">执行器类型</typeparam>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        protected static IExecutor Create<T>(in string cmd, in ICollection<string> args)
            where T : class, IPrCourse, new()
        {
            return Activator.CreateInstance<T>().SetFileName(cmd).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">执行器类型</typeparam>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static IExecutor Create<T>(in string cmd, in string args) where T : class, IPrCourse, new()
        {
            return Activator.CreateInstance<T>().SetFileName(cmd).SetInArgs(args).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">执行器类型</typeparam>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="args">格式化参数</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        protected static IExecutor Create<T>(in string cmd, in StringBuilder args) where T : class, IPrCourse, new()
        {
            return Activator.CreateInstance<T>().SetFileName(cmd).SetInArgs(args.ToString()).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">执行器类型</typeparam>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <param name="verb">管理员权限</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        protected static IExecutor Create<T>(in string cmd, in EPrVerb verb) where T : class, IPrCourse, new()
        {
            return Activator.CreateInstance<T>().SetInVers(verb).SetFileName(cmd).Execute();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">执行器类型</typeparam>
        /// <param name="cmd">CMD路径:NoNull</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        protected static IExecutor Create<T>(in string cmd) where T : class, IPrCourse, new()
        {
            return Activator.CreateInstance<T>().SetFileName(cmd).Execute();
        }

        #endregion
    }
}