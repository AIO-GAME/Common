/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-11
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AIO
{
    public static partial class PrGCloud
    {
        /// <summary>
        /// 上传文件到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="key">元数据key</param>
        /// <param name="value">元数据value</param>
        /// <remarks>
        /// --content-type=image/png
        /// --cache-control=public,max-age=3600
        /// --content-language=unset
        /// --content-encoding=unset
        /// --content-disposition=disposition
        /// </remarks>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool UploadFile(string remote, string location, string key = "", string value = "")
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (!File.Exists(location)) return false;
            remote = remote.Replace("\\", "/").TrimEnd('/');
            var ExecutorUpload = Create(Gcloud, $"storage cp \"{location}\" \"gs://{remote}\"").Sync();
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return ExecutorUpload.ExitCode == 0;

            var ExecutorUpdate = Create(Gcloud, $"storage objects update \"gs://{remote}\" \"--{key}={value}\"")
                .Sync();
            return ExecutorUpdate.ExitCode == 0;
        }

        /// <summary>
        /// 上传文件到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="key">元数据key</param>
        /// <param name="value">元数据value</param>
        /// <remarks>
        /// --content-type=image/png
        /// --cache-control=public,max-age=3600
        /// --content-language=unset
        /// --content-encoding=unset
        /// --content-disposition=disposition
        /// </remarks>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> UploadFileAsync(string remote, string location, string key = "",
            string value = "")
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (!File.Exists(location)) return false;
            remote = remote.Replace("\\", "/").TrimEnd('/');
            var ExecutorUpload = await Create(Gcloud, $"storage cp \"{location}\" \"gs://{remote}\"");
            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return ExecutorUpload.ExitCode == 0;

            var ExecutorUpdate = await Create(Gcloud, $"storage objects update \"gs://{remote}\" \"--{key}={value}\"");
            return ExecutorUpdate.ExitCode == 0;
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="onProgress">进度回调</param>
        /// <param name="key">元数据key</param>
        /// <param name="value">元数据value</param>
        /// <remarks>
        /// --content-type=image/png
        /// --cache-control=public,max-age=3600
        /// --content-language=unset
        /// --content-encoding=unset
        /// --content-disposition=disposition
        /// </remarks>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool UploadDir(string remote, string location,
            string key = "",
            string value = "",
            Action<string> onProgress = null)
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (!Directory.Exists(location)) return false;

            remote = remote.Replace("\\", "/").TrimEnd('/');

            var ExeUpload = Create(Gcloud, $"storage cp \"{location}\" \"gs://{remote}\" --recursive")
                .OnProgress((o, s) => { onProgress?.Invoke($"Uploading {s}"); }).Sync();
            if (ExeUpload.ExitCode != 0) return false;

            if (string.IsNullOrEmpty(key)) return true;
            if (string.IsNullOrEmpty(value)) return true;
            onProgress?.Invoke($"Update Metadata ：{remote}");
            return MetadataUpdate(remote, key, value);
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="onProgress">进度回调</param>
        /// <param name="key">元数据key</param>
        /// <param name="value">元数据value</param>
        /// <remarks>
        /// --content-type=image/png
        /// --cache-control=public,max-age=3600
        /// --content-language=unset
        /// --content-encoding=unset
        /// --content-disposition=disposition
        /// </remarks>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> UploadDirAsync(string remote, string location,
            string key = "",
            string value = "",
            Action<string> onProgress = null)
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (!Directory.Exists(location)) return false;

            remote = remote.Replace("\\", "/").TrimEnd('/');

            var ExeUpload = await Create(Gcloud, $"storage cp \"{location}\" \"gs://{remote}\" --recursive")
                .OnProgress((o, s) => { onProgress?.Invoke($"Uploading {s}"); });
            if (ExeUpload.ExitCode != 0) return false;
            if (string.IsNullOrEmpty(key)) return true;
            if (string.IsNullOrEmpty(value)) return true;
            onProgress?.Invoke($"Update Metadata ：{remote}");
            return await MetadataUpdateAsync(remote, key, value);
        }
    }
}