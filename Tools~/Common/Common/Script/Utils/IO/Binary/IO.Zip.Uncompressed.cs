/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

public partial class Utils
{
    public partial class IO
    {
        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="path">解压包路径</param>
        /// <param name="targetDirectory">保存路径</param>
        /// <param name="progressAction">进度回调</param>
        /// <param name="entryAction">文件实体回调</param>
        /// <param name="restoreDateTime">是否恢复提取文件的日期和时间的标志</param>
        public static void UncompressedZip(
            in string path,
            string targetDirectory,
            Action<float> progressAction,
            Action<string> entryAction,
            in bool restoreDateTime = false)
        {
            UncompressedZip(path, targetDirectory, progressAction, entryAction, string.Empty, string.Empty,
                restoreDateTime);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="path">解压包路径</param>
        /// <param name="targetDirectory">保存路径</param>
        /// <param name="progressAction">进度回调</param>
        /// <param name="entryAction">文件实体回调</param>
        /// <param name="fileFilter">文件过滤器</param>
        /// <param name="restoreDateTime">是否恢复提取文件的日期和时间的标志</param>
        public static void UncompressedZip(
            in string path,
            string targetDirectory,
            Action<float> progressAction,
            Action<string> entryAction,
            in string fileFilter,
            in bool restoreDateTime = false)
        {
            UncompressedZip(path, targetDirectory, progressAction, entryAction, fileFilter, string.Empty,
                restoreDateTime);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="path">解压包路径</param>
        /// <param name="targetDirectory">保存路径</param>
        /// <param name="progressAction">进度回调</param>
        /// <param name="entryAction">文件实体回调</param>
        /// <param name="fileFilter">文件过滤器</param>
        /// <param name="directoryFilter">文件夹过滤器</param>
        /// <param name="restoreDateTime">是否恢复提取文件的日期和时间的标志</param>
        public static void UncompressedZip(
            in string path,
            string targetDirectory,
            Action<float> progressAction,
            Action<string> entryAction,
            in string fileFilter,
            in string directoryFilter,
            in bool restoreDateTime = false)
        {
            if (!File.Exists(path)) throw new ArgumentException("要解压的文件不存在！");
            if (!Directory.Exists(targetDirectory)) throw new ArgumentException("要解压到的目录不存在！");

            var fileCount = 0;
            using (var zipStream = new ZipInputStream(File.OpenRead(path)))
            {
                ZipEntry entry;
                while ((entry = zipStream.GetNextEntry()) != null)
                    if (entry.IsFile)
                        fileCount++;

                zipStream.Close();
            }

            var extractedFiles = 0;
            new FastZip().ExtractZip(path, targetDirectory, FastZip.Overwrite.Always, fileName =>
            {
                entryAction?.Invoke(Path.Combine(targetDirectory, fileName));
                extractedFiles++;
                var progress = (float)extractedFiles / fileCount;
                progressAction.Invoke(progress);
                return true;
            }, fileFilter, directoryFilter, restoreDateTime);

            progressAction?.Invoke(1f);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="path">解压包路径</param>
        /// <param name="targetDirectory">保存路径</param>
        /// <param name="progressAction">进度回调</param>
        /// <param name="fileFilter">文件过滤器</param>
        /// <param name="restoreDateTime">是否恢复提取文件的日期和时间的标志</param>
        public static void UncompressedZip(
            in string path,
            string targetDirectory,
            Action<float> progressAction,
            in string fileFilter,
            in bool restoreDateTime = false)
        {
            UncompressedZip(path, targetDirectory, progressAction, fileFilter, string.Empty, restoreDateTime);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="path">解压包路径</param>
        /// <param name="targetDirectory">保存路径</param>
        /// <param name="progressAction">进度回调</param>
        /// <param name="restoreDateTime">是否恢复提取文件的日期和时间的标志</param>
        public static void UncompressedZip(
            in string path,
            string targetDirectory,
            Action<float> progressAction,
            in bool restoreDateTime = false)
        {
            UncompressedZip(path, targetDirectory, progressAction, string.Empty, string.Empty, restoreDateTime);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="path">解压包路径</param>
        /// <param name="targetDirectory">保存路径</param>
        /// <param name="progressAction">进度回调</param>
        /// <param name="fileFilter">文件过滤器</param>
        /// <param name="directoryFilter">文件夹过滤器</param>
        /// <param name="restoreDateTime">是否恢复提取文件的日期和时间的标志</param>
        public static void UncompressedZip(
            in string path,
            string targetDirectory,
            Action<float> progressAction,
            in string fileFilter,
            in string directoryFilter,
            in bool restoreDateTime = false)
        {
            if (!File.Exists(path)) throw new ArgumentException("要解压的文件不存在！");
            if (!Directory.Exists(targetDirectory)) throw new ArgumentException("要解压到的目录不存在！");
            var fileCount = 0;
            using (var zipStream = new ZipInputStream(File.OpenRead(path)))
            {
                ZipEntry entry;
                while ((entry = zipStream.GetNextEntry()) != null)
                    if (entry.IsFile)
                        fileCount++;

                zipStream.Close();
            }

            var extractedFiles = 0;
            new FastZip().ExtractZip(path, targetDirectory, FastZip.Overwrite.Always, fileName =>
            {
                extractedFiles++;
                var progress = (float)extractedFiles / fileCount;
                progressAction.Invoke(progress);
                return true;
            }, fileFilter, directoryFilter, restoreDateTime);

            progressAction.Invoke(1f);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="path">解压包路径</param>
        /// <param name="targetDirectory">保存路径</param>
        /// <param name="restoreDateTime">是否恢复提取文件的日期和时间的标志</param>
        public static IEnumerator<string> UncompressedZip(
            in string path,
            string targetDirectory,
            in bool restoreDateTime = false)
        {
            return UncompressedZip(path, targetDirectory, string.Empty, string.Empty, restoreDateTime);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="path">解压包路径</param>
        /// <param name="targetDirectory">保存路径</param>
        /// <param name="fileFilter">文件过滤器</param>
        /// <param name="restoreDateTime">是否恢复提取文件的日期和时间的标志</param>
        public static IEnumerator<string> UncompressedZip(
            in string path,
            string targetDirectory,
            in string fileFilter,
            in bool restoreDateTime = false)
        {
            return UncompressedZip(path, targetDirectory, fileFilter, string.Empty, restoreDateTime);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="path">解压包路径</param>
        /// <param name="targetDirectory">保存路径</param>
        /// <param name="fileFilter">文件过滤器</param>
        /// <param name="directoryFilter">文件夹过滤器</param>
        /// <param name="restoreDateTime">是否恢复提取文件的日期和时间的标志</param>
        public static IEnumerator<string> UncompressedZip(
            in string path,
            string targetDirectory,
            in string fileFilter,
            in string directoryFilter,
            in bool restoreDateTime = false)
        {
            if (!File.Exists(path)) throw new ArgumentException("要解压的文件不存在！");
            if (!Directory.Exists(targetDirectory)) throw new ArgumentException("要解压到的目录不存在！");

            using (var zipStream = new ZipInputStream(File.OpenRead(path)))
            {
                while (zipStream.GetNextEntry() != null)
                {
                }

                zipStream.Close();
            }

            var list = new List<string>();
            new FastZip().ExtractZip(path, targetDirectory, FastZip.Overwrite.Always, fileName =>
            {
                list.Add(fileName);
                return true;
            }, fileFilter, directoryFilter, restoreDateTime);

            return list.GetEnumerator();
        }
    }
}