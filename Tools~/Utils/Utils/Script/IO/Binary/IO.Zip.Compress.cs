using System;
using System.Collections.Generic;
using System.IO;

using ICSharpCode.SharpZipLib.Zip;

public partial class Utils
{
    public partial class IO
    {
        /// <summary>
        /// 压缩为ZIP包
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        /// <param name="progressAction">进度回调</param>
        public static void CompressedZip(
            in string savePath,
            in string sourcePath,
            in Action<float> progressAction,
            in string pattern = "*",
            in SearchOption option = SearchOption.AllDirectories)
        {
            if (!Directory.Exists(sourcePath)) throw new ArgumentException("要压缩的文件夹不存在！");

            var files = Directory.GetFiles(sourcePath, pattern, option);
            var buffer = new byte[4096]; // 缓冲区大小

            using (var zipStream = new ZipOutputStream(File.Create(savePath)))
            {
                zipStream.SetLevel(5); // 设置压缩级别
                for (var i = 0; i < files.Length; i++)
                {
                    var file = files[i];
                    var entryName = file.Substring(sourcePath.Length + 1);
                    var entry = new ZipEntry(entryName) { DateTime = DateTime.Now };
                    zipStream.PutNextEntry(entry);

                    using (var fs = File.OpenRead(file))
                    {
                        int bytesRead;
                        do
                        {
                            bytesRead = fs.Read(buffer, 0, buffer.Length);
                            zipStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }

                    var progress = (float)(i + 1) / files.Length; // 计算当前压缩进度
                    progressAction.Invoke(progress); // 调用回调函数并传入当前进度
                }

                zipStream.Finish();
            }

            progressAction.Invoke(1f);
        }

        /// <summary>
        /// 压缩为ZIP包
        /// </summary>
        /// <param name="sourcePath">源文件夹</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="pattern">匹配模式</param>
        /// <param name="option">搜索模式</param>
        public static void CompressedZip(
            in string savePath,
            in string sourcePath,
            in string pattern = "*",
            in SearchOption option = SearchOption.AllDirectories)
        {
            if (!Directory.Exists(sourcePath)) throw new ArgumentException("要压缩的文件夹不存在！");

            var files = Directory.GetFiles(sourcePath, pattern, option);
            var buffer = new byte[4096]; // 缓冲区大小

            using (var zipStream = new ZipOutputStream(File.Create(savePath)))
            {
                zipStream.SetLevel(5); // 设置压缩级别
                foreach (var file in files)
                {
                    var entryName = file.Substring(sourcePath.Length + 1);
                    var entry = new ZipEntry(entryName) { DateTime = DateTime.Now };
                    zipStream.PutNextEntry(entry);
                    using (var fs = File.OpenRead(file))
                    {
                        int bytesRead;
                        do
                        {
                            bytesRead = fs.Read(buffer, 0, buffer.Length);
                            zipStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }
                }

                zipStream.Finish();
            }
        }

        /// <summary>
        /// 压缩为ZIP包
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="files">1:文件路径 2:ZIP内部路径</param>
        public static void CompressedZip(
            in string savePath,
            in IEnumerable<(string, string)> files)
        {
            var buffer = new byte[4096]; // 缓冲区大小
            using (var zipStream = new ZipOutputStream(File.Create(savePath)))
            {
                zipStream.SetLevel(5); // 设置压缩级别
                foreach (var (ePath, eName) in files)
                {
                    var entry = new ZipEntry(eName) { DateTime = DateTime.Now };
                    zipStream.PutNextEntry(entry);
                    using (var fs = File.OpenRead(ePath))
                    {
                        int bytesRead;
                        do
                        {
                            bytesRead = fs.Read(buffer, 0, buffer.Length);
                            zipStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }
                }

                zipStream.Finish();
            }
        }

        /// <summary>
        /// 压缩为ZIP包
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="files">文件信息 1:外部路径 2:ZIP内部路径</param>
        /// <param name="progressAction">进度回调</param>
        public static void CompressedZip(
            in string savePath,
            in ICollection<(string, string)> files,
            in Action<float> progressAction)
        {
            var buffer = new byte[4096]; // 缓冲区大小

            using (var zipStream = new ZipOutputStream(File.Create(savePath)))
            {
                zipStream.SetLevel(5); // 设置压缩级别
                var index = 0;
                foreach (var (ePath, eName) in files)
                {
                    var entry = new ZipEntry(eName) { DateTime = DateTime.Now };
                    zipStream.PutNextEntry(entry);

                    using (var fs = File.OpenRead(ePath))
                    {
                        int bytesRead;
                        do
                        {
                            bytesRead = fs.Read(buffer, 0, buffer.Length);
                            zipStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }

                    var progress = (float)(index++ + 1) / files.Count; // 计算当前压缩进度
                    progressAction.Invoke(progress); // 调用回调函数并传入当前进度
                }

                zipStream.Finish();
            }

            progressAction.Invoke(1f);
        }

        /// <summary>
        /// 压缩为ZIP包
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="files">1:文件路径 2:ZIP内部路径</param>
        public static void CompressedZip(
            in string savePath,
            in IEnumerable<(FileInfo, string)> files)
        {
            var buffer = new byte[4096]; // 缓冲区大小
            using (var zipStream = new ZipOutputStream(File.Create(savePath)))
            {
                zipStream.SetLevel(5); // 设置压缩级别
                foreach (var (ePath, eName) in files)
                {
                    var entry = new ZipEntry(eName) { DateTime = DateTime.Now };
                    zipStream.PutNextEntry(entry);
                    using (var fs = File.OpenRead(ePath.FullName))
                    {
                        int bytesRead;
                        do
                        {
                            bytesRead = fs.Read(buffer, 0, buffer.Length);
                            zipStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }
                }

                zipStream.Finish();
            }
        }

        /// <summary>
        /// 压缩为ZIP包
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="files">文件信息 1:外部路径 2:ZIP内部路径</param>
        /// <param name="progressAction">进度回调</param>
        public static void CompressedZip(
            in string savePath,
            in ICollection<(FileInfo, string)> files,
            in Action<float> progressAction)
        {
            var buffer = new byte[4096]; // 缓冲区大小

            using (var zipStream = new ZipOutputStream(File.Create(savePath)))
            {
                zipStream.SetLevel(5); // 设置压缩级别
                var index = 0;
                foreach (var (ePath, eName) in files)
                {
                    var entry = new ZipEntry(eName) { DateTime = DateTime.Now };
                    zipStream.PutNextEntry(entry);

                    using (var fs = File.OpenRead(ePath.FullName))
                    {
                        int bytesRead;
                        do
                        {
                            bytesRead = fs.Read(buffer, 0, buffer.Length);
                            zipStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead > 0);
                    }

                    var progress = (float)(index++ + 1) / files.Count; // 计算当前压缩进度
                    progressAction.Invoke(progress); // 调用回调函数并传入当前进度
                }

                zipStream.Finish();
            }

            progressAction.Invoke(1f);
        }
    }
}
