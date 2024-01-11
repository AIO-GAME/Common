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
        /// <param name="metadata">是要修改的元数据的标志。
        /// </param>
        /// <remarks>
        /// --content-type=image/png
        /// --cache-control=public,max-age=3600
        /// --content-language=unset
        /// --content-encoding=unset
        /// --content-disposition=disposition
        /// </remarks>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool UploadFile(string remote, string location, string metadata = "")
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (!File.Exists(location)) return false;
            remote = remote.Replace("\\", "/").TrimEnd('/');
            var ExecutorUpload = Create("gcloud", $"storage cp \"{location}\" \"gs://{remote}\"").Sync();
            if (string.IsNullOrEmpty(metadata)) return ExecutorUpload.ExitCode == 0;

            var ExecutorUpdate = Create("gcloud", $"storage objects update \"gs://{remote}\" \"{metadata}\"")
                .Sync();
            return ExecutorUpdate.ExitCode == 0;
        }

        /// <summary>
        /// 上传文件到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="metadata">是要修改的元数据的标志。
        /// </param>
        /// <remarks>
        /// --content-type=image/png
        /// --cache-control=public,max-age=3600
        /// --content-language=unset
        /// --content-encoding=unset
        /// --content-disposition=disposition
        /// </remarks>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> UploadFileAsync(string remote, string location, string metadata = "")
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (!File.Exists(location)) return false;
            remote = remote.Replace("\\", "/").TrimEnd('/');
            var ExecutorUpload = await Create("gcloud", $"storage cp \"{location}\" \"gs://{remote}\"");
            if (string.IsNullOrEmpty(metadata)) return ExecutorUpload.ExitCode == 0;

            var ExecutorUpdate = await Create("gcloud", $"storage objects update \"gs://{remote}\" \"{metadata}\"");
            return ExecutorUpdate.ExitCode == 0;
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="onProgress">进度回调</param>
        /// <param name="metadata">是要修改的元数据的标志。
        /// 例如
        /// --content-type=image/png
        /// --cache-control=public,max-age=3600
        /// --content-language=unset
        /// --content-encoding=unset
        /// --content-disposition=disposition
        /// </param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool UploadDir(string remote, string location, string metadata = "",
            Action<string> onProgress = null)
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (!Directory.Exists(location)) return false;

            remote = remote.Replace("\\", "/").TrimEnd('/');
            var ExeUpload = Create("gcloud", $"storage cp \"{location}\" \"gs://{remote}/*\" --recursive")
                .OnProgress((o, s) => { onProgress?.Invoke($"Uploading {s}"); }).Sync();
            if (ExeUpload.ExitCode != 0) return false;
            if (string.IsNullOrEmpty(metadata)) return true;

            var index = location.Length + 1;
            var result = ExeUpload.ExitCode == 0;
            foreach (var file in Directory.GetFiles(location, "*.*", SearchOption.AllDirectories))
            {
                var temp = $"{remote}/{file.Substring(index).Replace("\\", "/")}";
                var te = Create("gcloud", $"storage objects update \"gs://{temp}\" \"{metadata}\"").Sync();
                if (result) result = te.ExitCode == 0;
                onProgress?.Invoke($"Update Metadata ：{temp} {(te.ExitCode == 0 ? "成功" : "失败")}");
            }

            return result;
        }

        /// <summary>
        /// 上传文件夹到存储桶
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的名称，例如 my-bucket</param>
        /// <param name="location">是对象的本地路径。例如 Desktop/dog.png。</param>
        /// <param name="onProgress">进度回调</param>
        /// <param name="metadata">是要修改的元数据的标志。
        /// 例如
        /// --content-type=image/png
        /// --cache-control=public,max-age=3600
        /// --content-language=unset
        /// --content-encoding=unset
        /// --content-disposition=disposition
        /// </param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> UploadDirAsync(string remote, string location, string metadata = "",
            Action<string> onProgress = null)
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (!Directory.Exists(location)) return false;

            remote = remote.Replace("\\", "/").TrimEnd('/');
            var ExeUpload = await Create("gcloud", $"storage cp \"{location}\" \"gs://{remote}/*\" --recursive")
                .OnProgress((o, s) => { onProgress?.Invoke($"Uploading {s}"); });
            if (ExeUpload.ExitCode != 0) return false;
            if (string.IsNullOrEmpty(metadata)) return true;

            var index = location.Length + 1;
            var result = ExeUpload.ExitCode == 0;
            foreach (var file in Directory.GetFiles(location, "*.*", SearchOption.AllDirectories))
            {
                var temp = $"{remote}/{file.Substring(index).Replace("\\", "/")}";
                var te = await Create("gcloud", $"storage objects update \"gs://{temp}\" \"{metadata}\"");
                if (result) result = te.ExitCode == 0;
                onProgress?.Invoke($"Update Metadata ：{temp} {(te.ExitCode == 0 ? "成功" : "失败")}");
            }

            return result;
        }
    }
}