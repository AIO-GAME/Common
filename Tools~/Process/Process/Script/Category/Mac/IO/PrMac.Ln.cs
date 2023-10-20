namespace AIO
{
    public partial class PrMac
    {
        // 指令名称 : ln
        //     使用权限 : 所有使用者
        //     使用方式 : ln [options] source dist，其中 option 的格式为 :
        // [-bdfinsvF] [-S backup-suffix] [-V {numbered,existing,simple}]
        // [--help] [--version] [--]
        // 说明 : Linux/Unix 档案系统中，有所谓的连结(link)，我们可以将其视为档案的别名，
        // 而连结又可分为两种 :
        // 硬连结(hard link)与软连结(symbolic link)， 硬连结的意思是一个档案可以有多个名称，
        // 而软连结的方式则是产生一个特殊的档案，该档案的内容是指向另一个档案的位置。
        // 硬连结是存在同一个档 案系统中，而软连结却可以跨越不同的档案系统。
        // ln source dist 是产生一个连结(dist)到 source，至于使用硬连结或软链结则由参数决定。
        // 不论是硬连结或软链结都不会将原本的档案复制一份，只会占用非常少量的磁碟空间。
        // -f : 链结时先将与 dist 同档名的档案删除
        // -d : 允许系统管理者硬链结自己的目录
        // -i : 在删除与 dist 同档名的档案时先进行询问
        // -n : 在进行软连结时，将 dist 视为一般的档案
        // -s : 进行软链结(symbolic link)
        //     -v : 在连结之前显示其档名
        // -b : 将在链结时会被覆写或删除的档案进行备份
        // -S SUFFIX : 将备份的档案都加上 SUFFIX 的字尾
        // -V METHOD : 指定备份的方式
        // --help : 显示辅助说明
        // --version : 显示版本
        //     范例 :
        // 将档案 yy 产生一个 symbolic link : zz
        //     ln -s yy zz
        //     将档案 yy 产生一个 hard link : zz
        //     ln yy xx

        /// <summary>
        /// 联接文件
        /// </summary>
        public static class Ln
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string source, string target, string args)
            {
                return Create(CMD_Ln, "{0} '{1}' '{2}'", args, source.Replace('\\', '/'), target.Replace('\\', '/'));
            }

            /// <summary>
            /// 软连接
            /// </summary>
            public static IExecutor Symbolic(string source, string target)
            {
                return Create(CMD_Ln, "-s '{0}' '{1}'", source.Replace('\\', '/'), target.Replace('\\', '/'));
            }

            /// <summary>
            /// 硬连接
            /// </summary>
            public static IExecutor Hard(string source, string target)
            {
                return Create(CMD_Ln, "'{0}' '{1}'", source.Replace('\\', '/'), target.Replace('\\', '/'));
            }

            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string args)
            {
                return Create(CMD_Ln, args);
            }
        }
    }
}