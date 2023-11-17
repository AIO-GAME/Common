using System;
using System.Net;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class Net
    {
        public partial class FTP
        {
            /// <summary>
            /// 创建文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="timeout">超时</param>
            public static bool CreateDir(string uri, string username, string password,
                ushort timeout = TIMEOUT)
            {
                if (Check(uri, username, password, timeout)) return true;
                try
                {
                    var request = CreateRequest(uri, username, password, WebRequestMethods.Ftp.MakeDirectory, timeout);
                    using var response = (FtpWebResponse)request.GetResponse();
                    var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                    response.Close();
                    request.Abort();
                    return status;
                }
                catch (WebException ex)
                {
#if DEBUG
                    Console.WriteLine("{0} {2}:{3}@{1} ->\n {4}",
                        nameof(CreateDir), ex.Response.ResponseUri, username, password, ex.Message);
#endif
                    return false;
                }
            }

            /// <summary>
            /// 创建文件夹
            /// </summary>
            /// <param name="remote">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="timeout">超时</param>
            public static async Task<bool> CreateDirAsync(string remote, string username, string password,
                ushort timeout = TIMEOUT)
            {
                if (await CheckAsync(remote, username, password, timeout)) return true;
                try
                {
                    var request = CreateRequest(remote, username, password, "MKD", timeout);
                    using var response = (FtpWebResponse)await request.GetResponseAsync();
                    var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                    response.Close();
                    request.Abort();
                    return status;
                }
                catch (WebException ex)
                {
#if DEBUG
                    Console.WriteLine("{0} {2}:{3}@{1} ->\n {4}",
                        nameof(CreateDirAsync), ex.Response.ResponseUri, username, password, ex.Message);
#endif
                    return false;
                }
            }

            /// <summary>
            /// 重命名文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="currentName">当前名称</param>
            /// <param name="newName">新名称</param>
            /// <param name="timeout">超时</param>
            public static bool ReName(string uri, string username, string password, string currentName,
                string newName, ushort timeout = TIMEOUT)
            {
                try
                {
                    var remote = string.IsNullOrEmpty(currentName) ? uri : string.Concat(uri, '/', currentName);
                    var request = CreateRequest(remote, username, password, "RENAME", timeout);
                    request.RenameTo = newName;
                    var response = (FtpWebResponse)request.GetResponse();
                    var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                    response.Close();
                    return status;
                }
                catch (WebException ex)
                {
#if DEBUG
                    Console.WriteLine("{0} {2}:{3}@{1} ->\n {4}",
                        nameof(ReName), ex.Response.ResponseUri, username, password, ex.Message);
#endif
                    return false;
                }
            }

            /// <summary>
            /// 重命名文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="currentName">当前名称</param>
            /// <param name="newName">新名称</param>
            /// <param name="timeout">超时</param>
            public static async Task<bool> ReNameAsync(string uri, string username, string password, string currentName,
                string newName, ushort timeout = TIMEOUT)
            {
                try
                {
                    var remote = string.IsNullOrEmpty(currentName) ? uri : string.Concat(uri, '/', currentName);
                    var request = CreateRequest(remote, username, password, "RENAME", timeout);
                    request.RenameTo = newName;
                    using var response = (FtpWebResponse)await request.GetResponseAsync();
                    var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                    response.Close();
                    request.Abort();
                    return status;
                }
                catch (WebException ex)
                {
#if DEBUG
                    Console.WriteLine("{0} {2}:{3}@{1} ->\n {4}",
                        nameof(ReNameAsync), ex.Response.ResponseUri, username, password, ex.Message);
#endif
                    return false;
                }
            }


            /// <summary>
            /// 移动文件
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="currentName">当前名称</param>
            /// <param name="newName">新名称</param>
            /// <param name="timeout">超时</param>
            public static bool Move(string uri, string username, string password, string currentName, string newName,
                ushort timeout = TIMEOUT)
            {
                return ReName(uri, username, password, currentName, newName, timeout);
            }

            /// <summary>
            /// 移动文件
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="currentName">当前名称</param>
            /// <param name="newName">新名称</param>
            /// <param name="timeout">超时</param>
            public static Task<bool> MoveAsync(string uri, string username, string password, string currentName,
                string newName, ushort timeout = TIMEOUT)
            {
                return ReNameAsync(uri, username, password, currentName, newName, timeout);
            }
        }
    }
}