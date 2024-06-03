namespace AIO
{
    public sealed partial class PrMac
    {
        #region Nested type: OsaScript

        /// <summary>
        /// Bundle ID
        /// </summary>
        public sealed class OsaScript
        {
            /// <summary>
            /// 执行查询Bundle ID
            /// </summary>
            public static IExecutor BundleID(string package)
            {
                return Create(CMD_OsaScript, "-e 'id of app' \"{0}\"", package);
            }

            /// <summary>
            /// 执行查询Bundle ID
            /// </summary>
            public static IExecutor BundleID(string args, string package)
            {
                return Create(CMD_OsaScript, "{0} \"{1}\"", args, package);
            }
        }

        #endregion
    }
}