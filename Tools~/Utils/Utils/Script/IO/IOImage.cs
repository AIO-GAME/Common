/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System.IO;

    /// <summary>
    /// IO Image
    /// </summary>
    public static partial class IOUtils
    {
        /// <summary>
        /// 读取图片
        /// </summary>
        public static byte[] ReadPhoto(string path)
        {
            if (File.Exists(path))
            {
                var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                var br = new BinaryReader(fs);
                return br.ReadBytes((int)fs.Length);
            }
            return EMPTY_BYTES;
        }

    }
}
