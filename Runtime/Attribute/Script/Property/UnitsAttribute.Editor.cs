/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-28
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace AIO
{
    internal partial class UnitsAttribute
    {
#if UNITY_EDITOR
        public virtual float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var lineCount = GetLineCount(property, label);
            return EditorGUIUtility.singleLineHeight * lineCount + EditorGUIUtility.standardVerticalSpacing * (lineCount - 1);
        }

        /// <summary> [Editor-Only]
        /// Determines how many lines tall the `property` should be.
        /// </summary>
        protected virtual int GetLineCount(SerializedProperty property, GUIContent label) => EditorGUIUtility.wideMode ? 1 : 2;

        /// <summary>[Editor-Only]
        /// Draws this attribute's fields for the `property`.
        /// </summary>
        public abstract void OnGUI(Rect area, SerializedProperty property, GUIContent label);

        protected void DoOptionalBeforeGUI(bool isOptional, Rect area, out Rect toggleArea, out bool guiWasEnabled, out float previousLabelWidth)
        {
            toggleArea = area;
            guiWasEnabled = GUI.enabled;
            previousLabelWidth = EditorGUIUtility.labelWidth;
            if (!isOptional) return;

            toggleArea.x += previousLabelWidth;

            toggleArea.width = UnitGUI.ToggleWidth;
            EditorGUIUtility.labelWidth += toggleArea.width;

            EditorGUIUtility.AddCursorRect(toggleArea, MouseCursor.Arrow);

            // We need to draw the toggle after everything else to it goes on top of the label. But we want it to
            // get priority for input events, so we disable the other controls during those events in its area.
            var currentEvent = Event.current;
            if (guiWasEnabled && toggleArea.Contains(currentEvent.mousePosition))
            {
                switch (currentEvent.type)
                {
                    case EventType.Repaint:
                    case EventType.Layout:
                        break;

                    default:
                        GUI.enabled = false;
                        break;
                }
            }
        }

        #region Do Special Field

        /// <summary>[Editor-Only]
        /// Draws a <see cref="EditorGUI.FloatField(Rect, GUIContent, float)"/> with an alternate string when it is not
        /// selected (for example, "1" might become "1s" to indicate "seconds").
        /// </summary>
        /// <remarks>
        /// This method treats most <see cref="EventType"/>s normally, but for <see cref="EventType.Repaint"/> it
        /// instead draws a text field with the converted string.
        /// </remarks>
        protected static double DoSpecialField(Rect area, GUIContent label, double value, CompactUnitConversionCache toString)
        {
            if (label != null && !string.IsNullOrEmpty(label.text))
            {
                if (Event.current.type != EventType.Repaint)
                    return EditorGUI.DoubleField(area, label, value);

                var dragArea = new Rect(area.x, area.y, EditorGUIUtility.labelWidth, area.height);
                EditorGUIUtility.AddCursorRect(dragArea, MouseCursor.SlideArrow);

                var text = toString.Convert(value, area.width - EditorGUIUtility.labelWidth);
                EditorGUI.TextField(area, label, text);
            }
            else
            {
                var indentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                if (Event.current.type != EventType.Repaint)
                    value = EditorGUI.DoubleField(area, value);
                else
                    EditorGUI.TextField(area, toString.Convert(value, area.width));

                EditorGUI.indentLevel = indentLevel;
            }

            return value;
        }

        /// <summary>[Editor-Only]
        /// Draws a <see cref="EditorGUI.FloatField(Rect, GUIContent, float)"/> with an alternate string when it is not
        /// selected (for example, "1" might become "1s" to indicate "seconds").
        /// </summary>
        /// <remarks>
        /// This method treats most <see cref="EventType"/>s normally, but for <see cref="EventType.Repaint"/> it
        /// instead draws a text field with the converted string.
        /// </remarks>
        public static long DoSpecialField(Rect area, GUIContent label, long value, CompactUnitConversionCache toString)
        {
            if (label != null && !string.IsNullOrEmpty(label.text))
            {
                if (Event.current.type != EventType.Repaint)
                    return EditorGUI.LongField(area, label, value);

                var dragArea = new Rect(area.x, area.y, EditorGUIUtility.labelWidth, area.height);
                EditorGUIUtility.AddCursorRect(dragArea, MouseCursor.SlideArrow);

                var text = toString.Convert(value, area.width - EditorGUIUtility.labelWidth);
                EditorGUI.TextField(area, label, text);
            }
            else
            {
                var indentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                if (Event.current.type != EventType.Repaint)
                    value = EditorGUI.LongField(area, value);
                else
                    EditorGUI.TextField(area, toString.Convert(value, area.width));

                EditorGUI.indentLevel = indentLevel;
            }

            return value;
        }

        /// <summary>[Editor-Only]
        /// Draws a <see cref="EditorGUI.FloatField(Rect, GUIContent, float)"/> with an alternate string when it is not
        /// selected (for example, "1" might become "1s" to indicate "seconds").
        /// </summary>
        /// <remarks>
        /// This method treats most <see cref="EventType"/>s normally, but for <see cref="EventType.Repaint"/> it
        /// instead draws a text field with the converted string.
        /// </remarks>
        public static int DoSpecialField(Rect area, GUIContent label, int value, CompactUnitConversionCache toString)
        {
            if (label != null && !string.IsNullOrEmpty(label.text))
            {
                if (Event.current.type != EventType.Repaint)
                    return EditorGUI.IntField(area, label, value);

                var dragArea = new Rect(area.x, area.y, EditorGUIUtility.labelWidth, area.height);
                EditorGUIUtility.AddCursorRect(dragArea, MouseCursor.SlideArrow);

                var text = toString.Convert(value, area.width - EditorGUIUtility.labelWidth);
                EditorGUI.TextField(area, label, text);
            }
            else
            {
                var indentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                if (Event.current.type != EventType.Repaint)
                    value = EditorGUI.IntField(area, value);
                else
                    EditorGUI.TextField(area, toString.Convert(value, area.width));

                EditorGUI.indentLevel = indentLevel;
            }

            return value;
        }

        /// <summary>[Editor-Only]
        /// Draws a <see cref="EditorGUI.FloatField(Rect, GUIContent, float)"/> with an alternate string when it is not
        /// selected (for example, "1" might become "1s" to indicate "seconds").
        /// </summary>
        /// <remarks>
        /// This method treats most <see cref="EventType"/>s normally, but for <see cref="EventType.Repaint"/> it
        /// instead draws a text field with the converted string.
        /// </remarks>
        public static float DoSpecialField(Rect area, GUIContent label, float value, CompactUnitConversionCache toString)
        {
            if (label != null && !string.IsNullOrEmpty(label.text))
            {
                if (Event.current.type != EventType.Repaint)
                    return EditorGUI.FloatField(area, label, value);

                var dragArea = new Rect(area.x, area.y, EditorGUIUtility.labelWidth, area.height);
                EditorGUIUtility.AddCursorRect(dragArea, MouseCursor.SlideArrow);

                var text = toString.Convert(value, area.width - EditorGUIUtility.labelWidth);
                EditorGUI.TextField(area, label, text);
            }
            else
            {
                var indentLevel = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                if (Event.current.type != EventType.Repaint)
                    value = EditorGUI.FloatField(area, value);
                else
                    EditorGUI.TextField(area, toString.Convert(value, area.width));

                EditorGUI.indentLevel = indentLevel;
            }

            return value;
        }
        #endregion
#endif
    }
}