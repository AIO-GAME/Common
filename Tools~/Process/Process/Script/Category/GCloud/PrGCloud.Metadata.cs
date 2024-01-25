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
        /// 更新元数据
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <param name="key">元数据key</param>
        /// <param name="value">元数据value</param>
        /// <remarks>
        /// --cache-control=no-cache
        /// --content-type=image/png
        /// --cache-control=public,max-age=3600
        /// --content-language=unset
        /// --content-encoding=unset
        /// --content-disposition=disposition
        /// </remarks>
        /// <exception cref="ArgumentNullException">传入值为NULL</exception>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool MetadataUpdate(string remote, string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            remote = remote.Replace("\\", "/").TrimEnd('/');
            return Create(Gsutil, $"-m setmeta -h \"{key}:{value}\" -r gs://{remote}\"").Sync().ExitCode == 0;
        }

        /// <summary>
        /// 更新元数据
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <param name="key">元数据key</param>
        /// <param name="value">元数据value</param>
        /// <remarks>
        /// --content-type=image/png
        /// --cache-control=public,max-age=3600
        /// --content-language=unset
        /// --content-encoding=unset
        /// --content-disposition=disposition
        /// </remarks>
        /// <exception cref="ArgumentNullException">传入值为NULL</exception>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> MetadataUpdateAsync(string remote, string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            remote = remote.Replace("\\", "/").TrimEnd('/');
            var result = await Create(Gsutil, $"-m setmeta -h \"{key.Trim('-')}:{value}\" -r gs://{remote}\"");
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 更新元数据
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static bool MetadataLook(string remote)
        {
            remote = remote.Replace("\\", "/").TrimEnd('/');
            return Create(Gcloud, $"storage objects describe \"gs://{remote}\"").Sync().ExitCode == 0;
        }

        /// <summary>
        /// 更新元数据
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <returns> Ture:成功 False: 失败 </returns>
        [DebuggerHidden, DebuggerNonUserCode]
        public static async Task<bool> MetadataLookAsync(string remote)
        {
            remote = remote.Replace("\\", "/").TrimEnd('/');
            var result = await Create(Gcloud, $"storage objects describe \"gs://{remote}\"");
            return result.ExitCode == 0;
        }
    }
}