using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 函数扩展
    /// </summary>
    public static partial class MethodBaseExtend
    {
        /// <summary>
        /// 获取一个方法的参数信息，但不包括扩展方法中的第一个“this”参数。
        /// </summary>
        /// <param name="methodBase">要获取参数信息的方法。</param>
        /// <returns>方法的参数信息（不包括扩展方法中的第一个“this”参数）。</returns>
        /// <remarks>如果方法是扩展方法，则跳过第一个参数（即“this”参数）。</remarks>
        /// <inheritdoc cref="MethodBase"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ParameterInfo> GetParametersWithoutThis(this MethodBase methodBase)
        {
            return methodBase.GetParameters().Skip(methodBase.IsExtensionMethod() ? 1 : 0);
        }
    }
}