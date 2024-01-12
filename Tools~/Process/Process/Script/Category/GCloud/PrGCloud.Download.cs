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
        /// <param name="remote">下载的对象的存储桶路径</param>
        /// <param name="location">保存对象的本地路径</param>
        /// <param name="overwrite">覆盖</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool DownloadFile(string remote, string location, bool overwrite = false)
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (File.Exists(location))
            {
                if (overwrite) File.Delete(location);
                else return false;
            }

            remote = remote.Replace("\\", "/").TrimEnd('/');
            var result = Create(Gcloud, $"storage cp \"gs://{remote}\" \"{location}\"").Sync();
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 上传文件到存储桶
        /// </summary>
        /// <param name="remote">下载的对象的存储桶路径</param>
        /// <param name="location">保存对象的本地路径</param>
        /// <param name="overwrite">覆盖</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> DownloadFileAsync(string remote, string location, bool overwrite = false)
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (File.Exists(location))
            {
                if (overwrite) File.Delete(location);
                else return false;
            }

            remote = remote.Replace("\\", "/").TrimEnd('/');
            var result = await Create(Gcloud, $"storage cp \"gs://{remote}\" \"{location}\"");
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 上传文件到存储桶
        /// </summary>
        /// <param name="remote">下载的对象的存储桶路径</param>
        /// <param name="location">保存对象的本地路径</param>
        /// <param name="overwrite">覆盖</param>
        /// <param name="onProgress">进度回调</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool DownloadDir(string remote, string location, bool overwrite = false,
            Action<string> onProgress = null)
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (Directory.Exists(location))
            {
                if (overwrite) Directory.Delete(location, true);
                else return false;
            }

            Directory.CreateDirectory(location);
            remote = remote.Replace("\\", "/").TrimEnd('/');
            var result = Create(Gsutil, $"-m cp -r \"gs://{remote}/*\" \"{location}\"").OnProgress((o, s) =>
            {
                onProgress?.Invoke($"Downloading : {s}");
            }).Sync();
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 上传文件到存储桶
        /// </summary>
        /// <param name="remote">下载的对象的存储桶路径</param>
        /// <param name="location">保存对象的本地路径</param>
        /// <param name="overwrite">覆盖</param>
        /// <param name="onProgress">进度回调</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> DownloadDirAsync(string remote, string location, bool overwrite = false,
            Action<string> onProgress = null)
        {
            location = location.Replace("\\", "/").TrimEnd('/');
            if (Directory.Exists(location))
            {
                if (overwrite) Directory.Delete(location, true);
                else return false;
            }

            Directory.CreateDirectory(location);
            remote = remote.Replace("\\", "/").TrimEnd('/');
            var result = await Create(Gsutil, $"-m cp -r \"gs://{remote}/*\" \"{location}\"").OnProgress((o, s) =>
            {
                onProgress?.Invoke($"Downloading : {s}");
            });
            return result.ExitCode == 0;
        }
    }
}