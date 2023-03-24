using System;

namespace AIO
{
    /// <summary>
    /// 模棱两可的运算符
    /// </summary>
    public sealed class AmbiguousOperatorException : OperatorException
    {
        /// <inheritdoc />
        public AmbiguousOperatorException(in string symbol, in Type leftType, in Type rightType) : base(
            $"Ambiguous use of operator '{symbol}' between types '{leftType?.ToString() ?? "null"}' and '{rightType?.ToString() ?? "null"}'.")
        {
        }
    }
}