/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-11
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

#region

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public static partial class PrGCloud
    {
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        [DebuggerHidden, DebuggerNonUserCode]
        public static byte[] ReadFile(string remote)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var temp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Create(Gsutil, $"cp \"gs://{remote}\" \"{temp}\"").Sync();
            var data = AHelper.IO.ReadFile(temp);
            AHelper.IO.DeleteFile(temp);
            return data;
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <param name="encoding">编码</param>
        [DebuggerHidden, DebuggerNonUserCode]
        public static string ReadText(string remote, Encoding encoding = null)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var temp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Create(Gsutil, $"cp \"gs://{remote}\" \"{temp}\"").Sync();
            var data = AHelper.IO.ReadText(temp, encoding);
            AHelper.IO.DeleteFile(temp);
            return data;
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<byte[]> ReadFileAsync(string remote)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var temp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            await Create(Gsutil, $"cp \"gs://{remote}\" \"{temp}\"");
            var data = AHelper.IO.ReadFile(temp);
            AHelper.IO.DeleteFile(temp);
            return data;
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="remote">存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <param name="encoding">编码</param>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<string> ReadTextAsync(string remote, Encoding encoding = null)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var temp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var result = await Create(Gsutil, $"cp \"gs://{remote}\" \"{temp}\"");
            if (result.ExitCode != 0)
            {
                if (AHelper.IO.ExistsFile(temp)) AHelper.IO.DeleteFile(temp);
                return string.Empty;
            }

            var data = AHelper.IO.ReadText(temp, encoding);
            AHelper.IO.DeleteFile(temp);
            return data;
        }
    }
}