/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-02
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public partial class AHandle
{
    /// <summary>
    /// HTTP 处理器
    /// </summary>
    public sealed class HTTP : IDisposable
    {
        /// <summary>
        /// 创建HTTP处理器
        /// </summary>
        /// <param name="remoteURL">远端路径</param>
        /// <returns>处理器</returns>
        public static HTTP Create(string remoteURL)
        {
            return new HTTP(remoteURL);
        }
        
        /// <summary>
        /// 远端路径
        /// </summary>
        public string RemoteURL { get; private set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public ushort TimeOut { get; set; } = AHelper.Net.TIMEOUT;

        /// <summary>
        /// 缓存大小
        /// </summary>
        public int BufferSize { get; set; } = AHelper.BUFFER_SIZE;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="remoteURL">远端跟路径</param>
        public HTTP(string remoteURL)
        {
            RemoteURL = remoteURL;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存文件夹路径</param>
        /// <param name="remotePath">远端需要下载文件路径</param>
        /// <param name="progress">进度回调</param>
        /// <param name="isOverWrite">是否覆盖</param>
        public void Download(string savePath, string remotePath,
            ProgressArgs progress = default, bool isOverWrite = false)
        {
            var sourcePath = Path.Combine(RemoteURL, remotePath);
            var targetPath = Path.Combine(savePath, Path.GetFileName(remotePath));
            AHelper.Net.HTTPDownload(sourcePath, targetPath, progress, isOverWrite, TimeOut,
                BufferSize);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存文件夹路径</param>
        /// <param name="remotePath">远端需要下载文件路径</param>
        /// <param name="progress">进度回调</param>
        /// <param name="isOverWrite">是否覆盖</param>
        public void Download(string savePath, IEnumerable<string> remotePath,
            ProgressArgs progress = default, bool isOverWrite = false)
        {
            var sourcePath = remotePath.Select(path => Path.Combine(RemoteURL, path)).ToArray();
            AHelper.Net.HTTPDownload(sourcePath, savePath, progress, isOverWrite, TimeOut, BufferSize);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存文件夹路径</param>
        /// <param name="remotePath">远端需要下载文件路径</param>
        /// <param name="progress">进度回调</param>
        /// <param name="isOverWrite">是否覆盖</param>
        public Task DownloadAsync(string savePath, string remotePath,
            ProgressArgs progress = default, bool isOverWrite = false)
        {
            var sourcePath = Path.Combine(RemoteURL, remotePath);
            var targetPath = Path.Combine(savePath, Path.GetFileName(remotePath));
            return AHelper.Net.HTTPDownloadAsync(sourcePath, targetPath, progress, isOverWrite, TimeOut,
                BufferSize);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="savePath">保存文件夹路径</param>
        /// <param name="remotePath">远端需要下载文件路径</param>
        /// <param name="progress">进度回调</param>
        /// <param name="isOverWrite">是否覆盖</param>
        public Task DownloadAsync(string savePath, IEnumerable<string> remotePath,
            ProgressArgs progress = default, bool isOverWrite = false)
        {
            var sourcePath = remotePath.Select(remote => Path.Combine(RemoteURL, Path.GetFileName(remote))).ToArray();
            return AHelper.Net.HTTPDownloadAsync(sourcePath, savePath, progress, isOverWrite, TimeOut, BufferSize);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            RemoteURL = null;
        }
    }
}