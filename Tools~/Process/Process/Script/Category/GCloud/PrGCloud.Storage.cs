/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-09
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AIO
{
    public partial class PrGCloud
    {
        /// <summary>
        /// Get Storage for commands
        /// </summary>
        public partial class Storage
        {
            /// <summary>
            /// 上传文件到存储桶
            /// </summary>
            /// <param name="BUCKET_PATH">下载的对象的存储桶路径</param>
            /// <param name="SAVE_TO_LOCATION">保存对象的本地路径</param>
            /// <param name="overwrite">覆盖</param>
            /// <returns>结果执行器</returns>
            [DebuggerHidden, DebuggerNonUserCode]
            public static IExecutor DownloadFile(string BUCKET_PATH, string SAVE_TO_LOCATION, bool overwrite = false)
            {
                SAVE_TO_LOCATION = SAVE_TO_LOCATION.Replace("\\", "/").TrimEnd('/');
                if (File.Exists(SAVE_TO_LOCATION) && !overwrite) return PrEmpty.Executor;
                BUCKET_PATH = BUCKET_PATH.Replace("\\", "/").TrimEnd('/');
                return Create().Input($"{CMD} {Usage.Storage} cp gs://{BUCKET_PATH} {SAVE_TO_LOCATION}");
            }

            /// <summary>
            /// 上传文件到存储桶
            /// </summary>
            /// <param name="BUCKET_PATH">是对象要上传到的存储桶的名称，例如 my-bucket</param>
            /// <param name="OBJECT_LOCATION">是对象的本地路径。例如 Desktop/dog.png。</param>
            /// <param name="METADATA_FLAG">是要修改的元数据的标志。
            /// 例如
            /// --content-type=image/png
            /// --cache-control=public,max-age=3600
            /// --content-language=unset
            /// --content-encoding=unset
            /// --content-disposition=disposition
            /// </param>
            /// <returns>结果执行器</returns>
            [DebuggerHidden, DebuggerNonUserCode]
            public static IExecutor UploadFile(string BUCKET_PATH, string OBJECT_LOCATION, string METADATA_FLAG = "")
            {
                OBJECT_LOCATION = OBJECT_LOCATION.Replace("\\", "/").TrimEnd('/');
                if (!File.Exists(OBJECT_LOCATION)) return PrEmpty.Executor;
                BUCKET_PATH = BUCKET_PATH.Replace("\\", "/").TrimEnd('/');
                var ExecutorUpload = Create().Input($"{CMD} {Usage.Storage} cp {OBJECT_LOCATION} gs://{BUCKET_PATH}");
                if (string.IsNullOrEmpty(METADATA_FLAG)) return ExecutorUpload;

                var ExecutorUpdate = Create().Input($"{CMD} storage objects update gs://{BUCKET_PATH} {METADATA_FLAG}");
                return ExecutorUpload.Link(ExecutorUpdate);
            }

            /// <summary>
            /// 上传文件夹到存储桶
            /// </summary>
            /// <param name="BUCKET_PATH">是对象要上传到的存储桶的名称，例如 my-bucket</param>
            /// <param name="OBJECT_LOCATION">是对象的本地路径。例如 Desktop/dog.png。</param>
            /// <param name="METADATA_FLAG">是要修改的元数据的标志。
            /// 例如
            /// --content-type=image/png
            /// --cache-control=public,max-age=3600
            /// --content-language=unset
            /// --content-encoding=unset
            /// --content-disposition=disposition
            /// </param>
            /// <returns>结果执行器</returns>
            [DebuggerHidden, DebuggerNonUserCode]
            public static IExecutor UploadDir(string BUCKET_PATH, string OBJECT_LOCATION, string METADATA_FLAG = "")
            {
                OBJECT_LOCATION = OBJECT_LOCATION.Replace("\\", "/").TrimEnd('/');
                if (!Directory.Exists(OBJECT_LOCATION)) return PrEmpty.Executor;

                BUCKET_PATH = BUCKET_PATH.Replace("\\", "/").TrimEnd('/');
                var ExeUpload = Create()
                    .Input($"{CMD} {Usage.Storage} cp {OBJECT_LOCATION} gs://{BUCKET_PATH} --recursive");
                if (string.IsNullOrEmpty(METADATA_FLAG)) return ExeUpload;

                var ExeUpdate = Create();
                var index = OBJECT_LOCATION.Length + 1;
                foreach (var file in Directory.GetFiles(OBJECT_LOCATION, "*.*", SearchOption.AllDirectories))
                {
                    var temp = $"{BUCKET_PATH}/{file.Substring(index).Replace("\\", "/")}";
                    ExeUpdate.Input($"{CMD} {Usage.Storage} objects update gs://{temp} {METADATA_FLAG}");
                }

                return ExeUpload.Link(ExeUpdate);
            }

            /// <summary>
            /// 更新元数据
            /// </summary>
            /// <param name="BUCKET_PATH">是对象要上传到的存储桶的路径，例如 my-bucket/data/text.png</param>
            /// <param name="METADATA_FLAG">是要修改的元数据的标志。
            /// 例如
            /// --content-type=image/png
            /// --cache-control=public,max-age=3600
            /// --content-language=unset
            /// --content-encoding=unset
            /// --content-disposition=disposition
            /// </param>
            /// <returns>结果执行器</returns>
            public static IExecutor MetadataUpdate(string BUCKET_PATH, string METADATA_FLAG)
            {
                if (string.IsNullOrEmpty(METADATA_FLAG)) throw new Exception("METADATA_FLAG is null");
                BUCKET_PATH = BUCKET_PATH.Replace("\\", "/").TrimEnd('/');
                return Create().Input($"{CMD} {Usage.Storage} objects update gs://{BUCKET_PATH} {METADATA_FLAG}");
            }

            /// <summary>
            /// 更新元数据
            /// </summary>
            /// <param name="BUCKET_PATH">是对象要上传到的存储桶的路径，例如 my-bucket/data/text.png</param>
            /// <returns>结果执行器</returns>
            public static IExecutor MetadataLook(string BUCKET_PATH)
            {
                BUCKET_PATH = BUCKET_PATH.Replace("\\", "/").TrimEnd('/');
                return Create(Usage.Storage, $"objects describe gs://{BUCKET_PATH}");
            }
        }
    }
}