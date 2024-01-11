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
        public static List<LnkTools> Data
        {
            get
            {
                if (LnkToolList is null) GetLnkTools();
                return LnkToolList;
            }
        }

        private static List<LnkTools> LnkToolList { get; set; }

        private static void GetLnkTools()
        {
            if (LnkToolList == null) LnkToolList = Pool.List<LnkTools>();
            else LnkToolList.Clear();
            var types = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.GetName().Name.Contains("Editor")) continue;
                types.AddRange(assembly.GetTypes()
                    .Where(type => !type.IsEnum)
                    .Where(type => !type.IsInterface));
            }

            try
            {
                foreach (var method in
                         from type in types
                         // 过滤构造函数
                         select type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                         into methods
                         from method in methods
                         where !method.IsConstructor
                         where method.IsDefined(typeof(LnkToolsAttribute), false)
                         where method.ReturnType == typeof(bool) || method.ReturnType == typeof(void)
                         select method) LnkToolList.Add(new LnkTools(method));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            LnkToolList.Sort((x, y) =>
            {
                if (x.Priority < y.Priority) return -1;
                return x.Priority == y.Priority ? 0 : 1;
            });
        }
    }

    internal class LnkTools
    {
        public GUIContent Content { get; }

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackgroundColor { get; }

        /// <summary>
        /// 前景色
        /// </summary>
        public Color ForegroundColor { get; }

        /// <summary>
        /// 快捷工具触发模式
        /// </summary>
        public LnkToolsMode Mode { get; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// 方法
        /// </summary>
        private MethodInfo Method { get; }

        private static GUIContent SetContent(LnkToolsAttribute attribute, MethodInfo method)
        {
            GUIContent Temp = null;
            if (!string.IsNullOrEmpty(attribute.IconBuiltin))
            {
                Temp = EditorGUIUtility.IconContent(attribute.IconBuiltin);
                Temp.text = string.Empty;
            }
            else if (!string.IsNullOrEmpty(attribute.IconRelative))
                Temp = new GUIContent
                {
                    image = AssetDatabase.LoadAssetAtPath<Texture2D>(attribute.IconRelative)
                };
            else if (!string.IsNullOrEmpty(attribute.IconResource))
                Temp = new GUIContent
                {
                    image = Resources.Load<Texture2D>(attribute.IconResource)
                };

            if (Temp?.image is null)
            {
                Temp = new GUIContent
                {
                    text = string.IsNullOrEmpty(attribute.Text) ? method.Name : attribute.Text,
                    tooltip = attribute.Tooltip
                };
            }
            else Temp.tooltip = attribute.Tooltip;

            return Temp;
        }

        public bool hasIcon => Content.image != null;

        public bool hasReturn => Method.ReturnType == typeof(bool);

        public LnkTools(MethodInfo method)
        {
            var attribute = method.GetCustomAttribute<LnkToolsAttribute>();

            Content = SetContent(attribute, method);
            ForegroundColor = ColorUtility.TryParseHtmlString(attribute.ForegroundColor, out var color1)
                ? color1
                : Color.white;
            BackgroundColor = ColorUtility.TryParseHtmlString(attribute.BackgroundColor, out var color2)
                ? color2
                : new Color(0.3592f, 0.3592f, 0.3592f); //#616161
            Mode = attribute.Mode;
            Priority = attribute.Priority;
            Method = method;
            if (Method is null) throw new NullReferenceException(nameof(Method));
        }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool Status { get; private set; }

        public void Invoke(object obj, object[] parameters)
        {
            if (hasReturn)
            {
                Status = (bool)Method.Invoke(obj, parameters);
            }
            else Method.Invoke(obj, parameters);
        }

        public void Invoke()
        {
            if (hasReturn)
            {
                Status = (bool)Method.Invoke(null, null);
            }
            else Method.Invoke(null, null);
        }
    }
}