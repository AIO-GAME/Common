namespace AIO
{
    public partial class PrMac
    {
        /// <summary>
        /// 阅读和发送电子邮件
        /// </summary>
        public static class Mail
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(string command)
            {
                return Create(CMD_Mail, command);
            }
        }
    }
}