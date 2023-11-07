/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-02
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        /// Cookie
        /// </summary>
        public string Cookie { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="remoteURL">远端跟路径</param>
        private HTTP(string remoteURL)
        {
            RemoteURL = remoteURL;
        }

        #region Delete

        /// <summary>
        /// 请求删除指定的资源
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public string Delete(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Delete(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求删除指定的资源
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Delete(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Delete(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求删除指定的资源
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Delete(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Delete(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求删除指定的资源
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> DeleteAsync(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.DeleteAsync(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求删除指定的资源
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> DeleteAsync(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.DeleteAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求删除指定的资源
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> DeleteAsync(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.DeleteAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        #endregion

        #region Download

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
            AHelper.Net.HTTP.Download(sourcePath, targetPath, progress, isOverWrite, TimeOut,
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
            AHelper.Net.HTTP.Download(sourcePath, savePath, progress, isOverWrite, TimeOut, BufferSize);
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
            return AHelper.Net.HTTP.DownloadAsync(sourcePath, targetPath, progress, isOverWrite, TimeOut,
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
            return AHelper.Net.HTTP.DownloadAsync(sourcePath, savePath, progress, isOverWrite, TimeOut, BufferSize);
        }

        #endregion

        #region Put

        /// <summary>
        /// 向指定资源位置上传其最新内容
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Put(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Put(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 向指定资源位置上传其最新内容
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Put(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Put(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 向指定资源位置上传其最新内容
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> PutAsync(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.PutAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 向指定资源位置上传其最新内容
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> PutAsync(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.PutAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        #endregion

        #region Trace

        /// <summary>
        /// 回显服务器收到的请求
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public string Trace(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Trace(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 回显服务器收到的请求
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Trace(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Trace(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 回显服务器收到的请求
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Trace(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Trace(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 回显服务器收到的请求
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> TraceAsync(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.TraceAsync(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 回显服务器收到的请求
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> TraceAsync(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.TraceAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 回显服务器收到的请求
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> TraceAsync(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.TraceAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        #endregion

        #region Options

        /// <summary>
        /// 返回服务器正对特定资源所支持的HTTP请求方法
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public string Options(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Options(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 返回服务器正对特定资源所支持的HTTP请求方法
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Options(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Options(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 返回服务器正对特定资源所支持的HTTP请求方法
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Options(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Options(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 返回服务器正对特定资源所支持的HTTP请求方法
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> OptionsAsync(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.OptionsAsync(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 返回服务器正对特定资源所支持的HTTP请求方法
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> OptionsAsync(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.OptionsAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 返回服务器正对特定资源所支持的HTTP请求方法
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> OptionsAsync(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.OptionsAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        #endregion

        #region Head

        /// <summary>
        /// 请求获取特定的资源的响应消息报告
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public string Head(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Head(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的资源的响应消息报告
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Head(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Head(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的资源的响应消息报告
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Head(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Head(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的资源的响应消息报告
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> HeadAsync(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.HeadAsync(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的资源的响应消息报告
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> HeadAsync(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.HeadAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的资源的响应消息报告
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> HeadAsync(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.HeadAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        #endregion

        #region Connect

        /// <summary>
        /// 预留给能够将连接改为管道方式的代理服务器
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public string Connect(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Connect(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 预留给能够将连接改为管道方式的代理服务器
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Connect(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Connect(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 预留给能够将连接改为管道方式的代理服务器
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Connect(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Connect(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 预留给能够将连接改为管道方式的代理服务器
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> ConnectAsync(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.ConnectAsync(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 预留给能够将连接改为管道方式的代理服务器
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> ConnectAsync(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.ConnectAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 预留给能够将连接改为管道方式的代理服务器
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> ConnectAsync(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.ConnectAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        #endregion

        #region Get

        /// <summary>
        /// 请求获取特定的内容
        /// </summary>
        /// <returns>服务器返回内容</returns>
        public string Get()
        {
            return AHelper.Net.HTTP.Get(RemoteURL, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的内容
        /// </summary>
        /// <returns>服务器返回内容</returns>
        public Stream GetStream()
        {
            return AHelper.Net.HTTP.GetStream(RemoteURL, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的内容
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public string Get(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Get(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的内容
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public Stream GetStream(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.GetStream(sourcePath, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的内容
        /// </summary>
        /// <returns>服务器返回内容</returns>
        public Task<string> GetAsync()
        {
            return AHelper.Net.HTTP.GetAsync(RemoteURL, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的内容
        /// </summary>
        /// <returns>服务器返回内容</returns>
        public Task<Stream> GetStreamAsync()
        {
            return AHelper.Net.HTTP.GetStreamAsync(RemoteURL, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的内容
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> GetAsync(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.GetAsync(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求获取特定的内容
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public Task<Stream> GetStreamAsync(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.GetStreamAsync(sourcePath, TimeOut, Cookie);
        }

        #endregion

        #region Post

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public string Post(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Post(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Post(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Post(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public string Post(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.Post(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> PostAsync(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.PostAsync(sourcePath, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> PostAsync(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.PostAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<string> PostAsync(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.PostAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public Stream PostStream(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.PostStream(sourcePath, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Stream PostStream(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.PostStream(sourcePath, data, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Stream PostStream(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.PostStream(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <returns>服务器返回内容</returns>
        public Task<Stream> PostStreamAsync(string remoteRelativePath)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.PostStreamAsync(sourcePath, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<Stream> PostStreamAsync(string remoteRelativePath, byte[] data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.PostStreamAsync(sourcePath, data, TimeOut, Cookie);
        }

        /// <summary>
        /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
        /// </summary>
        /// <param name="remoteRelativePath">远端相对路径</param>
        /// <param name="data">数据</param>
        /// <returns>服务器返回内容</returns>
        public Task<Stream> PostStreamAsync(string remoteRelativePath, string data)
        {
            var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
            return AHelper.Net.HTTP.PostStreamAsync(sourcePath, data, Encoding, TimeOut, Cookie);
        }

        #endregion

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            RemoteURL = null;
        }
    }
}