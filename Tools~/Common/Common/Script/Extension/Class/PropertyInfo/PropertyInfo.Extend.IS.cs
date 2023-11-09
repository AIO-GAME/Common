using System.Reflection;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 属性信息扩展
    /// </summary>
    public static partial class ExtendPropertyInfo
    {
        /// <summary>
        /// 是否为静态
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsStatic(this PropertyInfo propertyInfo)
        {
            return (propertyInfo.GetGetMethod(true)?.IsStatic ?? false) ||
                   (propertyInfo.GetSetMethod(true)?.IsStatic ?? false);
        }
    }
}