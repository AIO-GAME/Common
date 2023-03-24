/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    using ICSharpCode.SharpZipLib.Zip;
    using System;
    using System.IO;

    /// <summary>
    /// ZIP工具
    /// </summary>
    public static partial class IOUtils
    {
        //        /// <summary>
        //        /// 压缩为ZIP包
        //        /// </summary>
        //        /// <param name="Name"></param>
        //        /// <param name="Src"></param>
        //        /// <param name="Target"></param>
        //        public static void WriteZip(string Name, string Src, string Target, bool Override = false)
        //        {
        //            if (!ExistsFolder(Src)) return; //文件夹不存在

        //            if (!Name.Contains(".zip")) Name = string.Concat(Name, ".zip");
        //            var ZipFilePath = Path.Combine(Target, Name);
        //            if (Override)
        //            {   //Zip文件夹是否出现同名 如果同名 是否需要覆盖
        //                if (ExistsFile(ZipFilePath)) DeleteFile(ZipFilePath);
        //                ZipFile.Create(Src, ZipFilePath, CompressionLevel.Fastest, false);
        //            }
        //            else if (!ExistsFile(ZipFilePath))
        //            {
        //                ZipFile.CreateFromDirectory(Src, ZipFilePath, CompressionLevel.Fastest, false);
        //            }
        //        }

        //        /// <summary>
        //        /// 压缩
        //        /// </summary>
        //        /// <param name="files"></param>
        //        /// <param name="entryNames"></param>
        //        /// <param name="zip"></param>
        //        /// <param name="compression"></param>
        //        /// <param name="progressAction"></param>
        //        public static void WriteZip(List<string> files,
        //                                    List<string> entryNames,
        //                                    string zip,
        //                                    CompressionLevel compression,
        //                                    Action<float> progressAction = null)
        //        {
        //            if (!ExistsFolder(new FileInfo(zip).Directory.ToString()))
        //            {
        //                throw new ArgumentException("保存目录不存在");
        //            }
        //            foreach (string c in files)
        //            {
        //                if (!ExistsFile(c))
        //                {
        //                    throw new ArgumentException(string.Format("文件{0} 不存在！", c));
        //                }
        //            }
        //            if (ExistsFile(zip))
        //            {
        //                DeleteFile(zip);
        //            }
        //            try
        //            {
        //                using (ZipArchive za = ZipFile.Open(zip, ZipArchiveMode.Create))
        //                {
        //                    for (int i = 0; i < files.Count; i++)
        //                    {
        //                        var file = files[i];
        //                        string entryName = null;
        //                        if (entryNames != null)
        //                            entryName = entryNames[i];
        //                        if (string.IsNullOrEmpty(entryName))
        //                            entryName = GetFileName(file, false);
        //                        za.CreateEntryFromFile(file, entryName);
        //                    }
        //                }
        //            }
        //            catch (Exception e) { Console.WriteLine(e); }
        //        }

        ///// <summary>
        ///// 解压
        ///// </summary>
        ///// <param name="zip">解压包路径</param>
        ///// <param name="save">解压存放目录</param>
        //public static void ReadZip(string zip, string save)
        //{
        //    if (!ExistsFile(zip))
        //    {
        //        throw new ArgumentException("要解压的文件不存在。");
        //    }

        //    if (!ExistsFolder(save))
        //    {
        //        CreateFloder(save);
        //    }
        //    ZipFile.ExtractToDirectory(zip, save);
        //}

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="zip">解压包路径</param>
        /// <param name="save">保存路径</param>
        /// <param name="progressAction">进度回调</param>
        /// <param name="entryAction"></param>
        public static void ReadZip(string zip, string save, Action<float> progressAction = null, Action<string> entryAction = null)
        {
            if (!FileExists(zip))
            {
                throw new ArgumentException("要解压的文件不存在。");
            }

            if (!DirExists(save))
            {
                throw new ArgumentException("要解压到的目录不存在！");
            }

            long totalLength = 0;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zip)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    totalLength += theEntry.Size;
                }
            }

            long currentLength = 0;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zip)))
            {
                ZipEntry theEntry;
                byte[] data = new byte[2048];
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = Path.GetFileName(theEntry.Name);
                    if (string.IsNullOrEmpty(fileName))
                        continue;
                    string filePath = save + theEntry.Name;
                    var directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    using (FileStream streamWriter = File.Create(save + theEntry.Name))
                    {
                        int size = 2048;
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }

                            currentLength += size;
                            progressAction?.Invoke(currentLength * 1f / totalLength);
                        }

                        entryAction?.Invoke(save + theEntry.Name);
                    }
                }
            }
        }

        //        public static void ReadZip(Stream sizeStram,
        //                                      Stream stram,
        //                                      string path,
        //                                      Action<float> progressAction = null,
        //                                      Action<string> entryAction = null)
        //        {
        //            if (!ExistsFolder(path))
        //            {
        //                throw new ArgumentException("要解压到的目录不存在！");
        //            }

        //#if false
        //            long totalLength = 1;
        //            using (ZipInputStream s = new ZipInputStream(sizeStram))
        //            {
        //                ZipEntry theEntry;
        //                while ((theEntry = s.GetNextEntry()) != null)
        //                {
        //                    totalLength += theEntry.Size;
        //                }
        //            }

        //            long currentLength = 0;
        //            using (ZipInputStream s = new ZipInputStream(stram))
        //            {
        //                ZipEntry theEntry;
        //                byte[] data = new byte[2048];
        //                while ((theEntry = s.GetNextEntry()) != null)
        //                {
        //                    string fileName = Path.GetFileName(theEntry.Name);
        //                    if (string.IsNullOrEmpty(fileName))
        //                        continue;
        //                    string filePath = path + theEntry.Name;
        //                    var directory = Path.GetDirectoryName(filePath);
        //                    if (!Directory.Exists(directory))
        //                    {
        //                        Directory.CreateDirectory(directory);
        //                    }
        //                    using (FileStream streamWriter = File.Create(path + theEntry.Name))
        //                    {
        //                        int size = 2048;
        //                        while (true)
        //                        {
        //                            size = s.Read(data, 0, data.Length);
        //                            if (size > 0)
        //                            {
        //                                streamWriter.Write(data, 0, size);
        //                            }
        //                            else
        //                            {
        //                                break;
        //                            }
        //                            currentLength += size;
        //                            progressAction?.Invoke(currentLength * 1f / totalLength);
        //                        }
        //                        entryAction?.Invoke(path + theEntry.Name);
        //                    }
        //                }
        //            }
        //#endif
        //        }
    }
}