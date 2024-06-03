namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Write

        /// <summary>
        /// 给另一用户发送报文
        /// </summary>
        public static class Write
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command)
            {
                return Create(CMD_Write, command);
            }
        }

        #endregion
    }
}