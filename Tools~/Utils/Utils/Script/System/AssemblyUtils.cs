/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// 程序集
    /// </summary>
    public static class AssemblyUtils
    {
        /// <summary>
        /// 获取所有的类
        /// </summary>
        public static Dictionary<int, Type> GetAllType()
        {
            var List = new Dictionary<int, Type>();
            foreach (var assemblie in GetReferanceAssemblies(AppDomain.CurrentDomain))
            {
                foreach (var type in assemblie.GetTypes())
                {
                    var hash = type.GetHashCode();
                    if (!List.ContainsKey(hash)) List.Add(hash, type);
                }
            }
            return List;
        }

        /// <summary>
        /// 获取所有的类
        /// </summary>
        public static Dictionary<int, Type> GetAllType<T>()
        {
            var List = new Dictionary<int, Type>();
            foreach (var assemblie in GetReferanceAssemblies(AppDomain.CurrentDomain))
            {
                foreach (var type in assemblie.GetTypes())
                {
                    var hash = type.GetHashCode();
                    if (type.IsSubclassOf(typeof(T)))
                    {
                        if (!List.ContainsKey(hash))
                            List.Add(hash, type);
                    }
                }
            }
            return List;
        }

        /// <summary>
        /// 获取所有程序集
        /// </summary>
        public static List<Assembly> GetReferanceAssemblies(AppDomain domain)
        {
            var list = new List<Assembly>();
            foreach (var item in domain.GetAssemblies())
                GetReferanceAssemblies(item, list);
            return list;
        }

        /// <summary>
        /// 获取全部程序集中 包含指定特性的类 输出 key=命名空间加类名 value=类
        /// </summary>
        public static Dictionary<string, T> GetAllAssemblieHasAttributeType<T>() where T : Attribute
        {
            var List = new Dictionary<string, T>();
            foreach (var assemblie in GetReferanceAssemblies(AppDomain.CurrentDomain))
            {
                foreach (var type in assemblie.GetTypes())
                {
                    if (Attribute.IsDefined(type, typeof(T)))
                    {
                        List.Add(type.FullName, type.GetCustomAttribute<T>());
                    }
                }
            }
            return List;
        }

        private static void GetReferanceAssemblies(Assembly assembly, IList<Assembly> list)
        {
            try
            {
                foreach (var item in assembly.GetReferencedAssemblies())
                {
                    var ass = Assembly.Load(item);
                    if (!list.Contains(ass))
                    {
                        list.Add(ass);
                        GetReferanceAssemblies(ass, list);
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        public static MethodInfo GetMethodInfo(string AssemblyName, string TypeName, string MethodName)
        {
            return Assembly.Load(AssemblyName).GetType(TypeName).GetMethod(MethodName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        public static MethodInfo GetMethodInfo(Assembly AssemblyName, string TypeName, string MethodName)
        {
            return AssemblyName.GetType(TypeName).GetMethod(MethodName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        public static MethodInfo GetMethodInfo<T>(T TypeName, string MethodName) where T : Type
        {
            return TypeName.GetMethod(MethodName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }
    }
}
