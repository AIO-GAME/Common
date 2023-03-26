using System;

namespace AIO
{
    /// <summary>
    /// 这是一个密封类 GenericClosingException，继承自 Exception
    /// </summary>
    public sealed class GenericClosingException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GenericClosingException(in string message) : base(message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GenericClosingException(in Type open, in Type closed) : base(
            $"Open-constructed type '{open}' is not assignable from closed-constructed type '{closed}'.")
        {
        }
    }
}