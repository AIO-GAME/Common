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
            /// <param name="BUCKET_NAME">下载的对象的存储桶名称</param>
            /// <param name="OBJECT_NAME">下载的对象的名称</param>
            /// <param name="SAVE_TO_LOCATION">保存对象的本地路径</param>
            /// <returns>结果执行器</returns>
            [DebuggerHidden, DebuggerNonUserCode]
            public static IExecutor DownloadFile(string BUCKET_NAME, string OBJECT_NAME, string SAVE_TO_LOCATION)
            {
                return Create(Usage.Storage, $"cp gs://{BUCKET_NAME}/{OBJECT_NAME}", $"{SAVE_TO_LOCATION}");
            }

            /// <summary>
            /// 上传文件到存储桶
            /// </summary>
            /// <param name="BUCKET_NAME">是对象要上传到的存储桶的名称，例如 my-bucket</param>
            /// <param name="OBJECT_LOCATION">是对象的本地路径。例如 Desktop/dog.png。</param>
            /// <returns>结果执行器</returns>
            [DebuggerHidden, DebuggerNonUserCode]
            public static IExecutor UploadFile(string BUCKET_NAME, string OBJECT_LOCATION)
            {
                return Create(Usage.Storage, $"cp {OBJECT_LOCATION}", $"gs://{BUCKET_NAME}/");
            }

            /// <summary>
            /// 上传文件到存储桶
            /// </summary>
            /// <param name="BUCKET_NAME">是对象要上传到的存储桶的名称，例如 my-bucket</param>
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
            public static IExecutor UploadFile(string BUCKET_NAME, string OBJECT_LOCATION, string METADATA_FLAG)
            {
                return Create(Usage.Storage, $"cp {OBJECT_LOCATION}", $"gs://{BUCKET_NAME}/")
                    .Input(
                        $"{CMD} storage objects update gs://{BUCKET_NAME}/{Path.GetFileName(OBJECT_LOCATION)} {METADATA_FLAG}");
            }

            /// <summary>
            /// 上传文件夹到存储桶
            /// </summary>
            /// <param name="BUCKET_NAME">是对象要上传到的存储桶的名称，例如 my-bucket</param>
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
            public static IExecutor UploadDir(string BUCKET_NAME, string OBJECT_LOCATION, string METADATA_FLAG)
            {
                var index = OBJECT_LOCATION.Replace("\\", "/").LastIndexOf('/') + 1;
                var executor = Create(Usage.Storage, $"cp {OBJECT_LOCATION}", $"gs://{BUCKET_NAME}/", "--recursive");
                foreach (var file in Directory.GetFiles(OBJECT_LOCATION, "*.*", SearchOption.AllDirectories))
                {
                    executor.Input(
                        $"{CMD} storage objects update gs://{BUCKET_NAME}/{file.Substring(index).Replace("\\", "/")} {METADATA_FLAG}");
                }

                return executor;
            }

            /// <summary>
            /// 上传文件夹到存储桶
            /// </summary>
            /// <param name="BUCKET_NAME">是对象要上传到的存储桶的名称，例如 my-bucket</param>
            /// <param name="OBJECT_LOCATION">是对象的本地路径。例如 Desktop/dog.png。</param>
            /// <returns>结果执行器</returns>
            [DebuggerHidden, DebuggerNonUserCode]
            public static IExecutor UploadDir(string BUCKET_NAME, string OBJECT_LOCATION)
            {
                return Create(Usage.Storage, $"cp {OBJECT_LOCATION}", $"gs://{BUCKET_NAME}/", "--recursive");
            }

            /// <summary>
            /// 更新元数据
            /// </summary>
            /// <param name="BUCKET_NAME">是对象要上传到的存储桶的名称，例如 my-bucket</param>
            /// <param name="OBJECT_NAME">修改其元数据的对象的名称，例如 pets/dog.png。</param>
            /// <param name="METADATA_FLAG">是要修改的元数据的标志。
            /// 例如
            /// --content-type=image/png
            /// --cache-control=public,max-age=3600
            /// --content-language=unset
            /// --content-encoding=unset
            /// --content-disposition=disposition
            /// </param>
            /// <returns>结果执行器</returns>
            public static IExecutor MetadataUpdate(string BUCKET_NAME, string OBJECT_NAME, string METADATA_FLAG)
            {
                return Create(Usage.Storage, $"objects update gs://{BUCKET_NAME}/{OBJECT_NAME} {METADATA_FLAG}");
            }

            /// <summary>
            /// 更新元数据
            /// </summary>
            /// <param name="BUCKET_NAME">是对象要上传到的存储桶的名称，例如 my-bucket</param>
            /// <param name="OBJECT_NAME">修改其元数据的对象的名称，例如 pets/dog.png。</param>
            /// <returns>结果执行器</returns>
            public static IExecutor MetadataLook(string BUCKET_NAME, string OBJECT_NAME)
            {
                return Create(Usage.Storage, $"objects describe gs://{BUCKET_NAME}/{OBJECT_NAME}");
            }
        }
    }
}