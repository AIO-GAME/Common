/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GERect
    {
        #region void

        public static void Button(Rect rect, string content, GUIStyle style, Action action)
        {
            if (action is null) return;
            if (GUI.Button(rect, content, style)) action();
        }

        public static void Button(Rect rect, Texture content, GUIStyle style, Action action)
        {
            if (action is null) return;
            if (GUI.Button(rect, content, style)) action();
        }

        public static void Button(Rect rect, GUIContent content, GUIStyle style, Action action)
        {
            if (action is null) return;
            if (GUI.Button(rect, content, style)) action();
        }

        public static void Button(Rect rect, string content, Action action)
        {
            if (action is null) return;
            if (GUI.Button(rect, content)) action();
        }

        public static void Button(Rect rect, GUIContent content, Action action)
        {
            if (action is null) return;
            if (GUI.Button(rect, content)) action();
        }

        public static void Button(Rect rect, Texture content, Action action)
        {
            if (action is null) return;
            if (GUI.Button(rect, content)) action();
        }

        public static void Button(Vector2 pos, Vector2 size, string content, GUIStyle style, Action action)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), content, style)) action();
        }

        public static void Button(Vector2 pos, Vector2 size, Texture content, GUIStyle style, Action action)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), content, style)) action();
        }

        public static void Button(Vector2 pos, Vector2 size, GUIContent content, GUIStyle style, Action action)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), content, style)) action();
        }

        public static void Button(Vector2 pos, Vector2 size, string content, Action action)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), content)) action();
        }

        public static void Button(Vector2 pos, Vector2 size, GUIContent content, Action action)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), content)) action();
        }

        public static void Button(Vector2 pos, Vector2 size, Texture content, Action action)
        {
            if (action is null) return;
            if (GUI.Button(new Rect(pos - size / 2, size), content)) action();
        }

        #endregion

        #region bool

        public static bool Button(Rect rect, string content, GUIStyle style)
        {
            return GUI.Button(rect, content, style);
        }

        public static bool Button(Rect rect, Texture content, GUIStyle style)
        {
            return GUI.Button(rect, content, style);
        }

        public static bool Button(Rect rect, GUIContent content, GUIStyle style)
        {
            return GUI.Button(rect, content, style);
        }

        public static bool Button(Rect rect, string content)
        {
            return GUI.Button(rect, content);
        }

        public static bool Button(Rect rect, GUIContent content)
        {
            return GUI.Button(rect, content);
        }

        public static bool Button(Rect rect, Texture content)
        {
            return GUI.Button(rect, content);
        }

        public static bool Button(Vector2 pos, Vector2 size, string content, GUIStyle style)
        {
            return GUI.Button(new Rect(pos - size / 2, size), content, style);
        }

        public static bool Button(Vector2 pos, Vector2 size, Texture content, GUIStyle style)
        {
            return GUI.Button(new Rect(pos - size / 2, size), content, style);
        }

        public static bool Button(Vector2 pos, Vector2 size, GUIContent content, GUIStyle style)
        {
            return GUI.Button(new Rect(pos - size / 2, size), content, style);
        }

        public static bool Button(Vector2 pos, Vector2 size, string content)
        {
            return GUI.Button(new Rect(pos - size / 2, size), content);
        }

        public static bool Button(Vector2 pos, Vector2 size, GUIContent content)
        {
            return GUI.Button(new Rect(pos - size / 2, size), content);
        }

        public static bool Button(Vector2 pos, Vector2 size, Texture content)
        {
            return GUI.Button(new Rect(pos - size / 2, size), content);
        }

        #endregion
    }
}
