/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// 类扩展
    /// </summary>
    /// <see>
    ///     <cref>https://docs.microsoft.com/en-us/dotnet/api/system.type?view=net-5.0</cref>
    /// </see>
    public static partial class TypeExtend
    {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

        /// <summary>
        /// 返回类型本身，具有一致的接口。
        /// </summary>
        /// <param name="type">要解析的类型</param>
        /// <returns>输入的类型实例本身</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type Resolve(this Type type)
        {
            return type;
        }

        /// <summary>
        /// 将对象的所有字段和属性重置为它们的默认值。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="fieldNames">要重置的字段名称（可选）</param>
        /// <param name="propNames">要重置的属性名称（可选）</param>
        /// <param name="resetReadOnly">是否重置只读字段和属性（默认为false）</param>
        /// <param name="typeFilter">用于过滤要重置的对象的类型（默认为null）</param>
        /// <param name="customFilter">自定义过滤器</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(
            this T obj,
            in ICollection<string> fieldNames,
            in ICollection<string> propNames,
            in Type typeFilter,
            in Func<Type, bool> customFilter,
            in bool resetReadOnly = false
        ) where T : class, new()
        {
            // 如果typeFilter不为null，则只重置符合条件的对象
            if (typeFilter != null && !typeFilter.IsInstanceOfType(obj)) return;

            // 如果customFilter不为null，则只重置符合条件的对象
            if (customFilter != null && !customFilter(obj.GetType())) return;

            if (fieldNames is null) throw new ArgumentNullException(nameof(fieldNames));
            if (propNames is null) throw new ArgumentNullException(nameof(propNames));

            ResetFields(obj, fieldNames, resetReadOnly);
            ResetProperties(obj, propNames, resetReadOnly);
        }

        /// <summary>
        /// 将对象的所有字段和属性重置为它们的默认值，可以根据自定义过滤器和类型过滤器进行筛选。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="fieldNames">要重置的字段名称（可选）</param>
        /// <param name="propNames">要重置的属性名称（可选）</param>
        /// <param name="customFilter">自定义过滤器</param>
        /// <param name="resetReadOnly">是否重置只读字段和属性（默认为false）</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(
            this T obj,
            in ICollection<string> fieldNames,
            in ICollection<string> propNames,
            in Func<Type, bool> customFilter,
            in bool resetReadOnly = false
        ) where T : class, new()
        {
            // 如果customFilter不为null，则只重置符合条件的对象
            if (customFilter != null && !customFilter(obj.GetType())) return;

            if (fieldNames is null) throw new ArgumentNullException(nameof(fieldNames));
            if (propNames is null) throw new ArgumentNullException(nameof(propNames));

            ResetFields(obj, fieldNames, resetReadOnly);
            ResetProperties(obj, propNames, resetReadOnly);
        }

        /// <summary>
        /// 将对象的所有字段和属性重置为它们的默认值，可以根据自定义过滤器和类型过滤器进行筛选。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="fieldNames">要重置的字段名称（可选）</param>
        /// <param name="propNames">要重置的属性名称（可选）</param>
        /// <param name="typeFilter">用于过滤要重置的对象的类型（默认为null）</param>
        /// <param name="resetReadOnly">是否重置只读字段和属性（默认为false）</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(
            this T obj,
            in ICollection<string> fieldNames,
            in ICollection<string> propNames,
            in Type typeFilter,
            in bool resetReadOnly = false
        ) where T : class, new()
        {
            // 如果typeFilter不为null，则只重置符合条件的对象
            if (typeFilter != null && !typeFilter.IsInstanceOfType(obj)) return;

            if (fieldNames is null) throw new ArgumentNullException(nameof(fieldNames));
            if (propNames is null) throw new ArgumentNullException(nameof(propNames));

            ResetFields(obj, fieldNames, resetReadOnly);
            ResetProperties(obj, propNames, resetReadOnly);
        }

        /// <summary>
        /// 将对象的所有字段和属性重置为它们的默认值，可以根据自定义过滤器和类型过滤器进行筛选。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="typeFilter">用于过滤要重置的对象的类型（默认为null）</param>
        /// <param name="customFilter">自定义过滤器</param>
        /// <param name="resetReadOnly">是否重置只读字段和属性（默认为false）</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(
            this T obj,
            in Type typeFilter,
            in Func<Type, bool> customFilter,
            in bool resetReadOnly = false
        ) where T : class, new()
        {
            // 如果typeFilter不为null，则只重置符合条件的对象
            if (typeFilter != null && !typeFilter.IsInstanceOfType(obj)) return;

            // 如果customFilter不为null，则只重置符合条件的对象
            if (customFilter != null && !customFilter(obj.GetType())) return;

            ResetFields(obj, resetReadOnly);
            ResetProperties(obj, resetReadOnly);
        }

        /// <summary>
        /// 将对象的所有字段和属性重置为它们的默认值，可以根据自定义过滤器和类型过滤器进行筛选。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="customFilter">自定义过滤器</param>
        /// <param name="resetReadOnly">是否重置只读字段和属性（默认为false）</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(
            this T obj,
            in Func<Type, bool> customFilter,
            in bool resetReadOnly = false
        ) where T : class, new()
        {
            // 如果customFilter不为null，则只重置符合条件的对象
            if (customFilter != null && !customFilter(obj.GetType())) return;

            ResetFields(obj, resetReadOnly);
            ResetProperties(obj, resetReadOnly);
        }

        /// <summary>
        /// 将对象的所有字段和属性重置为它们的默认值，可以根据自定义过滤器和类型过滤器进行筛选。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="typeFilter">用于过滤要重置的对象的类型（默认为null）</param>
        /// <param name="resetReadOnly">是否重置只读字段和属性（默认为false）</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(
            this T obj,
            in bool resetReadOnly,
            in Type typeFilter
        ) where T : class, new()
        {
            // 如果typeFilter不为null，则只重置符合条件的对象
            if (typeFilter != null && !typeFilter.IsInstanceOfType(obj)) return;

            ResetFields(obj, resetReadOnly);
            ResetProperties(obj, resetReadOnly);
        }

        /// <summary>
        /// 将对象的所有字段和属性重置为它们的默认值，可以根据自定义过滤器和类型过滤器进行筛选。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="fieldNames">要重置的字段名称（可选）</param>
        /// <param name="propNames">要重置的属性名称（可选）</param>
        /// <param name="resetReadOnly">是否重置只读字段和属性（默认为false）</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(
            this T obj,
            in ICollection<string> fieldNames,
            in ICollection<string> propNames,
            in bool resetReadOnly = false
        ) where T : class, new()
        {
            if (fieldNames is null) throw new ArgumentNullException(nameof(fieldNames));
            if (propNames is null) throw new ArgumentNullException(nameof(propNames));
            ResetFields(obj, fieldNames, resetReadOnly);
            ResetProperties(obj, propNames, resetReadOnly);
        }

        /// <summary>
        /// 将对象的所有字段和属性重置为它们的默认值，可以根据自定义过滤器和类型过滤器进行筛选。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="resetReadOnly">是否重置只读字段和属性（默认为false）</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Reset<T>(this T obj, in bool resetReadOnly = false) where T : class, new()
        {
            ResetFields(obj, resetReadOnly);
            ResetProperties(obj, resetReadOnly);
        }

        /// <summary>
        /// 重置指定字段名称的字段为它们的默认值。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="fieldNames">要重置的字段名称</param>
        /// <param name="resetReadOnly">是否重置只读字段（默认为false）</param>
        public static void ResetFields<T>(this T obj, in ICollection<string> fieldNames = null, in bool resetReadOnly = false)
        {
            // 遍历所有字段并将其设置为默认值
            foreach (var field in obj.GetType().GetFields(Flags))
            {
                // 排除静态字段、只读字段和继承自其他类的字段
                if (!field.IsStatic &&
                    (resetReadOnly || !field.IsInitOnly) &&
                    field.DeclaringType == obj.GetType())
                {
                    // 如果指定了要重置的字段名称，则只重置这些字段
                    if (fieldNames == null || fieldNames.Contains(field.Name))
                    {
                        try
                        {
                            // 如果字段是数组，则创建一个新数组，长度与原数组相同，并将其赋值给该字段
                            if (field.FieldType.IsArray)
                            {
                                var elementType = field.FieldType.GetElementType();
                                var newArray = Array.CreateInstance(elementType, ((Array)field.GetValue(obj)).Length);
                                field.SetValue(obj, newArray);
                            }
                            else // 如果字段不是数组，则创建一个新实例并将其赋值给该字段
                            {
                                field.SetValue(obj, Activator.CreateInstance(field.FieldType));
                            }
                        }
                        catch (Exception e)
                        {
                            // 如果发生异常，则打印错误信息并继续执行
                            Console.WriteLine($"Failed to reset field {field.Name}: {e.Message}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 重置指定字段名称的字段为它们的默认值。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="resetReadOnly">是否重置只读字段（默认为false）</param>
        public static void ResetFields<T>(this T obj, in bool resetReadOnly = false)
        {
            // 遍历所有字段并将其设置为默认值
            foreach (var field in obj.GetType().GetFields(Flags))
            {
                // 排除静态字段、只读字段和继承自其他类的字段
                if (!field.IsStatic &&
                    (resetReadOnly || !field.IsInitOnly) &&
                    field.DeclaringType == obj.GetType())
                {
                    try
                    {
                        // 如果字段是数组，则创建一个新数组，长度与原数组相同，并将其赋值给该字段
                        if (field.FieldType.IsArray)
                        {
                            var elementType = field.FieldType.GetElementType();
                            var newArray = Array.CreateInstance(elementType, ((Array)field.GetValue(obj)).Length);
                            field.SetValue(obj, newArray);
                        }
                        else // 如果字段不是数组，则创建一个新实例并将其赋值给该字段
                        {
                            field.SetValue(obj, Activator.CreateInstance(field.FieldType));
                        }
                    }
                    catch (Exception e)
                    {
                        // 如果发生异常，则打印错误信息并继续执行
                        Console.WriteLine($"Failed to reset field {field.Name}: {e.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 重置指定属性名称的属性为它们的默认值。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="resetReadOnly">是否重置只读属性（默认为false）</param>
        public static void ResetProperties<T>(this T obj, in bool resetReadOnly = false)
        {
            // 遍历所有属性并将其设置为默认值
            foreach (var prop in obj.GetType().GetProperties(Flags))
            {
                // 排除静态属性、只读属性和继承自其他类的属性
                if (prop.CanWrite &&
                    (resetReadOnly || prop.GetSetMethod(true) != null) &&
                    prop.DeclaringType == obj.GetType())
                {
                    try
                    {
                        // 创建一个新实例并将其赋值给该属性
                        prop.SetValue(obj, Activator.CreateInstance(prop.PropertyType));
                    }
                    catch (Exception e)
                    {
                        // 如果发生异常，则打印错误信息并继续执行
                        Console.WriteLine($"Failed to reset property {prop.Name}: {e.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 重置指定属性名称的属性为它们的默认值。
        /// </summary>
        /// <typeparam name="T">要重置的对象类型</typeparam>
        /// <param name="obj">要重置的对象</param>
        /// <param name="propNames">要重置的属性名称</param>
        /// <param name="resetReadOnly">是否重置只读属性（默认为false）</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetProperties<T>(this T obj, in ICollection<string> propNames, in bool resetReadOnly = false)
        {
            foreach (var prop in obj.GetType().GetProperties(Flags))
            {
                if (prop.CanWrite &&
                    (resetReadOnly || prop.GetSetMethod(true) != null) &&
                    prop.DeclaringType == obj.GetType())
                {
                    if (propNames == null || propNames.Contains(prop.Name))
                    {
                        try
                        {
                            prop.SetValue(obj, Activator.CreateInstance(prop.PropertyType));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Failed to reset property {prop.Name}: {e.Message}");
                        }
                    }
                }
            }
        }
    }
}