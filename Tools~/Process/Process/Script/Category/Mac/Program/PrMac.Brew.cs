namespace AIO
{
    public sealed partial class PrMac
    {
        #region Nested type: Brew

        /// <summary>
        /// 安装命令
        /// </summary>
        public sealed class Brew
        {
            /// <summary>
            /// 安装包
            /// </summary>
            /// <param name="package">包名</param>
            /// <returns>结果执行器</returns>
            public static IExecutor Install(string package)
            {
                return Create(CMD_Brew, "install {0}", package);
            }
        }

        #endregion
    }
}