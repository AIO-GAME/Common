namespace AIO.PY4N.Exception
{
    /// <summary>
    /// 转换拼音的字符非汉字字符
    /// </summary>
    public class UnsupportedUnicodeException : PinyinException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public UnsupportedUnicodeException(string message) : base(message) { }
    }
}