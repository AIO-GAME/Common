namespace AIO
{
    public sealed partial class PrMac
    {
        #region Nested type: Chmod

        /// <summary>
        /// 权限相关类
        /// </summary>
        public static class Chmod
        {
            /// <summary>
            /// 设置权限的方法
            /// </summary>
            /// <param name="target">目标文件的路径</param>
            /// <param name="args">需要设置的权限</param>
            /// <returns>一个实现了 IExecutor 接口的对象，表示设置权限的执行器</returns>
            public static IExecutor Set(in string target, int args)
            {
                return Create(CMD_Chmod, $"{args} \"{target.Replace('\\', '/')}\"");
            }

            /// <summary>
            /// 设置权限的方法
            /// </summary>
            /// <param name="target">目标文件的路径</param>
            /// <param name="args">需要设置的权限</param>
            /// <returns>一个实现了 IExecutor 接口的对象，表示设置权限的执行器</returns>
            public static IExecutor Set(in string target, string args)
            {
                return Create(CMD_Chmod, $"{args} \"{target.Replace('\\', '/')}\"");
            }

            /// <summary>
            /// 为目标文件添加读、写、执行权限的方法
            /// </summary>
            /// <param name="target">目标文件的路径</param>
            /// <returns>一个实现了 IExecutor 接口的对象，表示设置权限的执行器</returns>
            public static IExecutor AddRWX(in string target)
            {
                return Set(target, "+r+w+x");
            }

            /// <summary>
            /// 为目标文件减少读、写、执行权限的方法
            /// </summary>
            /// <param name="target">目标文件的路径</param>
            /// <returns>一个实现了 IExecutor 接口的对象，表示设置权限的执行器</returns>
            public static IExecutor SubRWX(in string target)
            {
                return Set(target, "-r-w-x");
            }

            /// <summary>
            /// 为目标文件添加追加权限的方法
            /// </summary>
            /// <param name="target">目标文件的路径</param>
            /// <returns>一个实现了 IExecutor 接口的对象，表示设置权限的执行器</returns>
            public static IExecutor AddAW(in string target)
            {
                return Set(target, "+a+w");
            }

            /// <summary>
            /// 为目标文件减少追加权限的方法
            /// </summary>
            /// <param name="target">目标文件的路径</param>
            /// <returns>一个实现了 IExecutor 接口的对象，表示设置权限的执行器</returns>
            public static IExecutor SubAW(in string target)
            {
                return Set(target, "-a-w");
            }

            /// <summary>
            /// 为目标文件设置权限 777
            /// </summary>
            /// <param name="target">目标文件的路径</param>
            /// <returns>一个实现了 IExecutor 接口的对象，表示设置权限的执行器</returns>
            public static IExecutor Set777(in string target)
            {
                return Set(target, 777);
            }

            /// <summary>
            /// 为目标文件设置权限 rwxr-xr-x 
            /// </summary>
            /// <param name="target">目标文件的路径</param>
            /// <returns>一个实现了 IExecutor 接口的对象，表示设置权限的执行器</returns>
            public static IExecutor Set755(in string target)
            {
                return Set(target, 755);
            }
        }

        #endregion
    }
}