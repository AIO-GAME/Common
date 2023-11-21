/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-02
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

public partial class AHelper
{
    public partial class Net
    {
        public partial class HTTP
        {
            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrls">远端路径</param>
            /// <param name="localPath">保存根路径</param>
            /// <param name="progress">回调</param>
            /// <param name="isOverWrite">覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">容量</param>
            /// <exception cref="Exception">异常</exception>
            public static async Task DownloadAsync(IEnumerable<string> remoteUrls, string localPath,
                ProgressArgs progress = default,
                bool isOverWrite = false,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var httpWebResponses = new Dictionary<string, HttpWebResponse>();
                var fileStreams = new Dictionary<string, FileStream>();
                foreach (var remoteUrl in remoteUrls)
                {
                    var remote = remoteUrl.Replace("\\", "/");
                    FileStream outputStream;
                    HttpWebResponse response;
                    try
                    {
                        var request = (HttpWebRequest)WebRequest.Create(new Uri(remote));
                        request.Timeout = timeout;

                        var local = Path.Combine(localPath, Path.GetFileName(remote));
                        outputStream = await AddFileHeaderAsync(local, remote, isOverWrite);
                        if (outputStream is null) continue;

                        var temp = outputStream.Position - CODE.Length;
                        if (temp > 0) request.AddRange(temp);

                        response = (HttpWebResponse)await request.GetResponseAsync();
                        progress.Total += response.ContentLength;
                    }
                    catch (WebException e)
                    {
                        progress.OnError?.Invoke(e);
                        continue;
                    }

                    fileStreams.Add(remote, outputStream);
                    httpWebResponses.Add(remote, response);
                }

                foreach (var item in httpWebResponses.Keys)
                {
                    progress.CurrentInfo = item;
                    progress.Current += fileStreams[item].Position - CODE.Length;
                    var responseStream = httpWebResponses[item].GetResponseStream();
                    if (responseStream is null) throw new AIO.NetGetResponseStream("HTTP", httpWebResponses[item]);

                    var buffer = new byte[bufferSize];
                    var readCount = await responseStream.ReadAsync(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        await fileStreams[item].WriteAsync(buffer, 0, readCount);
                        progress.Current += readCount;
                        readCount = await responseStream.ReadAsync(buffer, 0, bufferSize);
                    }

                    responseStream.Close();
                    httpWebResponses[item].Close();
                    await RemoveFileHeaderAsync(fileStreams[item]);
                    fileStreams[item].Close();
                }


                progress.OnComplete?.Invoke();
            }

            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrls">远端路径</param>
            /// <param name="localPath">保存根路径</param>
            /// <param name="progress">回调</param>
            /// <param name="isOverWrite">覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">容量</param>
            /// <exception cref="Exception">异常</exception>
            public static void Download(IEnumerable<string> remoteUrls, string localPath,
                ProgressArgs progress = default,
                bool isOverWrite = false,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var httpWebResponses = new Dictionary<string, HttpWebResponse>();
                var fileStreams = new Dictionary<string, FileStream>();
                foreach (var remoteUrl in remoteUrls)
                {
                    var remote = remoteUrl.Replace("\\", "/");

                    FileStream outputStream;
                    HttpWebResponse response;
                    try
                    {
                        var request = (HttpWebRequest)WebRequest.Create(new Uri(remote));
                        request.Timeout = timeout;
                        outputStream = AddFileHeader(Path.Combine(localPath, Path.GetFileName(remote)), remote,
                            isOverWrite);

                        if (outputStream is null) continue;
                        var temp = outputStream.Position - CODE.Length;
                        if (temp > 0) request.AddRange(temp);

                        response = (HttpWebResponse)request.GetResponse();
                        progress.Total += response.ContentLength;
                    }
                    catch (WebException e)
                    {
                        progress.OnError?.Invoke(e);
                        continue;
                    }


                    fileStreams.Add(remote, outputStream);
                    httpWebResponses.Add(remote, response);
                }

                foreach (var item in httpWebResponses.Keys)
                {
                    progress.CurrentInfo = item;
                    progress.Current += fileStreams[item].Position - CODE.Length;
                    var responseStream = httpWebResponses[item].GetResponseStream();
                    if (responseStream is null) throw new AIO.NetGetResponseStream("HTTP", httpWebResponses[item]);

                    var buffer = new byte[bufferSize];
                    var readCount = responseStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        fileStreams[item].Write(buffer, 0, readCount);
                        progress.Current += readCount;
                        readCount = responseStream.Read(buffer, 0, bufferSize);
                    }

                    responseStream.Close();
                    httpWebResponses[item].Close();
                    RemoveFileHeader(fileStreams[item]);
                    fileStreams[item].Close();
                }

                progress.OnComplete?.Invoke();
            }
        }
    }
}