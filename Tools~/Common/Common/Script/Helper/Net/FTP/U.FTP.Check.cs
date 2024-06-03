#region

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: FTP

        public partial class FTP
        {
            /// <summary>
            /// 判断FTP路径是否为文件
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns>Ture:是 False:不是</returns>
            public static bool CheckFile(string uri, string user, string pass, ushort timeout = Net.TIMEOUT)
            {
                try
                {
                    var remote = FixShortcuts(uri);
                    var startIndex = remote.LastIndexOf('/') + 1;
                    if (startIndex <= "ftp://".Length) return false;
                    var dirname = remote.Substring(startIndex, remote.Length - startIndex).Trim(' ', '/', '\\');
                    var parent = remote.Substring(0, startIndex);
                    var request = CreateRequestDir(parent, user, pass, "LIST", timeout);

                    using var response = (FtpWebResponse)request.GetResponse();
                    using var stream = response.GetResponseStream();
                    if (stream is null) return false;
                    using var reader = new StreamReader(stream);
                    var lines = reader.ReadToEnd();
                    var status = lines.Trim().SplitLine().Where(line => !line.StartsWith("d")).Any(line => line.EndsWith(dirname));

                    request.Abort();
                    return status;
                }
                catch (WebException)
                {
                    return false;
                }
            }

            /// <summary>
            /// 判断FTP路径是否为文件
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns>Ture:是 False:不是</returns>
            public static async Task<bool> CheckFileAsync(string uri, string user, string pass,
                                                          ushort timeout = Net.TIMEOUT)
            {
                try
                {
                    var remote = FixShortcuts(uri);
                    var startIndex = remote.LastIndexOf('/') + 1;
                    if (startIndex <= "ftp://".Length) return false;
                    var dirname = remote.Substring(startIndex, remote.Length - startIndex).Trim(' ', '/', '\\');
                    var parent = remote.Substring(0, startIndex);
                    var request = CreateRequestDir(parent, user, pass, "LIST", timeout);

                    using var response = (FtpWebResponse)await request.GetResponseAsync();
                    using var stream = response.GetResponseStream();
                    if (stream is null) return false;
                    using var reader = new StreamReader(stream);
                    var lines = await reader.ReadToEndAsync();
                    var status = lines.Trim().SplitLine().Where(line => !line.StartsWith("d")).Any(line => line.EndsWith(dirname));
                    request.Abort();
                    return status;
                }
                catch (WebException)
                {
                    return false;
                }
            }

            /// <summary>
            /// 判断FTP路径是否为文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns>Ture:是 False:不是</returns>
            public static bool CheckDir(string uri, string user, string pass, ushort timeout = Net.TIMEOUT)
            {
                try
                {
                    var remote = FixShortcuts(uri);
                    var startIndex = remote.LastIndexOf('/') + 1;
                    if (startIndex <= "ftp://".Length) return false;
                    var dirname = remote.Substring(startIndex, remote.Length - startIndex).Trim(' ', '/', '\\');
                    var parent = remote.Substring(0, startIndex);
                    var request = CreateRequestDir(parent, user, pass, "LIST", timeout);
                    using var response = (FtpWebResponse)request.GetResponse();
                    using var stream = response.GetResponseStream();
                    if (stream is null) return false;
                    using var reader = new StreamReader(stream);
                    var lines = reader.ReadToEnd();
                    var status = lines.Trim().SplitLine().Where(line => line.StartsWith("d")).Any(line => line.EndsWith(dirname));

                    request.Abort();
                    return status;
                }
                catch (WebException)
                {
                    return false;
                }
            }

            /// <summary>
            /// 判断FTP路径是否为文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns>Ture:是 False:不是</returns>
            public static async Task<bool> CheckDirAsync(string uri, string user, string pass,
                                                         ushort timeout = Net.TIMEOUT)
            {
                try
                {
                    var remote = FixShortcuts(uri);
                    var startIndex = remote.LastIndexOf('/') + 1;
                    if (startIndex <= "ftp://".Length) return false;
                    var dirname = remote.Substring(startIndex, remote.Length - startIndex).Trim(' ', '/', '\\');
                    var parent = remote.Substring(0, startIndex);
                    var request = CreateRequestDir(parent, user, pass, "LIST", timeout);
                    using var response = (FtpWebResponse)await request.GetResponseAsync();
                    using var stream = response.GetResponseStream();
                    if (stream is null) return false;
                    using var reader = new StreamReader(stream, Encoding.UTF8);
                    var lines = await reader.ReadToEndAsync();
                    var status = lines.Trim().SplitLine().Where(line => !line.StartsWith("-")).Any(line => line.EndsWith(dirname));

                    request.Abort();
                    return status;
                }
                catch (WebException)
                {
                    return false;
                }
            }

            #region 检查FTP连接

            /// <summary>
            /// 判断FTP连接
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns>Ture:有效 False:无效</returns>
            public static bool Check(string uri, string user, string pass, ushort timeout = Net.TIMEOUT)
            {
                try
                {
                    var remote = FixShortcuts(uri);
                    var startIndex = remote.LastIndexOf('/') + 1;
                    if (startIndex <= "ftp://".Length) return false;
                    var dirname = remote.Substring(startIndex, remote.Length - startIndex).Trim(' ', '/', '\\');
                    var parent = remote.Substring(0, startIndex);
                    var request = CreateRequestDir(parent, user, pass, "NLST", timeout);

                    using var response = (FtpWebResponse)request.GetResponse();
                    using var stream = response.GetResponseStream();
                    if (stream is null) return false;
                    using var reader = new StreamReader(stream);
                    var lines = reader.ReadToEnd();
                    var status = lines.Trim().Split().Any(line => line.StartsWith(dirname));
                    request.Abort();
                    return status;
                }
                catch (WebException ex)
                {
#if DEBUG
                    Console.WriteLine("{0} {2}:{3}@{1} ->\n {4}",
                                      nameof(Check), ex.Response.ResponseUri, user, pass, ex.Message);
#endif
                    return false;
                }
            }

            /// <summary>
            /// 判断FTP连接
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <returns>Ture:有效 False:无效</returns>
            public static async Task<bool> CheckAsync(string uri, string user, string pass,
                                                      ushort timeout = Net.TIMEOUT)
            {
                try
                {
                    var remote = FixShortcuts(uri);
                    var startIndex = remote.LastIndexOf('/') + 1;
                    if (startIndex <= "ftp://".Length) return false;
                    var dirname = remote.Substring(startIndex, remote.Length - startIndex).Trim(' ', '/', '\\');
                    var parent = remote.Substring(0, startIndex);
                    var request = CreateRequestDir(parent, user, pass, "NLST", timeout);

                    using var response = (FtpWebResponse)await request.GetResponseAsync();
                    using var stream = response.GetResponseStream();
                    if (stream is null) return false;
                    using var reader = new StreamReader(stream);
                    var lines = await reader.ReadToEndAsync();
                    var status = lines.Trim().Split().Any(line => line.StartsWith(dirname));

                    request.Abort();
                    return status;
                }
                catch (WebException ex)
                {
#if DEBUG
                    Console.WriteLine("{0} {2}:{3}@{1} ->\n {4}",
                                      nameof(CheckAsync), ex.Response.ResponseUri, user, pass, ex.Message);
#endif
                    return false;
                }
            }

            #endregion
        }

        #endregion
    }
}