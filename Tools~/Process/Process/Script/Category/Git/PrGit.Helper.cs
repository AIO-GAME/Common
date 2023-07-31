/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-31
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System.Threading.Tasks;

namespace AIO
{
    public partial class PrGit
    {
        /// <summary>
        /// <see cref="PrGit"/> <see cref="Helper"/>
        /// </summary>
        public static class Helper
        {
            /// <summary>
            /// <see cref="PrGit"/> <see cref="Helper"/> <see cref="Upload"/> 上传 只支持最简单的操作
            /// </summary>
            /// <param name="target">GIT 文件夹</param>
            /// <param name="commitData">提交信息</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Upload(string target, string commitData = "-m \"default submission information\"")
            {
                var ret = Pull.Update(target);
                ret.Link(Add.ALL(target));
                ret.Link(Commit.Default(target, commitData));
                ret.Link(Push.Update(target));
                return ret;
            }

            /// <summary>
            /// <see cref="PrGit"/> <see cref="Helper"/> <see cref="GetBehind"/> 获取当前分支落后的提交数
            /// </summary>
            /// <param name="target">GIT 文件夹</param>
            /// <returns>
            /// 0:当前分支是最新的
            /// >1:当前分支落后的提交数
            /// -1:未知错误
            /// </returns>
            public static async Task<int> GetBehind(string target)
            {
                await Fetch.Origin(target);
                var ret = await Status.Execute(target);
                var lines = ret.StdOut.ToString().Split('\n');
                foreach (var line in lines)
                {
                    if (!line.StartsWith("Your branch")) continue;
                    if (line.StartsWith("Your branch is up to date with")) return 0;
                    if (line.StartsWith("Your branch is behind"))
                    {
                        var behind = line.Split(' ')[6];
                        return int.Parse(behind);
                    }
                }

                return -1;
            }
        }
    }
}