/*|==========|*|
|*|Author:   |*| -> xi nan
|*|Date:     |*| -> 2023-05-31
|*|==========|*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditorInternal;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// Debug.Log 跳转 忽略指定文件 指定函数
    /// </summary>
    internal static partial class ConsoleWindowHelper
    {
        private static Type m_ConsoleWindow;

        private static Dictionary<string, bool> IgnoreClass { get; } = new Dictionary<string, bool>();

        private static Dictionary<string, bool> IgnoreMethod { get; } = new Dictionary<string, bool>();

        private static Dictionary<string, bool> IgnoreList { get; } = new Dictionary<string, bool>();
      

        static ConsoleWindowHelper()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    var attr = type.GetCustomAttribute<IgnoreConsoleJumpAttribute>();
                    var fullName = type.FullName;
                    // 获取Type的真实文件路径地址 使用Net framework实现
                    if (string.IsNullOrEmpty(fullName)) continue;
                    if (attr != null)
                    {
                        IgnoreClass[fullName] = attr.Ignore;
                        IgnoreList[attr.FilePath] = attr.Ignore;
                        Debug.Log(attr);
                        continue;
                    }
                    //
                    // foreach (var method in type.GetMethods(BindingFlags.Instance |
                    //                                        BindingFlags.Static |
                    //                                        BindingFlags.Public |
                    //                                        BindingFlags.NonPublic))
                    // {
                    //     var methodAttr = method.GetCustomAttribute<IgnoreConsoleJumpAttribute>();
                    //     if (methodAttr is null) continue;
                    //     Debug.Log("ConsoleWindowHelper methodAttr");
                    //     string key;
                    //
                    //     var IsAsync = method.GetCustomAttribute<AsyncStateMachineAttribute>() != null;
                    //     if (IsAsync)
                    //     {
                    //         key = $"{fullName}/<{method.Name}>";
                    //     }
                    //     else
                    //     {
                    //         var parameterInfo = string.Join(",",
                    //             method.GetParameters().Select(param => param.ParameterType.FullName));
                    //         key = $"{fullName}:{method.Name} ({parameterInfo})";
                    //     }
                    //
                    //     Debug.Log(IgnoreConsoleJumpAttribute.GetCodeFileName(type));
                    //     if (string.IsNullOrEmpty(key)) continue;
                    //     IgnoreMethod[key] = methodAttr.Ignore;
                    // }
                }
            }
        }

        [OnOpenAsset(0)] //1 : 使用OnOpenAsset属性 接管当有资源打开时的操作
        private static bool OnOpenAssetLog(int instanceID, int line, int column)
        {
            var asset = AssetDatabase.GetAssetPath(instanceID);
            if (!IgnoreList.ContainsKey(asset)) return false;

            if (m_ConsoleWindow == null) m_ConsoleWindow = Type.GetType("UnityEditor.ConsoleWindow,UnityEditor");

            if (EditorWindow.focusedWindow.GetType() != m_ConsoleWindow) return false;

            var activeText = //m_ActiveText包含了当前Log的全部信息
                m_ConsoleWindow?.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);

            var consoleWindowFiledInfo = //ms_ConsoleWindow 是ConsoleWindow的对象字段
                m_ConsoleWindow?.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);

            var consoleWindowInstance = consoleWindowFiledInfo?.GetValue(null); //从对象字段中得到这个对象
            var str = activeText?.GetValue(consoleWindowInstance).ToString(); //得到Log信息,用于后面解析

            var (path, lineIndex) = GetSubStringInStackStr(str, line); //解析出对应的.cs文件全路径 和 行号 
            if (lineIndex != -1)
                InternalEditorUtility.OpenFileAtLineExternal(path, lineIndex); //跳转到正确行号
            return true;
        }

        private const string textBeforeFilePath = ") (at ";

        /// <summary>
        /// 这个方法主要是参考 unity源码ConsoleWindow中的StacktraceWithHyperlinks函数 
        /// </summary>
        /// <param name="stackStr"></param>
        /// <param name="needIndex"></param>
        /// <returns></returns>
        private static (string, int) GetSubStringInStackStr(string stackStr, int needIndex)
        {
            var lines = stackStr.Split(new string[] { "\n" }, StringSplitOptions.None);

            var tempIndex = 0;
            var count = lines.Length;
            for (var i = 0; i < count; i++)
            {
                var isContinue = false;
                foreach (var ignore in IgnoreClass
                             .Where(ignore => lines[i].StartsWith(ignore.Key)))
                {
                    if (ignore.Value) return (string.Empty, -1);
                    isContinue = true;
                    break;
                }

                foreach (var ignore in IgnoreMethod
                             .Where(ignore => lines[i].StartsWith(ignore.Key)))
                {
                    if (ignore.Value) return (string.Empty, -1);
                    isContinue = true;
                    break;
                }

                if (isContinue) continue;

                var filePathIndex = lines[i].IndexOf(textBeforeFilePath, StringComparison.Ordinal);
                if (filePathIndex <= 0) continue;

                filePathIndex += textBeforeFilePath.Length;
                if (lines[i][filePathIndex] == '<') continue;

                var filePathPart = lines[i].Substring(filePathIndex);
                var lineIndex = filePathPart.LastIndexOf(":", StringComparison.Ordinal);
                if (lineIndex <= 0) continue;

                var endLineIndex = filePathPart.LastIndexOf(")", StringComparison.Ordinal);
                if (endLineIndex <= 0) continue;

                var lineString = filePathPart.Substring(lineIndex + 1, endLineIndex - (lineIndex + 1));
                var filePath = filePathPart.Substring(0, lineIndex);
                if (tempIndex++ < needIndex) continue;

                return (filePath, int.Parse(lineString));
            }

            return (string.Empty, -1);
        }
    }
}