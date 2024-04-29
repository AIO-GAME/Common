namespace AIO
{
    public partial class PrCmd
    {
        #region Nested type: Rmdir

        /// <summary>
        /// 删除文件夹
        /// </summary>
        public static class Rmdir
        {
            /// <summary>
            /// 执行
            /// </summary>
            public static IExecutor Execute(in string target)
            {
                return Create().Input(string.Format("{0} \"{1}\" /S /Q", CMD_Rmdir, target.Replace('/', '\\')));
            }
        }

        #endregion
    }
}