﻿#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class AHandle
    {
        #region Nested type: HTTP

        /// <summary>
        /// HTTP 处理器
        /// </summary>
        public sealed class Http : IDisposable
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="remoteURL">远端跟路径</param>
            private Http(string remoteURL) { RemoteURL = remoteURL; }

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
            public int BufferSize { get; set; } = AHelper.Net.BUFFER_SIZE;

            /// <summary>
            /// Cookie
            /// </summary>
            public string Cookie { get; set; }

            /// <summary>
            /// 内容类型
            /// </summary>
            public string ContentType { get; set; } = "application/json";

            /// <summary>
            /// 编码
            /// </summary>
            public Encoding Encoding { get; set; } = Encoding.UTF8;

            #region IDisposable Members

            /// <summary>
            /// 释放
            /// </summary>
            public void Dispose() { RemoteURL = null; }

            #endregion

            /// <summary>
            /// 创建HTTP处理器
            /// </summary>
            /// <param name="remoteURL">远端路径</param>
            /// <returns>处理器</returns>
            public static Http Create(string remoteURL) { return new Http(remoteURL); }

            #region Delete

            /// <summary>
            /// 请求删除指定的资源
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public string Delete(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.Delete(sourcePath, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Delete(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Delete(sourcePath, data, Encoding, TimeOut, Cookie);
            }

            /// <summary>
            /// 请求删除指定的资源
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public Task<string> DeleteAsync(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.DeleteAsync(sourcePath, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.DeleteAsync(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.DeleteAsync(sourcePath, data, Encoding, TimeOut, Cookie);
            }

            #endregion

            #region Download

            /// <summary>
            /// 下载文件
            /// </summary>
            /// <param name="savePath">保存文件夹路径</param>
            /// <param name="remotePath">远端需要下载文件路径</param>
            /// <param name="aProgress">进度回调</param>
            /// <param name="isOverWrite">是否覆盖</param>
            public void Download(string         savePath,
                                 string         remotePath,
                                 IProgressEvent aProgress   = default,
                                 bool           isOverWrite = false)
            {
                var sourcePath = Path.Combine(RemoteURL, remotePath);
                var targetPath = Path.Combine(savePath, Path.GetFileName(remotePath));
                var operation  = AHelper.Http.DownloadOperation(sourcePath, targetPath, isOverWrite, TimeOut, BufferSize);
                operation.Event = aProgress;
                operation.Wait();
            }

            /// <summary>
            /// 下载文件
            /// </summary>
            /// <param name="savePath">保存文件夹路径</param>
            /// <param name="remotePath">远端需要下载文件路径</param>
            /// <param name="aProgress">进度回调</param>
            /// <param name="isOverWrite">是否覆盖</param>
            public void Download(string              savePath,
                                 IEnumerable<string> remotePath,
                                 IProgressEvent      aProgress   = default,
                                 bool                isOverWrite = false)
            {
                var sourcePath = remotePath.Select(path => Path.Combine(RemoteURL, path)).ToArray();
                var operation  = AHelper.Http.Download(sourcePath, savePath, isOverWrite, TimeOut, BufferSize);
                operation.Event = aProgress;
                operation.Wait();
            }

            /// <summary>
            /// 下载文件
            /// </summary>
            /// <param name="savePath">保存文件夹路径</param>
            /// <param name="remotePath">远端需要下载文件路径</param>
            /// <param name="aProgress">进度回调</param>
            /// <param name="isOverWrite">是否覆盖</param>
            public Task DownloadAsync(string         savePath,
                                      string         remotePath,
                                      IProgressEvent aProgress   = default,
                                      bool           isOverWrite = false)
            {
                var sourcePath = Path.Combine(RemoteURL, remotePath);
                var targetPath = Path.Combine(savePath, Path.GetFileName(remotePath));
                var operation  = AHelper.Http.DownloadOperation(sourcePath, targetPath, isOverWrite, TimeOut, BufferSize);
                operation.Event = aProgress;
                return operation.WaitAsync();
            }

            /// <summary>
            /// 下载文件
            /// </summary>
            /// <param name="savePath">保存文件夹路径</param>
            /// <param name="remotePath">远端需要下载文件路径</param>
            /// <param name="aProgress">进度回调</param>
            /// <param name="isOverWrite">是否覆盖</param>
            public Task DownloadAsync(string              savePath,
                                      IEnumerable<string> remotePath,
                                      IProgressEvent      aProgress   = default,
                                      bool                isOverWrite = false)
            {
                var sourcePath = remotePath.Select(remote => Path.Combine(RemoteURL, Path.GetFileName(remote))).ToArray();
                var operation  = AHelper.Http.Download(sourcePath, savePath, isOverWrite, TimeOut, BufferSize);
                operation.Event = aProgress;
                return operation.WaitAsync();
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
                return AHelper.Http.Put(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Put(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.PutAsync(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.PutAsync(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Trace(sourcePath, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Trace(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Trace(sourcePath, data, Encoding, TimeOut, Cookie);
            }

            /// <summary>
            /// 回显服务器收到的请求
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public Task<string> TraceAsync(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.TraceAsync(sourcePath, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.TraceAsync(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.TraceAsync(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Options(sourcePath, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Options(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Options(sourcePath, data, Encoding, TimeOut, Cookie);
            }

            /// <summary>
            /// 返回服务器正对特定资源所支持的HTTP请求方法
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public Task<string> OptionsAsync(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.OptionsAsync(sourcePath, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.OptionsAsync(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.OptionsAsync(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Head(sourcePath, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Head(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Head(sourcePath, data, Encoding, TimeOut, Cookie);
            }

            /// <summary>
            /// 请求获取特定的资源的响应消息报告
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public Task<string> HeadAsync(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.HeadAsync(sourcePath, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.HeadAsync(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.HeadAsync(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Connect(sourcePath, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Connect(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.Connect(sourcePath, data, Encoding, TimeOut, Cookie);
            }

            /// <summary>
            /// 预留给能够将连接改为管道方式的代理服务器
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public Task<string> ConnectAsync(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.ConnectAsync(sourcePath, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.ConnectAsync(sourcePath, data, Encoding, TimeOut, Cookie);
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
                return AHelper.Http.ConnectAsync(sourcePath, data, Encoding, TimeOut, Cookie);
            }

            #endregion

            #region Get

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <returns>服务器返回内容</returns>
            public string Get() { return AHelper.Http.Get(RemoteURL, Encoding, TimeOut, Cookie); }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <returns>服务器返回内容</returns>
            public Stream GetStream() { return AHelper.Http.GetStream(RemoteURL, TimeOut, Cookie); }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public string Get(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.Get(sourcePath, Encoding, TimeOut, Cookie);
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public Stream GetStream(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.GetStream(sourcePath, TimeOut, Cookie);
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <returns>服务器返回内容</returns>
            public Task<string> GetAsync() { return AHelper.Http.GetAsync(RemoteURL, Encoding, TimeOut, Cookie); }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <returns>服务器返回内容</returns>
            public Task<Stream> GetStreamAsync() { return AHelper.Http.GetStreamAsync(RemoteURL, TimeOut, Cookie); }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public Task<string> GetAsync(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.GetAsync(sourcePath, Encoding, TimeOut, Cookie);
            }

            /// <summary>
            /// 请求获取特定的内容
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public Task<Stream> GetStreamAsync(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.GetStreamAsync(sourcePath, TimeOut, Cookie);
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
                return AHelper.Http.Post(sourcePath, GetOption());
            }

            private AHelper.Http.Option GetOption()
            {
                return new AHelper.Http.Option
                {
                    Encoding    = Encoding,
                    Timeout     = TimeOut,
                    Cookie      = Cookie,
                    ContentType = ContentType
                };
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
                return AHelper.Http.Post(sourcePath, data, GetOption());
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
                return AHelper.Http.Post(sourcePath, data, GetOption());
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public Task<string> PostAsync(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.PostAsync(sourcePath, GetOption());
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
                return AHelper.Http.PostAsync(sourcePath, data, GetOption());
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
                return AHelper.Http.PostAsync(sourcePath, data, GetOption());
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public Stream PostStream(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.PostStream(sourcePath, GetOption());
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
                return AHelper.Http.PostStream(sourcePath, data, GetOption());
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
                return AHelper.Http.PostStream(sourcePath, data, GetOption());
            }

            /// <summary>
            /// 请求服务器接受所指定的文档作为对所标识的URI的新的从属实体
            /// </summary>
            /// <param name="remoteRelativePath">远端相对路径</param>
            /// <returns>服务器返回内容</returns>
            public Task<Stream> PostStreamAsync(string remoteRelativePath)
            {
                var sourcePath = Path.Combine(RemoteURL, remoteRelativePath);
                return AHelper.Http.PostStreamAsync(sourcePath, GetOption());
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
                return AHelper.Http.PostStreamAsync(sourcePath, data, GetOption());
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
                return AHelper.Http.PostStreamAsync(sourcePath, data, GetOption());
            }

            #endregion
        }

        #endregion
    }
}