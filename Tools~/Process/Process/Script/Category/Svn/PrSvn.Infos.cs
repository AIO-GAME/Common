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
        /// 信息 命令
        /// </summary>
        public sealed class Infos
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
                return Create(work, "info {0}", args);
            }
        }
    }
}