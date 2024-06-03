/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-11
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public static partial class PrGCloud
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remotes">存储桶的路径列表</param>
        /// <param name="onProgress">进度回调</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        private static bool DeleteFile(IEnumerable<string> remotes, Action<string> onProgress)
        {
            var messages = new StringBuilder();
            foreach (var item in remotes.Distinct())
            {
                if (string.IsNullOrEmpty(item)) continue;
                messages.Append($"\"gs://{item.Replace('\\', '/').TrimEnd('/')}\" ");
            }

            var executor = Create(Gsutil, $" -m rm {messages}");
            if (onProgress != null) executor.OnProgress((o, s) => { onProgress.Invoke($"Delete : {s}"); });
            var result = executor.Sync();
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remotes">存储桶的路径列表</param>
        /// <param name="onProgress">进度回调</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> DeleteFileAsync(IEnumerable<string> remotes, Action<string> onProgress)
        {
            var messages = new StringBuilder();
            foreach (var item in remotes.Distinct())
            {
                if (string.IsNullOrEmpty(item)) continue;
                messages.Append($"\"gs://{item.Replace('\\', '/').TrimEnd('/')}\" ");
            }

            var executor = Create(Gsutil, $"-m rm {messages}");
            var result = onProgress != null
                ? await executor.OnProgress((o, s) => { onProgress.Invoke($"Delete : {s}"); })
                : await executor;
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remotes">存储桶的路径列表</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        public static Task<bool> DeleteFileAsync(IEnumerable<string> remotes)
        {
            return DeleteFileAsync(remotes, Console.WriteLine);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remotes">存储桶的路径列表</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        public static bool DeleteFile(IEnumerable<string> remotes)
        {
            return DeleteFile(remotes, Console.WriteLine);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remote">存储桶的路径</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool DeleteFile(string remote)
        {
            return DeleteFile(new[] { remote }, Console.WriteLine);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remote">存储桶的路径</param>
        /// <param name="onProgress">进度回调</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool DeleteFile(string remote, Action<string> onProgress)
        {
            return DeleteFile(new[] { remote }, onProgress);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remotes">存储桶的路径列表</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool DeleteFile(params string[] remotes)
        {
            return DeleteFile(remotes, Console.WriteLine);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remotes">存储桶的路径列表</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static Task<bool> DeleteFileAsync(params string[] remotes)
        {
            return DeleteFileAsync(remotes, Console.WriteLine);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="onProgress">进度回调</param>
        /// <param name="remote">存储桶的路径</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static Task<bool> DeleteFileAsync(string remote, Action<string> onProgress)
        {
            return DeleteFileAsync(new[] { remote }, onProgress);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static Task<bool> DeleteFileAsync(string remote)
        {
            return DeleteFileAsync(new[] { remote }, Console.WriteLine);
        }
    }
}