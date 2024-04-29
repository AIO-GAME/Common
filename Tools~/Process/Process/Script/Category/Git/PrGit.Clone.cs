/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;

#endregion

namespace AIO
{
    public partial class PrGit
    {
        #region Nested type: Clone

        /// <summary>
        /// <see cref="PrGit"/> <see cref="Clone"/> 克隆
        /// </summary>
        public static class Clone
        {
            /// <summary>
            /// 克隆指定分支
            /// </summary>
            /// <param name="work">目标文件夹</param>
            /// <param name="url">clone列表</param>
            /// <param name="branch">分支</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Branch(string work, string url, string branch)
            {
                if (string.IsNullOrEmpty(url))
                    throw new ArgumentException(string.Concat("Url not a valid address : ", url));
                if (string.IsNullOrEmpty(branch))
                    throw new ArgumentNullException(string.Concat("branch not a valid string : ", branch));

                return Create(work,
                              $"clone --recursive --shallow-submodules --progress --verbose --depth 1 -b {branch} \"{url.TrimEnd('/', '\\')}\"");
            }

            /// <summary>
            /// 克隆指定分支
            /// </summary>
            /// <param name="work">目标文件夹</param>
            /// <param name="url">clone列表</param>
            /// <param name="branch">分支</param>
            /// <param name="alias">别名</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Branch(string work, string url, string alias, string branch)
            {
                if (string.IsNullOrEmpty(url))
                    throw new ArgumentException(string.Concat("Url not a valid address : ", url));
                if (string.IsNullOrEmpty(branch))
                    throw new ArgumentNullException(string.Concat("branch not a valid string : ", branch));
                if (string.IsNullOrEmpty(alias))
                    throw new ArgumentNullException(string.Concat("alias not a valid dir : ", alias));

                return Create(work,
                              $"clone --recursive --shallow-submodules --progress --verbose --depth 1 -b {branch} \"{url.TrimEnd('/', '\\')}\" {alias}");
            }

            /// <summary>
            /// 克隆指定 Tag
            /// </summary>
            /// <param name="work">目标文件夹</param>
            /// <param name="url">clone列表</param>
            /// <param name="tag">标签</param>
            /// <param name="alias">别名</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Tag(string work, string url, string alias, string tag)
            {
                if (string.IsNullOrEmpty(url))
                    throw new ArgumentException(string.Concat("Url not a valid address : ", url));
                if (string.IsNullOrEmpty(tag))
                    throw new ArgumentNullException(string.Concat("Url not a valid branch : ", tag));

                return Create(work,
                              $"clone --recursive --shallow-submodules --progress --verbose --depth 1 --branch {tag} \"{url.TrimEnd('/', '\\')}\" {alias}");
            }

            /// <summary>
            /// 克隆指定 Tag
            /// </summary>
            /// <param name="work">目标文件夹</param>
            /// <param name="url">clone列表</param>
            /// <param name="tag">标签</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Tag(string work, string url, string tag)
            {
                if (string.IsNullOrEmpty(url))
                    throw new ArgumentException(string.Concat("Url not a valid address : ", url));
                if (string.IsNullOrEmpty(tag))
                    throw new ArgumentNullException(string.Concat("Url not a valid branch : ", tag));

                return Create(work,
                              $"clone --recursive --shallow-submodules --progress --verbose --depth 1 --branch {tag} \"{url.TrimEnd('/', '\\')}\"");
            }

            /// <summary>
            /// 克隆默认分支
            /// </summary>
            /// <param name="work">目标文件夹</param>
            /// <param name="url">clone列表</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Default(string work, string url)
            {
                if (string.IsNullOrEmpty(url))
                    throw new ArgumentException(string.Concat("Url not a valid address : ", url));
                return Create(work,
                              $"clone --recursive --shallow-submodules --progress --verbose --depth 1 \"{url.TrimEnd('/', '\\')}\"");
            }

            /// <summary>
            /// 执行
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="args">参数</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Execute(string work, string args)
            {
                if (string.IsNullOrEmpty(args)) throw new ArgumentNullException(nameof(args));
                return Create(work, "clone {0}", args);
            }
        }

        #endregion
    }
}