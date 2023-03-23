namespace AIO
{
    using System.Text;

    /// <summary>
    /// 字符扩展
    /// </summary>
    public static partial class CharExtend
    {
        /// <summary>
        /// 重复字符
        /// </summary>
        public static string Repeat(this char c, int repeat)
        {
            return new string(c, repeat);
        }

        /// <summary>
        /// 重复N此 复制传入数据
        /// </summary>
        public static string Clone(this char s, in int num)
        {
            if (num <= 0) return s.ToString();
            var builder = new StringBuilder();
            for (var i = 0; i < num; i++) builder.Append(s);
            return builder.ToString();
        }

        /// <summary>
        /// 重复N此 复制传入数据
        /// </summary>
        public static string Clone(this char s, in uint num)
        {
            if (num <= 0) return s.ToString();
            var builder = new StringBuilder();
            for (var i = 0; i < num; i++) builder.Append(s);
            return builder.ToString();
        }

    }
}