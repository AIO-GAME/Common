/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace UnityEditor
{
    public partial class UtilsEditor
    {
        /// <summary>
        /// Window Util
        /// </summary>
        public static class Window
        {
            internal static readonly Dictionary<string, EditorWindow> WindowList = new Dictionary<string, EditorWindow>(64);

            internal static string GetWindowKey<T>(string title)
            {
                var key = "";
                var bytes = Encoding.UTF8.GetBytes(string.Concat(typeof(T).FullName, '|', title));
                foreach (var num in new MD5CryptoServiceProvider().ComputeHash(bytes))
                    key += num.ToString("x2");
                if (!WindowList.ContainsKey(key)) return key;
                WindowList[key].SaveChanges();
                WindowList[key].Repaint();
                WindowList.Remove(key);
                return key;
            }

            internal static EditorWindow Command<T>(T window) where T : EditorWindow
            {
                if (!window.hasFocus)
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
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static EditorWindow Open<T>(string title, bool utility, bool focus) where T : EditorWindow
            {
                if (string.IsNullOrEmpty(title)) title = typeof(T).Name;
                var key = GetWindowKey<T>(title);
                if (!WindowList.TryGetValue(key, out var value))
                {
                    WindowList.Add(key, EditorWindow.GetWindow<T>(utility, title, focus));
                    value = WindowList[key];
                    value.Show(true);
                }

                return Command(value);
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
                if (!WindowList.TryGetValue(key, out var value))
                {
                    WindowList.Add(key, (types == null || types.Length == 0)
                        ? EditorWindow.GetWindow<T>(title, focus)
                        : EditorWindow.GetWindow<T>(title, focus, types));
                    value = WindowList[key];
                    value.Show(true);
                }

                return Command(value);
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
            public static EditorWindow Open<T>(Rect rect, string title, bool utility = true, bool focus = true) where T : EditorWindow
            {
                if (string.IsNullOrEmpty(title)) title = typeof(T).Name;
                var key = GetWindowKey<T>(title);
                if (!WindowList.TryGetValue(key, out var value))
                {
                    WindowList.Add(key, EditorWindow.GetWindowWithRect<T>(rect, utility, title, focus));
                    value = WindowList[key];
                    value.Show(true);
                }

                return Command(value);
            }

            /// <summary>
            /// 释放窗口
            /// </summary>
            /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
            /// <returns><see cref="EditorWindow"/></returns>
            public static void Free<T>(T window) where T : EditorWindow
            {
                if (WindowList.ContainsValue(window))
                {
                    var title = typeof(T).Name;
                    var key = GetWindowKey<T>(title);
                    WindowList.Remove(key);
                }
            }
        }
    }
}