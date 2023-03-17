namespace AIO
{
    public partial class PrMac
    {
        /// <summary>
        /// 在本地主机与远程主机之间复制文件
        /// </summary>
        public static class Rcp
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command)
            {
                return Create(CMD_Rcp, command);
            }
        }
    }
}