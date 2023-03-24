using System;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    public partial class TypeExtend
    {
        /// <summary>
        /// 获取指定类型及其基类和接口实现的列表。
        /// </summary>
        /// <param name="type">要获取列表的类型。</param>
        /// <returns>包含指定类型及其基类和接口实现的 <see cref="IEnumerable{Type}"/> 对象。</returns>
        public static IEnumerable<Type> AndBaseTypeAndInterfaces(this Type type)
        {
            return type.Yield().Concat(type.BaseTypeAndInterfaces());
        }

        /// <summary>
        /// 获取指定类型的基类和实现的接口列表。
        /// </summary>
        /// <param name="type">要获取列表的类型。</param>
        /// <param name="inheritedInterfaces">指定是否包括继承的接口，默认为 true。</param>
        /// <returns>包含指定类型的基类和实现的接口列表的 <see cref="IEnumerable{Type}"/> 对象。</returns>
        public static IEnumerable<Type> BaseTypeAndInterfaces(this Type type, bool inheritedInterfaces = true)
        {
            var types = Enumerable.Empty<Type>();
            if (type.BaseType != null) types = types.Concat(type.BaseType.Yield());
            types = types.Concat(type.GetInterfaces(inheritedInterfaces));
            return types;
        }

        /// <summary>
        /// 获取指定类型及其实现的接口列表。
        /// </summary>
        /// <param name="type">要获取列表的类型。</param>
        /// <returns>包含指定类型及其实现的接口列表的 <see cref="IEnumerable{Type}"/> 对象。</returns>
        public static IEnumerable<Type> AndInterfaces(this Type type)
        {
            return type.Yield().Concat(type.GetInterfaces());
        }

        /// <summary>
        /// 获取指定类型及其继承层次结构中的所有类型列表。
        /// </summary>
        /// <param name="type">要获取列表的类型。</param>
        /// <returns>包含指定类型及其继承层次结构中的所有类型列表的 <see cref="IEnumerable{Type}"/> 对象。</returns>
        public static IEnumerable<Type> AndHierarchy(this Type type)
        {
            return type.Yield().Concat(type.Hierarchy());
        }

        /// <summary>
        /// 返回包含当前 <see cref="Type"/> 的单元素序列。
        /// </summary>
        /// <param name="t">要返回的类型。</param>
        /// <returns>一个包含指定类型的单元素序列。</returns>
        public static IEnumerable<Type> Yield(this Type t)
        {
            yield return t;
        }

        /// <summary>
        /// 获取指定类型及其继承层次结构中的所有类型列表，不包括实现的接口。
        /// </summary>
        /// <param name="type">要获取列表的类型。</param>
        /// <returns>包含指定类型及其继承层次结构中的所有类型列表（不包括实现的接口）的 <see cref="IEnumerable{Type}"/> 对象。</returns>
        public static IEnumerable<Type> Hierarchy(this Type type)
        {
            var baseType = type.BaseType;
            while (baseType != null)
            {
                yield return baseType;
                foreach (var @interface in baseType.GetInterfaces(false)) yield return @interface;
                baseType = baseType.BaseType;
            }
        }
    }
}