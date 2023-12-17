/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-02
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AIO;

public partial class AHelper
{
    public partial class Net
    {
        public partial class HTTP
        {
            private class HttpDownloadsOperation : AOperation
            {
                private List<string> Remote { get; }
                private string LocalPath { get; }
                private bool IsOverWrite { get; }
                private ushort Timeout { get; }
                private int BufferSize { get; }
                private CancellationTokenSource cancellationTokenSource { get; }
                private CancellationToken cancellationToken;
                private Dictionary<string, HttpWebResponse> httpWebResponses;
                private Dictionary<string, HttpWebRequest> httpWebRequest;
                private Dictionary<string, FileStream> fileStreams;

                public HttpDownloadsOperation(
                    IEnumerable<string> remoteUrls,
                    string localPath,
                    bool isOverWrite = false,
                    ushort timeout = TIMEOUT,
                    int bufferSize = BUFFER_SIZE)
                {
                    Remote = new List<string>();
                    LocalPath = localPath;
                    IsOverWrite = isOverWrite;
                    Timeout = timeout;
                    BufferSize = bufferSize;
                    // 创建一个取消令牌，用于取消下载操作
                    cancellationTokenSource = new CancellationTokenSource();
                    cancellationToken = cancellationTokenSource.Token;
                    httpWebResponses = new Dictionary<string, HttpWebResponse>();
                    httpWebRequest = new Dictionary<string, HttpWebRequest>();
                    fileStreams = new Dictionary<string, FileStream>();
                    foreach (var remoteUrl in remoteUrls)
                    {
                        if (string.IsNullOrEmpty(remoteUrl))
                        {
                            progress.OnError?.Invoke(new WebException("RemoteUrl is null or empty"));
                            continue;
                        }

                        Remote.Add(remoteUrl.Replace("\\", "/"));
                    }
                }

                protected override void OnBegin()
                {
                    foreach (var remote in Remote)
                    {
                        var request = (HttpWebRequest)WebRequest.Create(new Uri(remote));
                        request.Timeout = Timeout;
                        httpWebRequest[remote] = request;
                    }
                }

                protected override IEnumerator OnWaitCo()
                {
                    for (var index = Remote.Count - 1; index >= 0; index--)
                    {
                        while (State == EProgressState.Pause) yield return Task.Delay(100, cancellationToken);
                        var remote = Remote[index];
                        if (fileStreams.ContainsKey(remote)) continue;

                        var local = Path.Combine(LocalPath, Path.GetFileName(remote));
                        FileStream outputStream = null;
                        yield return AddFileHeaderCo(local, remote, fs => outputStream = fs, IsOverWrite,
                            cancellationToken);
                        if (outputStream is null)
                        {
                            Remote.RemoveAt(index);
                            continue;
                        }

                        fileStreams[remote] = outputStream;
                        var temp = outputStream.Position - CODE.Length;
                        if (temp > 0) httpWebRequest[remote].AddRange(temp);
                        while (State == EProgressState.Pause) yield return Task.Delay(100, cancellationToken);
                        httpWebResponses[remote] = (HttpWebResponse)httpWebRequest[remote].GetResponse();
                        progress.Total += httpWebResponses[remote].ContentLength;
                        progress.CurrentInfo = remote;
                        progress.StartValue += temp;
                        Remote.RemoveAt(index);
                    }

                    var buffer = new byte[BufferSize];
                    foreach (var remote in Remote)
                    {
                        progress.CurrentInfo = remote;
                        var responseStream = httpWebResponses[remote].GetResponseStream();
                        if (responseStream is null)
                        {
                            progress.OnError?.Invoke(new NetGetResponseStream("HTTP", httpWebResponses[remote]));
                            continue;
                        }

                        var readCount = responseStream.Read(buffer, 0, BufferSize);
                        while (readCount > 0)
                        {
                            if (State == EProgressState.Running)
                            {
                                yield return fileStreams[remote]
                                    .WriteAsync(buffer, 0, readCount, cancellationToken);
                                progress.Current += readCount;
                                readCount = responseStream.Read(buffer, 0, BufferSize);
                            }
                            else
                            {
                                yield return Task.Delay(100, cancellationToken);
                            }
                        }

                        responseStream.Close();
                        httpWebResponses[remote].Close();
                        yield return RemoveFileHeaderAsync(fileStreams[remote], cancellationToken: cancellationToken);
                        fileStreams[remote].Close();
                    }

                    State = EProgressState.Finish;
                }

