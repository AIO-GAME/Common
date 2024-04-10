#region

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public static partial class PrGCloud
    {
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <param name="onProgress">进度回调</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> DeleteDirAsync(string remote, Action<string> onProgress)
        {
            if (string.IsNullOrEmpty(remote)) throw new ArgumentNullException(nameof(remote));
            var executor = Create(Gsutil, $"-m rm -r \"gs://{remote.Replace("\\", "/")}\"");
            var result = onProgress != null
                ? await executor.OnProgress((o, s) => { onProgress.Invoke($"Delete : {s}"); })
                : await executor;
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <param name="onProgress">进度回调</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool DeleteDir(string remote, Action<string> onProgress)
        {
            if (string.IsNullOrEmpty(remote)) throw new ArgumentNullException(nameof(remote));
            var executor = Create(Gsutil, $"-m rm -r \"gs://{remote.Replace("\\", "/")}\"");
            var result = onProgress != null
                ? executor.OnProgress((o, s) => { onProgress.Invoke($"Delete : {s}"); }).Sync()
                : executor.Sync();
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<bool> DeleteDirAsync(string remote)
        {
            return DeleteDirAsync(remote, Console.WriteLine);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DeleteDir(string remote)
        {
            return DeleteDir(remote, Console.WriteLine);
        }
    }
}