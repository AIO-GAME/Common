#region

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace AIO.UEditor
{
    [IgnoreConsoleJump]
    public static partial class GELayout
    {
        private static Color HeaderBorderColor
        {
            get
            {
                var shade = EditorGUIUtility.isProSkin ? 0.12f : 0.6f;
                return new Color(shade, shade, shade, 1);
            }
        }

        private static Color HeaderNormalColor
        {
            get
            {
                var shade = EditorGUIUtility.isProSkin ? 62f / 255f : 205f / 255f;
                return new Color(shade, shade, shade, 1);
            }
        }

        private static Color HeaderHoverColor
        {
            get
            {
                var shade = EditorGUIUtility.isProSkin ? 70f / 255f : 215f / 255f;
                return new Color(shade, shade, shade, 1);
            }
        }

        #region List

        /// <summary>
        ///     绘制 List 列表
        /// </summary>
        /// <param name="label">标签 <see cref="string" /></param>
        /// <param name="array">显示的折叠状态 <see cref="IList&lt;T&gt;" /></param>
        /// <param name="foldout">显示的折叠状态 <see cref="bool" /></param>
        /// <param name="tips">提示信息 <see cref="Action" /></param>
        /// <param name="showFunc">显示回调函数 <see cref="Action" /></param>
        /// <param name="addFunc">添加回调函数 <see cref="Func&lt;T&gt;" /></param>
        /// <param name="labelStyle">标签显示风格 <see cref="GUIStyle" /></param>
        /// <param name="bgStyle">背景显示风格 <see cref="GUIStyle" /></param>
        /// <returns>true:呈现子对象,false:隐藏<see cref="bool" /></returns>
        private static bool FieldList<T>(GTContent label,
                                         IList<T>  array, bool foldout, Action tips, Action<int> showFunc,
                                         Func<T>   addFunc,
                                         GUIStyle  labelStyle, GUIStyle bgStyle)
        {
            if (showFunc is null)
            {
                EditorGUILayout.HelpBox("(call back / add func) action is null", MessageType.Error, true);
                return false;
            }

            if (array is null) array = new List<T>();
            foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, label.Content, bgStyle);
            if (foldout)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(label.Content, labelStyle);
                if (addFunc != null)
                    if (GUILayout.Button("+", GUILayout.Width(20)))
                        array.Add(addFunc.Invoke());

                EditorGUILayout.EndHorizontal();

                if (tips != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    tips.Invoke();
                    EditorGUILayout.EndHorizontal();
                }

                for (var i = array.Count - 1; i >= 0; i--)
                {
                    var i1 = array.Count - 1 - i;
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField((i1 + 1).ToString("00"), GUILayout.Width(20));
                    showFunc.Invoke(i1);
                    if (GUILayout.Button("-", GUILayout.Width(20))) array.RemoveAt(i1);
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
            return foldout;
        }

        private static void FieldList<T>(GTContent label,
                                         IList<T>  array,      Action   tips, Action<int> showFunc, Func<T> addFunc,
                                         GUIStyle  labelStyle, GUIStyle bgStyle)
        {
            if (showFunc is null)
            {
                EditorGUILayout.HelpBox("(call back / add func) action is null", MessageType.Error, true);
                return;
            }

            if (array is null) array = new List<T>();
            EditorGUILayout.BeginVertical(bgStyle ?? GUIStyle.none);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, labelStyle ?? GUIStyle.none);
            if (addFunc != null)
                if (GUILayout.Button("+", GUILayout.Width(20)))
                    array.Add(addFunc.Invoke());

            EditorGUILayout.EndHorizontal();

            if (tips != null)
            {
                EditorGUILayout.BeginHorizontal();
                tips.Invoke();
                EditorGUILayout.EndHorizontal();
            }

            for (var i = array.Count - 1; i >= 0; i--)
            {
                var i1 = array.Count - 1 - i;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField((i1 + 1).ToString("00"), GUILayout.Width(20));
                showFunc.Invoke(i1);
                if (GUILayout.Button("-", GUILayout.Width(20))) array.RemoveAt(i1);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        #endregion

        #region 隔行

        /// <summary>
        ///     分隔符
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Separator()
        {
            EditorGUILayout.Separator();
        }

        /// <summary>
        ///     分隔符
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Separator(int number)
        {
            for (var i = 0; i < number; i++) EditorGUILayout.Separator();
        }

        /// <summary>
        ///     隔行
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Space()
        {
            EditorGUILayout.Space();
        }

        /// <summary>
        ///     隔行
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Space(float width)
        {
            EditorGUILayout.Space(width);
        }

#if UNITY_2019_1_OR_NEWER

        /// <summary>
        ///     隔行
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Space(int number, float width, bool expand = true)
        {
            for (var i = 0; i < number; i++) EditorGUILayout.Space(width, expand);
        }

#endif

        #endregion

        public static T FieldObject<T>(Rect rect, T obj, bool allowSceneObject, GUIStyle objStyle, GUIStyle minStyle)
            where T : Object
        {
            return (T)FieldObject(rect, obj, typeof(T), allowSceneObject, objStyle, minStyle);
        }

        public static Object FieldObject(Rect rect, Object obj, bool allowSceneObject = false)
        {
            return FieldObject(rect, obj, typeof(Object), allowSceneObject);
        }

        public static Object FieldObject(Rect rect, Object obj, GUIStyle objStyle, GUIStyle minStyle = null)
        {
            return FieldObject(rect, obj, typeof(Object), false, objStyle, minStyle);
        }

        public static Object FieldObject(
            Rect     rect,
            Object   obj, Type type,
            bool     allowSceneObject,
            GUIStyle objStyle = null,
            GUIStyle minStyle = null)
        {
            var guiContent = EditorGUIUtility.ObjectContent(obj, type);
            guiContent.text = obj ? AssetDatabase.GetAssetPath(obj) : guiContent.text;

            if (objStyle == null) objStyle = GEStyle.ObjectField;
            if (minStyle == null) minStyle = GEStyle.ObjectFieldButton;

            GUI.Box(rect, string.Empty, objStyle);

            var height    = Mathf.Min(rect.height - 2, rect.width);
            var cell      = new Rect(rect.x + 5, rect.y + 1, rect.width - height - 5, height);
            var controlID = GUIUtility.GetControlID(FocusType.Passive, cell);
            if (EditorGUI.DropdownButton(cell, guiContent, FocusType.Passive, GEStyle.DDItemStyle))
            {
                if (obj)
                {
                    UnityEditor.Selection.activeObject = obj;
                    EditorGUIUtility.PingObject(obj);
                }
            }

            cell.x     += cell.width;
            cell.width =  height;
            if (EditorGUI.DropdownButton(cell, GUIContent.none, FocusType.Passive, minStyle))
            {
                if (GUI.enabled)
                {
                    var searchFilter = typeof(SceneAsset).IsAssignableFrom(type) ? "t:Scene" : $"t:{type.Name}";
                    EditorGUIUtility.ShowObjectPicker<Object>(obj, allowSceneObject, searchFilter, controlID);
                }
            }

            switch (Event.current.GetTypeForControl(controlID)) // 判断当前是否有资源拖拽到控件上
            {
                case EventType.ExecuteCommand:
                    if (Event.current.commandName == "ObjectSelectorUpdated" &&
                        controlID == EditorGUIUtility.GetObjectPickerControlID())
                    {
                        obj = EditorGUIUtility.GetObjectPickerObject();
                        Event.current.Use();
                    }

                    break;

                case EventType.KeyDown:
                    if (GUI.enabled)
                    {
                        if (Event.current.keyCode == KeyCode.Delete)
                        {
                            obj = null;
                            Event.current.Use();
                        }
                    }

                    break;

                case EventType.DragUpdated:
                case EventType.DragPerform:
                {
                    if (GUI.enabled)
                    {
                        if (!rect.Contains(Event.current.mousePosition)) break;
                        if (DragAndDrop.objectReferences.Length != 1) break;
                        var target = DragAndDrop.objectReferences[0];
                        if (!type.IsInstanceOfType(target)) break;
                        if (!allowSceneObject && !EditorUtility.IsPersistent(target))
                        {
                            DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                            break;
                        }

                        DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                        if (Event.current.type == EventType.DragPerform)
                        {
                            DragAndDrop.AcceptDrag();
                            obj = DragAndDrop.objectReferences[0];
                            DragAndDrop.activeControlID = 0;
                        }

                        Event.current.Use();
                    }

                    break;
                }
                case EventType.DragExited:
                {
                    if (GUI.enabled)
                    {
                        if (!cell.Contains(Event.current.mousePosition)) break;
                        DragAndDrop.activeControlID = controlID;
                        DragAndDrop.visualMode      = DragAndDropVisualMode.Rejected;
                        HandleUtility.Repaint();
                    }

                    break;
                }
            }

            return obj;
        }
    }
}