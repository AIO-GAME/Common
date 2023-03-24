﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class TypeExtend
    {
        public static MemberInfo[] GetExtendedMembers(this Type type, BindingFlags flags)
        {
            var members = type.GetMembers(flags).ToHashSet();

            foreach (var extensionMethod in type.GetExtensionMethods())
            {
                members.Add(extensionMethod);
            }

            return members.ToArray();
        }

        private static readonly Lazy<ExtensionMethodCache> ExtensionMethodsCache;
        private static readonly Lazy<Dictionary<Type, MethodInfo[]>> InheritedExtensionMethodsCache;
        private static readonly Lazy<HashSet<MethodInfo>> GenericExtensionMethods;
        private static readonly List<Type> _types = new List<Type>();

        internal class ExtensionMethodCache
        {
            internal ExtensionMethodCache()
            {
                // Cache a list of all extension methods in assemblies
                // http://stackoverflow.com/a/299526
                Cache = _types
                    .Where(type => type.IsStatic() && !type.IsGenericType && !type.IsNested)
                    .SelectMany(type => type.GetMethods())
                    .Where(method => method.IsExtension())
                    .ToArray();
            }

            internal readonly MethodInfo[] Cache;
        }

        static TypeExtend()
        {
            ExtensionMethodsCache = new Lazy<ExtensionMethodCache>(() => new ExtensionMethodCache(), true);
            InheritedExtensionMethodsCache = new Lazy<Dictionary<Type, MethodInfo[]>>(() => new Dictionary<Type, MethodInfo[]>(), true);
            GenericExtensionMethods = new Lazy<HashSet<MethodInfo>>(() => new HashSet<MethodInfo>(), true);
        }

        private static IEnumerable<MethodInfo> GetInheritedExtensionMethods(Type thisArgumentType)
        {
            var methodInfos = ExtensionMethodsCache.Value.Cache;
            foreach (var extensionMethod in methodInfos)
            {
                var compatibleThis = extensionMethod.GetParameters()[0].ParameterType.IsMakeGenericTypeVia(thisArgumentType);

                if (compatibleThis)
                {
                    if (extensionMethod.ContainsGenericParameters)
                    {
                        var closedConstructedParameterTypes = thisArgumentType.Yield().Concat(extensionMethod.GetParametersWithoutThis().Select(p => p.ParameterType));

                        var closedConstructedMethod = extensionMethod.MakeGenericMethodVia(closedConstructedParameterTypes.ToArray());

                        GenericExtensionMethods.Value.Add(closedConstructedMethod);

                        yield return closedConstructedMethod;
                    }
                    else
                    {
                        yield return extensionMethod;
                    }
                }
            }
        }

        public static IEnumerable<MethodInfo> GetExtensionMethods(this Type thisArgumentType, bool inherited = true)
        {
            if (inherited)
            {
                lock (InheritedExtensionMethodsCache)
                {
                    if (!InheritedExtensionMethodsCache.Value.TryGetValue(thisArgumentType, out var inheritedExtensionMethods))
                    {
                        inheritedExtensionMethods = GetInheritedExtensionMethods(thisArgumentType).ToArray();
                        InheritedExtensionMethodsCache.Value.Add(thisArgumentType, inheritedExtensionMethods);
                    }

                    return inheritedExtensionMethods;
                }
            }
            else
            {
                var methodInfos = ExtensionMethodsCache.Value.Cache;
                return methodInfos.Where(method => method.GetParameters()[0].ParameterType == thisArgumentType);
            }
        }

        /// <summary>
        /// 获取指定类型及其基类中所有名为 <paramref name="memberName"/> 的成员列表。
        /// </summary>
        /// <param name="type">要获取成员列表的类型。</param>
        /// <param name="memberName">成员名称。</param>
        /// <returns>包含指定类型及其基类中所有名为 <paramref name="memberName"/> 的成员列表的 <see cref="MemberInfo"/> 数组。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MemberInfo[] GetFlattenedMember(this Type type, in string memberName)
        {
            var result = new List<MemberInfo>();

            while (type != null)
            {
                var members = type.GetDeclaredMembers();

                foreach (var item in members)
                {
                    if (item.Name == memberName)
                    {
                        result.Add(item);
                    }
                }

                type = type.Resolve().BaseType;
            }

            return result.ToArray();
        }

        /// <summary>
        /// 获取指定类型及其基类中第一个名为 <paramref name="methodName"/> 的方法。
        /// </summary>
        /// <param name="type">要获取方法的类型。</param>
        /// <param name="methodName">方法名称。</param>
        /// <returns>指定类型及其基类中第一个名为 <paramref name="methodName"/> 的方法，如果未找到则返回 null。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo GetFlattenedMethod(this Type type, in string methodName)
        {
            while (type != null)
            {
                var methods = type.GetDeclaredMethods();

                foreach (var item in methods)
                {
                    if (item.Name == methodName)
                    {
                        return item;
                    }
                }

                type = type.Resolve().BaseType;
            }

            return null;
        }

        /// <summary>
        /// 获取指定类型及其基类中所有名为 <paramref name="methodName"/> 的方法列表。
        /// </summary>
        /// <param name="type">要获取方法列表的类型。</param>
        /// <param name="methodName">方法名称。</param>
        /// <returns>包含指定类型及其基类中所有名为 <paramref name="methodName"/> 的方法列表的 <see cref="MethodInfo"/> 枚举。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetFlattenedMethods(this Type type, string methodName)
        {
            while (type != null)
            {
                var methods = type.GetDeclaredMethods();

                foreach (var item in methods)
                {
                    if (item.Name == methodName)
                    {
                        yield return item;
                    }
                }

                type = type.Resolve().BaseType;
            }
        }

        /// <summary>
        /// 获取指定类型及其基类中第一个名为 <paramref name="propertyName"/> 的属性。
        /// </summary>
        /// <param name="type">要获取属性的类型。</param>
        /// <param name="propertyName">属性名称。</param>
        /// <returns>指定类型及其基类中第一个名为 <paramref name="propertyName"/> 的属性，如果未找到则返回 null。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo GetFlattenedProperty(this Type type, in string propertyName)
        {
            while (type != null)
            {
                var properties = type.GetDeclaredProperties();

                foreach (var item in properties)
                {
                    if (item.Name == propertyName)
                    {
                        return item;
                    }
                }

                type = type.Resolve().BaseType;
            }

            return null;
        }

        /// <summary>
        /// 获取指定类型所实现的所有接口列表，可选择是否包括继承的接口。
        /// </summary>
        /// <param name="type">要获取接口列表的类型。</param>
        /// <param name="includeInherited">指定是否包括继承的接口。</param>
        /// <returns>包含指定类型所实现的所有接口列表的 <see cref="Type"/> 枚举。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetInterfaces(this Type type, in bool includeInherited)
        {
            if (includeInherited || type.BaseType == null)
            {
                return type.GetInterfaces();
            }

            return type.GetInterfaces().Except(type.BaseType.GetInterfaces());
        }

        /// <summary>
        /// 获取泛型列表类型的元素类型。如果无法确定元素类型，则返回 null（或 <see cref="System.Object"/>）。
        /// </summary>
        /// <param name="listType">要获取元素类型的泛型列表类型。</param>
        /// <param name="allowNonGeneric">指定当列表类型不是泛型列表类型时，是否返回 <see cref="System.Object"/> 作为元素类型。</param>
        /// <returns>泛型列表类型的元素类型，如果无法确定元素类型，则返回 null（或 <see cref="System.Object"/>）。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetListElementType(Type listType, in bool allowNonGeneric)
        {
            if (listType == null) throw new ArgumentNullException(nameof(listType));

            // http://stackoverflow.com/questions/4452590

            if (listType.IsArray) return listType.GetElementType();

            if (typeof(IList).IsAssignableFrom(listType))
            {
                var genericListInterface =
                    listType
                        .AndInterfaces()
                        .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IList<>));

                if (genericListInterface == null)
                {
                    if (allowNonGeneric)
                    {
                        return typeof(object);
                    }
                    else
                    {
                        return null;
                    }
                }

                return genericListInterface.GetGenericArguments()[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取可枚举类型的元素类型。如果无法确定元素类型，则返回 null（或 <see cref="System.Object"/>）。
        /// </summary>
        /// <param name="enumerableType">要获取元素类型的可枚举类型。</param>
        /// <param name="allowNonGeneric">指定当列表类型不是泛型列表类型时，是否返回 <see cref="System.Object"/> 作为元素类型。</param>
        /// <returns>可枚举类型的元素类型，如果无法确定元素类型，则返回 null（或 <see cref="System.Object"/>）。</returns>
        /// <see>
        ///     <cref>http://stackoverflow.com/a/12728562</cref>
        /// </see>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetEnumerableElementType(Type enumerableType, in bool allowNonGeneric)
        {
            if (enumerableType == null)
            {
                throw new ArgumentNullException(nameof(enumerableType));
            }

            if (typeof(IEnumerable).IsAssignableFrom(enumerableType))
            {
                var genericEnumerableInterface = enumerableType
                    .AndInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

                if (genericEnumerableInterface == null)
                {
                    if (allowNonGeneric) return typeof(object);
                    return null;
                }

                return genericEnumerableInterface.GetGenericArguments()[0];
            }

            return null;
        }

        /// <summary>
        /// 获取字典类型的键或值类型。如果无法确定键或值类型，则返回 null（或 <see cref="System.Object"/>）。
        /// </summary>
        /// <param name="dictionaryType">要获取键或值类型的字典类型。</param>
        /// <param name="allowNonGeneric">指定当字典类型不是泛型字典类型时，是否返回 <see cref="System.Object"/> 作为键或值类型。</param>
        /// <param name="genericArgumentIndex">指定要获取的泛型参数的索引。</param>
        /// <returns>字典类型的键或值类型，如果无法确定键或值类型，则返回 null（或 <see cref="System.Object"/>）。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetDictionaryItemType(Type dictionaryType, in bool allowNonGeneric, in int genericArgumentIndex)
        {
            if (dictionaryType == null) throw new ArgumentNullException(nameof(dictionaryType));

            if (typeof(IDictionary).IsAssignableFrom(dictionaryType))
            {
                var genericDictionaryInterface = dictionaryType
                    .AndInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));

                if (genericDictionaryInterface == null)
                {
                    if (allowNonGeneric) return typeof(object);
                    return null;
                }

                return genericDictionaryInterface.GetGenericArguments()[genericArgumentIndex];
            }

            return null;
        }

        /// <summary>
        /// 获取字典类型的键类型。如果无法确定键类型，则返回 null（或 <see cref="System.Object"/>）。
        /// </summary>
        /// <param name="dictionaryType">要获取键类型的字典类型。</param>
        /// <param name="allowNonGeneric">指定当字典类型不是泛型字典类型时，是否返回 <see cref="System.Object"/> 作为键类型。</param>
        /// <returns>字典类型的键类型，如果无法确定键类型，则返回 null（或 <see cref="System.Object"/>）。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetDictionaryKeyType(Type dictionaryType, in bool allowNonGeneric)
        {
            return GetDictionaryItemType(dictionaryType, allowNonGeneric, 0);
        }

        /// <summary>
        /// 获取字典类型的值类型。如果无法确定值类型，则返回 null（或 <see cref="System.Object"/>）。
        /// </summary>
        /// <param name="dictionaryType">要获取值类型的字典类型。</param>
        /// <param name="allowNonGeneric">指定当字典类型不是泛型字典类型时，是否返回 <see cref="System.Object"/> 作为值类型。</param>
        /// <returns>字典类型的值类型，如果无法确定值类型，则返回 null（或 <see cref="System.Object"/>）。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type GetDictionaryValueType(Type dictionaryType, in bool allowNonGeneric)
        {
            return GetDictionaryItemType(dictionaryType, allowNonGeneric, 1);
        }

        /// <summary>
        /// 安全地获取程序集中定义的所有类型。如果无法加载某些类型，则跳过它们并返回已成功加载的类型列表。
        /// </summary>
        /// <param name="assembly">要获取类型列表的程序集。</param>
        /// <returns>包含程序集中定义的所有已成功加载类型的 <see cref="Type"/> 枚举。</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypesSafely(this Assembly assembly)
        {
            Type[] types;

            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex) when (ex.Types.Any(t => t != null))
            {
                types = ex.Types.Where(t => t != null).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load types in assembly '{assembly}'.\n{ex}");
                yield break;
            }

            foreach (var type in types)
            {
                // Apparently void can be returned somehow:
                // http://support.ludiq.io/topics/483
                if (type == typeof(void))
                {
                    continue;
                }

                yield return type;
            }
        }
    }
}