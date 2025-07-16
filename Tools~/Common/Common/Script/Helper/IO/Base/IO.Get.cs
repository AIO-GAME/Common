#region

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 获取临时文件夹路径
            /// </summary>
            /// <param name="fileName">文件名</param>
            public static string GetTempPath(string fileName = null)
            {
                var temp                   = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                if (fileName != null) temp = Path.Combine(temp, fileName);
                return temp.Replace('\\', Path.AltDirectorySeparatorChar);
            }

            /// <summary>
            /// 获取指定路径相对于给定目录的相对路径。
            /// </summary>
            /// <param name="path">要获取其相对路径的文件或文件夹的路径。</param>
            /// <param name="directory">相对路径将基于此目录计算的目标目录。</param>
            /// <returns>相对路径字符串。</returns>
            /// <exception cref="ArgumentNullException">当 path 或 directory 为 null 时，抛出此异常。</exception>
            /// <exception cref="UriFormatException">使用 URI 库时，如果 path 或 directory 不是有效的 URI 字符串，则抛出此异常。</exception>
            public static string GetRelativePath(string path, string directory)
            {
                if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
                if (string.IsNullOrEmpty(directory)) throw new ArgumentNullException(nameof(directory));

                if (!directory.EndsWith(Path.AltDirectorySeparatorChar.ToString())) directory += Path.AltDirectorySeparatorChar;

                try
                {
                    // Optimization: Try a simple substring if possible
                    path      = path.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    directory = directory.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                    if (path.StartsWith(directory, StringComparison.Ordinal)) return path.Substring(directory.Length);
                    // Otherwise, use the URI library

                    var pathUri   = new Uri(path);
                    var folderUri = new Uri(directory);

                    return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('\\', Path.AltDirectorySeparatorChar));
                }
                catch (UriFormatException uriFormatException)
                {
                    throw new UriFormatException(
                                                 $"Failed to get relative path.\nPath: {path}\nDirectory:{directory}\n{uriFormatException}");
                }
            }

            /// <summary>
            /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5
            /// 实现稍微复杂，但可使用与传输文件或接收文件时同步计算MD5值
            /// 可自定义缓冲区大小，计算速度较快
            /// </summary>
            internal static string GetMD5ByHashAlgorithm(Stream stream, long bufferSize = 1024 * 16)
            {
                using (var inputStream = stream)
                {
                    using var hashAlgorithm = new MD5CryptoServiceProvider();
                    {
                        int readLength;                    //每次读取长度
                        var output = new byte[bufferSize]; //计算MD5
                        var buffer = new byte[bufferSize];
                        while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0) hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);

                        //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                        hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
                        var md5 = BitConverter.ToString(hashAlgorithm.Hash);
                        hashAlgorithm.Clear();
                        inputStream.Close();
                        return md5.Replace("-", "").ToLower();
                    }
                }
            }

            /// <summary>
            /// 获取.lnk文件的目标路径
            /// </summary>
            /// <param name="filepath"> .lnk文件的路径</param>
            /// <returns> .lnk文件的目标路径</returns>
            public static string GetLnkTargetPath(string filepath)
            {
                using var br = new BinaryReader(File.OpenRead(filepath));
                br.ReadBytes(0x14);            // skip the first 20 bytes (HeaderSize and LinkCLSID)
                uint lflags = br.ReadUInt32(); // read the LinkFlags structure (4 bytes)
                if ((lflags & 0x01) == 1)      // if the HasLinkTargetIDList bit is set then skip the stored IDList
                {                              // structure and header
                    br.ReadBytes(0x34);
                    var skip = br.ReadUInt16(); // this counts of how far we need to skip ahead
                    br.ReadBytes(skip);
                }

                var length = br.ReadUInt32(); // get the number of bytes the path contains
                br.ReadBytes(0x0C);           // skip 12 bytes (LinkInfoHeaderSize, LinkInfoFlgas, and VolumeIDOffset)
                var lbpos = br.ReadUInt32();  // Find the location of the LocalBasePath position
                // Skip to the path position
                // (subtract the length of the read (4 bytes), the length of the skip (12 bytes), and
                // the length of the lbpos read (4 bytes) from the lbpos)
                br.ReadBytes((int)lbpos - 0x14);
                var size     = length - lbpos - 0x02;
                var bytePath = br.ReadBytes((int)size);
                return Encoding.UTF8.GetString(bytePath, 0, bytePath.Length);
            }
        }

        #endregion
    }
}