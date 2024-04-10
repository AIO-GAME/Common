/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-09
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

#region

using System.Diagnostics;
using System.Text;

#endregion

namespace AIO
{
    public partial class PrCurl
    {
        /// <summary>
        /// Show version number and quit
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <param name="silent">静默模式</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static IExecutor Version(string remote, bool silent = false)
        {
            var str = new StringBuilder();
            str.Append(Usage.Version);
            if (silent) str.Append(Usage.Silent);
            str.Append(" ").Append(remote);
            return Create().SetInArgs(str).Execute();
        }
    }
}