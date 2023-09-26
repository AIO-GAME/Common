/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-08-09
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        public static void Field<T>(string label, IList<T> list, Action<T> cb, GUIStyle labelStyle = null,
            GUIStyle contextStyle = null
        )
        {
            if (list is null)
            {
                EditorGUILayout.HelpBox($"{label} -> array is null", MessageType.None, true);
                return;
            }

            EditorGUILayout.BeginVertical(contextStyle ?? GEStyle.DDHeaderStyle);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, labelStyle ?? GEStyle.DropzoneStyle);
            if (GUILayout.Button("+", GUILayout.Width(20))) list.Add(default);
            EditorGUILayout.EndHorizontal();

            for (var i = list.Count - 1; i >= 0; i--)
            {
                var i1 = list.Count - 1 - i;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField((i1 + 1).ToString("00"), GUILayout.Width(20));
                cb?.Invoke(list[i1]);
                if (GUILayout.Button("-", GUILayout.Width(20))) list.RemoveAt(i1);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        public static bool Field<T>(string label,
            IList<T> list,
            bool show,
            Action<T> cb,
            Func<T> addFunc,
            GUIStyle labelStyle = null,
            GUIStyle contextStyle = null
        )
        {
            if (list is null)
            {
                EditorGUILayout.HelpBox($"{label} -> array is null", MessageType.None, true);
                return false;
            }

            show = EditorGUILayout.BeginFoldoutHeaderGroup(show, label, contextStyle ?? GEStyle.DDHeaderStyle);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, labelStyle ?? GEStyle.DropzoneStyle);
            if (GUILayout.Button("+", GUILayout.Width(20))) list.Add(addFunc.Invoke());
            EditorGUILayout.EndHorizontal();

            for (var i = list.Count - 1; i >= 0; i--)
            {
                var i1 = list.Count - 1 - i;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField((i1 + 1).ToString("00"), GUILayout.Width(20));
                cb?.Invoke(list[i1]);
                if (GUILayout.Button("-", GUILayout.Width(20))) list.RemoveAt(i1);

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
            return show;
        }

        public static bool Field<T>(string label,
            IList<T> list,
            bool show,
            Action<int, T> cb,
            Func<T> addFunc,
            GUIStyle labelStyle = null,
            GUIStyle contextStyle = null
        )
        {
            if (list is null)
            {
                HelpBox($"{label} -> list is null");
                return false;
            }

            show = VFoldoutHeader(() =>
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(label, labelStyle ?? GEStyle.DropzoneStyle);
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    list.Add(addFunc.Invoke());
                }

                EditorGUILayout.EndHorizontal();

                for (var i = list.Count - 1; i >= 0; i--)
                {
                    var i1 = list.Count - 1 - i;
                    EditorGUILayout.BeginHorizontal();

                    Label((i1 + 1).ToString("00"), GUILayout.Width(20));
                    cb?.Invoke(i1, list[i1]);
                    if (GUILayout.Button("-", GUILayout.Width(20)))
                    {
                        list.RemoveAt(i1);
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }, label, show);
            return show;
        }

        public static bool Field<T>(string label,
            IList<T> list,
            bool show,
            Func<T, T> cb,
            Func<T> addFunc,
            GUIStyle labelStyle = null,
            GUIStyle contextStyle = null
        )
        {
            if (list is null)
            {
                HelpBox($"{label} -> list is null");
                return false;
            }

            show = VFoldoutHeader(() =>
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(label, labelStyle ?? GEStyle.DropzoneStyle);
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    list.Add(addFunc.Invoke());
                }

                EditorGUILayout.EndHorizontal();
                for (var i = list.Count - 1; i >= 0; i--)
                {
                    var i1 = list.Count - 1 - i;
                    EditorGUILayout.BeginHorizontal();

                    Label((i1 + 1).ToString("00"), GUILayout.Width(20));
                    list[i1] = cb.Invoke(list[i1]);
                    if (GUILayout.Button("-", GUILayout.Width(20)))
                    {
                        list.RemoveAt(i1);
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }, label, show);
            return show;
        }

        public static void Field<T>(string label,
            IList<T> list,
            Action<T> cb,
            Func<T> addFunc,
            GUIStyle labelStyle = null,
            GUIStyle contextStyle = null
        )
        {
            if (list is null)
            {
                HelpBox($"{label} -> list is null");
                return;
            }

            Vertical(() =>
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(label, labelStyle ?? GEStyle.DropzoneStyle);
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    list.Add(addFunc.Invoke());
                }

                EditorGUILayout.EndHorizontal();

                for (var i = list.Count - 1; i >= 0; i--)
                {
                    var i1 = list.Count - 1 - i;
                    EditorGUILayout.BeginHorizontal();

                    Label((i1 + 1).ToString("00"), GUILayout.Width(20));
                    cb?.Invoke(list[i1]);
                    if (GUILayout.Button("-", GUILayout.Width(20)))
                    {
                        list.RemoveAt(i1);
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }, contextStyle ?? GEStyle.DDHeaderStyle);
        }

        public static void Field<T>(string label,
            IList<T> list,
            Action tips,
            Action<T> cb,
            Func<T> addFunc,
            GUIStyle labelStyle = null,
            GUIStyle contextStyle = null
        )
        {
            if (list is null)
            {
                HelpBox($"{label} -> list is null");
                return;
            }

            Vertical(() =>
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(label, labelStyle ?? GEStyle.DropzoneStyle);
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    list.Add(addFunc.Invoke());
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                Label("", GUILayout.Width(20));
                tips?.Invoke();
                Label("", GUILayout.Width(20));
                EditorGUILayout.EndHorizontal();

                for (var i = list.Count - 1; i >= 0; i--)
                {
                    var i1 = list.Count - 1 - i;
                    EditorGUILayout.BeginHorizontal();

                    Label((i1 + 1).ToString("00"), GUILayout.Width(20));
                    cb?.Invoke(list[i1]);
                    if (GUILayout.Button("-", GUILayout.Width(20)))
                    {
                        list.RemoveAt(i1);
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }, contextStyle ?? GEStyle.DDHeaderStyle);
        }

        public static void Field<T>(string label,
            IList<T> list,
            Func<T, T> cb,
            GUIStyle labelStyle = null,
            GUIStyle contextStyle = null
        )
        {
            if (list is null)
            {
                HelpBox($"{label} -> list is null");
                return;
            }

            Vertical(() =>
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(label, labelStyle ?? GEStyle.DropzoneStyle);
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    list.Add(default);
                    return;
                }

                EditorGUILayout.EndHorizontal();

                for (var i = list.Count - 1; i >= 0; i--)
                {
                    var i1 = list.Count - 1 - i;
                    EditorGUILayout.BeginHorizontal();

                    Label((i1 + 1).ToString("00"), GUILayout.Width(20));
                    list[i1] = cb.Invoke(list[i1]);
                    if (GUILayout.Button("-", GUILayout.Width(20)))
                    {
                        list.RemoveAt(i1);
                        return;
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }, contextStyle ?? GEStyle.DDHeaderStyle);
        }

        public static void Field<T>(string label,
            IList<T> list,
            Action<int, T> cb,
            GUIStyle labelStyle = null,
            GUIStyle contextStyle = null
        ) where T : new()
        {
            if (list is null)
            {
                HelpBox($"{label} -> list is null");
                return;
            }

            Vertical(() =>
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(label, labelStyle ?? GEStyle.DropzoneStyle);
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    list.Add(new T());
                }

                EditorGUILayout.EndHorizontal();

                for (var i = list.Count - 1; i >= 0; i--)
                {
                    var i1 = list.Count - 1 - i;
                    EditorGUILayout.BeginHorizontal();

                    cb?.Invoke(i1, list[i1]);
                    if (GUILayout.Button("-", GUILayout.Width(20))) list.RemoveAt(i1);
                    EditorGUILayout.EndHorizontal();
                }
            }, contextStyle ?? GEStyle.DDHeaderStyle);
        }
    }
}