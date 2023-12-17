/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-17
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

public partial class AHelper
{
    /// <summary>
    /// 网络 工具类
    /// </summary>
    public partial class Net
    {
        public partial class FTP
        {
            private class FTPDownloadDirOperation : AOperation
            {
                private string uri;
                private string username;
                private string password;
                private string localPath;
                private SearchOption option = SearchOption.AllDirectories;
                private string pattern = "*";
                private bool overwrite = false;
                private ushort timeout = TIMEOUT;
                private int bufferSize = BUFFER_SIZE;

                /// <param name="uri">路径</param>
                /// <param name="username">用户名</param>
                /// <param name="password">密码</param>
                /// <param name="localPath">本地文件路径</param>
                /// <param name="pattern">搜索过滤</param>
                /// <param name="option">搜索模式</param>
                /// <param name="overwrite">是否覆盖</param>
                /// <param name="timeout">超时</param>
                /// <param name="bufferSize">下载缓存大小</param>
                public FTPDownloadDirOperation(
                    string uri,
                    string username,
                    string password,
                    string localPath,
                    SearchOption option = SearchOption.AllDirectories,
                    string pattern = "*",
                    bool overwrite = false,
                    ushort timeout = TIMEOUT,
                    int bufferSize = BUFFER_SIZE)
                {
                    this.uri = uri;
                    this.username = username;
                    this.password = password;
                    this.localPath = localPath;
                    this.option = option;
                    this.pattern = pattern;
                    this.overwrite = overwrite;
                    this.timeout = timeout;
                    this.bufferSize = bufferSize;
                }

                protected override void OnWait()
                {
                    var remoteList = GetDownloadList(uri, username, password, option, pattern, timeout);
                    var dict = new Dictionary<string, long>();
                    foreach (var remoteAbs in remoteList)
                    {
                        if (progress.State == EProgressState.Cancel)
                        {
                            progress.OnError?.Invoke(new Exception("User Cancel"));
                            return;
                        }

                        var remote = string.Concat(uri, '/', remoteAbs);
                        var fileSize = GetFileSize(remote, username, password, timeout);
                        if (fileSize <= 0) continue;
                        progress.Total += fileSize;
                        dict.Add(remoteAbs, fileSize);
                    }

                    foreach (var item in dict)
                    {
                        if (progress.State == EProgressState.Cancel)
                        {
                            progress.OnError?.Invoke(new Exception("User Cancel"));
                            return;
                        }

                        try
                        {
                            var remote = string.Concat(uri, '/', item.Key);
                            var request = CreateRequestDir(remote, username, password, "RETR", timeout);

                            progress.CurrentInfo = request.RequestUri.AbsoluteUri;

                            using var response = request.GetResponse();
                            using (var responseStream = response.GetResponseStream())
                            {
                                if (responseStream is null) throw new Exception("FTP Stream is Null");

                                var local = Path.Combine(localPath, item.Key).Replace('\\', '/');
                                var localDir = new DirectoryInfo(local.Substring(0, local.LastIndexOf('/')));

                                if (!localDir.Exists) Directory.CreateDirectory(localDir.FullName);
                                else if (File.Exists(local) && !overwrite) continue;

                                using (var outputStream =
                                       new FileStream(local, FileMode.OpenOrCreate, FileAccess.Write))
                                {
                                    var contentLen = 0L;
                                    while (outputStream.Position < item.Value)
                                    {
                                        responseStream.CopyTo(outputStream, bufferSize);
                                        contentLen = outputStream.Position - contentLen;
                                        progress.Current += contentLen;
                                    }

                                    outputStream.Flush();
                                }
                            }

                            request.Abort();
                        }
                        catch (WebException ex)
                        {
                            File.Delete(localPath);
                            progress.OnError?.Invoke(ex);
                            progress.State = EProgressState.Fail;
                            return;
                        }
                    }

                    progress.State = EProgressState.Finish;
                }

