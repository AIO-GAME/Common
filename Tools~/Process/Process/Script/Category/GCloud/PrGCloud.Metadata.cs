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
        /// <param name="metadata">是要修改的元数据的标志。 </param>
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
        public static bool MetadataUpdate(string remote, string metadata)
        {
            if (string.IsNullOrEmpty(metadata))
                throw new ArgumentNullException(nameof(metadata));
            remote = remote.Replace("\\", "/").TrimEnd('/');
            return Create("gcloud", $"storage objects update \"gs://{remote}\" \"{metadata}\"").Sync()
                .ExitCode == 0;
        }

        /// <summary>
        /// 更新元数据
        /// </summary>
        /// <param name="remote">是对象要上传到的存储桶的路径，例如 my-bucket/data/text.png</param>
        /// <param name="metadata">是要修改的元数据的标志。 </param>
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
        public static async Task<bool> MetadataUpdateAsync(string remote, string metadata)
        {
            if (string.IsNullOrEmpty(metadata)) throw new ArgumentNullException(nameof(metadata));
            remote = remote.Replace("\\", "/").TrimEnd('/');
            var result = await Create("gcloud", $"storage objects update \"gs://{remote}\" \"{metadata}\"");
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
            return Create("gcloud", $"storage objects describe \"gs://{remote}\"").Sync().ExitCode == 0;
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
            var result = await Create("gcloud", $"storage objects describe \"gs://{remote}\"");
            return result.ExitCode == 0;
        }
    }
}