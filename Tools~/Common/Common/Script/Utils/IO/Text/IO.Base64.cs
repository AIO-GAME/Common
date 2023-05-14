using System.Runtime.CompilerServices;

public partial class Utils
{
    public partial class IO
    {
        /// <summary>
        /// 读取 Base64 文件 根据编码
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadBase64<T>(
            in string path,
            in string charset = "utf-8")
        {
            return Base64.Deserialize<T>(ReadText(path, charset));
        }

        /// <summary>
        /// 读取 Base64 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadBase64UTF8<T>(
            in string path)
        {
            return Base64.Deserialize<T>(ReadUTF8(path));
        }

        /// <summary>
        /// 写入 Base64 文件 根据编码
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteBase64<T>(
            in string path,
            in T value,
            in string charset = "utf-8")
        {
            if (value == null) return;
            WriteText(path, Base64.Serialize(value), charset);
        }

        /// <summary>
        /// 写入 Base64 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteBase64UTF8<T>(
            in string path,
            in T value)
        {
            if (value == null) return;
            WriteUTF8(path, Base64.Serialize(value));
        }
    }
}
