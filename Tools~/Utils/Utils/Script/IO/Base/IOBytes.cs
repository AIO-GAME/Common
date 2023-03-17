/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System.Threading.Tasks;

namespace AIO
{
    /// <summary>
    /// IO ByteBuffer
    /// </summary>
    public static partial class IOUtils
    {
        #region Read

        /// <summary>
        /// 加载 Byte Array
        /// </summary>
        /// <param name="path">路径</param>
        public static byte[] ReadByteArray(string path)
        {
            return ReadFile(path);
        }

        /// <summary>
        /// 异步 加载 Byte Array
        /// </summary>
        /// <param name="path">路径</param>
        public static async Task<byte[]> ReadByteArrayAsync(string path)
        {
            return await ReadFileAsync(path);
        }

        #endregion


        //---------------------------------------------------------------------------------//

        #region Write

        /// <summary>
        /// 写入数据
        /// </summary>
        public static bool WriteByteArray(string path, byte[] bytes, bool coded = false, bool conact = false)
        {
            if (coded)
                for (var i = 0; i < bytes.Length; i++)
                    bytes[i] = EncodingBitByte(bytes[i]);
            return WriteFile(path, bytes, conact);
        }

        /// <summary>
        /// 异步 写入数据
        /// </summary>
        public static async Task<bool> WriteByteArrayAsync(string path, byte[] bytes, bool coded = false, bool conact = false)
        {
            if (coded)
                for (var i = 0; i < bytes.Length; i++)
                    bytes[i] = EncodingBitByte(bytes[i]);
            return await WriteFileAsync(path, bytes, conact);
        }

        #endregion
    }
}