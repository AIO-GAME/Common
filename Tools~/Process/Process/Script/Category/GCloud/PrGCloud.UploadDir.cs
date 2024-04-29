/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-19
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public static partial class PrGCloud
    {
        private const string CMD_STR_UploadDir = "-m cp -r \"{0}\" \"gs://{1}/\"";

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="onProgress">进度回调</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool UploadDir(string remote, string location, Action<string> onProgress)
        {
            location = location.Replace('\\', '/').TrimEnd('/');
            if (!Directory.Exists(location)) return false;
            var messages = string.Format(CMD_STR_UploadDir, location, remote.Replace('\\', '/').TrimEnd('/'));
            var executor = Create(Gsutil, messages);
            if (onProgress != null) executor.OnProgress((o, s) => { onProgress.Invoke($"Uploading {s}"); });
            var result = executor.Sync();
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="onProgress">进度回调</param>
        /// <param name="metadata">元数据</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool UploadDir(string                      remote,   string         location,
                                     IDictionary<string, string> metadata, Action<string> onProgress)
        {
            location = location.Replace('\\', '/').TrimEnd('/');
            if (!Directory.Exists(location)) return false;
            var messages = new StringBuilder();
            foreach (var pair in metadata)
            {
                if (string.IsNullOrEmpty(pair.Value)) continue;
                messages.Append($"-h \"{pair.Key.Trim('-')}:{pair.Value}\" ");
            }

            messages.AppendFormat(CMD_STR_UploadDir, location, remote.Replace('\\', '/').TrimEnd('/'));
            var executor = Create(Gsutil, messages.ToString());
            if (onProgress != null) executor.OnProgress((o, s) => { onProgress.Invoke($"Uploading {s}"); });
            var result = executor.Sync();
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="onProgress">进度回调</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> UploadDirAsync(string remote, string location, Action<string> onProgress)
        {
            if (string.IsNullOrEmpty(remote)) throw new ArgumentNullException(nameof(remote));
            if (string.IsNullOrEmpty(location)) throw new ArgumentNullException(nameof(location));
            location = location.Replace('\\', '/').TrimEnd('/');
            if (!Directory.Exists(location)) return false;
            var messages = string.Format(CMD_STR_UploadDir, location, remote.Replace('\\', '/').TrimEnd('/'));
            var executor = Create(Gsutil, messages);
            var result = onProgress != null
                ? await executor.OnProgress((o, s) => { onProgress.Invoke($"Uploading : {s}"); })
                : await executor;
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="onProgress">进度回调</param>
        /// <param name="metadata">元数据</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> UploadDirAsync(
            string                      remote,
            string                      location,
            IDictionary<string, string> metadata,
            Action<string>              onProgress)
        {
            if (string.IsNullOrEmpty(remote)) throw new ArgumentNullException(nameof(remote));
            if (string.IsNullOrEmpty(location)) throw new ArgumentNullException(nameof(location));
            location = location.Replace('\\', '/').TrimEnd('/');
            if (!Directory.Exists(location)) return false;
            var messages = new StringBuilder();
            foreach (var pair in metadata)
            {
                if (string.IsNullOrEmpty(pair.Value)) continue;
                messages.Append($"-h \"{pair.Key.Trim('-')}:{pair.Value}\" ");
            }

            messages.AppendFormat(CMD_STR_UploadDir, location, remote.Replace('\\', '/').TrimEnd('/'));
            var executor = Create(Gsutil, messages.ToString());
            var result = onProgress != null
                ? await executor.OnProgress((o, s) => { onProgress.Invoke($"Uploading : {s}"); })
                : await executor;
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="key">元数据key</param>
        /// <param name="value">元数据value</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static Task<bool> UploadDirAsync(
            string remote,
            string location,
            string key,
            string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                return UploadDirAsync(remote, location, Console.WriteLine);
            var metadata = new Dictionary<string, string> { { key, value } };
            return UploadDirAsync(remote, location, metadata, Console.WriteLine);
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="onProgress">进度回调</param>
        /// <param name="key">元数据key</param>
        /// <param name="value">元数据value</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static Task<bool> UploadDirAsync(
            string         remote,
            string         location,
            string         key,
            string         value,
            Action<string> onProgress)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                return UploadDirAsync(remote, location, Console.WriteLine);
            var metadata = new Dictionary<string, string> { { key, value } };
            return UploadDirAsync(remote, location, metadata, onProgress);
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="metadata">元数据</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static Task<bool> UploadDirAsync(
            string                      remote,
            string                      location,
            IDictionary<string, string> metadata)
        {
            if (metadata is null || metadata.Count == 0)
                return UploadDirAsync(remote, location, Console.WriteLine);
            return UploadDirAsync(remote, location, metadata, Console.WriteLine);
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static Task<bool> UploadDirAsync(string remote, string location)
        {
            return UploadDirAsync(remote, location, Console.WriteLine);
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="key">元数据key</param>
        /// <param name="value">元数据value</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool UploadDir(
            string remote,
            string location,
            string key,
            string value)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                return UploadDir(remote, location, Console.WriteLine);
            var metadata = new Dictionary<string, string> { { key, value } };
            return UploadDir(remote, location, metadata, Console.WriteLine);
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="onProgress">进度回调</param>
        /// <param name="key">元数据key</param>
        /// <param name="value">元数据value</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool UploadDir(
            string         remote,
            string         location,
            string         key,
            string         value,
            Action<string> onProgress)
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                return UploadDir(remote, location, Console.WriteLine);
            var metadata = new Dictionary<string, string> { { key, value } };
            return UploadDir(remote, location, metadata, onProgress);
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="metadata">元数据</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool UploadDir(
            string                      remote,
            string                      location,
            IDictionary<string, string> metadata)
        {
            if (metadata is null || metadata.Count == 0)
                return UploadDir(remote, location, Console.WriteLine);
            return UploadDir(remote, location, metadata, Console.WriteLine);
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool UploadDir(string remote, string location)
        {
            return UploadDir(remote, location, Console.WriteLine);
        }
    }
}