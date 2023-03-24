using System.Reflection;

namespace AIO
{
    /// <summary>
    /// 属性信息扩展
    /// </summary>
    public static partial class PropertyInfoExtend
    {
        /// <summary>
        /// 是否为静态
        /// </summary>
        public static bool IsStatic(this PropertyInfo propertyInfo)
        {
            return (propertyInfo.GetGetMethod(true)?.IsStatic ?? false) ||
                   (propertyInfo.GetSetMethod(true)?.IsStatic ?? false);
        }
    }
}