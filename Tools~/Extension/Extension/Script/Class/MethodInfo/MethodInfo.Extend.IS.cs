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
        public static bool IsUserDefinedConversion(this MethodInfo method)
        {
            return method.IsSpecialName && (method.Name == "op_Implicit" || method.Name == "op_Explicit");
        }
        
        public static bool IsExtensionMethod(this MemberInfo memberInfo)
        {
            return memberInfo is MethodInfo methodInfo && methodInfo.IsExtension();
        }
        
        public static bool IsExtension(this MethodInfo methodInfo)
        {
            return methodInfo.HasAttribute<ExtensionAttribute>(false);
        }

        public static bool HasAttribute<TAttribute>(this MemberInfo element, bool inherit = true)
            where TAttribute : Attribute
        {
            return element.IsDefined(typeof(TAttribute), inherit);
        }

        /// <summary>
        /// 是否为静态
        /// </summary>
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