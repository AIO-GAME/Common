using System;

namespace AIO
{
    /// <summary>
    /// 运算符异常
    /// </summary>
    public abstract class AExpOperator : InvalidCastException
    {
        /// <inheritdoc />
        protected AExpOperator() : base()
        {
        }

        /// <inheritdoc />
        protected AExpOperator(in string message) : base(message)
        {
        }

        /// <inheritdoc />
        protected AExpOperator(in string message, in Exception innerException) : base(message, innerException)
        {
        }
    }
}