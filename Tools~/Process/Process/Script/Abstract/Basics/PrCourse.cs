/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 进程构造器
    /// </summary>
    public abstract partial class PrCourse : IPrCourse, IPrCourseRead
    {
        /// <inheritdoc/>
        public ProcessStartInfo Info { get; protected set; }

        /// <inheritdoc/>
        public bool EnableOutput { get; set; } = true;

        /// <summary>
        /// 开启日志
        /// </summary>
        public static bool IsLog { get; set; } = true;

        /// <summary>
        /// 是否捕获异常
        /// </summary>
        public static bool IsCache { get; set; } = false;

        /// <summary>
        /// 编码
        /// </summary>
        protected Encoding encoding;

        /// <summary>
        /// 创建进程构造器
        /// </summary>
        public PrCourse()
        {
            Info = new ProcessStartInfo();
        }

        /// <inheritdoc/>
        public virtual IExecutor Execute()
        {
            return new Executor(Info, EnableOutput);
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Info = null;
        }

        /// <inheritdoc/>
        public IPrCourse SetInEncoding(Encoding defEncoding)
        {
            encoding = defEncoding;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetRedirectInput(bool value)
        {
            Info.RedirectStandardInput = value;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetFileName(in string target)
        {
            if (string.IsNullOrEmpty(target)) throw new ArgumentNullException(nameof(target));
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    Info.FileName = target.Replace('/', '\\');
                    break;
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                    Info.FileName = target.Replace('\\', '/');
                    break;
                default:
                    Info.FileName = target;
                    break;
            }

            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetRedirectOutput(in bool value)
        {
            Info.RedirectStandardOutput = value;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetRedirectError(in bool value)
        {
            Info.RedirectStandardError = value;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInArgs(in ICollection<string> args)
        {
            if (args == null || args.Count == 0) return this;
            Info.Arguments = string.Join(" ", args.Select(arg => arg));
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInArgs(in string format, params object[] args)
        {
            if (string.IsNullOrEmpty(format)) return this;
            Info.Arguments = string.Format(format, args).Trim();
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInArgs(in string args)
        {
            if (string.IsNullOrEmpty(args)) return this;
            Info.Arguments = args.Trim();
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInArgs(in StringBuilder args)
        {
            if (args is null || args.Length == 0) return this;
            Info.Arguments = args.ToString().Trim();
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInDomain(in string domain = null)
        {
            Info.Domain = domain ?? AppDomain.CurrentDomain.DynamicDirectory;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInErrorDialog(in bool value)
        {
            Info.ErrorDialog = value;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInLoadUserProfile(in bool value)
        {
            Info.LoadUserProfile = value;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetUserName(in string username)
        {
            Info.UserName = username;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInPassword(in string username)
        {
            var pass = new SecureString();
            foreach (var item in username)
                pass.AppendChar(item);
            Info.Password = pass;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInUseShellExecute(in bool value)
        {
            Info.UseShellExecute = value;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInCreateNoWindow(in bool value)
        {
            Info.CreateNoWindow = value;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInWindow(in ProcessWindowStyle style = ProcessWindowStyle.Normal)
        {
            Info.WindowStyle = style;
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetInVers(in EPrVerb verb)
        {
            Info.Verb = verb.ToString();
            return this;
        }

        /// <inheritdoc/>
        public IPrCourse SetWorkingDir(in string workDir)
        {
            Info.WorkingDirectory = workDir;
            return this;
        }
    }
}