namespace AIO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// This wraps reflection types so that it is portable across different Unity
    /// runtimes.
    /// </summary>
    public static class fsPortableReflection
    {
        /// <summary>
        /// 空类型数组
        /// </summary>
        public static Type[] EmptyTypes = { };

        #region Attribute Queries

#if USE_TYPEINFO
        public static TAttribute GetAttribute<TAttribute>(Type type)
            where TAttribute : Attribute
        {
            return GetAttribute<TAttribute>(type.GetTypeInfo());
        }

        public static Attribute GetAttribute(Type type, Type attributeType)
        {
            return GetAttribute(type.GetTypeInfo(), attributeType, /*shouldCache:*/ false);
        }

        public static bool HasAttribute(Type type, Type attributeType)
        {
            return GetAttribute(type, attributeType) != null;
        }

#endif

        /// <summary>
        /// Returns true if the given attribute is defined on the given element.
        /// </summary>
        public static bool HasAttribute<TAttribute>(in MemberInfo element)
        {
            return HasAttribute(element, typeof(TAttribute));
        }

        /// <summary>
        /// Returns true if the given attribute is defined on the given element.
        /// </summary>
        public static bool HasAttribute<TAttribute>(in MemberInfo element, in bool shouldCache)
        {
            return HasAttribute(element, typeof(TAttribute), shouldCache);
        }

        /// <summary>
        /// Returns true if the given attribute is defined on the given element.
        /// </summary>
        public static bool HasAttribute(in MemberInfo element, in Type attributeType)
        {
            return HasAttribute(element, attributeType, true);
        }

        /// <summary>
        /// Returns true if the given attribute is defined on the given element.
        /// </summary>
        public static bool HasAttribute(in MemberInfo element, in Type attributeType, in bool shouldCache)
        {
            // LAZLO / LUDIQ FIX
            // Inheritance on property overrides. MemberInfo.IsDefined ignores the inherited parameter.
            // https://stackoverflow.com/questions/2520035
            return Attribute.IsDefined(element, attributeType, true);
            //return element.IsDefined(attributeType, true);
        }

        /// <summary>
        /// Fetches the given attribute from the given MemberInfo. This method
        /// applies caching and is allocation free (after caching has been
        /// performed).
        /// </summary>
        /// <param name="element">
        /// The MemberInfo the get the attribute from.
        /// </param>
        /// <param name="attributeType">The type of attribute to fetch.</param>
        /// <param name="shouldCache"></param>
        /// <returns>The attribute or null.</returns>
        public static Attribute GetAttribute(in MemberInfo element, in Type attributeType, in bool shouldCache)
        {
            var query = new AttributeQuery
            {
                MemberInfo = element,
                AttributeType = attributeType
            };

            Attribute attribute;
            if (_cachedAttributeQueries.TryGetValue(query, out attribute) == false)
            {
                // LAZLO / LUDIQ FIX
                // Inheritance on property overrides. MemberInfo.IsDefined ignores the inherited parameter
                //var attributes = element.GetCustomAttributes(attributeType, /*inherit:*/ true).ToArray();
                var attributes = Attribute.GetCustomAttributes(element, attributeType, true).ToArray();

                if (attributes.Length > 0)
                {
                    attribute = (Attribute)attributes[0];
                }
                if (shouldCache)
                {
                    _cachedAttributeQueries[query] = attribute;
                }
            }

            return attribute;
        }

        /// <summary>
        /// Fetches the given attribute from the given MemberInfo.
        /// </summary>
        /// <typeparam name="TAttribute">
        /// The type of attribute to fetch.
        /// </typeparam>
        /// <param name="element">
        /// The MemberInfo to get the attribute from.
        /// </param>
        /// <param name="shouldCache">
        /// Should this computation be cached? If this is the only time it will
        /// ever be done, don't bother caching.
        /// </param>
        /// <returns>The attribute or null.</returns>
        public static TAttribute GetAttribute<TAttribute>(in MemberInfo element, in bool shouldCache)
            where TAttribute : Attribute
        {
            return (TAttribute)GetAttribute(element, typeof(TAttribute), shouldCache);
        }

        /// <summary>
        /// Fetches the given attribute from the given MemberInfo.
        /// </summary>
        /// <typeparam name="TAttribute">
        /// The type of attribute to fetch.
        /// </typeparam>
        /// <param name="element">
        /// The MemberInfo to get the attribute from.
        /// </param>
        /// <param name="shouldCache">
        /// Should this computation be cached? If this is the only time it will
        /// ever be done, don't bother caching.
        /// </param>
        /// <returns>The attribute or null.</returns>
        public static TAttribute GetAttribute<TAttribute>(in MemberInfo element)
            where TAttribute : Attribute
        {
            return GetAttribute<TAttribute>(element, /*shouldCache:*/ true);
        }

        private struct AttributeQuery
        {
            public MemberInfo MemberInfo;
            public Type AttributeType;
        }

        private static IDictionary<AttributeQuery, Attribute> _cachedAttributeQueries =
            new Dictionary<AttributeQuery, Attribute>(new AttributeQueryComparator());

        private class AttributeQueryComparator : IEqualityComparer<AttributeQuery>
        {
            public bool Equals(AttributeQuery x, AttributeQuery y)
            {
                return
                    x.MemberInfo == y.MemberInfo &&
                    x.AttributeType == y.AttributeType;
            }

            public int GetHashCode(AttributeQuery obj)
            {
                return
                    obj.MemberInfo.GetHashCode() +
                    (17 * obj.AttributeType.GetHashCode());
            }
        }

        #endregion Attribute Queries

        /// <summary>
        /// 转化为成员信息
        /// </summary>
        public static MemberInfo AsMemberInfo(Type type)
        {
            return type;
        }

        /// <summary>
        /// 判断是否为类型
        /// </summary>
        public static bool IsType(MemberInfo member)
        {
            return member is Type;
        }

        public static Type AsType(MemberInfo member)
        {
            return (Type)member;
        }

        #region Extensions

#if USE_TYPEINFO_EXTENSIONS
        public static bool IsAssignableFrom(this Type parent, Type child)
        {
            return parent.GetTypeInfo().IsAssignableFrom(child.GetTypeInfo());
        }

        public static Type GetElementType(this Type type)
        {
            return type.GetTypeInfo().GetElementType();
        }

        public static MethodInfo GetSetMethod(this PropertyInfo member, bool nonPublic = false)
        {
            // only public requested but the set method is not public
            if (nonPublic == false && member.SetMethod != null && member.SetMethod.IsPublic == false) return null;

            return member.SetMethod;
        }

        public static MethodInfo GetGetMethod(this PropertyInfo member, bool nonPublic = false)
        {
            // only public requested but the set method is not public
            if (nonPublic == false && member.GetMethod != null && member.GetMethod.IsPublic == false) return null;

            return member.GetMethod;
        }

        public static MethodInfo GetBaseDefinition(this MethodInfo method)
        {
            return method.GetRuntimeBaseDefinition();
        }

        public static Type[] GetInterfaces(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces.ToArray();
        }

        public static Type[] GetGenericArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments.ToArray();
        }

#endif

        #endregion Extensions
    }
}
