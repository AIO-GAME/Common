using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class FTP
    {
        private class FTPUploadFileOperation : AOperation
        {
            private string uri { get; }
            private string user { get; }
            private string pass { get; }
            private string localPath { get; }
            private ushort timeout { get; }
            private int bufferSize { get; }
            private FileInfo info;

            /// <summary>
            /// FTP上传文件
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="localPath">本地文件路径</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public FTPUploadFileOperation(
                string uri,
                string user,
                string pass,
                string localPath,
                ushort timeout = Net.TIMEOUT,
                int bufferSize = Net.BUFFER_SIZE)
            {
                this.uri = uri;
                this.user = user;
                this.pass = pass;
                this.localPath = localPath;
                this.timeout = timeout;
                this.bufferSize = bufferSize;
            }

            protected override void OnBegin()
            {
                info = new FileInfo(localPath);
                if (info.Exists) return;
                State = EProgressState.Fail;
                Event.OnError?.Invoke(
                    new FileNotFoundException($"FTP Upload : Target File Not Found {localPath}"));
            }

            protected override void OnWait()
            {
                try
                {
                    var startIndex = uri.LastIndexOf('/') + 1;
                    var parent = uri.Substring(0, startIndex);
                    if (!CheckDir(parent, user, pass, timeout))
                    {
                        if (!CreateDir(parent, user, pass, timeout))
                        {
                            State = EProgressState.Fail;
                            Event.OnError?.Invoke(
                                new WebException($"FTP Upload Folder Create Dir Error -> {uri}"));
                            return;
                        }
                    }

                    var request = CreateRequestFile(uri, user, pass, "STOR", timeout);
                    request.ContentLength = info.Length;
                    TotalValue = info.Length;
                    CurrentInfo = info.FullName;

                    using (var requestStream = request.GetRequestStream())
                    {
                        using (var fileStream = info.OpenRead())
                        {
                            var memoryBuff = new byte[bufferSize];
                            var contentLen = fileStream.Read(memoryBuff, 0, bufferSize);
                            while (contentLen > 0)
                            {
                                switch (State)
                                {
                                    case EProgressState.Cancel:
                                        DeleteFile(uri, user, pass, timeout);
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
                    Event.OnError?.Invoke(ex);
                    State = EProgressState.Fail;
                }
            }

            protected override async Task OnWaitAsync()
            {
                try
                {
                    var startIndex = uri.LastIndexOf('/') + 1;
                    var parent = uri.Substring(0, startIndex);
                    if (!await CheckDirAsync(parent, user, pass, timeout))
                    {
                        if (!await CreateDirAsync(parent, user, pass, timeout))
                        {
                            State = EProgressState.Fail;
                            Event.OnError?.Invoke(
                                new WebException($"FTP Upload Folder Create Dir Error -> {uri}"));
                            return;
                        }
                    }

                    var request = CreateRequestFile(uri, user, pass, "STOR", timeout);
                    request.ContentLength = info.Length;
                    TotalValue = info.Length;
                    CurrentInfo = info.FullName;
                    using (var requestStream = await request.GetRequestStreamAsync())
                    {
                        using (var fileStream = info.OpenRead())
                        {
                            var memoryBuff = new byte[bufferSize];
                            var contentLen =
                                await fileStream.ReadAsync(memoryBuff, 0, bufferSize, cancellationToken);
                            while (contentLen > 0)
                            {
                                switch (State)
                                {
                                    case EProgressState.Cancel:
                                        await DeleteFileAsync(uri, user, pass, timeout);
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
                    State = EProgressState.Finish;
                }
                catch (WebException ex)
                {
                    await DeleteFileAsync(uri, user, pass, timeout);
                    Event.OnError?.Invoke(ex);
                    State = EProgressState.Fail;
                }
            }

            protected override void OnDispose()
            {
                info = null;
            }
        }

        /// <summary>
        /// FTP上传文件
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <param name="localPath">本地文件路径</param>
        /// <param name="timeout">超时</param>
        /// <param name="bufferSize">缓存大小</param>
        public static IProgressOperation UploadFile(
            string uri,
            string user,
            string pass,
            string localPath,
            ushort timeout = Net.TIMEOUT,
            int bufferSize = Net.BUFFER_SIZE
        )
        {
            return new FTPUploadFileOperation(uri, user, pass, localPath, timeout, bufferSize);
        }
    }
}