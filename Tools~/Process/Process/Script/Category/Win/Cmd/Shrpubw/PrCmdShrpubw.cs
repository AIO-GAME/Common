namespace AIO
{
    public partial class PrCmd
    {
        /// <summary>
        /// windows 共享文件夹
        /// </summary>
        public static class Shrpubw
        {
            /// <summary>
            /// 创建 windows 共享文件夹
            /// </summary>
            public static IExecutor Create(in string target)
            {
                return PrCmd.Create().Input(string.Format("{0} /s \"{1}\"", CMD_Shrpubw, target.Replace('/', '\\')));
            }

            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(in string messages)
            {
                return PrCmd.Create().Input(messages);
            }
        }
    }
}