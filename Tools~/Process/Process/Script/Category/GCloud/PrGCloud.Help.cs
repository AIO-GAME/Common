/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-09
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.Diagnostics;

namespace AIO
{
    public partial class PrGCloud
    {
        /// <summary>
        /// Get help for commands
        /// </summary>
        /// <returns>结果执行器</returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static IExecutor Help()
        {
            return Create(Usage.Help);
        }
    }
}