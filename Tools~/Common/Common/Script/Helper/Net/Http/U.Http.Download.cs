using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

public partial class AHelper
{
    internal const int BUFFER_SIZE = 2048;

    public partial class Net
    {
        /// <summary>
        /// HTTP 下载文件
        /// </summary>
        /// <param name="remotePath">远端路径</param>
        /// <param name="localPath">保存路径</param>
        /// <param name="progress">回调</param>
        /// <param name="isOverWrite">覆盖</param>
        /// <param name="timeout">超时</param>
        /// <param name="bufferSize">容量</param>
        /// <exception cref="Exception">异常</exception>
        public static void HTTPDownload(
            string remotePath,
            string localPath,
            ProgressArgs progress = default,
            bool isOverWrite = false,
            ushort timeout = TIMEOUT,
            int bufferSize = BUFFER_SIZE
        )
        {
            var remote = remotePath.Replace("\\", "/");
            var request = (HttpWebRequest)WebRequest.Create(new Uri(remote));
            request.Timeout = timeout;

            var outputStream = AddFileHeader(localPath, remote, isOverWrite);
            if (outputStream is null) return;
            var temp = outputStream.Position - CODE.Length;
            if (temp > 0) request.AddRange(temp);

            HttpWebResponse response = null;
            Stream responseStream = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                progress.Total += response.ContentLength;
                progress.Current += outputStream.Position - CODE.Length;
                progress.CurrentName = remote;
                responseStream = response.GetResponseStream();
                if (responseStream is null) throw new Exception("HTTP Stream is Null");

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
            catch (Exception ex)
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
        /// <param name="remotePath">远端路径</param>
        /// <param name="localPath">保存路径</param>
        /// <param name="progress">回调</param>
        /// <param name="isOverWrite">覆盖</param>
        /// <param name="timeout">超时</param>
        /// <param name="bufferSize">容量</param>
        /// <exception cref="Exception">异常</exception>
        public static async Task HTTPDownloadAsync(string remotePath, string localPath,
            ProgressArgs progress = default,
            bool isOverWrite = false,
            ushort timeout = TIMEOUT,
            int bufferSize = BUFFER_SIZE
        )
        {
            var remote = remotePath.Replace("\\", "/");
            var request = WebRequest.CreateHttp(new Uri(remote));
            request.Timeout = timeout;

            var outputStream = await AddFileHeaderAsync(localPath, remote,isOverWrite);
            if (outputStream is null) return;
            var temp = outputStream.Position - CODE.Length;
            if (temp > 0) request.AddRange(temp);

            HttpWebResponse response = null;
            Stream responseStream = null;
            try
            {
                response = (HttpWebResponse)await request.GetResponseAsync();
                progress.Total = response.ContentLength;
                progress.Current += outputStream.Position - CODE.Length;
                progress.CurrentName = remote;
                responseStream = response.GetResponseStream();
                if (responseStream is null) throw new Exception("HTTP Stream is Null");

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
            catch (Exception ex)
            {
                responseStream?.Close();
                outputStream.Close();
                response?.Close();
                progress.OnError?.Invoke(ex);
            }
        }
    }
}