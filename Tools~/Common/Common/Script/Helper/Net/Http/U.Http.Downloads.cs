/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-02
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AIO
{
    public partial class AHelper
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
                private Dictionary<string, HttpWebResponse> httpWebResponses { get; }
                private Dictionary<string, HttpWebRequest> httpWebRequest { get; }
                private Dictionary<string, FileStream> fileStreams { get; }

                public HttpDownloadsOperation(
                    IEnumerable<string> remoteUrls,
                    string localPath,
                    bool isOverWrite = false,
                    ushort timeout = Net.TIMEOUT,
                    int bufferSize = Net.BUFFER_SIZE)
                {
                    Remote = new List<string>();
                    LocalPath = localPath;
                    IsOverWrite = isOverWrite;
                    Timeout = timeout;
                    BufferSize = bufferSize;
                    httpWebResponses = new Dictionary<string, HttpWebResponse>();
                    httpWebRequest = new Dictionary<string, HttpWebRequest>();
                    fileStreams = new Dictionary<string, FileStream>();
                    foreach (var remoteUrl in remoteUrls)
                    {
                        if (string.IsNullOrEmpty(remoteUrl))
                        {
                            Event.OnError?.Invoke(new WebException("RemoteUrl is null or empty"));
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
                            var outputStream = Net.AddFileHeader(local, () => GetMD5(remote), IsOverWrite);
                            if (outputStream is null)
                            {
                                Remote.RemoveAt(index);
                                continue;
                            }

                            if (!outputStream.CanWrite)
                            {
                                StartValue += IO.GetFileLength(local);
                                Remote.RemoveAt(index);
                                return;
                            }

                            fileStreams[remote] = outputStream;
                            var temp = outputStream.Position - Net.CODE.Length;
                            if (temp > 0) httpWebRequest[remote].AddRange(temp);
                            while (State == EProgressState.Pause) Thread.Sleep(100);
                            httpWebResponses[remote] = (HttpWebResponse)httpWebRequest[remote].GetResponse();
                            TotalValue += httpWebResponses[remote].ContentLength;
                            CurrentInfo = remote;
                            StartValue += temp;
                        }
                        catch (WebException e)
                        {
                            Event.OnError?.Invoke(e);
                            Remote.RemoveAt(index);
                        }
                    }

                    foreach (var remote in Remote)
                    {
                        CurrentInfo = remote;
                        var responseStream = httpWebResponses[remote].GetResponseStream();
                        if (responseStream is null)
                        {
                            Event.OnError?.Invoke(new AExpNetGetResponseStream("HTTP", httpWebResponses[remote]));
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
                                    CurrentValue += readCount;
                                    readCount = responseStream.Read(buffer, 0, BufferSize);
                                }
                                else Thread.Sleep(100);
                            }

                            responseStream.Close();
                            httpWebResponses[remote].Close();
                            Net.RemoveFileHeader(fileStreams[remote]);
                            fileStreams[remote].Close();
                        }
                        catch (Exception e)
                        {
                            State = EProgressState.Fail;
                            responseStream.Close();
                            httpWebResponses[remote].Close();
                            fileStreams[remote].Close();
                            Event.OnError?.Invoke(e);
                            return;
                        }
                    }

                    State = EProgressState.Finish;
                }

                protected override void OnDispose()
                {
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
                            var outputStream = await Net.AddFileHeaderAsync(local, () => GetMD5Async(remote), IsOverWrite,
                                cancellationToken);
                            if (outputStream is null)
                            {
                                Remote.RemoveAt(index);
                                continue;
                            }

                            if (!outputStream.CanWrite)
                            {
                                StartValue += IO.GetFileLength(local);
                                Remote.RemoveAt(index);
                                return;
                            }

                            fileStreams[remote] = outputStream;
                            var temp = outputStream.Position - Net.CODE.Length;
                            if (temp > 0) httpWebRequest[remote].AddRange(temp);
                            while (State == EProgressState.Pause) await Task.Delay(100, cancellationToken);
                            httpWebResponses[remote] = (HttpWebResponse)await httpWebRequest[remote].GetResponseAsync();
                            TotalValue += httpWebResponses[remote].ContentLength;
                            CurrentInfo = remote;
                            StartValue += temp;
                        }
                        catch (WebException e)
                        {
                            Event.OnError?.Invoke(e);
                            Remote.RemoveAt(index);
                        }
                    }

                    var buffer = new byte[BufferSize];
                    foreach (var remote in Remote)
                    {
                        CurrentInfo = remote;
                        var responseStream = httpWebResponses[remote].GetResponseStream();
                        if (responseStream is null)
                        {
                            Event.OnError?.Invoke(new AExpNetGetResponseStream("HTTP", httpWebResponses[remote]));
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
                                    CurrentValue += readCount;
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
                            await Net.RemoveFileHeaderAsync(fileStreams[remote], cancellationToken: cancellationToken);
                            fileStreams[remote].Close();
                        }
                        catch (Exception e)
                        {
                            responseStream.Close();
                            httpWebResponses[remote].Close();
                            fileStreams[remote].Close();
                            State = EProgressState.Fail;
                            Event.OnError?.Invoke(e);
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
                ushort timeout = Net.TIMEOUT,
                int bufferSize = Net.BUFFER_SIZE
            )
            {
                return new HttpDownloadsOperation(remoteUrls, localPath, isOverWrite, timeout, bufferSize);
            }
        }
    }
}