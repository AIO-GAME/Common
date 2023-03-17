/*---------------------------------------------*
||Author:        |*|XiNan                    |*|
||Date:          |*|2021-07-08               |*|
||Time:          |*|11:44:51                 |*|
||E-Mail:        |*|1398581458@qq.com        |*|
||---------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    /// <summary>
    /// 
    /// </summary>
    public static class ClassUtils
    {
        /// <summary>
        /// 清除事件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        public static void RemoveAllEvent<T>(T obj, string name)
        {
            var newType = obj.GetType();
            foreach (var item in newType.GetEvents())
            {
                if (name != item.Name) continue;
                var _Field = newType.GetField(item.Name, BindingFlags.Instance | BindingFlags.NonPublic);
                if (_Field == null) continue;
                var _FieldValue = _Field.GetValue(obj);
                if (!(_FieldValue is Delegate objectDelegate)) continue;
                var invokeList = objectDelegate.GetInvocationList();
                foreach (var del in invokeList)
                    item.RemoveEventHandler(obj, del);
                break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static MethodInfo[] GetMembers(object obj, BindingFlags flags)
        {
            var type = obj.GetType();
            return type.GetMethods(flags);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static MethodInfo[] GetMethods(object obj, BindingFlags flags)
        {
            var type = obj.GetType();
            var list = new List<MethodInfo>();

            var methods = type.GetMethods(flags);
            foreach (var item in methods)
            {
                if (item.MemberType == MemberTypes.Method)
                    list.Add(item);
            }

            return list.ToArray();
        }

        /// <summary>
        /// C# Type类获取类型方法(通过字符串型的类名)
        /// </summary>
        public static Type GetType(string typeName)
        {
            Type type;
            var assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
            var assemblyArrayLength = assemblyArray.Length;
            for (var i = 0; i < assemblyArrayLength; ++i)
            {
                type = assemblyArray[i].GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            for (var i = 0; (i < assemblyArrayLength); ++i)
            {
                var typeArray = assemblyArray[i].GetTypes();
                var typeArrayLength = typeArray.Length;
                for (var j = 0; j < typeArrayLength; ++j)
                {
                    if (typeArray[j].Name.Equals(typeName))
                    {
                        return typeArray[j];
                    }
                }
            }

            return null;
        }
    }
}