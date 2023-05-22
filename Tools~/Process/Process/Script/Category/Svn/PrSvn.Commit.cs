/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-05-21               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

namespace AIO
{
    public partial class PrSvn
    {
        /// <summary>
        /// 提交 命令
        /// </summary>
        public sealed class Commit
        {
            /// <summary>
            /// 执行
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <param name="args">参数</param>
            /// <returns>执行器</returns>
            public static IExecutor Execute(in string work, in string args)
            {
                if (string.IsNullOrEmpty(args)) throw new System.ArgumentNullException(nameof(args));
                return Create(work, "ci {0} --force-log", args);
            }

            /// <summary>
            /// 提交全部
            /// </summary>
            /// <param name="work">文件夹</param>
            /// <param name="message">提交消息</param>
            /// <returns>执行器</returns>
            public static IExecutor ALL(in string work, in string message)
            {
                return Create(work, "ci -m {0} --force-log --no-unlock", message);
            }
        }
    }
}