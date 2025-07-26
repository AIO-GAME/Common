namespace AIO.PY4N.Exception
{
    /// <summary>
    /// 拼音异常类
    /// </summary>
    public class PinyinException : System.Exception
    {
        /// <summary>
        /// 拼音异常构造函数
        /// </summary>
        /// <param name="message"></param>
        public PinyinException(string message) : base(message) { }
    }
}