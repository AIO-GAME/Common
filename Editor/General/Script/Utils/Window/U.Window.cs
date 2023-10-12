/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace AIO.UEditor
{
    public partial class EHelper
    {
        /// <summary>
        /// Window Util
        /// </summary>
        public static class Window
        {
            internal static readonly Dictionary<string, EditorWindow> WindowList =
                new Dictionary<string, EditorWindow>(64);

            internal static string GetWindowKey(Type type, string title)
            {
                if (!type.IsSubclassOf(typeof(EditorWindow)))
                {
                    Debug.LogError("Type is not EditorWindow");
                    return string.Empty;
                }

                var bytes = Encoding.UTF8.GetBytes(string.Concat(type.FullName, '|', title));
                var key = string.Join("",
                    new MD5CryptoServiceProvider().ComputeHash(bytes).Select(b => b.ToString("x2")).ToArray());
                if (!WindowList.ContainsKey(key)) return key;
#if UNITY_2020_1_OR_NEWER
                WindowList[key].SaveChanges();
#endif
                WindowList[key].Repaint();
                WindowList.Remove(key);
                return key;
            }

            internal static string GetWindowKey<T>(string title) where T : EditorWindow
            {
                return GetWindowKey(typeof(T), title);
            }

            internal static EditorWindow Command<T>(T window) where T : EditorWindow
            {
#if UNITY_2020_1_OR_NEWER
                if (!window.hasFocus)
#endif
                {
                    window.wantsMouseMove = true;
                    window.Focus();
                }

                return window;
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="utility">边框</param>
            /// <param name="focus">聚焦</param>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <returns><see cref="EditorWindow"/></returns>
            public static EditorWindow Open(Type type, string title, bool utility, bool focus)
            {
                if (!type.IsSubclassOf(typeof(EditorWindow)))
                {
                    Debug.LogError("Type is not EditorWindow");
                    return null;
                }

                if (string.IsNullOrEmpty(title)) title = type.Name;
                var key = GetWindowKey(type, title);
                if (WindowList.TryGetValue(key, out var value)) return Command(value);
                WindowList.Add(key, EditorWindow.GetWindow(type, utility, title, focus));
                value = WindowList[key];
                value.Show(true);

                return Command(value);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="utility">边框</param>
            /// <param name="focus">聚焦</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static EditorWindow Open<T>(string title, bool utility, bool focus) where T : EditorWindow
            {
                return Open(typeof(T), title, utility, focus);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="types">类型数组</param>
            /// <param name="focus">聚焦</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static EditorWindow Open<T>(string title, bool focus, params Type[] types) where T : EditorWindow
            {
                if (string.IsNullOrEmpty(title)) title = typeof(T).Name;
                var key = GetWindowKey<T>(title);
                if (WindowList.TryGetValue(key, out var value)) return Command(value);
                WindowList.Add(key, (types == null || types.Length == 0)
                    ? EditorWindow.GetWindow<T>(title, focus)
                    : EditorWindow.GetWindow<T>(title, focus, types));
                value = WindowList[key];
                value.Show(true);
                return Command(value);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="type"><see cref="EditorWindow"/></param>
            /// <param name="utility">边框</param>
            public static EditorWindow Open(Type type, bool utility)
            {
                var title = type.Name;
                var key = GetWindowKey(type, title);
                if (WindowList.TryGetValue(key, out var value)) return Command(value);
                WindowList.Add(key, EditorWindow.GetWindow(type, utility));
                value = WindowList[key];
                value.Show(true);
                return Command(value);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            public static EditorWindow Open(Type type, string label, ICollection<Type> desiredDockNextTo)
            {
                var key = GetWindowKey(type, label);
                if (WindowList.TryGetValue(key, out var instance)) return Command(instance);
                WindowList.Add(key, EditorWindow.GetWindow(type, false, label, false));
                instance = WindowList[key];
                if (desiredDockNextTo != null && desiredDockNextTo.Count > 0)
                {
                    var assembly = Assembly.GetAssembly(typeof(EditorWindow));

                    var containerWindowType = assembly.GetType("UnityEditor.ContainerWindow");
                    var viewType = assembly.GetType("UnityEditor.View");
                    var dockAreaType = assembly.GetType("UnityEditor.DockArea");

                    if (containerWindowType.GetProperty("windows",
                                BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty)
                            ?.GetValue(null, null) is not Array windowsObj) return null;

                    const BindingFlags propertyBind =
                        BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance;
                    var dockAreaMethods = dockAreaType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                    MethodInfo dockAreaMethodAddTab = null;
                    foreach (var dockAreaMethod in dockAreaMethods)
                    {
                        if (dockAreaMethod.Name != "AddTab") continue;
                        if (dockAreaMethod.GetParameters().Length != 2) continue;
                        dockAreaMethodAddTab = dockAreaMethod;
                    }

                    var containerWindowRootView = containerWindowType.GetProperty("rootView", propertyBind);
                    var viewAllChildren = viewType.GetProperty("allChildren", propertyBind);
                    if (dockAreaMethodAddTab != null && containerWindowRootView != null && viewAllChildren != null)
                    {
                        foreach (var desired in desiredDockNextTo)
                        {
                            if (desired is null) continue;
                            if (desired == instance.GetType()) continue;
                            foreach (var window in windowsObj)
                            {
                                if (window is null) continue;
                                if (window.Equals(instance)) continue;
                                var v1 = containerWindowRootView.GetValue(window, null);
                                if (v1 is null) continue;
                                if (viewAllChildren.GetValue(v1, null) is not Array v2) continue;

                                foreach (var allChild in v2)
                                {
                                    if (allChild is null) continue;
                                    if (!dockAreaType.IsInstanceOfType(allChild)) continue;
                                    if (dockAreaType.GetField("m_Panes", BindingFlags.NonPublic | BindingFlags.Instance)
                                            ?.GetValue(allChild) is not List<EditorWindow> mPanes) continue;
                                    foreach (var item in mPanes)
                                    {
                                        if (item is null) continue;
                                        if (item.GetType() == desired) continue;
                                        if (item == instance) continue;
                                        dockAreaMethodAddTab.Invoke(allChild, new object[] { instance, true });
                                        return instance;
                                    }
                                }
                            }
                        }
                    }
                }

                // instance.Show();
                return Command(instance);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            public static EditorWindow Open(Type type, ICollection<Type> desiredDockNextTo)
            {
                return Open(type, type.Name, desiredDockNextTo);
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="types">类型数组</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(Type[] types) where T : EditorWindow
            {
                var title = typeof(T).Name;
                var key = GetWindowKey<T>(title);
                if (!WindowList.TryGetValue(key, out var value))
                {
                    WindowList.Add(key, (types == null || types.Length == 0)
                        ? EditorWindow.GetWindow<T>(true)
                        : EditorWindow.GetWindow<T>(types));
                    value = WindowList[key];
                    value.Show(true);
                }

                return Command(value) as T;
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="types">类型数组</param>
            /// <param name="focus">聚焦</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static T Open<T>(bool focus, params Type[] types) where T : EditorWindow
            {
                var title = typeof(T).Name;
                var key = GetWindowKey<T>(title);
                if (!WindowList.TryGetValue(key, out var value))
                {
                    WindowList.Add(key, (types == null || types.Length == 0)
                        ? EditorWindow.GetWindow<T>(focus)
                        : EditorWindow.GetWindow<T>(types));
                    value = WindowList[key];
                    value.Show(true);
                }

                return Command(value) as T;
            }

            /// <summary>
            /// 打开窗口
            /// </summary>
            /// <param name="title">标题</param>
            /// <param name="types">类型数组</param>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static EditorWindow Open<T>(string title, params Type[] types) where T : EditorWindow
            {
                if (string.IsNullOrEmpty(title)) title = typeof(T).Name;
                var key = GetWindowKey<T>(title);
                if (!WindowList.TryGetValue(key, out var value))
                {
                    WindowList.Add(key, (types == null || types.Length == 0)
                        ? EditorWindow.GetWindow<T>(title)
                        : EditorWindow.GetWindow<T>(title, types));
                    value = WindowList[key];
                    value.Show(true);
                }

                return Command(value);
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
            public static EditorWindow Open<T>(Rect rect, string title, bool utility = true, bool focus = true)
                where T : EditorWindow
            {
                if (string.IsNullOrEmpty(title)) title = typeof(T).Name;
                var key = GetWindowKey<T>(title);
                if (WindowList.TryGetValue(key, out var value)) return Command(value);
                WindowList.Add(key, EditorWindow.GetWindowWithRect<T>(rect, utility, title, focus));
                value = WindowList[key];
                value.Show(true);

                return Command(value);
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
                var key = GetWindowKey(type, title);
                if (WindowList.TryGetValue(key, out var value)) return Command(value);
                WindowList.Add(key, EditorWindow.GetWindowWithRect(type, rect, utility, title));
                value = WindowList[key];
                value.Show(true);
                return Command(value);
            }

            /// <summary>
            /// 释放窗口
            /// </summary>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static void Free<T>(T window) where T : EditorWindow
            {
                if (!WindowList.ContainsValue(window)) return;
                var title = typeof(T).Name;
                var key = GetWindowKey<T>(title);
                WindowList.Remove(key);
            }
        }
    }
}