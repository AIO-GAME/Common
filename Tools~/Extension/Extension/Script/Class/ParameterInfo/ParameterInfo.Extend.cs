using System;
using System.Reflection;

namespace AIO
{
    /// <summary>
    /// 参数信息扩展
    /// </summary>
    public static partial class ParameterInfoExtend
    {
        /// <summary>
        /// 是否为Out
        /// </summary>
        public static bool HasOutModifier(this ParameterInfo parameterInfo)
        {
            if (parameterInfo is null) throw new ArgumentNullException(nameof(parameterInfo));
            return parameterInfo.IsOut && parameterInfo.ParameterType.IsByRef;
        }
    }
}