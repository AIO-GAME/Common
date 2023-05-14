using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class MethodInfoExtend
    {
        /// <summary>
        /// 检查给定方法是否为用户定义的转换运算符。
        /// </summary>
        /// <param name="method">要检查的方法</param>
        /// <returns>如果方法为用户定义的转换运算符，则为 true；否则为 false。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUserDefinedConversion(this MethodInfo method)
            => method.IsSpecialName && (method.Name == "op_Implicit" || method.Name == "op_Explicit");

        /// <summary>
        /// 判断一个成员是否为扩展方法。
        /// </summary>
        /// <param name="memberInfo">要判断的成员。</param>
        /// <returns>如果成员是扩展方法，则返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 扩展方法必须定义在静态类中，并且必须被 static 关键字修饰。
        /// </remarks>
        /// <inheritdoc cref="MemberInfo"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsExtensionMethod(this MemberInfo memberInfo)
            => memberInfo is MethodInfo methodInfo && methodInfo.IsExtension();

        /// <summary>
        /// 判断一个方法是否为扩展方法。
        /// </summary>
        /// <param name="methodInfo">要判断的方法。</param>
        /// <returns>如果方法是扩展方法，则返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 扩展方法必须定义在静态类中，并且必须被 static 关键字修饰。
        /// </remarks>
        /// <inheritdoc cref="MethodInfo"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsExtension(this MethodInfo methodInfo)
            => methodInfo.HasAttribute<ExtensionAttribute>(false);

        /// <summary>
        /// 判断指定的元素是否具有指定类型的特性。
        /// </summary>
        /// <typeparam name="TAttribute">要判断的特性类型。</typeparam>
        /// <param name="element">要判断的元素。</param>
        /// <param name="inherit">指示是否搜索继承链以查找特性。</param>
        /// <returns>如果元素具有指定类型的特性，则返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 特性是在编译时确定的元数据，可以用于为程序集、模块、类型、成员等元素添加元数据。
        /// </remarks>
        /// <inheritdoc cref="MemberInfo"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttribute<TAttribute>(this MemberInfo element, bool inherit = true) where TAttribute : Attribute
            => element.IsDefined(typeof(TAttribute), inherit);

        /// <summary>
        /// 判断一个成员是否为静态成员。
        /// </summary>
        /// <param name="memberInfo">要判断的成员。</param>
        /// <returns>如果成员是静态成员，则返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 静态成员属于类型，而不是对象。静态成员可以通过类型名或实例访问。
        /// </remarks>
        /// <inheritdoc cref="MemberInfo"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsStatic(this MemberInfo memberInfo)
        {
            switch (memberInfo)
            {
                case FieldInfo info:
                    return info.IsStatic;
                case PropertyInfo info:
                    return info.IsStatic();
                case MethodBase @base:
                    return @base.IsStatic;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}