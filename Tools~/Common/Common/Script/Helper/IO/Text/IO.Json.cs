using System.Runtime.CompilerServices;

public partial class UtilsGen
{
    public partial class IO
    {
        /// <summary>
        /// 读取Json文件 根据编码
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadJson<T>(
            in string path,
            in string charset = "utf-8")
        {
            return Json.Deserialize<T>(ReadText(path, charset));
        }

        /// <summary>
        /// 读取Json文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadJsonUTF8<T>(in string path)
        {
            return Json.Deserialize<T>(ReadUTF8(path));
        }

        /// <summary>
        /// 写入Json文件 根据编码
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteJson<T>(
            in string path,
            in T value,
            in bool concat = false,
            in string charset = "utf-8")
        {
            if (value == null) return;
            WriteText(path, Json.Serialize(value), charset, concat);
        }

        /// <summary>
        /// 写入Json文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteJsonUTF8<T>(
            in string path,
            in T text,
            in bool concat = false)
        {
            if (text == null) return;
            WriteUTF8(path, Json.Serialize(text), concat);
        }
    }
}
