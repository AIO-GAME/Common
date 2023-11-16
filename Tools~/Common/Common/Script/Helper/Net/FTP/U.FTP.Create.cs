using System;
using System.IO;
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
            /// <param name="remotePath">文件夹路径</param>
            public static bool CreateDir(string uri, string username, string password, string remotePath,
                ushort timeout = TIMEOUT)
            {
                try
                {
                    var remote = string.IsNullOrEmpty(remotePath) ? uri : string.Concat(uri, '/', remotePath);
                    var ftp = (FtpWebRequest)WebRequest.Create(new Uri(remote));
                    ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
                    ftp.Timeout = timeout;
                    ftp.Credentials = new NetworkCredential(username, password);
                    var response = (FtpWebResponse)ftp.GetResponse();
                    var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                    response.Close();
                    return status;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

            /// <summary>
            /// 创建文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="timeout">超时</param>
            /// <param name="remotePath">文件夹路径</param>
            public static async Task<bool> CreateDirAsync(string uri, string username, string password,
                string remotePath, ushort timeout = TIMEOUT)
            {
                try
                {
                    var remote = string.IsNullOrEmpty(remotePath) ? uri : string.Concat(uri, '/', remotePath);
                    var ftp = (FtpWebRequest)WebRequest.Create(new Uri(remote));
                    ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
                    ftp.Timeout = timeout;
                    ftp.Credentials = new NetworkCredential(username, password);
                    var response = (FtpWebResponse)await ftp.GetResponseAsync();
                    var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                    response.Close();
                    return status;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                    var ftp = (FtpWebRequest)WebRequest.Create(new Uri(remote));
                    ftp.Method = WebRequestMethods.Ftp.Rename;
                    ftp.RenameTo = newName;
                    ftp.Timeout = timeout;
                    ftp.Credentials = new NetworkCredential(username, password);
                    var response = (FtpWebResponse)ftp.GetResponse();
                    var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                    response.Close();
                    return status;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                    var ftp = (FtpWebRequest)WebRequest.Create(new Uri(remote));
                    ftp.Method = WebRequestMethods.Ftp.Rename;
                    ftp.RenameTo = newName;
                    ftp.Timeout = timeout;
                    ftp.Credentials = new NetworkCredential(username, password);
                    var response = (FtpWebResponse)await ftp.GetResponseAsync();
                    var status = response.StatusCode == FtpStatusCode.PathnameCreated;
                    response.Close();
                    return status;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}