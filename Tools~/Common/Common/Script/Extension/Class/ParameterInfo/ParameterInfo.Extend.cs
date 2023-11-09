using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 参数信息扩展
    /// </summary>
    public static partial class ExtendParameterInfo
    {
        /// <summary>
        /// 是否为Out
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasOutModifier(this ParameterInfo parameterInfo)
        {
            if (parameterInfo is null) throw new ArgumentNullException(nameof(parameterInfo));
            return parameterInfo.IsOut && parameterInfo.ParameterType.IsByRef;
        }
    }
}