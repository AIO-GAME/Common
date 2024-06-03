/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;
using System.Diagnostics;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// Curl[WIN][MAC][LINUX]
    /// </summary>
    /// <remarks>
    /// 当前命令为Windows版，Linux版和Mac版命令参数可能有所不同
    /// </remarks>
    public sealed partial class PrCurl : PrCourse
    {
        /// <summary>
        /// 命令
        /// </summary>
        public const string CMD = "curl";

        [DebuggerHidden, DebuggerNonUserCode]
        private PrCurl() { }

        /// <summary>
        /// 运行
        /// </summary>
        /// <returns><see cref="IExecutor"/></returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public override IExecutor Execute()
        {
            try
            {
                SetFileName(CMD);
                SetInCreateNoWindow(true);
                SetInUseShellExecute(false);
                SetRedirectError(true);
                SetRedirectOutput(true);

                if (string.IsNullOrEmpty(Info.WorkingDirectory))
                    Info.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

                encoding = encoding ?? Encoding.UTF8;

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

            return new Executor(Info, EnableOutput);
        }

        private static IPrCourse Create()
        {
            return new PrCurl().SetInEncoding(Encoding.UTF8);
        }

        /// <summary>
        /// Create a new instance of PrCurl
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns><see cref="PrCurl"/></returns>
        public static IPrCourse Create(string args)
        {
            return new PrCurl().SetInEncoding(Encoding.UTF8).SetInArgs(args);
        }

        #region Nested type: Usage

        /// <summary>
        /// 用法
        /// </summary>
        public static class Usage
        {
            /// <summary>
            /// HTTP POST data        
            /// </summary>
            public const string Data = " -d \"{0}\"";

            /// <summary>
            /// Fail fast with no output on HTTP errors        
            /// </summary>
            public const string Fail = " --fail";

            /// <summary>
            /// Get help for commands                  
            /// </summary>
            public const string Help = " --help '{0}'";

            /// <summary>
            /// Include protocol response headers in the output
            /// </summary>
            public const string Include = " --include";

            /// <summary>
            /// Write to file instead of stdout                
            /// </summary>
            public const string Output = " --output '{0}'";

            /// <summary>
            /// Write output to a file named as the remote file
            /// </summary>
            public const string RemoteName = " --remote-name";

            /// <summary>
            /// Silent mode                          
            /// </summary>
            public const string Silent = " --silent";

            /// <summary>
            /// --upload-file &lt;file>   Transfer local FILE to destination             
            /// </summary>
            public const string UploadFile = " --upload-file '{0}'";

            /// <summary>
            /// --user &lt;user:password> Server user and password                
            /// </summary>
            public const string User = " --user '{0}'";

            /// <summary>
            /// Send User-Agent &lt;name> to server        
            /// </summary>    
            public const string UserAgent = " --user-agent '{0}'";

            /// <summary>
            /// Make the operation more talkative           
            /// </summary>   
            public const string Verbose = " --verbose";

            /// <summary>
            /// Show version number and quit     
            /// </summary>
            public const string Version = " --version";

            /// <summary>
            /// HTTP POST data
            /// </summary>
            public const string XPost = " -X POST";

            /// <summary>
            /// HTTP POST data
            /// </summary>
            public const string Header = " -H \"{0}:{1}\"";
        }

        #endregion
    }
}