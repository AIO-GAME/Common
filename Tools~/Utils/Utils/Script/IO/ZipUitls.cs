using System.IO;
using System.IO.Compression;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 
    /// </summary>
    public static class ZipUitls
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcFile"></param>
        /// <returns></returns>
        public static byte[] Compress(in string srcFile)
        {
            if (!File.Exists(srcFile)) return null;
            return Compress(File.ReadAllBytes(srcFile), 0);
        }

        /// <summary>
        /// 解压ZIP文件
        /// </summary>
        /// <param name="bts"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] bts, in int offset = 0)
        {
            if (bts == null) return null;
            using (var ms = new MemoryStream())
            {
                using (var gzip = new GZipStream(ms, CompressionLevel.Fastest))
                {
                    gzip.Write(bts, offset, bts.Length - offset);
                    gzip.Close();
                    ms.Flush();
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bts"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] bts, int offset = 0)
        {
            if (bts == null) return null;
            using (var ms = new MemoryStream(bts))
            {
                ms.Seek(offset, SeekOrigin.Begin);
                using (var gzip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    using (var oms = new MemoryStream())
                    {
                        gzip.CopyTo(oms);
                        oms.Flush();
                        return oms.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bts"></param>
        /// <param name="encoding"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string Decompress(byte[] bts, Encoding encoding, int offset = 0)
        {
            var r = Decompress(bts, offset);
            if (r == null) return string.Empty;
            return encoding.GetString(r);
        }
    }
}