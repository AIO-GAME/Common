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
        [MenuItem("AIO/ProfilerSpace/Create")]
        private static async void Create()
        {
            var out_dir = OUTPUT_DIR;
            var out_file = OUTPUT_FILE;
            Console.WriteLine(out_file);
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
                            var METHOD_FULL_NAME = method.ToDetails();
                            var CLASS_NAME = type.ToDetails();
                            var METHOD_NAME = method.Name;
                            await writer.WriteLineAsync($"#region {METHOD_FULL_NAME}");
                            await writer.FlushAsync();
                            try
                            {
                                var FULL_NAME = $"{CLASS_NAME}_{method.Name}".Replace('.', '_');

                                // 需要考虑泛型函数 将泛型函数的名称转换为泛型函数的名称
                                var TITLE = string.Format("{0}.{1}({2})",
                                    CLASS_NAME,
                                    method.Name,
                                    METHOD_FULL_NAME.FullParameter);

                                temp.Clear();
                                // 获取函数参数 包含参数属性 类型 名称
                                var METHOD_PARAMETERS = METHOD_FULL_NAME.FullParameter;
                                var METHOD_PARAMETERS_INVOKE = string.Concat("new object[] {",
                                    string.Join(", ", METHOD_FULL_NAME.ParameterNames),
                                    "}");
                                // 获取函数指针地址
                                // var PTR = method.MethodHandle.GetFunctionPointer().ToString("X");
                                //
                                var METHOD_RETURN = METHOD_FULL_NAME.ReturnType; // 获取函数返回值
                                content.Clear();
                                content.AppendLine(Template);
                                if (METHOD_RETURN.Name == "void")
                                {
                                    content.Replace("RETURN_VALUE", "");
                                    content.Replace("var _ = ", "");
                                }
                                else content.Replace("RETURN_VALUE", $"\n        return _ as {METHOD_RETURN};");

                                content.Replace("CLASS_NAME", CLASS_NAME);
                                content.Replace("METHOD_NAME", METHOD_NAME);
                                content.Replace("METHOD_RETURN", METHOD_RETURN);
                                content.Replace("FULL_NAME_PTR", string.Concat("PTR_", FULL_NAME));
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

        private static IList<IProfilerHook> Hooks;

        [AInit(EInitAttrMode.RuntimeAfterAssembliesLoaded, int.MinValue)]
        public static void Init()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // 获取指定程序集中的所有类型
                if (assembly.GetName().Name.Contains("Assembly-CSharp-Editor") == false) continue;
                foreach (var type in assembly.GetTypes())
                {
                    if (type.Name == "ProfilerHookHelper")
                    {
                        // 获取 Get 静态方法
                        var method = type.GetMethod("Get", BindingFlags.NonPublic | BindingFlags.Static);
                        if (method is null) continue;
                        // 调用 Get 方法 获取所有的 ProfilerHook
                        Hooks = method.Invoke(null, null) as IList<IProfilerHook>;
                    }
                }
            }

            if (Hooks is null) return;
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