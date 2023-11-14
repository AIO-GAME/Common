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
        /// Get help for commands
        /// </summary>
        /// <param name="category">用法类型</param>
        /// <param name="silent">静默模式</param>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static IExecutor Help(string category = "", bool silent = false)
        {
            var str = new StringBuilder();
            str.AppendFormat(Usage.Help, category);
            if (silent) str.Append(Usage.Silent);
            return Create().SetInArgs(str).Execute();
        }
    }
}