                protected override async Task OnWaitAsync()
                {
                    var remoteList =
                        await GetDownloadListAsync(uri, username, password, option, pattern, timeout);
                    var dict = new Dictionary<string, long>();
                    foreach (var remoteAbs in remoteList)
                    {
                        if (progress.State == EProgressState.Cancel)
                        {
                            progress.OnError?.Invoke(new Exception("User Cancel"));
                            return;
                        }

                        var remote = string.Concat(uri, '/', remoteAbs);
                        var fileSize = await GetFileSizeAsync(remote, username, password, timeout);
                        if (fileSize <= 0) continue;
                        progress.Total += fileSize;
                        dict.Add(remoteAbs, fileSize);
                    }

                    foreach (var item in dict)
                    {
                        if (progress.State == EProgressState.Cancel)
                        {
                            progress.OnError?.Invoke(new Exception("User Cancel"));
                            return;
                        }

                        try
                        {
                            var remote = string.Concat(uri, '/', item.Key);
                            var request = CreateRequestDir(remote, username, password, "RETR", timeout);
                            progress.CurrentInfo = request.RequestUri.AbsoluteUri;


                            using var response = await request.GetResponseAsync();
                            using (var responseStream = response.GetResponseStream())
                            {
                                if (responseStream is null) throw new Exception("FTP Stream is Null");

                                var local = Path.Combine(localPath, item.Key).Replace('\\', '/');
                                var localDir = new DirectoryInfo(local.Substring(0, local.LastIndexOf('/')));
                                if (!localDir.Exists) Directory.CreateDirectory(localDir.FullName);
                                else if (File.Exists(local) && !overwrite) continue;

                                using (var outputStream =
                                       new FileStream(local, FileMode.OpenOrCreate, FileAccess.Write))
                                {
                                    var contentLen = 0L;
                                    while (outputStream.Position < item.Value)
                                    {
                                        await responseStream.CopyToAsync(outputStream, bufferSize);
                                        contentLen = outputStream.Position - contentLen;
                                        progress.Current += contentLen;
                                    }

                                    await outputStream.FlushAsync();
                                }
                            }

                            request.Abort();
                        }
                        catch (WebException ex)
                        {
                            File.Delete(localPath);
                            progress.State = EProgressState.Fail;
                            progress.OnError?.Invoke(ex);
                            return;
                        }
                    }

                    progress.State = EProgressState.Finish;
                }
            }

            /// <summary>
            /// FTP下载文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="username">用户名</param>
            /// <param name="password">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="pattern">搜索过滤</param>
            /// <param name="option">搜索模式</param>
            /// <param name="overwrite">是否覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">下载缓存大小</param>
            public static IProgressOperation DownloadDir(
                string uri,
                string username,
                string password,
                string localPath,
                SearchOption option = SearchOption.AllDirectories,
                string pattern = "*",
                bool overwrite = false,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                return new FTPDownloadDirOperation(uri, username, password, localPath, option, pattern, overwrite,
                    timeout, bufferSize);
            }

            /// <summary>
            /// 获取文件下载列表
            /// </summary>
            private static async Task<IEnumerable<string>> GetDownloadListAsync(string uri, string user,
                string pass,
                SearchOption option = SearchOption.AllDirectories,
                string pattern = "*",
                ushort timeout = TIMEOUT, string abs = null
            )
            {
                var remoteList =
                    (await GetRemoteListAsync(uri, user, pass, AHandle.FTP.ListType.File, pattern, timeout))
                    .Select(file => string.Concat(abs, '/', Path.GetFileName(file)).Trim('/'))
                    .ToList();

                if (option != SearchOption.AllDirectories) return remoteList;

                foreach (var absPath in (await GetRemoteListAsync(
                             uri, user, pass, AHandle.FTP.ListType.Directory, pattern, timeout))
                         .Select(Path.GetFileName))
                {
                    var collection = GetDownloadList(
                        string.Concat(uri, '/', absPath), user, pass, option, pattern, timeout,
                        string.Concat(abs, '/', absPath).Trim('/'));
                    remoteList.AddRange(collection);
                }

                return remoteList;
            }
        }
    }
}