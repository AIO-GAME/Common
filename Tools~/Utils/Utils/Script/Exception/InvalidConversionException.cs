using System;

namespace AIO
{
    /// <summary>
    /// 表示当进行无效类型转换时引发的异常。
    /// </summary>
    public class InvalidConversionException : InvalidCastException
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public InvalidConversionException()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvalidConversionException(in string message) : base(message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public InvalidConversionException(in string message, in Exception innerException)
            : base(message, innerException)
        {
        }
    }
}