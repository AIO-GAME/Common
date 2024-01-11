/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-11
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AIO
{
    public static partial class PrGCloud
    {
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="remote">存储桶路径</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool Exists(string remote)
        {
            remote = remote.Replace("\\", "/");
            var result = Create("gsutil", $"ls \"gs://{remote}\"").Sync();
            var content = result.StdOut.ToString();
            return !string.IsNullOrEmpty(content) && content.TrimStart("gs://")
                .Split(new[] { "gs://" }, StringSplitOptions.RemoveEmptyEntries)
                .Any(line => line.Trim().EndsWith(remote));
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="remote">存储桶路径</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> ExistsAsync(string remote)
        {
            remote = remote.Replace("\\", "/");
            var result = await Create("gsutil", $"ls \"gs://{remote}\"");
            var content = result.StdOut.ToString();
            return !string.IsNullOrEmpty(content) && content.TrimStart("gs://")
                .Split(new[] { "gs://" }, StringSplitOptions.RemoveEmptyEntries)
                .Any(line => line.Trim().EndsWith(remote));
        }
    }
}