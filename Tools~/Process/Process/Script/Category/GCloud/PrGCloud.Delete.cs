/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-11
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AIO
{
    public static partial class PrGCloud
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool DeleteFile(string remote)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var result = Create("gsutil", $"rm \"gs://{remote}\"").Sync();
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> DeleteFileAsync(string remote)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var result = await Create("gsutil", $"rm \"gs://{remote}\"");
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <param name="onProgress">进度回调</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> DeleteDirAsync(string remote, Action<string> onProgress = null)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var result = await Create("gsutil", $"-m rm -r \"gs://{remote}\"").OnProgress((o, s) =>
            {
                onProgress?.Invoke($"Delete file : {s}");
            });
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <param name="onProgress">进度回调</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool DeleteDir(string remote, Action<string> onProgress = null)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var result = Create("gsutil", $"-m rm -r \"gs://{remote}\"").OnProgress((o, s) =>
            {
                onProgress?.Invoke($"Delete file : {s}");
            }).Sync();
            return result.ExitCode == 0;
        }
    }
}