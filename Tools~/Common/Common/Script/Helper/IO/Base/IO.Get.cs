/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.IO;
using System.Security.Cryptography;

namespace AIO
{
    public partial class AHelper
    {
        public partial class IO
        {
            /// <summary>
            /// 获取临时文件夹路径
            /// </summary>
            /// <param name="fileName">文件名</param>
            public static string GetTempPath(string fileName = null)
            {
                var temp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                if (fileName != null) temp = Path.Combine(temp, fileName);
                return temp;
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

                if (!directory.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    directory += Path.DirectorySeparatorChar;
                }

                try
                {
                    // Optimization: Try a simple substring if possible
                    path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                    directory = directory.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

                    if (path.StartsWith(directory, StringComparison.Ordinal))
                    {
                        return path.Substring(directory.Length);
                    }

                    // Otherwise, use the URI library

                    var pathUri = new Uri(path);
                    var folderUri = new Uri(directory);

                    return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString()
                        .Replace('/', Path.DirectorySeparatorChar));
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
                        int readLength; //每次读取长度
                        var output = new byte[bufferSize]; //计算MD5
                        var buffer = new byte[bufferSize];
                        while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
                        }

                        //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
                        hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
                        var md5 = BitConverter.ToString(hashAlgorithm.Hash);
                        hashAlgorithm.Clear();
                        inputStream.Close();
                        return md5.Replace("-", "").ToLower();
                    }
                }
            }
        }
    }
}