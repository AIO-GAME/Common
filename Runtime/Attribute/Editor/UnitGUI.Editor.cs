/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-07-28

|||✩ - - - - - |*/

#if UNITY_EDITOR
using System;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AIO
{
    internal static partial class UnitGUI
    {
        /// <summary>
        /// Returns <see cref="EditorGUIUtility.singleLineHeight"/>.
        /// </summary>
        public static float LineHeight => EditorGUIUtility.singleLineHeight;

        /// <summary>
        /// Returns <see cref="EditorGUIUtility.standardVerticalSpacing"/>.
        /// </summary>
        public static float StandardSpacing => EditorGUIUtility.standardVerticalSpacing;

        /// <summary>
        /// The number of pixels of indentation for each <see cref="EditorGUI.indentLevel"/> increment.
        /// </summary>
        public static float IndentSize
        {
            get
            {
                if (_IndentSize < 0)
                {
                    var indentLevel = EditorGUI.indentLevel;
                    EditorGUI.indentLevel = 1;
                    _IndentSize = EditorGUI.IndentedRect(new Rect()).x;
                    EditorGUI.indentLevel = indentLevel;
                }

                return _IndentSize;
            }
        }

        /// <summary>A more compact <see cref="EditorStyles.miniButton"/> with a fixed size as a tiny box.</summary>
        public static GUIStyle MiniButton
        {
            get
            {
                if (_MiniButton == null)
                {
                    _MiniButton = new GUIStyle(EditorStyles.miniButton)
                    {
                        margin = new RectOffset(0, 0, 2, 0),
                        padding = new RectOffset(2, 3, 2, 2),
                        alignment = TextAnchor.MiddleCenter,
                        fixedHeight = LineHeight,
                        fixedWidth = LineHeight - 1
                    };
                }

                return _MiniButton;
            }
        }

        /// <summary>
        /// Wrapper around <see cref="UnityEditorInternal.InternalEditorUtility.RepaintAllViews"/>.
        /// </summary>
        public static void RepaintEverything() => InternalEditorUtility.RepaintAllViews();

        /// <summary>
        /// Begins a vertical layout group using the given style and decreases the
        /// <see cref="EditorGUIUtility.labelWidth"/> to compensate for the indentation.
        /// </summary>
        public static void BeginVerticalBox(GUIStyle style)
        {
            if (style == null)
            {
                GUILayout.BeginVertical();
                return;
            }

            GUILayout.BeginVertical(style);
            EditorGUIUtility.labelWidth -= style.padding.left;
        }

        /// <summary>
        /// Ends a layout group started by <see cref="BeginVerticalBox"/> and restores the
        /// <see cref="EditorGUIUtility.labelWidth"/>.
        /// </summary>
        public static void EndVerticalBox(GUIStyle style)
        {
            if (style != null)
                EditorGUIUtility.labelWidth += style.padding.left;

            GUILayout.EndVertical();
        }

        /// <summary>
        /// The <see cref="EditorGUIUtility.labelWidth"/> from before <see cref="BeginTightLabel"/>.
        /// </summary>
        private static float _TightLabelWidth;

        /// <summary>
        /// Stores the <see cref="EditorGUIUtility.labelWidth"/> and changes it to the exact width of the `label`.
        /// </summary>
        public static string BeginTightLabel(string label)
        {
            _TightLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = CalculateLabelWidth(label) + EditorGUI.indentLevel * IndentSize;
            return GetNarrowText(label);
        }

        /// <summary>
        /// Reverts <see cref="EditorGUIUtility.labelWidth"/> to its previous value.
        /// </summary>
        public static void EndTightLabel()
        {
            EditorGUIUtility.labelWidth = _TightLabelWidth;
        }


        /// <summary>
        /// Returns the `text` without any spaces if <see cref="EditorGUIUtility.wideMode"/> is false.
        /// Otherwise simply returns the `text` without any changes.
        /// </summary>
        public static string GetNarrowText(string text)
        {
            if (EditorGUIUtility.wideMode ||
                string.IsNullOrEmpty(text))
                return text;

            if (_NarrowTextCache == null)
                _NarrowTextCache = new ConversionCache<string, string>((str) => str.Replace(" ", ""));

            return _NarrowTextCache.Convert(text);
        }


        /// <summary>Loads an icon texture and sets it to use <see cref="FilterMode.Bilinear"/>.</summary>
        public static Texture LoadIcon(string name)
        {
            var icon = (Texture)EditorGUIUtility.Load(name);
            if (icon != null)
                icon.filterMode = FilterMode.Bilinear;
            return icon;
        }

        /// <summary>
        /// Invokes `onDrop` if the <see cref="Event.current"/> is a drag and drop event inside the `dropArea`.
        /// </summary>
        public static void HandleDragAndDrop<T>(Rect dropArea, Func<T, bool> validate, Action<T> onDrop,
            DragAndDropVisualMode mode = DragAndDropVisualMode.Link) where T : class
        {
            if (!dropArea.Contains(Event.current.mousePosition))
                return;

            bool isDrop;
            switch (Event.current.type)
            {
                case EventType.DragUpdated:
                    isDrop = false;
                    break;

                case EventType.DragPerform:
                    isDrop = true;
                    break;

                default:
                    return;
            }

            TryDrop(DragAndDrop.objectReferences, validate, onDrop, isDrop, mode);
        }

        /// <summary>
        /// Updates the <see cref="DragAndDrop.visualMode"/> or calls `onDrop` for each of the `objects`.
        /// </summary>
        private static void TryDrop<T>(IEnumerable objects,
            Func<T, bool> validate,
            Action<T> onDrop,
            bool isDrop,
            DragAndDropVisualMode mode) where T : class
        {
            if (objects == null)
                return;

            var droppedAny = false;

            foreach (var obj in objects)
            {
                var t = obj as T;

                if (t != null && (validate == null || validate(t)))
                {
                    Deselect();

                    if (!isDrop)
                    {
                        DragAndDrop.visualMode = mode;
                        break;
                    }
                    else
                    {
                        onDrop(t);
                        droppedAny = true;
                    }
                }
            }

            if (droppedAny)
                GUIUtility.ExitGUI();
        }


        /// <summary>
        /// Uses <see cref="GUILayoutUtility.GetRect(float, float)"/> to get a <see cref="Rect"/> occupying a single
        /// standard line with the <see cref="StandardSpacing"/> added according to the specified `spacing`.
        /// </summary>
        public static Rect LayoutSingleLineRect(SpacingMode spacing = SpacingMode.None)
        {
            Rect rect;
            switch (spacing)
            {
                case SpacingMode.None:
                    return GUILayoutUtility.GetRect(0, LineHeight);

                case SpacingMode.Before:
                    rect = GUILayoutUtility.GetRect(0, LineHeight + StandardSpacing);
                    rect.yMin += StandardSpacing;
                    return rect;

                case SpacingMode.After:
                    rect = GUILayoutUtility.GetRect(0, LineHeight + StandardSpacing);
                    rect.yMax -= StandardSpacing;
                    return rect;

                case SpacingMode.BeforeAndAfter:
                    rect = GUILayoutUtility.GetRect(0, LineHeight + StandardSpacing * 2);
                    rect.yMin += StandardSpacing;
                    rect.yMax -= StandardSpacing;
                    return rect;

                default:
                    throw new ArgumentException($"Unknown {nameof(StandardSpacing)}: " + spacing, nameof(spacing));
            }
        }

        /// <summary>
        /// If the <see cref="Rect.height"/> is positive, this method moves the <see cref="Rect.y"/> by that amount and
        /// adds the <see cref="UnityEditor.EditorGUIUtility.standardVerticalSpacing"/>.
        /// </summary>
        public static void NextVerticalArea(ref Rect area)
        {
            if (area.height > 0)
                area.y += area.height + StandardSpacing;
        }
    }
}
#endif
