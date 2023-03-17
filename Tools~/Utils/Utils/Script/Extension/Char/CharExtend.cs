namespace AIO
{
    using System.Text;

    public static partial class CharExtend
    {
        /// <summary>
        /// 重复字符
        /// </summary>
        public static string Repeat(this char c, int repeat)
        {
            return new string(c, repeat);
        }
    }
}
