namespace AIO
{
    public partial class PrWin
    {
        #region Nested type: Shutdown

        /// <summary>
        /// windows 重启命令
        /// </summary>
        public static class Shutdown
        {
            /// <summary>
            /// windows 重启命令
            /// </summary>
            public static IExecutor Restart(in int time = 600)
            {
                return Create(Cmd_Shutdown, "-r -t {0}", time);
            }

            /// <summary>
            /// windows 注销命令
            /// </summary>
            public static IExecutor LogOff(in int time = 600)
            {
                return Create(Cmd_Shutdown, "-l -t {0}", time);
            }

            /// <summary>
            /// windows 60秒倒计时关机命令
            /// </summary>
            public static IExecutor Timer(in int time = 600)
            {
                return Create(Cmd_Shutdown, "-s -t {0}", time);
            }

            /// <summary>
            /// windows 取消倒计时关机
            /// </summary>
            public static IExecutor TimerCancel()
            {
                return Create(Cmd_Shutdown, "-a");
            }
        }

        #endregion
    }
}