#region

using System.Reflection;

#endregion

namespace AIO
{
    /// <summary>
    /// 为支持自定义属性的反映对象提供自定义属性。扩展
    /// </summary>
    public static class ExtendICustomAttributeProvider
    {
        /// <summary>
        /// 获取自定义属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="member">类型</param>
        /// <param name="inherit">为 true 时，查找继承的自定义特性的层次结构链。</param>
        /// <returns>属性集合</returns>
        public static T[] GetCustomAttributes<T>(this ICustomAttributeProvider member, in bool inherit)
        {
            return member.GetCustomAttributes(typeof(T), inherit) as T[];
        }

        /// <summary>
        /// Returns the first <typeparamref name="TAttribute"/> attribute on the `member` or <c>null</c> if there is none.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider member, bool inherit = false)
        where TAttribute : class
        {
            var type = typeof(TAttribute);
            if (member.IsDefined(type, inherit))
                return (TAttribute)member.GetCustomAttributes(type, inherit)[0];
            return null;
        }
    }
}