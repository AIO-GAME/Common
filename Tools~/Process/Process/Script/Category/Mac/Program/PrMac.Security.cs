#region

using System.IO;

#endregion

namespace AIO
{
    public sealed partial class PrMac
    {
        #region Nested type: Security

        /// <summary>
        ///
        /// </summary>
        public sealed class Security
        {
            /// <summary>
            ///
            /// </summary>
            public static IExecutor CMS(string target)
            {
                if (!target.Contains("mobileprovision")) throw new FileNotFoundException("[PrMac Security] file path not exist mobileprovision");
                if (!File.Exists(target)) throw new FileNotFoundException($"[PrMac Security] file path not exist {target}");
                var cmd = string.Format("cms -D -i {0}", target);
                return Create(CMD_Security, cmd);
            }
        }

        #endregion
    }
}