using System.Text;

public partial class AHelper
{
    public partial class IO
    {
        /// <summary>
        /// 读取 Base64 文件 根据编码
        /// </summary>
        public static T ReadBase64<T>(
            in string path,
            Encoding charset = null)
        {
            return Base64.Deserialize<T>(ReadText(path, charset));
        }

        /// <summary>
        /// 读取 Base64 文件 编码utf-8
        /// </summary>
        public static T ReadBase64UTF8<T>(
            in string path)
        {
            return Base64.Deserialize<T>(ReadUTF8(path));
        }

        /// <summary>
        /// 写入 Base64 文件 根据编码
        /// </summary>
        public static void WriteBase64<T>(
            in string path,
            in T value,
            Encoding charset = null) where T : struct
        {
            WriteText(path, Base64.Serialize(value), charset);
        }

        /// <summary>
        /// 写入 Base64 文件 编码utf-8
        /// </summary>
        public static void WriteBase64UTF8<T>(
            in string path,
            in T value) where T : struct
        {
            WriteUTF8(path, Base64.Serialize(value));
        }

        /// <summary>
        /// 写入 Base64 文件 编码utf-8
        /// </summary>
        public static void WriteBase64UTF8<T>(
            in string path,
            T value) where T : class
        {
            if (value == null) return;
            WriteUTF8(path, Base64.Serialize(value));
        }
    }
}