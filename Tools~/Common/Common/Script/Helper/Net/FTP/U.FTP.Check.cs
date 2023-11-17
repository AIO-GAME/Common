using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AIO;

public partial class AHelper
{
    public partial class Net
    {
        public partial class FTP
        {
            #region 检查FTP连接

            /// <summary>
            /// 判断FTP连接
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns>Ture:有效 False:无效</returns>
            public static bool Check(string uri, string username, string password, ushort timeout = TIMEOUT)
            {
                try
                {
                    var remote = FixShortcuts(uri);
                    var eindex = remote.LastIndexOf('/');
                    var parent = remote.Substring(0, eindex);
                    var dirname = remote.Substring(eindex + 1, remote.Length - parent.Length - 1).Trim(' ', '/', '\\');

                    var request = CreateRequest(parent, username, password, "NLST", timeout);
                    using var response = (FtpWebResponse)request.GetResponse();
                    using var stream = response.GetResponseStream();
                    if (stream is null) return false;

                    using TextReader reader = new StreamReader(stream);
                    var lines = reader.ReadToEnd();
                    return lines.Split()
                        .Where(line => !string.IsNullOrEmpty(line))
                        .Any(line => line.EndsWith(dirname));
                }
                catch (WebException)
                {
                    return false;
                }
            }

            /// <summary>
            /// 判断FTP连接
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns>Ture:有效 False:无效</returns>
            public static async Task<bool> CheckAsync(string uri, string username, string password,
                ushort timeout = TIMEOUT)
            {
                try
                {
                    var remote = FixShortcuts(uri);
                    var eindex = remote.LastIndexOf('/');
                    var parent = remote.Substring(0, eindex);
                    var dirname = remote.Substring(eindex + 1, remote.Length - parent.Length - 1).Trim(' ', '/', '\\');

                    var request = CreateRequest(parent, username, password, "NLST", timeout);
                    using var response = (FtpWebResponse)await request.GetResponseAsync();
                    using var stream = response.GetResponseStream();
                    if (stream is null) return false;

                    using TextReader reader = new StreamReader(stream);
                    var lines = await reader.ReadToEndAsync();
                    var status = lines.Split()
                        .Where(line => !string.IsNullOrEmpty(line))
                        .Any(line => line.EndsWith(dirname));
                    Console.WriteLine("{0},{1},{2}", lines, status, dirname);
                    return status;
                }
                catch (WebException ex)
                {
#if DEBUG
                    Console.WriteLine("{0} {2}:{3}@{1} ->\n {4}",
                        nameof(CheckAsync), ex.Response.ResponseUri, username, password, ex.Message);
#endif
                    return false;
                }
            }

            #endregion
        }
    }
}