/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;

namespace AIO
{
    public partial class PrGit
    {
        /// <summary>
        /// <see cref="PrGit"/> <see cref="Clone"/> 克隆
        /// </summary>
        public static class Clone
        {
            /// <summary>
            /// 克隆指定分支
            /// </summary>
            /// <param name="wrok">目标文件夹</param>
            /// <param name="url">clone列表</param>
            /// <param name="branch">分支</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Branch(in string wrok, in string url, in string branch)
            {
                if (string.IsNullOrEmpty(branch)) throw new ArgumentNullException(nameof(branch));
                if (!NetUtils.UrlCheck(url)) throw new ArgumentException(string.Concat("Url not a valid address : ", url));
                return Create(wrok, "clone \"{1}\" {2} {3}", url, "-b", branch, "--recurse-submodules --progress --verbose");
            }

            /// <summary>
            /// 克隆默认分支
            /// </summary>
            /// <param name="wrok">目标文件夹</param>
            /// <param name="url">clone列表</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Default(in string wrok, in string url)
            {
                if (!NetUtils.UrlCheck(url)) throw new ArgumentException(string.Concat("Url not a valid address : ", url));
                return Create(wrok, "clone \"{1}\" {2}", url, "--recurse-submodules --progress --verbose");
            }

            /// <summary>
            /// 执行
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="args">参数</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Execute(in string work, in string args)
            {
                if (string.IsNullOrEmpty(args)) throw new ArgumentNullException(nameof(args));
                return Create(work, "clone {0}", args);
            }
        }
    }
}