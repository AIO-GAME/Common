#region

using System;
using System.Buffers;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: Http

        public partial class Http
        {
            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="localPath">保存路径</param>
            /// <param name="isOverWrite">覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">容量</param>
            /// <param name="againCount">重试次数</param>
            /// <exception cref="Exception">异常</exception>
            public static Task DownloadAsync(
                string remoteUrl,
                string localPath,
                bool   isOverWrite = false,
                ushort timeout     = Net.TIMEOUT,
                int    bufferSize  = Net.BUFFER_SIZE,
                int    againCount  = 3
            ) => new HttpDownloadOperation(remoteUrl, localPath, isOverWrite, timeout, bufferSize, againCount).Begin().WaitAsync();

            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="localPath">保存路径</param>
            /// <param name="onProgress">进度回调</param>
            /// <param name="isOverWrite">覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">容量</param>
            /// <param name="againCount">重试次数</param>
            /// <exception cref="Exception">异常</exception>
            public static Task DownloadAsync(
                string                remoteUrl,
                string                localPath,
                Action<IProgressInfo> onProgress,
                bool                  isOverWrite = false,
                ushort                timeout     = Net.TIMEOUT,
                int                   bufferSize  = Net.BUFFER_SIZE,
                int                   againCount  = 3
            )
            {
                var operation = new HttpDownloadOperation(remoteUrl, localPath, isOverWrite, timeout, bufferSize, againCount);
                operation.Event.OnProgress = onProgress;
                return operation.Begin().WaitAsync();
            }

            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="localPath">保存路径</param>
            /// <param name="isOverWrite">覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">容量</param>
            /// <param name="againCount">重试次数</param>
            /// <exception cref="Exception">异常</exception>
            public static Task DownloadAsync(
                Uri    remoteUrl,
                string localPath,
                bool   isOverWrite = false,
                ushort timeout     = Net.TIMEOUT,
                int    bufferSize  = Net.BUFFER_SIZE,
                int    againCount  = 3
            ) => new HttpDownloadOperation(remoteUrl.ToString(), localPath, isOverWrite, timeout, bufferSize, againCount).Begin().WaitAsync();

            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="localPath">保存路径</param>
            /// <param name="isOverWrite">覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">容量</param>
            /// <param name="againCount">重试次数</param>
            /// <exception cref="Exception">异常</exception>
            public static void Download(
                string remoteUrl,
                string localPath,
                bool   isOverWrite = false,
                ushort timeout     = Net.TIMEOUT,
                int    bufferSize  = Net.BUFFER_SIZE,
                int    againCount  = 3
            )
            {
                var operation = new HttpDownloadOperation(remoteUrl, localPath, isOverWrite, timeout, bufferSize, againCount);
                operation.Begin().Wait();
            }

            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="localPath">保存路径</param>
            /// <param name="isOverWrite">覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">容量</param>
            /// <param name="againCount">重试次数</param>
            /// <exception cref="Exception">异常</exception>
            public static IProgressOperation DownloadOperation(
                string remoteUrl,
                string localPath,
                bool   isOverWrite = false,
                ushort timeout     = Net.TIMEOUT,
                int    bufferSize  = Net.BUFFER_SIZE,
                int    againCount  = 3
            )
            {
                return new HttpDownloadOperation(remoteUrl, localPath, isOverWrite, timeout, bufferSize, againCount);
            }

            #region Nested type: HttpDownloadOperation

            private class HttpDownloadOperation : AOperation
            {
                private FileStream     outputStream;
                private HttpWebRequest request;

                public HttpDownloadOperation(
                    string remoteUrl,
                    string localPath,
                    bool   isOverWrite = false,
                    ushort timeout     = Net.TIMEOUT,
                    int    bufferSize  = Net.BUFFER_SIZE,
                    int    againCount  = 3
                )
                {
                    Remote      = remoteUrl.Replace("\\", "/");
                    LocalPath   = new FileInfo(localPath);
                    IsOverWrite = isOverWrite;
                    Timeout     = timeout;
                    BufferSize  = bufferSize;
                    AgainCount  = againCount;
                }

                private string   Remote      { get; }
                private FileInfo LocalPath   { get; }
                private bool     IsOverWrite { get; }
                private ushort   Timeout     { get; }
                private int      BufferSize  { get; }
                private int      AgainCount  { get; }

                protected override void OnPause()  { }
                protected override void OnResume() { }

                protected override void OnBegin()
                {
                    request         = (HttpWebRequest)WebRequest.Create(new Uri(Remote));
                    request.Timeout = Timeout;
                }

                protected override async Task OnWaitAsync()
                {
                    outputStream = await Net.AddFileHeaderAsync(LocalPath, () => GetMD5Async(Remote), IsOverWrite, cancellationToken);
                    if (outputStream is null)
                    {
                        State = EProgressState.Finish;
                        return;
                    }


                    if (!outputStream.CanWrite)
                    {
                        State      =  EProgressState.Finish;
                        StartValue += IO.GetFileLength(LocalPath);
                        return;
                    }

                    var             againCount     = 0;
                    HttpWebResponse response       = null;
                    Stream          responseStream = null;
                    CurrentInfo = Remote;
                    var builder = new StringBuilder();
                    again:
                    TotalValue = outputStream.Length;
                    StartValue = outputStream.Position - Net.CODE.Length;
                    if (StartValue > 0) request.AddRange(StartValue);
                    else StartValue = 0;

                    var buffer = ArrayPool<byte>.Shared.Rent(BufferSize);
                    try
                    {
                        response = (HttpWebResponse)await request.GetResponseAsync();
                        while (State == EProgressState.Pause) await Task.Delay(100, cancellationToken);

                        TotalValue += response.ContentLength;
                        var mb = TotalValue.ToConverseStringFileSize();

                        responseStream = response.GetResponseStream();
                        if (responseStream is null) throw new AExpNetGetResponseStream("HTTP", response);
                        var readCount = await responseStream.ReadAsync(buffer, 0, BufferSize, cancellationToken);
                        while (readCount > 0)
                        {
                            againCount = 0;
                            if (State == EProgressState.Running)
                            {
                                await outputStream.WriteAsync(buffer, 0, readCount, cancellationToken);
                                CurrentValue += readCount;
                                readCount    =  await responseStream.ReadAsync(buffer, 0, BufferSize, cancellationToken);
                            }
                            else await Task.Delay(100, cancellationToken);

                            builder.Clear();
                        }

                        await Net.RemoveFileHeaderAsync(outputStream, cancellationToken: cancellationToken);
                        await outputStream.FlushAsync(cancellationToken);
                        responseStream.Close();
                        outputStream.Close();
                        response.Close();
                        State = EProgressState.Finish;
                    }
                    catch (TaskCanceledException tex)
                    {
                        await outputStream.FlushAsync(cancellationToken);
                        responseStream?.Close();
                        outputStream.Close();
                        response?.Close();
                        State = EProgressState.Fail;
                        Event.OnError?.Invoke(tex);
                    }
                    catch (WebException ex)
                    {
                        responseStream?.Close();
                        response?.Close();
                        if (++againCount <= AgainCount)
                        {
                            Console.WriteLine($" - Download failed :  retrying {againCount}/{AgainCount}...");
                            goto again;
                        }

                        await outputStream.FlushAsync(cancellationToken);
                        outputStream.Close();
                        State = EProgressState.Fail;
                        Event.OnError?.Invoke(ex);
                    }

                    ArrayPool<byte>.Shared.Return(buffer);
                    responseStream?.Dispose();
                    outputStream?.Dispose();
                    response?.Dispose();
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
                        State      =  EProgressState.Finish;
                        StartValue += IO.GetFileLength(LocalPath);
                        return;
                    }

                    var temp = outputStream.Position - Net.CODE.Length;
                    if (temp > 0) request.AddRange(temp);

                    HttpWebResponse response       = null;
                    Stream          responseStream = null;
                    try
                    {
                        response = (HttpWebResponse)request.GetResponse();
                        while (State == EProgressState.Pause) Thread.Sleep(100);
                        TotalValue  = response.ContentLength;
                        CurrentInfo = Remote;

                        responseStream =  response.GetResponseStream();
                        StartValue     += temp;

                        if (responseStream is null) throw new AExpNetGetResponseStream("HTTP", response);
                        var buffer = new byte[BufferSize];

                        var readCount = responseStream.Read(buffer, 0, BufferSize);
                        while (readCount > 0)
                        {
                            if (State == EProgressState.Running)
                            {
                                outputStream.Write(buffer, 0, readCount);
                                CurrentValue += readCount;
                                readCount    =  responseStream.Read(buffer, 0, BufferSize);
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

                    responseStream?.Dispose();
                    outputStream?.Dispose();
                    response?.Dispose();
                }

                protected override void OnDispose() { request?.Abort(); }
            }

            #endregion
        }

        #endregion
    }
}