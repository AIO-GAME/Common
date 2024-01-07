/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-17
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AIO
{
    public partial class AHelper
    {
        public partial class FTP
        {
            private class FTPUploadDirOperation : AOperation
            {
                private string uri { get; }
                private string user { get; }
                private string pass { get; }
                private string localPath { get; }
                private SearchOption option { get; }
                private string pattern { get; }
                private ushort timeout { get; }
                private int bufferSize { get; }

                protected override void OnDispose()
                {
                    info = null;
                }

                /// <summary>
                /// FTP上传文件夹
                /// </summary>
                /// <param name="uri">路径</param>
                /// <param name="user">用户名</param>
                /// <param name="pass">密码</param>
                /// <param name="localPath">本地文件路径</param>
                /// <param name="option">搜索模式</param>
                /// <param name="pattern">匹配模式</param>
                /// <param name="timeout">超时</param>
                /// <param name="bufferSize">缓存大小</param>
                public FTPUploadDirOperation(
                    string uri,
                    string user,
                    string pass,
                    string localPath,
                    SearchOption option = SearchOption.AllDirectories,
                    string pattern = "*",
                    ushort timeout = Net.TIMEOUT,
                    int bufferSize = Net.BUFFER_SIZE)
                {
                    this.uri = uri.Replace('\\', '/');
                    this.user = user;
                    this.pass = pass;
                    this.localPath = localPath;
                    this.option = option;
                    this.pattern = pattern;
                    this.timeout = timeout;
                    this.bufferSize = bufferSize;
                }

                protected override void OnWait()
                {
                    foreach (var dicInfo in info.GetDirectories(pattern, option))
                    {
                        var relativePath = dicInfo.FullName.Replace(info.FullName, "");
                        var remote = string.Concat(uri, '/', relativePath);
                        if (CheckDir(remote, user, pass, timeout)) continue;
                        if (CreateDir(remote, user, pass, timeout)) continue;
                        Event.OnError?.Invoke(new WebException($"FTP Upload Folder Create Dir Error -> {remote}"));
                        return;
                    }

                    var fileInfos = info.GetFiles(pattern, option);
                    foreach (var fileInfo in fileInfos) TotalValue += fileInfo.Length;
                    var memoryBuff = new byte[bufferSize];
                    foreach (var fileInfo in fileInfos)
                    {
                        while (State == EProgressState.Pause) Thread.Sleep(100);

                        var relativePath = fileInfo.FullName.Replace(info.FullName, "").Trim('/', '\\');
                        var remote = string.Concat(uri, '/', relativePath);
                        var request = CreateRequestFile(remote, user, pass, "STOR", timeout);
                        request.ContentLength = fileInfo.Length;
                        CurrentInfo = relativePath;

                        try
                        {
                            using (var requestStream = request.GetRequestStream())
                            {
                                using (var fileStream = fileInfo.OpenRead())
                                {
                                    var contentLen = fileStream.Read(memoryBuff, 0, bufferSize);
                                    while (contentLen > 0)
                                    {
                                        switch (State)
                                        {
                                            case EProgressState.Cancel:
                                                DeleteFile(remote, user, pass, timeout);
                                                Event.OnError?.Invoke(new TaskCanceledException());
                                                return;
                                            case EProgressState.Running:
                                                CurrentValue += contentLen;
                                                requestStream.Write(memoryBuff, 0, contentLen);
                                                contentLen = fileStream.Read(memoryBuff, 0, bufferSize);
                                                break;
                                            default:
                                                Thread.Sleep(100);
                                                break;
                                        }
                                    }
                                }

                                requestStream.Flush();
                            }


                            request.Abort();
                            State = EProgressState.Finish;
                        }
                        catch (WebException ex)
                        {
                            request.Abort();
                            DeleteFile(remote, user, pass, timeout);
                            Event.OnError?.Invoke(ex);
                            State = EProgressState.Fail;
                            return;
                        }
                    }
                }

                private DirectoryInfo info;

                protected override void OnBegin()
                {
                    info = new DirectoryInfo(localPath);
                    if (info.Exists) return;
                    State = EProgressState.Fail;
                    Event.OnError?.Invoke(
                        new DirectoryNotFoundException($"ftp upload folder : target file not found {localPath}"));
                }

                protected override async Task OnWaitAsync()
                {
                    foreach (var directoryInfo in info.GetDirectories(pattern, option))
                    {
                        var relativePath = directoryInfo.FullName.Replace(info.FullName, "");
                        var remote = string.Concat(uri, '/', relativePath);
                        CurrentInfo = $"Check Dir -> {remote}";
                        if (await CheckDirAsync(remote, user, pass, timeout)) continue;
                        CurrentInfo = $"Create Dir -> {remote}";
                        if (await CreateDirAsync(remote, user, pass, timeout)) continue;
                        State = EProgressState.Fail;
                        Event.OnError?.Invoke(new WebException($"FTP Upload Folder Create Dir Error -> {remote}"));
                        return;
                    }

                    var fileInfos = info.GetFiles(pattern, option);
                    foreach (var fileInfo in fileInfos) TotalValue += fileInfo.Length;
                    var memoryBuff = new byte[bufferSize];
                    foreach (var fileInfo in fileInfos)
                    {
                        if (State == EProgressState.Cancel)
                        {
                            Event.OnError?.Invoke(new TaskCanceledException());
                            return;
                        }

                        while (State == EProgressState.Pause) await Task.Delay(100, cancellationToken);

                        var relativePath = fileInfo.FullName.Replace(info.FullName, "").Trim('/', '\\');
                        var remote = string.Concat(uri, '/', relativePath);
                        var request = CreateRequestFile(remote, user, pass, "STOR", -1);
                        request.ContentLength = fileInfo.Length;
                        CurrentInfo = relativePath;
                        try
                        {
                            using (var requestStream = await request.GetRequestStreamAsync())
                            {
                                using (var fileStream = fileInfo.OpenRead())
                                {
                                    var contentLen =
                                        await fileStream.ReadAsync(memoryBuff, 0, bufferSize, cancellationToken);
                                    while (contentLen > 0)
                                    {
                                        switch (State)
                                        {
                                            case EProgressState.Cancel:
                                                await DeleteFileAsync(remote, user, pass, timeout);
                                                Event.OnError?.Invoke(new TaskCanceledException());
                                                return;
                                            case EProgressState.Running:
                                                CurrentValue += contentLen;
                                                await requestStream.WriteAsync(memoryBuff, 0, contentLen,
                                                    cancellationToken);
                                                contentLen = await fileStream.ReadAsync(memoryBuff, 0, bufferSize,
                                                    cancellationToken);
                                                break;
                                            default:
                                                await Task.Delay(100, cancellationToken);
                                                break;
                                        }
                                    }
                                }

                                await requestStream.FlushAsync(cancellationToken);
                            }

                            request.Abort();
                        }
                        catch (WebException ex)
                        {
                            await DeleteFileAsync(remote, user, pass, timeout);
                            State = EProgressState.Fail;
                            Event.OnError?.Invoke(ex);
                            request.Abort();
                            return;
                        }
                    }

                    State = EProgressState.Finish;
                }
            }

            /// <summary>
            /// FTP上传文件夹
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="option">搜索模式</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public static IProgressOperation UploadDir(
                string uri,
                string user,
                string pass,
                string localPath,
                SearchOption option = SearchOption.AllDirectories,
                string pattern = "*",
                ushort timeout = Net.TIMEOUT,
                int bufferSize = Net.BUFFER_SIZE
            )
            {
                return new FTPUploadDirOperation(uri, user, pass, localPath, option, pattern, timeout, bufferSize);
            }
        }
    }
}