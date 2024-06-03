#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 模棱两可的运算符
    /// </summary>
    public sealed class AExpAmbiguousAExpOperator : AExpOperator
    {
        /// <inheritdoc />
        public AExpAmbiguousAExpOperator(in string symbol, in Type leftType, in Type rightType) : base(
            $"Ambiguous use of operator '{symbol}' between types '{leftType?.ToString() ?? "null"}' and '{rightType?.ToString() ?? "null"}'.") { }
    }
}