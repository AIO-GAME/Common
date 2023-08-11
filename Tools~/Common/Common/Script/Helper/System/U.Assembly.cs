/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public partial class AHelper
{
    /// <summary>
    /// 程序集
    /// </summary>
    public static class Assembly
    {
        /// <summary>
        /// 获取所有的类
        /// </summary>
        public static Dictionary<int, Type> GetAllType()
        {
            var List = new Dictionary<int, Type>();
            foreach (var assemble in GetReferanceAssemblies(AppDomain.CurrentDomain))
            {
                foreach (var type in assemble.GetTypes())
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
            foreach (var assemble in GetReferanceAssemblies(AppDomain.CurrentDomain))
            {
                foreach (var type in assemble.GetTypes())
                {
                    var hash = type.GetHashCode();
                    if (!type.IsSubclassOf(typeof(T))) continue;
                    if (!List.ContainsKey(hash))
                        List.Add(hash, type);
                }
            }

            return List;
        }

        /// <summary>
        /// 获取所有程序集
        /// </summary>
        public static List<System.Reflection.Assembly> GetReferanceAssemblies(AppDomain domain)
        {
            var list = new List<System.Reflection.Assembly>();
            foreach (var item in domain.GetAssemblies())
                GetReferanceAssemblies(item, list);
            return list;
        }

        /// <summary>
        /// 获取全部程序集中 包含指定特性的类 输出 key=命名空间加类名 value=类
        /// </summary>
        public static Dictionary<string, T> GetAllAssemblieHasAttributeType<T>() where T : Attribute
        {
            return (from assemble in GetReferanceAssemblies(AppDomain.CurrentDomain)
                from type in assemble.GetTypes()
                where Attribute.IsDefined(type, typeof(T))
                select type).ToDictionary(type => type.FullName, type => type.GetCustomAttribute<T>());
        }


        private static void GetReferanceAssemblies(System.Reflection.Assembly assembly, ICollection<System.Reflection.Assembly> list)
        {
            try
            {
                foreach (var item in assembly.GetReferencedAssemblies())
                {
                    var ass = System.Reflection.Assembly.Load(item);
                    if (list.Contains(ass)) continue;
                    list.Add(ass);
                    GetReferanceAssemblies(ass, list);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        public static MethodInfo GetMethodInfo(string AssemblyName, string TypeName, string MethodName)
        {
            return System.Reflection.Assembly.Load(AssemblyName).GetType(TypeName).GetMethod(MethodName,
                BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>
        /// 获取方法
        /// </summary>
        public static MethodInfo GetMethodInfo(System.Reflection.Assembly AssemblyName, string TypeName, string MethodName)
        {
            return AssemblyName.GetType(TypeName).GetMethod(MethodName,
                BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
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