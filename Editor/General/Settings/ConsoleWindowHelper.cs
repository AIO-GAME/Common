#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// Debug.Log 跳转 忽略指定文件 指定函数
    /// </summary>
    public static class ConsoleWindowHelper
    {
        private const  string              textBeforeFilePath = ") (at ";
        private static ConsoleWindowConfig config;

        private static Type m_ConsoleWindow;

        private static bool isInit;

        private static Dictionary<string, bool> IgnoreClass { get; } = new Dictionary<string, bool>();

        private static Dictionary<string, bool> IgnoreMethod { get; } = new Dictionary<string, bool>();

        private static void Init()
        {
            config = ConsoleWindowConfig.GetOrCreate();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!config.Assemblies.Contains(assembly.GetName().Name)) continue;
                foreach (var type in assembly.GetTypes())
                {
                    var attr     = type.GetCustomAttribute<IgnoreConsoleJumpAttribute>();
                    var fullName = type.FullName;
                    // 获取Type的真实文件路径地址 使用Net framework实现
                    if (string.IsNullOrEmpty(fullName)) continue;
                    if (attr != null)
                    {
                        if (IgnoreClass.ContainsKey(fullName))
                        {
                            if (!IgnoreClass[fullName] && attr.Ignore) IgnoreClass[fullName] = true;
                        }
                        else
                        {
                            IgnoreClass[fullName] = attr.Ignore;
                        }
                    }

                    foreach (var method in type.GetMethods(
                                                           BindingFlags.Instance
                                                         | BindingFlags.Static |
                                                           BindingFlags.DeclaredOnly |
                                                           BindingFlags.Public |
                                                           BindingFlags.NonPublic |
                                                           BindingFlags.FlattenHierarchy
                                                          ))
                    {
                        if (!method.HasAttribute<IgnoreConsoleJumpAttribute>()) continue;
                        var methodAttr = method.GetCustomAttribute<IgnoreConsoleJumpAttribute>();
                        if (methodAttr is null) continue;
                        if (!IgnoreClass.ContainsKey(fullName)) IgnoreClass[fullName] = false;
                        string key;
                        var    IsAsync = method.GetCustomAttribute<AsyncStateMachineAttribute>() != null;
                        if (IsAsync)
                        {
                            key = $"{fullName}/<{method.Name}>";
                        }
                        else
                        {
                            // 获取泛型
                            if (method.IsGenericMethod)
                            {
                                var methodDefinition = method.GetGenericMethodDefinition();

                                var gList = methodDefinition.GetGenericArguments().SelectMany(param => param.GetGenericParameterConstraints()).ToArray();

                                var genericArgumentsStr = string.Join(",", gList.Select(param => param.FullName));

                                var parameterInfo = string.Join(",", methodDefinition.GetParameters()
                                                                                     .Select(param => string.IsNullOrEmpty(param.ParameterType.FullName)
                                                                                                 ? gList[param.ParameterType.GenericParameterPosition].FullName
                                                                                                 : param.ParameterType.FullName))
                                                          .Replace("System.Object", "object")
                                                          .Replace("System.String", "string")
                                                          .Replace("System.Single", "float")
                                                          .Replace("System.Boolean", "bool")
                                                          .Replace("System.Double", "double")
                                                          .Replace("System.Byte", "byte")
                                                          .Replace("System.Int16", "short")
                                                          .Replace("System.Int32", "int")
                                                          .Replace("System.Int64", "long")
                                                          .Replace("System.UInt32", "uint")
                                                          .Replace("System.UInt64", "ulong")
                                                          .Replace("System.UInt16", "ushort")
                                                          .Replace("System.SByte", "sbyte")
                                                          .Replace("System.Char", "char")
                                                          .Replace("System.Decimal", "decimal")
                                    ;

                                key = string.IsNullOrEmpty(genericArgumentsStr)
                                    ? $"{fullName}:{method.Name} ({parameterInfo})"
                                    : $"{fullName}:{method.Name}<{genericArgumentsStr}> ({parameterInfo})";
                            }
                            else
                            {
                                var parameterInfo = string.Join(",", method.GetParameters().Select(param => param.ParameterType.FullName))
                                                          .Replace("System.Object", "object")
                                                          .Replace("System.String", "string")
                                                          .Replace("System.Single", "float")
                                                          .Replace("System.Boolean", "bool")
                                                          .Replace("System.Double", "double")
                                                          .Replace("System.Byte", "byte")
                                                          .Replace("System.Int16", "short")
                                                          .Replace("System.Int32", "int")
                                                          .Replace("System.Int64", "long")
                                                          .Replace("System.UInt32", "uint")
                                                          .Replace("System.UInt64", "ulong")
                                                          .Replace("System.UInt16", "ushort")
                                                          .Replace("System.SByte", "sbyte")
                                                          .Replace("System.Char", "char")
                                                          .Replace("System.Decimal", "decimal");
                                key = $"{fullName}:{method.Name} ({parameterInfo})";
                            }
                        }

                        if (string.IsNullOrEmpty(key)) continue;
                        IgnoreMethod[key] = methodAttr.Ignore;
                    }
                }
            }

            isInit = true;
        }

        // [OnOpenAsset(0)] //1 : 使用OnOpenAsset属性 接管当有资源打开时的操作
        private static bool OnOpenAssetLog(int instanceID, int line, int column)
        {
            if (!isInit) Init();

            if (m_ConsoleWindow == null) m_ConsoleWindow = Type.GetType("UnityEditor.ConsoleWindow,UnityEditor");
            if (EditorWindow.focusedWindow.GetType() != m_ConsoleWindow) return false;

            var activeText = //m_ActiveText包含了当前Log的全部信息
                m_ConsoleWindow?.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);

            var consoleWindowFiledInfo = //ms_ConsoleWindow 是ConsoleWindow的对象字段
                m_ConsoleWindow?.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);

            var consoleWindowInstance = consoleWindowFiledInfo?.GetValue(null);                 //从对象字段中得到这个对象
            var str                   = activeText?.GetValue(consoleWindowInstance).ToString(); //得到Log信息,用于后面解析

            var (path, lineIndex) = GetSubStringInStackStr(str); //解析出对应的.cs文件全路径 和 行号 
            if (lineIndex == -1) return false;

            Debug.Log($"{path}:{lineIndex}");
            InternalEditorUtility.OpenFileAtLineExternal(path, lineIndex); //跳转到正确行号
            return true;
        }

        /// <summary>
        /// 这个方法主要是参考 unity源码ConsoleWindow中的StacktraceWithHyperlinks函数 
        /// </summary>
        private static (string, int) GetSubStringInStackStr(string stackStr)
        {
            var lines = stackStr.Split(new[] { "\n", "\r", "\t" }, StringSplitOptions.RemoveEmptyEntries);

            var count = lines.Length;
            for (var i = 0; i < count; i++)
            {
                var filePathIndex = lines[i].IndexOf(textBeforeFilePath, StringComparison.Ordinal);
                if (filePathIndex <= 0) continue;

                filePathIndex += textBeforeFilePath.Length;
                if (lines[i][filePathIndex] == '<') continue;

                var filePathPart = lines[i].Substring(filePathIndex);
                var lineIndex    = filePathPart.LastIndexOf(":", StringComparison.Ordinal);
                if (lineIndex <= 0) continue;

                var endLineIndex = filePathPart.LastIndexOf(")", StringComparison.Ordinal);
                if (endLineIndex <= 0) continue;

                var lineString = filePathPart.Substring(lineIndex + 1, endLineIndex - (lineIndex + 1));
                var filePath   = filePathPart.Substring(0, lineIndex);

                if (config.BlackList.Any(black => lines[i].StartsWith(black))) continue;
                var isContinue = false;
                var temp       = lines[i];
                foreach (var ignore in IgnoreClass)
                {
                    if (!temp.StartsWith(ignore.Key)) continue;
                    if (ignore.Value) return (string.Empty, -1);
                    isContinue = true;
                    break;
                }

                foreach (var ignore in IgnoreMethod)
                {
                    if (!temp.StartsWith(ignore.Key)) continue;
                    if (ignore.Value) return (string.Empty, -1);
                    isContinue = true;
                    break;
                }

                if (isContinue) continue;

                return (filePath, int.Parse(lineString));
            }

            return (string.Empty, -1);
        }
    }
}