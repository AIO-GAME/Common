using System;

public static partial class UtilsGen
{
    /// <summary>
    /// 生成一个新的 GUID 对象。
    /// </summary>
    /// <returns>新的 GUID 对象。</returns>
    public static class Guid
    {
        /// <summary>
        /// 生成一个新的 GUID 对象。
        /// </summary>
        /// <returns>新的 GUID 对象。</returns>
        public static System.Guid New()
        {
            return System.Guid.NewGuid();
        }

        /// <summary>
        /// 生成一个新的 GUID 字符串。
        /// </summary>
        /// <param name="format">指定 GUID 字符串的格式，默认为 "N" 格式。</param>
        /// <returns>新的 GUID 字符串。</returns>
        public static string New(in string format)
        {
            if (format == null) throw new ArgumentNullException(nameof(format));
            return System.Guid.NewGuid().ToString(format);
        }

        /// <summary>
        /// 生成一个新的 Base64 编码的 GUID 字符串。
        /// </summary>
        /// <returns>新的 Base64 编码的 GUID 字符串。</returns>
        public static string NewBase64()
        {
            var s = Convert.ToBase64String(System.Guid.NewGuid().ToByteArray());
            return s.Substring(0, s.Length - 2);
        }
    }
}