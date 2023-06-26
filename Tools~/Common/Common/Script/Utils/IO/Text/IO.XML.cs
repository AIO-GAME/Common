using System.Runtime.CompilerServices;

public partial class UtilsGen
{
    public partial class IO
    {
        /// <summary>
        /// 读取 XML 文件 根据编码
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadXml<T>(
            in string path,
            in string charset = "utf-8")
        {
            return Xml.Deserialize<T>(ReadText(path, charset));
        }

        /// <summary>
        /// 读取 XML 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadXmlUTF8<T>(
            in string path)
        {
            return Xml.Deserialize<T>(ReadUTF8(path));
        }

        /// <summary>
        /// 写入 XML 文件 根据编码
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteXml<T>(
            in string path,
            in T value,
            in string charset = "utf-8")
        {
            if (value == null) return;
            WriteText(path, Xml.Serialize(value), charset);
        }

        /// <summary>
        /// 写入 XML 文件 编码utf-8
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteXmlUTF8<T>(
            in string path,
            in T value)
        {
            if (value == null) return;
            WriteUTF8(path, Xml.Serialize(value));
        }
    }
}
