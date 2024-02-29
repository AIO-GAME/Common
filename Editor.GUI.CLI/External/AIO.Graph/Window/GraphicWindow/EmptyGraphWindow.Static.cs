using System;
using UnityEditor;
using UnityEngine;
using WindowUtil = AIO.UEditor.EHelper.Window;

namespace AIO.UEditor
{
    partial class EmptyGraphWindow
    {
        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
        /// <returns><see cref="EditorWindow"/></returns>
        protected static EditorWindow Open<T>() where T : EditorWindow
        {
            return WindowUtil.Open<T>();
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="utility">边框</param>
        /// <param name="focus">聚焦</param>
        /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
        /// <returns><see cref="EditorWindow"/></returns>
        protected static EditorWindow Open<T>(string title, bool utility, bool focus) where T : EditorWindow
        {
            return WindowUtil.Open<T>(title, utility, focus);
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="types">类型数组</param>
        /// <param name="focus">聚焦</param>
        /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
        /// <returns><see cref="EditorWindow"/></returns>
        protected static EditorWindow Open<T>(string title, bool focus, params Type[] types) where T : EditorWindow
        {
            return WindowUtil.Open<T>(title, focus, types);
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="types">类型数组</param>
        /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
        /// <returns><see cref="EditorWindow"/></returns>
        protected static EditorWindow Open<T>(string title, params Type[] types) where T : EditorWindow
        {
            return WindowUtil.Open<T>(title, types);
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
        protected static EditorWindow Open<T>(Rect rect, string title, bool utility = true, bool focus = true)
            where T : EditorWindow
        {
            return WindowUtil.Open<T>(rect, title, utility, focus);
        }

        /// <summary>
        /// 释放窗口
        /// </summary>
        /// <typeparam name="T"><see cref="EditorWindow"/></typeparam>
        /// <returns><see cref="EditorWindow"/></returns>
        protected static void Free<T>(T window) where T : EditorWindow
        {
            WindowUtil.Free(window);
        }

        protected void DrawVersion(string version)
        {
            using (new GUILayout.AreaScope(new Rect(0, position.height - 20, position.width, 20)))
            {
                GUILayout.Label($"Version {version}", EditorStyles.centeredGreyMiniLabel);
            }
        }
    }
}