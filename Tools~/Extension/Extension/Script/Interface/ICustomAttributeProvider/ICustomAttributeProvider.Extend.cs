namespace AIO
{
    using System.Reflection;

    /// <summary>
    /// 为支持自定义属性的反映对象提供自定义属性。扩展
    /// </summary>
    public static class ICustomAttributeProviderExtend
    {
        /// <summary>
        /// 获取自定义属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attributeType">类型</param>
        /// <param name="inherit">为 true 时，查找继承的自定义特性的层次结构链。</param>
        /// <returns>属性集合</returns>
        public static T[] GetCustomAttributes<T>(this ICustomAttributeProvider attributeType, in bool inherit)
        {
            return attributeType.GetCustomAttributes(typeof(T), inherit) as T[];
        }
    }
}
