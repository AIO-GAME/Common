namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Mesg

        /// <summary>
        /// 允许或拒绝接收报文
        /// </summary>
        public static class Mesg
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command)
            {
                return Create(CMD_Mesg, command);
            }
        }

        #endregion
    }
}