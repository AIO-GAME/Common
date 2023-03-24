using System;

namespace AIO
{
    /// <summary>
    /// 运算符异常
    /// </summary>
    public abstract class OperatorException : InvalidCastException
    {
        /// <inheritdoc />
        protected OperatorException() : base()
        {
        }

        /// <inheritdoc />
        protected OperatorException(in string message) : base(message)
        {
        }

        /// <inheritdoc />
        protected OperatorException(in string message, in Exception innerException) : base(message, innerException)
        {
        }
    }
}