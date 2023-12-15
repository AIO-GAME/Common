/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-02
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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

                protected override async Task OnWaitAsync()
                {
                    for (var index = Remote.Count - 1; index >= 0; index--)
                    {
                        while (State == ProgressState.Pause) await Task.Delay(100, cancellationToken);
                        var remote = Remote[index];
                        try
                        {
                            var local = Path.Combine(LocalPath, Path.GetFileName(remote));
                            var outputStream =
                                await AddFileHeaderAsync(local, remote, IsOverWrite, cancellationToken);
                            if (outputStream is null)
                            {
                                Remote.RemoveAt(index);
                                continue;
                            }

                            fileStreams[remote] = outputStream;
                        }
                        catch (WebException e)
                        {
                            progress.OnError?.Invoke(e);
                            Remote.RemoveAt(index);
                            continue;
                        }

                        if (fileStreams.ContainsKey(remote))
                        {
                            var temp = fileStreams[remote].Position - CODE.Length;
                            if (temp > 0) httpWebRequest[remote].AddRange(temp);
                            while (State == ProgressState.Pause) await Task.Delay(100, cancellationToken);
                            httpWebResponses[remote] = (HttpWebResponse)await httpWebRequest[remote].GetResponseAsync();
                            progress.Total += httpWebResponses[remote].ContentLength;
                            progress.CurrentInfo = remote;
                            if (temp > 0) progress.Current += temp;
                        }
                    }

                    foreach (var remote in Remote)
                    {
                        progress.CurrentInfo = remote;
                        var responseStream = httpWebResponses[remote].GetResponseStream();
                        if (responseStream is null)
                        {
                            progress.OnError?.Invoke(new AIO.NetGetResponseStream("HTTP", httpWebResponses[remote]));
                            continue;
                        }

                        try
                        {
                            var buffer = new byte[BufferSize];
                            while (State == ProgressState.Pause) await Task.Delay(100, cancellationToken);
                            var readCount = await responseStream.ReadAsync(buffer, 0, BufferSize, cancellationToken);

                            while (readCount > 0)
                            {
                                if (State == ProgressState.Running)
                                {
                                    await fileStreams[remote].WriteAsync(buffer, 0, readCount, cancellationToken);
                                    progress.Current += readCount;
                                    readCount = await responseStream.ReadAsync(buffer, 0, BufferSize,
                                        cancellationToken);
                                }
                                else await Task.Delay(100, cancellationToken);
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
                            progress.OnError?.Invoke(e);
                        }
                    }

                    State = ProgressState.Finish;
                    progress.OnComplete?.Invoke();
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

            // /// <summary>
            // /// HTTP 下载文件
            // /// </summary>
            // /// <param name="remoteUrls">远端路径</param>
            // /// <param name="localPath">保存根路径</param>
            // /// <param name="iEvent">回调</param>
            // /// <param name="isOverWrite">覆盖</param>
            // /// <param name="timeout">超时</param>
            // /// <param name="bufferSize">容量</param>
            // /// <exception cref="Exception">异常</exception>
            // public static void Download(IEnumerable<string> remoteUrls, string localPath,
            //     IProgressEvent iEvent = null,
            //     bool isOverWrite = false,
            //     ushort timeout = TIMEOUT,
            //     int bufferSize = BUFFER_SIZE
            // )
            // {
            //     var httpWebResponses = new Dictionary<string, HttpWebResponse>();
            //     var fileStreams = new Dictionary<string, FileStream>();
            //     var progress = new AProgress(iEvent);
            //     foreach (var remoteUrl in remoteUrls)
            //     {
            //         var remote = remoteUrl.Replace("\\", "/");
            //
            //         FileStream outputStream;
            //         HttpWebResponse response;
            //         try
            //         {
            //             var request = (HttpWebRequest)WebRequest.Create(new Uri(remote));
            //             request.Timeout = timeout;
            //             outputStream = AddFileHeader(Path.Combine(localPath, Path.GetFileName(remote)), remote,
            //                 isOverWrite);
            //
            //             if (outputStream is null) continue;
            //             var temp = outputStream.Position - CODE.Length;
            //             if (temp > 0) request.AddRange(temp);
            //
            //             response = (HttpWebResponse)request.GetResponse();
            //             progress.Total += response.ContentLength;
            //         }
            //         catch (WebException e)
            //         {
            //             progress.Error(e);
            //             continue;
            //         }
            //
            //
            //         fileStreams.Add(remote, outputStream);
            //         httpWebResponses.Add(remote, response);
            //     }
            //
            //     foreach (var item in httpWebResponses.Keys)
            //     {
            //         progress.CurrentInfo = item;
            //         progress.Current += fileStreams[item].Position - CODE.Length;
            //         var responseStream = httpWebResponses[item].GetResponseStream();
            //         if (responseStream is null) throw new AIO.NetGetResponseStream("HTTP", httpWebResponses[item]);
            //
            //         var buffer = new byte[bufferSize];
            //         var readCount = responseStream.Read(buffer, 0, bufferSize);
            //         while (readCount > 0)
            //         {
            //             fileStreams[item].Write(buffer, 0, readCount);
            //             progress.Current += readCount;
            //             readCount = responseStream.Read(buffer, 0, bufferSize);
            //         }
            //
            //         responseStream.Close();
            //         httpWebResponses[item].Close();
            //         RemoveFileHeader(fileStreams[item]);
            //         fileStreams[item].Close();
            //     }
            //
            //     progress.Finish();
            // }
        }
    }
}