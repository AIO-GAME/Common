/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-07-03
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;

namespace AIO
{
    public partial class RHelper
    {
        public partial class IO
        {
            private const int ZIP_LEVEL = 0;
            private const string PACKAGE_PREFIX = "patch_";

            /// <summary>
            /// 打包文件，到目录
            /// </summary>
            public static IReadOnlyList<string> PackageFiles(
                int startIndex,
                IReadOnlyList<string> files,
                string sourcePath,
                string outPath,
                int mb = 10)
            {
                return CreateZipFiles(startIndex, files, sourcePath, outPath, PACKAGE_PREFIX, mb);
            }

            /// <summary>
            /// 把数据解压成文件
            /// </summary>
            public static int UnPackageBytes(byte[] bytes, string outPath)
            {
                var r = -1;
                try
                {
                    UnZip(bytes, outPath);
                    r = 0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return r;
            }

            /// <summary>
            /// 解包文件
            /// </summary>
            public static int UnPackageFile(string packageFile, string outPath)
            {
                byte[] buffer = null;
                var fileStream = File.OpenRead(packageFile);
                if (fileStream != null)
                {
                    buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                }

                return UnPackageBytes(buffer, outPath);
            }

            public static List<string> CreateZipFiles(
                int startIndex,
                IReadOnlyList<string> files,
                string sourcePath,
                string outPath,
                string prefixName,
                int mb)
            {
                ZipOutputStream zipStream = null;
                var packageFiles = new List<string>();
                var crc = new Crc32();
                var count = startIndex;
                for (int i = 0, max = files.Count; i < max; ++i)
                {
                    var file = files[i];
                    var fullname = string.Concat(sourcePath, file);
                    if (File.Exists(fullname))
                    {
                        var fileStream = File.OpenRead(fullname);
                        if (zipStream == null ||
                            (zipStream.Length != 0 && (zipStream.Length + fileStream.Length) > (1024 * 1024 * mb)))
                        {
                            if (zipStream != null)
                            {
                                zipStream.Finish();
                                zipStream.Close();
                                zipStream = null;
                            }

                            var f = string.Concat(prefixName, count++, @".pb");
                            packageFiles.Add(f);
                            zipStream = new ZipOutputStream(File.Create(string.Concat(outPath, f)));
                            zipStream.SetLevel(ZIP_LEVEL);
                        }

                        var buffer = new byte[fileStream.Length];
                        fileStream.Read(buffer, 0, buffer.Length);
                        var entry = new ZipEntry(file);
                        entry.DateTime = DateTime.Now;
                        entry.Size = fileStream.Length;
                        fileStream.Close();
                        crc.Reset();
                        crc.Update(buffer);
                        entry.Crc = crc.Value;
                        zipStream.PutNextEntry(entry);
                        zipStream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        Console.WriteLine("Not Exits File:" + fullname);
                    }
                }

                if (zipStream != null)
                {
                    zipStream.Finish();
                    zipStream.Close();
                }

                return packageFiles;
            }

            public static void UnZip(byte[] bytes, string directory)
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var zis = new ZipInputStream(new MemoryStream(bytes)))
                {
                    ZipEntry theEntry = null;
                    while ((theEntry = zis.GetNextEntry()) != null)
                    {
                        var directoryName = System.IO.Path.GetDirectoryName(theEntry.Name);
                        var fileName = System.IO.Path.GetFileName(theEntry.Name);
                        if (directoryName != string.Empty)
                        {
                            if (!Directory.Exists(directory + @"/" + directoryName))
                            {
                                Directory.CreateDirectory(directory + @"/" + directoryName);
                            }
                        }

                        if (fileName != string.Empty)
                        {
                            var fullName = System.IO.Path.Combine(directory, theEntry.Name);
                            var isExitst = File.Exists(fullName);
                            FileStream streamWriter = null;
                            if (!isExitst) streamWriter = File.Create(fullName);
                            var size = 2048;
                            var data = new byte[2048];
                            while (true)
                            {
                                size = zis.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    if (!isExitst)
                                        streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            if (!isExitst)
                            {
                                streamWriter.Close();
                                streamWriter.Dispose();
                            }
                        }
                    }

                    zis.Close();
                    zis.Dispose();
                }
            }
        }
    }
}
