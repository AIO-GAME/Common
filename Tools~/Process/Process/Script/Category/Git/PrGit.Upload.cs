/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    public partial class PrGit
    {
        /// <summary>
        /// 上传 只支持最简单的操作
        /// </summary>
        /// <param name="target">GIT 文件夹</param>
        /// <param name="commitData">提交信息</param>
        /// <returns></returns>
        public static IExecutor Upload(string target, string commitData = "-m \"default submission information\"")
        {
            var ret = Pull.Update(target);
            ret.Link(Add.ALL(target));
            ret.Link(Commit.Default(target, commitData));
            ret.Link(Push.Update(target));
            return ret;
        }
    }
}