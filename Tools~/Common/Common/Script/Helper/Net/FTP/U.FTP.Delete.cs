using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class Net
    {
        public partial class FTP
        {
            /// <summary>
            /// FTP指定删除文件
            /// </summary>
            /// <param name="remote">网址</param>
            /// <param name="user">账号</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <exception cref="WebException">异常信息</exception>
            /// <returns>删除返回信息</returns>
            public static bool DeleteFile(string remote, string user, string pass,
                ushort timeout = TIMEOUT)
            {
                try
                {
                    if (!CheckFile(remote, user, pass, timeout)) return true;
                    var request = CreateRequestFile(remote, user, pass, "DELE", timeout);
                    using var response = (FtpWebResponse)request.GetResponse();
                    request.Abort();
                    return true;
                }
                catch (WebException)
                {
                    return false;
                }
            }


            /// <summary>
            /// FTP指定删除文件
            /// </summary>
            /// <param name="remote">网址</param>
            /// <param name="user">账号</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <exception cref="Exception">异常信息</exception>
            /// <returns>删除返回信息</returns>
            public static async Task<bool> DeleteFileAsync(string remote, string user, string pass,
                ushort timeout = TIMEOUT)
            {
                try
                {
                    if (!await CheckFileAsync(remote, user, pass, timeout)) return true;
                    var request = CreateRequestFile(remote, user, pass, "DELE", timeout);
                    using var response = (FtpWebResponse)await request.GetResponseAsync();
                    request.Abort();
                    return true;
                }
                catch (WebException)
                {
                    return false;
                }
            }

            /// <summary>
            /// FTP指定删除文件夹
            /// </summary>
            /// <param name="remote">网址</param>
            /// <param name="user">账号</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <exception cref="WebException">异常信息</exception>
            /// <returns>删除状态</returns>
            public static bool DeleteDir(string remote, string user, string pass,
                ushort timeout = TIMEOUT)
            {
                try
                {
                    if (!CheckDir(remote, user, pass, timeout)) return true;
                    var files = GetRemoteListFile(remote, user, pass, null, timeout);
                    if (files.Any(item => !DeleteFile(string.Concat(remote, '/', Path.GetFileName(item)),
                            user, pass, timeout)))
                        return false;

                    var folders = GetRemoteListDir(remote, user, pass, null, timeout);
                    if (folders.Any(item => !DeleteDir(string.Concat(remote, '/', Path.GetFileName(item)),
                            user, pass, timeout)))
                        return false;

                    var request = CreateRequestDir(remote, user, pass, "RMD", timeout);
                    using var response = (FtpWebResponse)request.GetResponse();
                    request.Abort();
                    return true;
                }
                catch (WebException)
                {
                    return false;
                }
            }

            /// <summary>
            /// FTP指定删除文件夹
            /// </summary>
            /// <param name="remote">网址</param>
            /// <param name="user">账号</param>
            /// <param name="pass">密码</param>
            /// <param name="timeout">超时</param>
            /// <exception cref="WebException">异常信息</exception>
            /// <returns>删除状态</returns>
            public static async Task<bool> DeleteDirAsync(string remote, string user, string pass,
                ushort timeout = TIMEOUT)
            {
                try
                {
                    if (!await CheckDirAsync(remote, user, pass, timeout)) return true;
                    var files = await GetRemoteListFileAsync(remote, user, pass, null, timeout);
                    foreach (var target in files.Select(item => string.Concat(remote, '/', Path.GetFileName(item))))
                    {
                        if (!await DeleteFileAsync(target, user, pass, timeout))
                            return false;
                    }

                    var folders = await GetRemoteListDirAsync(remote, user, pass, null, timeout);
                    foreach (var target in folders.Select(item => string.Concat(remote, '/', Path.GetFileName(item))))
                    {
                        if (!await DeleteDirAsync(target, user, pass, timeout))
                            return false;
                    }

                    var request = CreateRequestDir(remote, user, pass, "RMD", timeout);
                    using var response = (FtpWebResponse)await request.GetResponseAsync();
                    request.Abort();
                    return true;
                }
                catch (WebException)
                {
                    return false;
                }
            }
        }
    }
}