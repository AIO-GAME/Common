using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AIO;

public partial class AHelper
{
    public partial class HTTP
    {
        private class HttpDownloadOperation : AOperation
        {
            private string Remote { get; }
            private string LocalPath { get; }
            private bool IsOverWrite { get; }
            private ushort Timeout { get; }
            private int BufferSize { get; }
            private FileStream outputStream;
            private HttpWebRequest request;

            public HttpDownloadOperation(
                string remoteUrl,
                string localPath,
                bool isOverWrite = false,
                ushort timeout = Net.TIMEOUT,
                int bufferSize = Net.BUFFER_SIZE)
            {
                Remote = remoteUrl.Replace("\\", "/");
                LocalPath = localPath;
                IsOverWrite = isOverWrite;
                Timeout = timeout;
                BufferSize = bufferSize;
            }

            protected override void OnBegin()
            {
                request = (HttpWebRequest)WebRequest.Create(new Uri(Remote));
                request.Timeout = Timeout;
            }

            protected override void OnPause()
            {
            }

            protected override void OnResume()
            {
            }

            protected override async Task OnWaitAsync()
            {
                outputStream = await Net.AddFileHeaderAsync(LocalPath, () => GetMD5Async(Remote), IsOverWrite,
                    cancellationToken);
                if (outputStream is null)
                {
                    State = EProgressState.Finish;
                    return;
                }

                if (!outputStream.CanWrite)
                {
                    State = EProgressState.Finish;
                    StartValue += IO.GetFileLength(LocalPath);
                    return;
                }

                var temp = outputStream.Position - Net.CODE.Length;
                if (temp > 0) request.AddRange(temp);

                HttpWebResponse response = null;
                Stream responseStream = null;
                try
                {
                    response = (HttpWebResponse)await request.GetResponseAsync();
                    while (State == EProgressState.Pause) await Task.Delay(100, cancellationToken);

                    TotalValue += response.ContentLength;
                    CurrentInfo = Remote;
                    StartValue += temp;

                    responseStream = response.GetResponseStream();
                    if (responseStream is null) throw new NetGetResponseStream("HTTP", response);

                    var buffer = new byte[BufferSize];
                    var readCount = await responseStream.ReadAsync(buffer, 0, BufferSize, cancellationToken);
                    while (readCount > 0)
                    {
                        if (State == EProgressState.Running)
                        {
                            await outputStream.WriteAsync(buffer, 0, readCount, cancellationToken);
                            CurrentValue += readCount;
                            readCount = await responseStream.ReadAsync(buffer, 0, BufferSize,
                                cancellationToken);
                        }
                        else
                        {
                            await Task.Delay(100, cancellationToken);
                        }
                    }

                    await Net.RemoveFileHeaderAsync(outputStream, cancellationToken: cancellationToken);
                    responseStream.Close();
                    outputStream.Close();
                    response.Close();
                    State = EProgressState.Finish;
                }
                catch (WebException ex)
                {
                    responseStream?.Close();
                    outputStream.Close();
                    response?.Close();
                    State = EProgressState.Fail;
                    Event.OnError?.Invoke(ex);
                }
            }

            protected override void OnWait()
            {
                if (State != EProgressState.Running) return;
                outputStream = Net.AddFileHeader(LocalPath, () => GetMD5(Remote), IsOverWrite);
                if (outputStream is null)
                {
                    State = EProgressState.Finish;
                    return;
                }

                if (!outputStream.CanWrite)
                {
                    State = EProgressState.Finish;
                    StartValue += IO.GetFileLength(LocalPath);
                    return;
                }

                var temp = outputStream.Position - Net.CODE.Length;
                if (temp > 0) request.AddRange(temp);

                HttpWebResponse response = null;
                Stream responseStream = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    while (State == EProgressState.Pause) Thread.Sleep(100);
                    TotalValue = response.ContentLength;
                    CurrentInfo = Remote;

                    responseStream = response.GetResponseStream();
                    StartValue += temp;

                    if (responseStream is null) throw new NetGetResponseStream("HTTP", response);
                    var buffer = new byte[BufferSize];

                    var readCount = responseStream.Read(buffer, 0, BufferSize);
                    while (readCount > 0)
                    {
                        if (State == EProgressState.Running)
                        {
                            outputStream.Write(buffer, 0, readCount);
                            CurrentValue += readCount;
                            readCount = responseStream.Read(buffer, 0, BufferSize);
                        }
                        else Thread.Sleep(100);
                    }

                    Net.RemoveFileHeader(outputStream);
                    responseStream.Close();
                    outputStream.Flush(true);
                    outputStream.Close();
                    response.Close();
                    State = EProgressState.Finish;
                }
                catch (WebException ex)
                {
                    responseStream?.Close();
                    outputStream.Close();
                    response?.Close();
                    State = EProgressState.Fail;
                    Event.OnError?.Invoke(ex);
                }
            }

            protected override void OnDispose()
            {
                request.Abort();
            }
        }

        /// <summary>
        /// HTTP 下载文件
        /// </summary>
        /// <param name="remoteUrl">远端路径</param>
        /// <param name="localPath">保存路径</param>
        /// <param name="isOverWrite">覆盖</param>
        /// <param name="timeout">超时</param>
        /// <param name="bufferSize">容量</param>
        /// <exception cref="Exception">异常</exception>
        public static IProgressOperation Download(
            string remoteUrl,
            string localPath,
            bool isOverWrite = false,
            ushort timeout = Net.TIMEOUT,
            int bufferSize = Net.BUFFER_SIZE
        )
        {
            return new HttpDownloadOperation(remoteUrl, localPath, isOverWrite, timeout, bufferSize);
        }
    }
}