                protected override void OnWait()
                {
                    for (var index = Remote.Count - 1; index >= 0; index--)
                    {
                        var remote = Remote[index];
                        if (fileStreams.ContainsKey(remote)) continue;
                        while (State == EProgressState.Pause) Thread.Sleep(100);
                        try
                        {
                            var local = Path.Combine(LocalPath, Path.GetFileName(remote));
                            var outputStream = AddFileHeader(local, GetMD5(remote), IsOverWrite);
                            if (outputStream is null)
                            {
                                Remote.RemoveAt(index);
                                continue;
                            }

                            fileStreams[remote] = outputStream;
                            var temp = outputStream.Position - CODE.Length;
                            if (temp > 0) httpWebRequest[remote].AddRange(temp);
                            while (State == EProgressState.Pause) Thread.Sleep(100);
                            httpWebResponses[remote] = (HttpWebResponse)httpWebRequest[remote].GetResponse();
                            progress.Total += httpWebResponses[remote].ContentLength;
                            progress.CurrentInfo = remote;
                            progress.StartValue += temp;
                        }
                        catch (WebException e)
                        {
                            progress.OnError?.Invoke(e);
                            Remote.RemoveAt(index);
                        }
                    }

                    foreach (var remote in Remote)
                    {
                        progress.CurrentInfo = remote;
                        var responseStream = httpWebResponses[remote].GetResponseStream();
                        if (responseStream is null)
                        {
                            progress.OnError?.Invoke(new NetGetResponseStream("HTTP", httpWebResponses[remote]));
                            continue;
                        }

                        try
                        {
                            var buffer = new byte[BufferSize];
                            while (State == EProgressState.Pause) Thread.Sleep(100);
                            var readCount = responseStream.Read(buffer, 0, BufferSize);

                            while (readCount > 0)
                            {
                                if (State == EProgressState.Running)
                                {
                                    fileStreams[remote].Write(buffer, 0, readCount);
                                    progress.Current += readCount;
                                    readCount = responseStream.Read(buffer, 0, BufferSize);
                                }
                                else Thread.Sleep(100);
                            }

                            responseStream.Close();
                            httpWebResponses[remote].Close();
                            RemoveFileHeader(fileStreams[remote]);
                            fileStreams[remote].Close();
                        }
                        catch (Exception e)
                        {
                            State = EProgressState.Fail;
                            responseStream.Close();
                            httpWebResponses[remote].Close();
                            fileStreams[remote].Close();
                            progress.OnError?.Invoke(e);
                            return;
                        }
                    }

                    State = EProgressState.Finish;
                }

                protected override void OnDispose()
                {
                    cancellationTokenSource.Cancel();
                    cancellationTokenSource.Dispose();
                    foreach (var remote in Remote)
                    {
                        httpWebRequest[remote].Abort();
                        httpWebResponses[remote].Close();
                        fileStreams[remote].Close();
                    }

                    httpWebRequest.Clear();
                    httpWebResponses.Clear();
                    fileStreams.Clear();
                }

                protected override async Task OnWaitAsync()
                {
                    for (var index = Remote.Count - 1; index >= 0; index--)
                    {
                        while (State == EProgressState.Pause) await Task.Delay(100, cancellationToken);
                        var remote = Remote[index];
                        if (fileStreams.ContainsKey(remote)) continue;
                        try
                        {
                            var local = Path.Combine(LocalPath, Path.GetFileName(remote));
                            var outputStream = await AddFileHeaderAsync(local, remote, IsOverWrite, cancellationToken);
                            if (outputStream is null)
                            {
                                Remote.RemoveAt(index);
                                continue;
                            }

                            fileStreams[remote] = outputStream;
                            var temp = outputStream.Position - CODE.Length;
                            if (temp > 0) httpWebRequest[remote].AddRange(temp);
                            while (State == EProgressState.Pause) await Task.Delay(100, cancellationToken);
                            httpWebResponses[remote] = (HttpWebResponse)await httpWebRequest[remote].GetResponseAsync();
                            progress.Total += httpWebResponses[remote].ContentLength;
                            progress.CurrentInfo = remote;
                            progress.StartValue += temp;
                        }
                        catch (WebException e)
                        {
                            progress.OnError?.Invoke(e);
                            Remote.RemoveAt(index);
                        }
                    }

                    var buffer = new byte[BufferSize];
                    foreach (var remote in Remote)
                    {
                        progress.CurrentInfo = remote;
                        var responseStream = httpWebResponses[remote].GetResponseStream();
                        if (responseStream is null)
                        {
                            progress.OnError?.Invoke(new NetGetResponseStream("HTTP", httpWebResponses[remote]));
                            continue;
                        }

                        try
                        {
                            var readCount = await responseStream.ReadAsync(buffer, 0, BufferSize, cancellationToken);
                            while (readCount > 0)
                            {
                                if (State == EProgressState.Running)
                                {
                                    await fileStreams[remote].WriteAsync(buffer, 0, readCount, cancellationToken);
                                    progress.Current += readCount;
                                    readCount = await responseStream.ReadAsync(buffer, 0, BufferSize,
                                        cancellationToken);
                                }
                                else
                                {
                                    await Task.Delay(100, cancellationToken);
                                }
                            }

                            responseStream.Close();
                            httpWebResponses[remote].Close();
                            await RemoveFileHeaderAsync(fileStreams[remote], cancellationToken: cancellationToken);
                            fileStreams[remote].Close();
                        }
                        catch (Exception e)
                        {
                            responseStream.Close();
                            httpWebResponses[remote].Close();
                            fileStreams[remote].Close();
                            State = EProgressState.Fail;
                            progress.OnError?.Invoke(e);
                            return;
                        }
                    }

                    State = EProgressState.Finish;
                }
            }

            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrls">远端路径</param>
            /// <param name="localPath">保存根路径</param>
            /// <param name="isOverWrite">覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">容量</param>
            /// <exception cref="Exception">异常</exception>
            public static IProgressOperation Download(IEnumerable<string> remoteUrls, string localPath,
                bool isOverWrite = false,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                return new HttpDownloadsOperation(remoteUrls, localPath, isOverWrite, timeout, bufferSize);
            }
        }
    }
}