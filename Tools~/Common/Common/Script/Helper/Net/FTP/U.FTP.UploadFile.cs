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
            private ushort timeout { get; }
            private int bufferSize { get; }

            private Stream stream;

            /// <summary>
            /// FTP上传文件
            /// </summary>
            /// <param name="uri">路径</param>
            /// <param name="user">用户名</param>
            /// <param name="pass">密码</param>
            /// <param name="stream">数据流</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">缓存大小</param>
            public FTPUploadFileOperation(
                string uri,
                string user,
                string pass,
                Stream stream,
                ushort timeout = Net.TIMEOUT,
                int bufferSize = Net.BUFFER_SIZE)
            {
                this.uri = uri.Replace('\\', '/');
                this.user = user;
                this.pass = pass;
                this.stream = stream;
                this.timeout = timeout;
                this.bufferSize = bufferSize;
            }

            protected override void OnBegin()
            {
                if (stream is null)
                {
                    State = EProgressState.Fail;
                    Event.OnError?.Invoke(
                        new FileNotFoundException($"FTP Upload : input stream is null!"));
                    return;
                }

                State = EProgressState.Running;
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
                    request.ContentLength = stream.Length;
                    TotalValue = stream.Length;
                    CurrentInfo = uri;

                    using (var requestStream = request.GetRequestStream())
                    {
                        var memoryBuff = new byte[bufferSize];
                        var contentLen = stream.Read(memoryBuff, 0, bufferSize);
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
                                    contentLen = stream.Read(memoryBuff, 0, bufferSize);
                                    break;
                                default:
                                    Thread.Sleep(100);
                                    break;
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
                    request.ContentLength = stream.Length;
                    TotalValue = stream.Length;
                    CurrentInfo = uri;
                    using (var requestStream = await request.GetRequestStreamAsync())
                    {
                        var memoryBuff = new byte[bufferSize];
                        var contentLen =
                            await stream.ReadAsync(memoryBuff, 0, bufferSize, cancellationToken);
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
                                    contentLen = await stream.ReadAsync(memoryBuff, 0, bufferSize,
                                        cancellationToken);
                                    break;
                                default:
                                    await Task.Delay(100, cancellationToken);
                                    break;
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
                stream = null;
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
            return new FTPUploadFileOperation(uri, user, pass, File.OpenRead(localPath), timeout, bufferSize);
        }


        /// <summary>
        /// FTP上传文件
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <param name="stream">数据流</param>
        /// <param name="timeout">超时</param>
        /// <param name="bufferSize">缓存大小</param>
        public static IProgressOperation UploadFile(
            string uri,
            string user,
            string pass,
            byte[] stream,
            ushort timeout = Net.TIMEOUT,
            int bufferSize = Net.BUFFER_SIZE
        )
        {
            var data = new MemoryStream(stream);
            data.Seek(0, SeekOrigin.Begin);
            return new FTPUploadFileOperation(uri, user, pass, data, timeout, bufferSize);
        }

        /// <summary>
        /// FTP上传文件
        /// </summary>
        /// <param name="uri">路径</param>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <param name="stream">数据流</param>
        /// <param name="timeout">超时</param>
        /// <param name="bufferSize">缓存大小</param>
        public static IProgressOperation UploadFile(
            string uri,
            string user,
            string pass,
            Stream stream,
            ushort timeout = Net.TIMEOUT,
            int bufferSize = Net.BUFFER_SIZE
        )
        {
            return new FTPUploadFileOperation(uri, user, pass, stream, timeout, bufferSize);
        }
    }
}