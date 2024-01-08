/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-12-07
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 快捷工具箱
    /// </summary>
    internal static partial class LnkToolsHelper
    {
        private static List<LnkTools> LnkToolList;

        private static void GetLnkTools()
        {
            if (LnkToolList == null) LnkToolList = new List<LnkTools>();
            else LnkToolList.Clear();
            var types = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.GetName().Name.Contains("Editor")) continue;
                types.AddRange(assembly.GetTypes());
            }

            foreach (var method in
                     from type in types
                     select type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                     into methods
                     from method in methods
                     where method.IsDefined(typeof(LnkToolsAttribute), false)
                     select method) LnkToolList.Add(new LnkTools(method));

            LnkToolList.Sort((x, y) =>
            {
                if (x.Priority < y.Priority) return -1;
                return x.Priority == y.Priority ? 0 : 1;
            });
        }

        private class LnkTools
        {
            public GUIContent Content { get; set; }
            public Color BGColor { get; set; }
            public LnkToolsMode Mode { get; set; }
            public int Priority { get; set; }
            public MethodInfo Method { get; set; }

            public LnkTools(MethodInfo method)
            {
                var attribute = method.GetCustomAttribute<LnkToolsAttribute>();
                var icon = EditorGUIUtility.IconContent(attribute.BuiltinIcon);
                Content = new GUIContent
                {
                    image = icon?.image,
                    tooltip = attribute.Tooltip
                };
                BGColor = new Color(attribute.R, attribute.G, attribute.B, attribute.A);
                Mode = attribute.Mode;
                Priority = attribute.Priority;
                Method = method;
            }
        }
    }
}