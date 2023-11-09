public partial class AHelper
{
    /// <summary>
    /// 网络 工具类
    /// </summary>
    public partial class Net
    {
        private Net()
        {
        }

        /// <summary>
        /// HTTP
        /// </summary>
        public partial class HTTP
        {
            private const string DELETE = nameof(DELETE);
            private const string GET = nameof(GET);
            private const string HEAD = nameof(HEAD);
            private const string POST = nameof(POST);
            private const string OPTIONS = nameof(OPTIONS);
            private const string PUT = nameof(PUT);
            private const string TRACE = nameof(TRACE);
            private const string CONNECT = nameof(CONNECT);

            private HTTP()
            {
            }
        }

        /// <summary>
        /// FTP
        /// </summary>
        public partial class FTP
        {
            private FTP()
            {
            }
        }
    }
}