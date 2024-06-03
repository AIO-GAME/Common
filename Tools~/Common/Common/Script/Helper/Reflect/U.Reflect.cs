#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: Reflect

        /// <summary>
        /// 反射工具库
        /// </summary>
        public static class Reflect
        {
            /// <summary>
            /// 过滤枚举过时字段
            /// </summary>
            /// <typeparam name="T">泛型</typeparam>
            /// <returns>泛型列表</returns>
            public static ICollection<T> GetEnumFilterOBS<T>()
            {
                return (from T target in Enum.GetValues(typeof(T))
                        let info = typeof(T).GetField(target.ToString())
                        let attribute = info.GetCustomAttribute<ObsoleteAttribute>()
                        where attribute == null
                        select target
                    ).ToArray();
            }

            /// <summary>
            /// 过滤过时属性
            /// </summary>
            /// <typeparam name="T">泛型</typeparam>
            /// <returns>属性列表</returns>
            public static ICollection<PropertyInfo> GetPropertyFilterOBS<T>(
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public)
            {
                return (from info in typeof(T).GetProperties(flags)
                        let attribute = info.GetCustomAttribute<ObsoleteAttribute>()
                        where attribute == null
                        select info
                    ).ToArray();
            }

            /// <summary>
            /// 过滤过时字段
            /// </summary>
            /// <typeparam name="T">泛型</typeparam>
            /// <returns>属性列表</returns>
            public static ICollection<FieldInfo> GetFieldFilterOBS<T>(
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public)
            {
                return (from info in typeof(T).GetFields(flags)
                        let attribute = info.GetCustomAttribute<ObsoleteAttribute>()
                        where attribute == null
                        select info
                    ).ToArray();
            }

            /// <summary>
            /// 过滤成员字段
            /// </summary>
            /// <typeparam name="T">泛型</typeparam>
            /// <returns>属性列表</returns>
            public static ICollection<MemberInfo> GetMemberFilterOBS<T>(
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public)
            {
                return (from info in typeof(T).GetMembers(flags)
                        let attribute = info.GetCustomAttribute<ObsoleteAttribute>()
                        where attribute == null
                        select info
                    ).ToArray();
            }

            /// <summary>
            /// 过滤函数字段
            /// </summary>
            /// <typeparam name="T">泛型</typeparam>
            /// <returns>属性列表</returns>
            public static ICollection<MethodInfo> GetMethodFilterOBS<T>(
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public)
            {
                return (from info in typeof(T).GetMethods(flags)
                        let attribute = info.GetCustomAttribute<ObsoleteAttribute>()
                        where attribute == null
                        select info
                    ).ToArray();
            }

            /// <summary>
            /// 从当前程序域的运行时程序集中获取所有类型
            /// </summary>
            /// <param name="filter">类型筛选器</param>
            /// <returns>所有类型集合</returns>
            public static Type[] GetTypesInRunTimeAssemblies(Func<Type, bool> filter)
            {
                return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes().Where(filter)).ToArray();
            }

            /// <summary>
            /// 从当前程序域的所有程序集中获取所有类型
            /// </summary>
            /// <returns>所有类型集合</returns>
            public static Type[] GetTypesInAllAssemblies()
            {
                return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).ToArray();
            }

            /// <summary>
            /// 从当前程序域的所有程序集中获取所有类型
            /// </summary>
            /// <param name="filter">类型筛选器</param>
            /// <returns>所有类型集合</returns>
            public static Type[] GetTypesInAllAssemblies(Func<Type, bool> filter)
            {
                return (from t1 in AppDomain.CurrentDomain.GetAssemblies()
                        from t in t1.GetTypes()
                        where filter(t)
                        select t).ToArray();
            }
        }

        #endregion
    }
}