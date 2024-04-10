#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 无效的运算符
    /// </summary>
    public sealed class AexpInvalidAExpOperator : AExpOperator
    {
        /// <inheritdoc />
        public AexpInvalidAExpOperator(in string symbol, in Type type) : base(
            $"Operator '{symbol}' cannot be applied to operand of type '{type?.ToString() ?? "null"}'.") { }

        /// <inheritdoc />
        public AexpInvalidAExpOperator(in string symbol, in Type leftType, in Type rightType) : base(
            $"Operator '{symbol}' cannot be applied to operands of type '{leftType?.ToString() ?? "null"}' and '{rightType?.ToString() ?? "null"}'.") { }
    }
}