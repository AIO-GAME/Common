using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AIO;

public partial class AHelper
{
    /// <summary>
    /// 容量缓存 : 1M
    /// </summary>
    internal const int BUFFER_SIZE = 1024 * 1024;

    public partial class Net
    {
        public partial class HTTP
        {
            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="localPath">保存路径</param>
            /// <param name="iEvent">回调</param>
            /// <param name="isOverWrite">覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">容量</param>
            /// <exception cref="Exception">异常</exception>
            public static void Download(
                string remoteUrl,
                string localPath,
                IProgressEvent iEvent = null,
                bool isOverWrite = false,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var remote = remoteUrl.Replace("\\", "/");
                var request = (HttpWebRequest)WebRequest.Create(new Uri(remote));
                request.Timeout = timeout;

                var outputStream = AddFileHeader(localPath, remote, isOverWrite);
                if (outputStream is null) return;
                var temp = outputStream.Position - CODE.Length;
                if (temp > 0) request.AddRange(temp);

                HttpWebResponse response = null;
                Stream responseStream = null;
                var progress = new AProgress(iEvent);
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    progress.Total += response.ContentLength;
                    progress.Current += outputStream.Position - CODE.Length;
                    progress.CurrentInfo = remote;
                    responseStream = response.GetResponseStream();
                    if (responseStream is null) throw new AIO.NetGetResponseStream("HTTP", response);

                    var buffer = new byte[bufferSize];
                    var readCount = responseStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);
                        progress.Current += readCount;
                        readCount = responseStream.Read(buffer, 0, bufferSize);
                    }

                    RemoveFileHeader(outputStream);
                    responseStream.Close();
                    outputStream.Close();
                    response.Close();
                    progress.OnComplete?.Invoke();
                }
                catch (WebException ex)
                {
                    responseStream?.Close();
                    outputStream.Close();
                    response?.Close();
                    progress.OnError?.Invoke(ex);
                }
            }


            /// <summary>
            /// HTTP 下载文件
            /// </summary>
            /// <param name="remoteUrl">远端路径</param>
            /// <param name="localPath">保存路径</param>
            /// <param name="iEvent">回调</param>
            /// <param name="isOverWrite">覆盖</param>
            /// <param name="timeout">超时</param>
            /// <param name="bufferSize">容量</param>
            /// <exception cref="Exception">异常</exception>
            public static async Task DownloadAsync(string remoteUrl, string localPath,
                IProgressEvent iEvent = null,
                bool isOverWrite = false,
                ushort timeout = TIMEOUT,
                int bufferSize = BUFFER_SIZE
            )
            {
                var remote = remoteUrl.Replace("\\", "/");
                var request = WebRequest.CreateHttp(new Uri(remote));
                request.Timeout = timeout;

                var outputStream = await AddFileHeaderAsync(localPath, remote, isOverWrite);
                if (outputStream is null) return;
                var temp = outputStream.Position - CODE.Length;
                if (temp > 0) request.AddRange(temp);
                var progress = new AProgress(iEvent);
                HttpWebResponse response = null;
                Stream responseStream = null;
                try
                {
                    response = (HttpWebResponse)await request.GetResponseAsync();
                    progress.Total = response.ContentLength;
                    progress.Current += outputStream.Position - CODE.Length;
                    progress.CurrentInfo = remote;
                    responseStream = response.GetResponseStream();
                    if (responseStream is null) throw new AIO.NetGetResponseStream("HTTP", response);

                    var buffer = new byte[bufferSize];
                    var readCount = await responseStream.ReadAsync(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        await outputStream.WriteAsync(buffer, 0, readCount);
                        progress.Current += readCount;

                        readCount = await responseStream.ReadAsync(buffer, 0, bufferSize);
                    }

                    await RemoveFileHeaderAsync(outputStream);
                    responseStream.Close();
                    outputStream.Close();
                    response.Close();
                    progress.OnComplete?.Invoke();
                }
                catch (WebException ex)
                {
                    responseStream?.Close();
                    outputStream.Close();
                    response?.Close();
                    progress.OnError?.Invoke(ex);
                }
            }
        }
    }
}