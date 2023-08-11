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
using WindowUtil = AIO.UEditor.EHelper.Window;
using UnityEditor;

namespace AIO.UEditor
{
    public partial class EmptyGraphWindow
    {
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
            var key = WindowUtil.GetWindowKey<T>(title);
            if (!WindowUtil.WindowList.TryGetValue(key, out var value))
            {
                WindowUtil.WindowList.Add(key, EditorWindow.GetWindow<T>(utility, title, focus));
                value = WindowUtil.WindowList[key];
                value.Show(true);
            }

            return WindowUtil.Command(value);
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
            var key = WindowUtil.GetWindowKey<T>(title);
            if (!WindowUtil.WindowList.TryGetValue(key, out var value))
            {
                WindowUtil.WindowList.Add(key, (types == null || types.Length == 0)
                    ? EditorWindow.GetWindow<T>(title, focus)
                    : EditorWindow.GetWindow<T>(title, focus, types));
                value = WindowUtil.WindowList[key];
                value.Show(true);
            }

            return WindowUtil.Command(value);
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
            var key = WindowUtil.GetWindowKey<T>(title);
            if (!WindowUtil.WindowList.TryGetValue(key, out var value))
            {
                WindowUtil.WindowList.Add(key, (types == null || types.Length == 0)
                    ? EditorWindow.GetWindow<T>(focus)
                    : EditorWindow.GetWindow<T>(types));
                value = WindowUtil.WindowList[key];
                value.Show(true);
            }

            return WindowUtil.Command(value) as T;
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
            var key = WindowUtil.GetWindowKey<T>(title);
            if (!WindowUtil.WindowList.TryGetValue(key, out var value))
            {
                WindowUtil.WindowList.Add(key, (types == null || types.Length == 0)
                    ? EditorWindow.GetWindow<T>(title)
                    : EditorWindow.GetWindow<T>(title, types));
                value = WindowUtil.WindowList[key];
                value.Show(true);
            }

            return WindowUtil.Command(value);
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
            var key = WindowUtil.GetWindowKey<T>(title);
            if (!WindowUtil.WindowList.TryGetValue(key, out var value))
            {
                WindowUtil.WindowList.Add(key, EditorWindow.GetWindowWithRect<T>(rect, utility, title, focus));
                value = WindowUtil.WindowList[key];
                value.Show(true);
            }

            return WindowUtil.Command(value);
        }

        /// <summary>
        /// 释放窗口
        /// </summary>
        /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
        /// <returns><see cref="EditorWindow"/></returns>
        public static void Free<T>(T window) where T : EditorWindow
        {
            if (WindowUtil.WindowList.ContainsValue(window))
            {
                var title = typeof(T).Name;
                var key = WindowUtil.GetWindowKey<T>(title);
                WindowUtil.WindowList.Remove(key);
            }
        }
    }
}
