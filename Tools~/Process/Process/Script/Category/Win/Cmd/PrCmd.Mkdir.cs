namespace AIO
{
    public partial class PrCmd
    {
        #region Nested type: Mkdir

        /// <summary>
        /// 创建目录命令
        /// </summary>
        public static class Mkdir
        {
            /// <summary>
            /// 创建目录
            /// </summary>
            public static IExecutor Directory(string target)
            {
                return Create().Input($"mkdir \"{target.Replace("/", "\\")}\"");
            }
        }

        #endregion
    }
}