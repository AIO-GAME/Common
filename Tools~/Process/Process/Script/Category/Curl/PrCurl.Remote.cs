/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-09
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Diagnostics;
using System.Text;

namespace AIO
{
    public partial class PrCurl
    {
        /// <summary>
        /// 远端连接
        /// </summary>
        /// <param name="userAndPassword">用户名和密码</param>
        /// <param name="userAgent">Send User-Agent &lt;name> to server</param>
        /// <param name="output">输出路径</param>
        /// <param name="include">在输出中是否包含协议头</param>
        /// <param name="verbose">显示更详细</param>
        /// <param name="remote">远端地址</param>
        /// <param name="fail">快速失败，HTTP错误没有输出</param>
        /// <param name="silent">静默模式</param> 
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static IExecutor Remote(string remote,
            string userAndPassword = null,
            string userAgent = null,
            string output = null,
            bool include = false,
            bool verbose = false,
            bool fail = false,
            bool silent = false
        )
        {
            var str = new StringBuilder();

            if (!string.IsNullOrEmpty(userAndPassword) && userAndPassword.Contains(":"))
                str.AppendFormat(Usage.User, userAndPassword);
            if (!string.IsNullOrEmpty(userAgent)) str.AppendFormat(Usage.UserAgent, userAgent);
            if (verbose) str.Append(Usage.Verbose);
            if (fail) str.Append(Usage.Fail);
            if (!string.IsNullOrEmpty(output)) str.AppendFormat(Usage.Output, output);
            if (include) str.Append(Usage.Include);
            if (silent) str.Append(Usage.Silent);

            return Create().SetInArgs(str.Append(" ").Append(remote)).Execute();
        }
    }
}