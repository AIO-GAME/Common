/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-03-22
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using MonoHook;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AIO.UEditor
{
    public interface IProfilerHook : IDisposable
    {
        /// <summary>
        /// 注入
        /// </summary>
        void Inject();
    }

    public abstract class ProfilerHook : IProfilerHook
    {
        /// <summary>
        /// 标题
        /// </summary>
        protected string Title { get; }

        /// <summary>
        /// 钩子
        /// </summary>
        private MethodHook Hook { get; }

        /// <summary>
        /// 钩子代理方法
        /// </summary>
        protected MethodBase ProxyMethod => Hook.proxyMethod;

        /// <summary>
        /// 目标方法
        /// </summary>
        protected abstract MethodBase MethodTarget();

        /// <summary>
        /// 替换方法
        /// </summary>
        protected abstract MethodBase MethodReplace();

        /// <summary>
        /// 代理方法
        /// </summary>
        protected abstract MethodBase MethodProxy();

        protected ProfilerHook(string title)
        {
            Title = title;
            var target = MethodTarget();
            if (target is null) throw new ArgumentNullException(nameof(target));
            var replacement = MethodReplace();
            if (replacement is null) throw new ArgumentNullException(nameof(replacement));
            var proxy = MethodProxy();
            if (proxy is null) throw new ArgumentNullException(nameof(proxy));
            Hook = new MethodHook(target, replacement, proxy);
        }

        protected ProfilerHook(string title, MethodBase target, MethodBase replacement, MethodBase proxy)
        {
            if (target is null) throw new ArgumentNullException(nameof(target));
            if (replacement is null) throw new ArgumentNullException(nameof(replacement));
            if (proxy is null) throw new ArgumentNullException(nameof(proxy));
            Title = title;
            Hook = new MethodHook(target, replacement, proxy);
        }

        /// <summary>
        /// 注入
        /// </summary>
        public void Inject() => Hook.Install();

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose() => Hook.Uninstall();
    }

    internal static partial class ProfilerHookTask
    {
        /// <summary>
        /// 转化为标准字符串 
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="target">类型</param>
        /// <returns>
        /// namespace.class
        ///namespace.class generic`1
        /// </returns>
        public static string ToStrAlias(this Type target)
        {
            if (target is null) return string.Empty;
            var str = GenericTypeToStr(target);
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return str.
                Replace("System.Void", "void").
                Replace("System.String", "string").
                Replace("System.Int32", "int").
                Replace("System.Single", "float").
                Replace("System.Boolean", "bool").
                Replace("System.Object", "object").
                Replace("System.Byte", "byte").
                Replace("System.Char", "char").
                Replace("System.Double", "double").
                Replace("System.UInt32", "uint").
                Replace("System.UInt64", "ulong").
                Replace("System.UInt16", "ushort").
                Replace("System.Int64", "long").
                Replace("System.Int16", "short").
                Replace("System.SByte", "sbyte").
                Replace("System.Decimal", "decimal").
                Replace('+', '.');
        }

        private static string GenericTypeToStr(Type target)
        {
            if (target is null) return string.Empty;
            var str = target.FullName;
            if (string.IsNullOrEmpty(str)) return target.FullName;
            if (target.IsGenericType)
            {
                var genericTypeStr = target.GetGenericTypeDefinition().FullName;
                if (!string.IsNullOrEmpty(genericTypeStr))
                {
                    var genericArguments = target.GetGenericArguments();
                    genericTypeStr = genericTypeStr.Substring(0, genericTypeStr.IndexOf('`'));
                    var genericArgumentsStr = string.Join(", ",
                        genericArguments.Select(genericArgument =>
                            genericArgument.IsGenericParameter
                                ? genericArgument.Name
                                : GenericTypeToStr(genericArgument)));
                    str = $"{genericTypeStr}<{genericArgumentsStr}>";
                }
            }

            return str;
        }

        public static string ToStrAlias(this ParameterInfo parameter)
        {
            var str = new StringBuilder();
            foreach (var variable in parameter.GetCustomAttributes())
            {
                str.Append(variable.
                    GetType().Name.
                    ToLower().
                    Replace("attribute", "").
                    Replace("paramarray", "params")
                ).Append(' ');
            }

            if (parameter.ParameterType.IsGenericParameter)
            {
                str.Append(parameter.ParameterType.Name);
            }
            else
            {
                str.Append(parameter.ParameterType.ToStrAlias());
            }

            return str.ToString();
        }


        public static string ToStrAlias(this MethodInfo method)
        {
            var str = new StringBuilder();
            str.Append(method.ReflectedType.ToStrAlias());
            if (method.IsStatic) str.Append('.');
            else str.Append(':');
            str.Append(method.Name);
            // 判断是否为泛型函数
            var temp = new StringBuilder();
            if (method.IsGenericMethod)
            {
                var genericArguments = method.GetGenericArguments();
                foreach (var argument in genericArguments)
                {
                    if (argument is null) continue;
                    temp.Append(argument.Name).Append(", ");
                }

                if (str.Length != 0)
                {
                    str.Append('<');
                    str.Append(temp.ToString().TrimEnd(',', ' ').Trim());
                    str.Append(">(");
                    temp.Clear();
                    str.Append(string.Join(", ", method.GetParameters().Select(p =>
                    {
                        // 获取参数的特性 param out ref 等等
                        foreach (var variable in p.GetCustomAttributes())
                        {
                            temp.Append(variable.
                                GetType().Name.
                                ToLower().
                                Replace("attribute", "").
                                Replace("paramarray", "params")
                            ).Append(' ');
                        }

                        // 判断是否为泛型参数
                        if (p.ParameterType.IsGenericParameter)
                        {
                            temp.Append(p.ParameterType.Name);
                        }
                        else
                        {
                            temp.Append(p.ParameterType.ToStrAlias());
                        }

                        return temp.Append(' ').Append(p.Name.ToLower()).ToString();
                    })));
                    str.Append(") where ");
                    str.Append(string.Join(", ", method.MakeGenericMethod(genericArguments).GetGenericArguments().
                        Select(g =>
                        {
                            var constraints = g.GetGenericParameterConstraints();
                            if (constraints.Length <= 0) return string.Empty;
                            return string.Concat(
                                g.ToStrAlias(),
                                " : ",
                                string.Join(", ", constraints.Select(c => c.ToStrAlias())));
                        })));
                }
            }
            else
            {
                str.Append('(');
                str.Append(string.Join(", ", method.GetParameters().Select(p => p.ParameterType.ToStrAlias())));
                str.Append(')');
            }

            if (method.ReturnType == typeof(void))
            {
                str.Append(" -> void");
            }
            else
            {
                str.Append(" -> ");
                str.Append(method.ReturnType.ToStrAlias());
            }

            return str.ToString();
        }

        [MenuItem("AIO/ProfilerSpace/Create")]
        private static async void Create()
        {
            var out_dir = OUTPUT_DIR;
            var out_file = OUTPUT_FILE;
            if (!AHelper.IO.ExistsDirEx(out_dir)) AHelper.IO.CreateDir(out_dir);
            if (AHelper.IO.ExistsFileEx(out_file)) AHelper.IO.DeleteFile(out_file);

            using (var writer = new StreamWriter(out_file, true, Encoding.UTF8))
            {
                await writer.WriteLineAsync("using System;");
                await writer.WriteLineAsync("using System.Reflection;");
                await writer.WriteLineAsync("using System.Runtime.CompilerServices;");
                await writer.WriteLineAsync("using UnityEngine.Profiling;");
                await writer.WriteLineAsync("using UnityEditor;");
                await writer.WriteLineAsync("using AIO.UEditor;\n");
                await writer.FlushAsync();

                var content = new StringBuilder();
                var temp = new StringBuilder();
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().
                             Where(assembly => assembly.GetCustomAttribute<ProfilerScopeAttribute>() != null))
                {
                    foreach (var type in assembly.GetTypes().
                                 Where(type => type.IsInterface == false))
                    {
                        foreach (var method in type.GetMethods().Where(method =>
                                         method.IsConstructor == false && // 排除构造函数
                                         // method.IsFinal == false && // 排除虚函数
                                         // method.IsGenericMethod == false && // 排除泛型函数
                                         // method.IsSpecialName == false && // 排除特殊函数
                                         method.IsAbstract == false // 排除抽象函数
                                 ).Where(method => method.GetCustomAttribute<ProfilerScopeAttribute>(false) != null))
                        {
                            var METHOD_FULL_NAME = ToStrAlias(method);
                            var CLASS_NAME = type.ToStrAlias();
                            var METHOD_NAME = method.Name;
                            await writer.WriteLineAsync($"#region {METHOD_FULL_NAME}");
                            await writer.FlushAsync();
                            try
                            {
                                var FULL_NAME = $"{CLASS_NAME}_{method.Name}".Replace('.', '_');

                                // 需要考虑泛型函数 将泛型函数的名称转换为泛型函数的名称
                                var TITLE = string.Concat(CLASS_NAME, ".", method.Name, "(",
                                    string.Join(", ",
                                        method.GetParameters().Select(p => string.Concat(p.ParameterType.ToStrAlias(),
                                            " ",
                                            p.Name.ToLower()))).Trim()
                                    , ")");


                                temp.Clear();
                                foreach (var parameter in method.GetParameters()) // 获取参数的特性 param out ref 等等
                                {
                                    foreach (var variable in parameter.GetCustomAttributes())
                                    {
                                        temp.Append(variable.
                                            GetType().Name.
                                            ToLower().
                                            Replace("attribute", "").
                                            Replace("paramarray", "params")
                                        ).Append(' ');
                                    }

                                    temp.Append(parameter.ParameterType.ToStrAlias());
                                    temp.Append(' ');
                                    temp.Append(parameter.Name.ToLower());
                                    temp.Append(", ");
                                }

                                var METHOD_PARAMETERS = temp.ToString().TrimEnd(',', ' '); // 获取函数参数 包含参数属性 类型 名称

                                var METHOD_PARAMETERS_INVOKE = string.Concat("new object[] {",
                                    string.Join(", ", method.GetParameters().Select(p => p.Name.ToLower())),
                                    "}");
                                // 获取函数指针地址
                                var PTR = method.MethodHandle.GetFunctionPointer().ToString("X");

                                var METHOD_RETURN = method.ReturnType.ToStrAlias();
                                content.Clear();
                                content.AppendLine(Template);
                                if (METHOD_RETURN == "void")
                                {
                                    content.Replace("RETURN_VALUE", "");
                                    content.Replace("var _ = ", "");
                                }
                                else content.Replace("RETURN_VALUE", $"\n        return _ as {METHOD_RETURN};");

                                content.Replace("CLASS_NAME", CLASS_NAME);
                                content.Replace("METHOD_NAME", METHOD_NAME);
                                content.Replace("METHOD_RETURN", METHOD_RETURN);
                                content.Replace("FULL_NAME_PTR", string.Concat(FULL_NAME, "_", PTR));
                                content.Replace("TITLE", TITLE);
                                content.Replace("METHOD_PARAMETERS_INVOKE", METHOD_PARAMETERS_INVOKE);
                                content.Replace("METHOD_PARAMETERS", METHOD_PARAMETERS);
                                content.Replace("METHOD_TARGET", Get_Method_Target(method));
                                await writer.WriteAsync(content.ToString());
                            }
                            catch (Exception e)
                            {
                                Debug.LogError(e);
                            }
                            finally
                            {
                                await writer.WriteLineAsync("\n#endregion\n");
                                await writer.FlushAsync();
                            }
                        }
                    }
                }

                content.Clear();
            }
        }

        private static List<IProfilerHook> Hooks;

        [AInit(EInitAttrMode.RuntimeAfterAssembliesLoaded, int.MinValue)]
        public static void Init()
        {
            Hooks = new List<IProfilerHook>();
            var supType = typeof(ProfilerHook);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // 获取指定程序集中的所有类型
                if (assembly.GetName().Name.Contains("Assembly-CSharp-Editor") == false) continue;
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(supType)) Hooks.Add((ProfilerHook)Activator.CreateInstance(type));
                }
            }

            Application.quitting += () =>
            {
                foreach (var hook in Hooks) hook?.Dispose();
                Hooks.Clear();
            };
            foreach (var hook in Hooks) hook?.Inject();
        }
    }

    internal static class ProfilerHookTest
    {
        private static MethodHook Hook1;

        private static MethodInfo MethodInfo1;
        private static MethodInfo MethodInfo2;
        private static MethodInfo MethodInfo3;

        [MenuItem("AIO/ProfilerSpace/Test")]
        public static void Test11()
        {
            MethodInfo1 =
                typeof(ProfilerHookTask).GetMethod(nameof(Test1), BindingFlags.NonPublic | BindingFlags.Static);
            if (MethodInfo1 is null) return;
            MethodInfo2 =
                typeof(ProfilerHookTask).GetMethod(nameof(Test2), BindingFlags.NonPublic | BindingFlags.Static);
            MethodInfo3 =
                typeof(ProfilerHookTask).GetMethod(nameof(Test3), BindingFlags.NonPublic | BindingFlags.Static);

            // 创建一个和 MethodInfo1 参数一样 返回值等等全都一样 的空内容函数
            var parameterInfos = MethodInfo1.GetParameters();
            var dynamicMethod = new DynamicMethod(
                string.Concat(MethodInfo1.Name, "_Proxy"),
                MethodInfo1.Attributes,
                MethodInfo1.CallingConvention,
                MethodInfo1.ReturnType,
                parameterInfos.Select(p => p.ParameterType).ToArray(),
                MethodInfo1.DeclaringType,
                true);
            for (var i = 0; i < parameterInfos.Length; i++)
            {
                dynamicMethod.DefineParameter(i + 1, parameterInfos[i].Attributes, parameterInfos[i].Name);
            }

            var il = dynamicMethod.GetILGenerator();
            il.Emit(OpCodes.Ldstr, "Begin Hook");
            il.Emit(OpCodes.Call, typeof(Console).GetMethod(nameof(Console.WriteLine), new[] { typeof(string) }));
            il.Emit(OpCodes.Ret);

            Hook1 = new MethodHook(MethodInfo1, MethodInfo2, dynamicMethod);
            Hook1.Install();
            Test1("111", "222");
            Hook1.Uninstall();
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static void Test1(string a, string b)
        {
            Console.WriteLine($"Hook Console {a} {b}");
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static void Test2(string a, string b)
        {
            Console.WriteLine("Begin Hook");
            Hook1.proxyMethod.Invoke(null, new object[] { a, b });
            Console.WriteLine("End Hook");
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static void Test3(string a, string b)
        {
            Console.WriteLine($"3333333333333 {a} {b}");
        }
    }
}