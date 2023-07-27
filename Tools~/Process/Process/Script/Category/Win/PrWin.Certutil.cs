namespace AIO
{
    public partial class PrWin
    {
        /// <summary>
        /// Certutil 命令
        /// </summary>
        public static class Certutil
        {
            /// <summary>
            /// 计算MD5
            /// </summary>
            public static IExecutor MD5(string target)
            {
                return Create(CMD_Certutil, "-hashfile \"{0}\" MD5", target.Replace('/', '\\'));
            }
        }
    }
}