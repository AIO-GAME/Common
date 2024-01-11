/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-11
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.ComponentModel;
using System.Runtime.Remoting.Activation;
using System.Threading.Tasks;

namespace AIO
{
    public static partial class PrGCloud
    {
        /// <summary>
        /// 获取文件的 MD5 值
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <returns>获取指定文件MD5</returns>
        public static string GetMD5(string remote)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var result = Create("gsutil", $"hash -m \"gs://{remote}\"").Sync();
            if (result.ExitCode != 0) return string.Empty;
            var content = result.StdOut.ToString();
            var hash = content.Split('\n')[1].Split(' ')[1].Replace("(md5):", "").Trim();
            return BitConverter.ToString(Convert.FromBase64String(hash)).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 获取文件的 MD5 值
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <returns>获取指定文件MD5</returns>
        public static async Task<string> GetMD5Async(string remote)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var result = await Create("gsutil", $"hash -m \"gs://{remote}\"");
            if (result.ExitCode != 0) return string.Empty;
            var content = result.StdOut.ToString();
            var hash = content.Split('\n')[1].Split(' ')[1].Replace("(md5):", "").Trim();
            var cloudHashBytes = Convert.FromBase64String(hash);
            return BitConverter.ToString(cloudHashBytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 获取文件的 MD5 Hash 值
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <returns>获取指定文件MD5</returns>
        public static string GetMD5Hash(string remote)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var result = Create("gsutil", $"hash -m \"gs://{remote}\"").Sync();
            if (result.ExitCode != 0) return string.Empty;
            var content = result.StdOut.ToString();
            return content.Split('\n')[1].Split(' ')[1].Replace("(md5):", "").Trim();
        }

        /// <summary>
        /// 获取文件的 MD5 Hash 值
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <returns>获取指定文件MD5</returns>
        public static async Task<string> GetMD5HashAsync(string remote)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var result = await Create("gsutil", $"hash -m \"gs://{remote}\"");
            if (result.ExitCode != 0) return string.Empty;
            var content = result.StdOut.ToString();
            return content.Split('\n')[1].Split(' ')[1].Replace("(md5):", "").Trim();
        }

        /// <summary>
        /// 获取文件的 CRC32C 值
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <returns>获取指定文件MD5</returns>
        public static string GetCRC32C(string remote)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var result = Create("gsutil", $"hash -m \"gs://{remote}\"").Sync();
            if (result.ExitCode != 0) return string.Empty;
            var content = result.StdOut.ToString();
            return content.Split('\n')[2].Split(' ')[1].Replace("(crc32c):", "").Trim();
        }

        /// <summary>
        /// 获取文件的 CRC32C 值
        /// </summary>
        /// <param name="remote">远端地址</param>
        /// <returns>获取指定文件MD5</returns>
        public static async Task<string> GetCRC32CAsync(string remote)
        {
            if (remote == null) throw new ArgumentNullException(nameof(remote));
            remote = remote.Replace("\\", "/");
            var result = await Create("gsutil", $"hash -m \"gs://{remote}\"");
            if (result.ExitCode != 0) return string.Empty;
            var content = result.StdOut.ToString();
            return content.Split('\n')[2].Split(' ')[1].Replace("(crc32c):", "").Trim();
        }
    }
}