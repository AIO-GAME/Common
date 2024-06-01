/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-06-26

|||✩ - - - - - |*/

#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace AIO.UEditor
{
    public partial class EHelper
    {
        #region Nested type: Window

        /// <summary>
        /// Window Util
        /// </summary>
        public static class Window
        {
            internal static readonly Dictionary<string, EditorWindow> WindowList =
                new Dictionary<string, EditorWindow>(64);

            private static string GetWindowKey(Type type, string title)
            {
                if (!type.IsSubclassOf(typeof(EditorWindow)))
                {
                    Debug.LogError("Type is not EditorWindow");
                    return string.Empty;
                }

                var bytes = Encoding.UTF8.GetBytes(string.Concat(type.FullName, '|', title));
                var key = string.Join("",
                                      new MD5CryptoServiceProvider().ComputeHash(bytes).Select(b => b.ToString("x2")).ToArray());
                return key;
            }

            internal static string GetWindowKey<T>(string title)
            where T : EditorWindow
            {
                return GetWindowKey(typeof(T), title);
            }

            private static EditorWindow Command<T>(T window)
            where T : EditorWindow
            {
                window.Show();
#if UNITY_2020_1_OR_NEWER
                window.SaveChanges();
#endif
                window.Repaint();
                return window;
            }

            private static EditorWindow Command(string key) { return Command(WindowList[key]); }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="focus">聚焦</param>
            /// <param name="utility">边框</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(bool utility, bool focus)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), GTContent.Empty, utility, focus);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="utility">边框</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(bool utility)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), GTContent.Empty, utility, true);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="focus">聚焦</param>
            /// <param name="utility">边框</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(GTContent title, bool utility, bool focus)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), title, utility, focus);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="utility">边框</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(GTContent title, bool utility)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), title, utility, true);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="focus">聚焦</param>
            /// <param name="utility">边框</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(string title, bool utility, bool focus)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), GTContent.Temp(title), utility, focus);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="utility">边框</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(string title, bool utility)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), GTContent.Temp(title), utility, true);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(string title)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), GTContent.Temp(title), false, true);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="types">类型数组</param>
            /// <param name="focus">聚焦</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(bool focus, params Type[] types)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), GTContent.Empty, focus, types);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="types">类型数组</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(params Type[] types)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), GTContent.Empty, true, types);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="types">类型数组</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(GTContent title, params Type[] types)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), title, true, types);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="types">类型数组</param>
            /// <param name="focus">聚焦</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(GTContent title, bool focus, params Type[] types)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), title, focus, types);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="types">类型数组</param>
            /// <param name="focus">聚焦</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(bool focus, ICollection<Type> types)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), GTContent.Empty, focus, types);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="types">类型数组</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(ICollection<Type> types)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), GTContent.Empty, true, types);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="types">类型数组</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(GTContent title, ICollection<Type> types)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), title, true, types);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="types">类型数组</param>
            /// <param name="focus">聚焦</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(GTContent title, bool focus, ICollection<Type> types)
            where T : EditorWindow
            {
                return (T)Open(typeof(T), title, focus, types);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="rect">矩形信息</param>
            /// <param name="title">标题</param>
            /// <param name="utility">边框</param>
            /// <param name="focus">聚焦</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(Rect rect, string title, bool utility = true, bool focus = true)
            where T : EditorWindow
            {
                if (string.IsNullOrEmpty(title)) title = typeof(T).Name;
                var key                                = GetWindowKey<T>(title);
                if (WindowList.TryGetValue(key, out var value)) return (T)Command(value);
                WindowList[key] = EditorWindow.GetWindowWithRect<T>(rect, utility, title, focus);
                WindowList[key].Show(true);
                return (T)Command(key);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="rect">矩形信息</param>
            /// <param name="title">标题</param>
            /// <param name="utility">边框</param>
            /// <returns><see cref="EditorWindow"/></returns>
            public static EditorWindow Open(Type type, Rect rect, string title, bool utility = true)
            {
                if (string.IsNullOrEmpty(title)) title = type.Name;
                var key                                = GetWindowKey(type, title);
                if (WindowList.TryGetValue(key, out var value)) return Command(value);
                WindowList[key] = EditorWindow.GetWindowWithRect(type, rect, utility, title);
                WindowList[key].Show(true);
                return Command(key);
            }

            /// <summary>
            /// 释放窗口
            /// </summary>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static void Free<T>(T window)
            where T : EditorWindow
            {
                string title;
                if (!window)
                {
                    title = typeof(T).Name;
                    var attribute                = typeof(T).GetCustomAttribute<GWindowAttribute>(false);
                    if (attribute != null) title = typeof(T).Name;
                }
                else
                {
                    title = window.titleContent.text;
                }

                var key = GetWindowKey(typeof(T), title);
                if (!WindowList.ContainsKey(key)) return;
                WindowList.Remove(key);
            }

            #region Open Type

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="type"><see cref="EditorWindow"/></param>
            public static EditorWindow Open(Type type) { return Open(type, string.Empty, false, true); }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="title">标题</param>
            public static EditorWindow Open(Type type, string title) { return Open(type, title, false, true); }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="title">标题</param>
            public static EditorWindow Open(Type type, GTContent title) { return Open(type, title, false, true); }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="utility">边框</param>
            public static EditorWindow Open(Type type, bool utility) { return Open(type, GTContent.Empty, utility, true); }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="utility">边框</param>
            /// <param name="focus">聚焦</param>
            public static EditorWindow Open(Type type, bool utility, bool focus) { return Open(type, GTContent.Empty, utility, focus); }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="utility">边框</param>
            /// <param name="title">标题</param>
            /// <param name="focus">聚焦</param>
            public static EditorWindow Open(Type type, GTContent title, bool utility, bool focus) { return Open(type, title.Content, utility, focus); }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="utility">边框</param>
            /// <param name="title">标题</param>
            /// <param name="focus">聚焦</param>
            public static EditorWindow Open(Type type, GUIContent title, bool utility, bool focus)
            {
                if (!type.IsSubclassOf(typeof(EditorWindow)))
                {
                    Debug.LogError("Type is not EditorWindow");
                    return null;
                }

                if (string.IsNullOrEmpty(title.text))
                {
                    var attribute                = type.GetCustomAttribute<GWindowAttribute>(false);
                    if (attribute != null) title = attribute.Title;
                }

                if (string.IsNullOrEmpty(title.text)) title.text = type.Name;

                var key = GetWindowKey(type, title.text);
                if (!WindowList.ContainsKey(key) || !WindowList[key])
                {
                    WindowList[key]              = ScriptableObject.CreateInstance(type) as EditorWindow;
                    WindowList[key].titleContent = title;
                    if (utility) WindowList[key].ShowUtility();
                    else WindowList[key].Show();
                }
                else if (focus)
                {
                    WindowList[key].Show();
                    WindowList[key].Focus();
                }
                else
                {
                    WindowList[key].Show();
                }
#if UNITY_2020_1_OR_NEWER
                WindowList[key].SaveChanges();
#endif
                WindowList[key].Repaint();
                return WindowList[key];
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="desiredDockNextTo">组</param>
            public static EditorWindow Open(Type type, ICollection<Type> desiredDockNextTo) { return Open(type, GTContent.Empty, true, desiredDockNextTo); }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="desiredDockNextTo">组</param>
            public static EditorWindow Open(Type type, params Type[] desiredDockNextTo) { return Open(type, GTContent.Empty, true, desiredDockNextTo); }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="desiredDockNextTo">组</param>
            public static EditorWindow Open(Type type, GTContent title, ICollection<Type> desiredDockNextTo)
            {
                return Open(type, title, true, desiredDockNextTo);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="desiredDockNextTo">组</param>
            public static EditorWindow Open(Type type, GUIContent title, ICollection<Type> desiredDockNextTo)
            {
                return Open(type, title, true, desiredDockNextTo);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="desiredDockNextTo">组</param>
            public static EditorWindow Open(Type type, GTContent title, params Type[] desiredDockNextTo) { return Open(type, title, true, desiredDockNextTo); }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="desiredDockNextTo">组</param>
            public static EditorWindow Open(Type type, GUIContent title, params Type[] desiredDockNextTo) { return Open(type, title, true, desiredDockNextTo); }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="desiredDockNextTo">组</param>
            public static EditorWindow Open(Type type, string title, params Type[] desiredDockNextTo)
            {
                return Open(type, GTContent.Temp(title), true, desiredDockNextTo);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="desiredDockNextTo">组</param>
            public static EditorWindow Open(Type type, string title, ICollection<Type> desiredDockNextTo)
            {
                return Open(type, GTContent.Temp(title), true, desiredDockNextTo);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="focus">聚焦</param>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="desiredDockNextTo">组</param>
            public static EditorWindow Open(Type type, GTContent title, bool focus, ICollection<Type> desiredDockNextTo)
            {
                return Open(type, title.Content, focus, desiredDockNextTo);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="focus">聚焦</param>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="desiredDockNextTo">组</param>
            public static EditorWindow Open(Type type, GUIContent title, bool focus, ICollection<Type> desiredDockNextTo)
            {
                if (!type.IsSubclassOf(typeof(EditorWindow)))
                {
                    Debug.LogError("Type is not EditorWindow");
                    return null;
                }

                if (string.IsNullOrEmpty(title.text))
                {
                    var attribute                = type.GetCustomAttribute<GWindowAttribute>(false);
                    if (attribute != null) title = attribute.GetTitle();
                }

                if (string.IsNullOrEmpty(title.text)) title.text = type.Name;

                var key = GetWindowKey(type, title.text);
                if (!WindowList.ContainsKey(key) || WindowList[key] == null)
                {
                    WindowList[key]              = ScriptableObject.CreateInstance(type) as EditorWindow;
                    WindowList[key].titleContent = title;
                    if (AddDock(WindowList[key], desiredDockNextTo)) return WindowList[key];
                }

                if (focus)
                {
                    WindowList[key].Show(true);
                    WindowList[key].Focus();
                }
                else
                {
                    WindowList[key].Show(true);
                }
#if UNITY_2020_1_OR_NEWER
                WindowList[key].SaveChanges();
#endif
                WindowList[key].Repaint();
                return WindowList[key];
            }

            private const BindingFlags PROPERTY_BIND =
                BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance;

            private static bool AddDock(Object instance, ICollection<Type> desiredDockNextTo)
            {
                if (desiredDockNextTo == null || desiredDockNextTo.Count == 0) return false;
                var assembly = Assembly.GetAssembly(typeof(EditorWindow));

                var containerWindowType = assembly.GetType("UnityEditor.ContainerWindow");

                if (!(containerWindowType?.GetProperty("windows",
                                                       BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty)
                                         ?.GetValue(null, null) is Array windowsObj)) return false;
                var containerWindowRootView = containerWindowType.GetProperty("rootView", PROPERTY_BIND);
                if (containerWindowRootView is null) return false;

                var dockAreaType    = assembly.GetType("UnityEditor.DockArea");
                var dockAreaMethods = dockAreaType?.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                var dockAreaMethodAddTab = dockAreaMethods?
                                           .Where(dockAreaMethod => dockAreaMethod.Name == "AddTab")
                                           .FirstOrDefault(dockAreaMethod => dockAreaMethod.GetParameters().Length == 2);
                if (dockAreaMethodAddTab is null) return false;

                var viewType        = assembly.GetType("UnityEditor.View");
                var viewAllChildren = viewType?.GetProperty("allChildren", PROPERTY_BIND);
                if (viewAllChildren is null) return false;
                var bind = BindingFlags.NonPublic | BindingFlags.Instance;
                var type = instance.GetType();
                foreach (var desired in desiredDockNextTo.Where(desired => desired == type))
                {
                    foreach (var v1 in windowsObj
                                       .Cast<object>()
                                       .Where(window => window != null)
                                       .Where(window => !window.Equals(instance))
                                       .Select(window => containerWindowRootView.GetValue(window, null))
                                       .Where(v1 => v1 != null))
                    {
                        if (!(viewAllChildren.GetValue(v1, null) is Array v2)) continue;
                        foreach (var allChild in v2
                                                 .Cast<object>()
                                                 .Where(allChild => allChild != null)
                                                 .Where(allChild => dockAreaType.IsInstanceOfType(allChild))
                                )
                        {
                            if (!(dockAreaType.GetField("m_Panes", bind)?.GetValue(allChild) is List<EditorWindow> mPanes)) continue;
                            if (mPanes.Where(item => item).Where(item => item != instance).All(item => item.GetType() != desired)) continue;
                            dockAreaMethodAddTab.Invoke(allChild, new object[] { instance, false, });
                            return true;
                        }
                    }
                }

                return false;
            }

            #endregion
        }

        #endregion
    }
}