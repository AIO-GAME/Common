using System;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEditor;

using UnityEngine;

namespace AIO
{
    /// <summary>
    /// 单位属性
    /// </summary>
    [Conditional(Strings.UnityEditor)]
    public class UnitsAttribute : SelfDrawerAttribute
    {
        /// <summary>
        /// The validation rule applied to the value.
        /// </summary>
        public Validate.Value Rule { get; set; }

        /// <summary>
        /// Creates a new <see cref="UnitsAttribute"/>.
        /// </summary>
        protected UnitsAttribute()
        {
        }

        /// <summary>Creates a new <see cref="UnitsAttribute"/>.</summary>
        public UnitsAttribute(string suffix)
        {
            SetUnits(new float[] { 1 }, new CompactUnitConversionCache[] { new CompactUnitConversionCache(suffix) }, 0);
        }

        /// <summary>Creates a new <see cref="UnitsAttribute"/>.</summary>
        public UnitsAttribute(float[] multipliers, IReadOnlyList<string> suffixes, int unitIndex = 0)
        {
            SetUnits(multipliers, new CompactUnitConversionCache[suffixes.Count], unitIndex);
            for (var i = 0; i < suffixes.Count; i++)
                DisplayConverters[i] = new CompactUnitConversionCache(suffixes[i]);
        }


        /// <summary>[Editor-Only] The unit conversion ratios.</summary>
        /// <remarks><c>valueInUnitX = valueInBaseUnits * Multipliers[x];</c></remarks>
        public float[] Multipliers { get; private set; }

        /// <summary>[Editor-Only] The converters used to generate display strings for each of the fields.</summary>
        public CompactUnitConversionCache[] DisplayConverters { get; private set; }

        /// <summary>[Editor-Only] The index of the <see cref="DisplayConverters"/> for the attributed serialized value.</summary>
        public int UnitIndex { get; private set; }

        /// <summary>[Editor-Only] Should the field have a toggle to set its value to <see cref="float.NaN"/>?</summary>
        public bool IsOptional { get; set; }

        /// <summary>[Editor-Only] The value to display if the actual value is <see cref="float.NaN"/>.</summary>
        public float DefaultValue { get; set; }

        /// <summary>[Editor-Only] Sets the unit details.</summary>
        protected void SetUnits(float[] multipliers, CompactUnitConversionCache[] displayConverters, int unitIndex = 0)
        {
            if (multipliers.Length != displayConverters.Length)
                throw new ArgumentException(
                    $"[Units] {nameof(Multipliers)} and {nameof(DisplayConverters)} must have the same Length.");

            if (unitIndex < 0 || unitIndex >= multipliers.Length)
                throw new ArgumentOutOfRangeException(
                    $"[Units] {nameof(UnitIndex)} must be an index in the {nameof(Multipliers)} array.");

            Multipliers = multipliers;
            DisplayConverters = displayConverters;
            UnitIndex = unitIndex;
        }

        /// <inheritdoc/>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var lineCount = GetLineCount(property, label);
            return EditorGUIUtility.singleLineHeight * lineCount + EditorGUIUtility.standardVerticalSpacing * (lineCount - 1);
        }

        /// <summary>[Editor-Only] Determines how many lines tall the `property` should be.</summary>
        protected virtual int GetLineCount(SerializedProperty property, GUIContent label)
            => EditorGUIUtility.wideMode ? 1 : 2;


        /// <summary>[Editor-Only] Begins a GUI property block to be ended by <see cref="EndProperty"/>.</summary>
        protected static void BeginProperty(Rect area, SerializedProperty property, ref GUIContent label, out float value)
        {
            label = EditorGUI.BeginProperty(area, label, property);
            EditorGUI.BeginChangeCheck();
            value = property.floatValue;
        }

        /// <summary>[Editor-Only] Ends a GUI property block started by <see cref="BeginProperty"/>.</summary>
        protected static void EndProperty(Rect area, SerializedProperty property, ref float value)
        {
            if (UnitGUI.TryUseClickEvent(area, 2))
                DefaultValueAttribute.SetToDefault(ref value, property);

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUI.EndProperty();
        }


        /// <summary>[Editor-Only]
        /// Draws this attribute's fields for the `property`.
        /// </summary>
        public override void OnGUI(Rect area, SerializedProperty property, GUIContent label)
        {
            BeginProperty(area, property, ref label, out var value);
            DoFieldGUI(area, label, ref value);
            EndProperty(area, property, ref value);
        }


        /// <summary>[Editor-Only] Draws this attribute's fields.</summary>
        protected void DoFieldGUI(Rect area, GUIContent label, ref float value)
        {
            var isMultiLine = area.height >= EditorGUIUtility.singleLineHeight * 2;
            area.height = EditorGUIUtility.singleLineHeight;

            DoOptionalBeforeGUI(IsOptional, area, out var toggleArea, out var guiWasEnabled, out var previousLabelWidth);

            var hasLabel = label != null && !string.IsNullOrEmpty(label.text);
            Rect allFieldArea;

            if (isMultiLine)
            {
                EditorGUI.LabelField(area, label);
                label = null;
                UnitGUI.NextVerticalArea(ref area);

                EditorGUI.indentLevel++;
                allFieldArea = EditorGUI.IndentedRect(area);
                EditorGUI.indentLevel--;
            }
            else if (hasLabel)
            {
                var labelXMax = area.x + EditorGUIUtility.labelWidth;
                allFieldArea = new Rect(labelXMax, area.y, area.xMax - labelXMax, area.height);
            }
            else
            {
                allFieldArea = area;
            }

            // Count the number of active fields.
            var count = 0;
            var last = 0;
            for (int i = 0; i < Multipliers.Length; i++)
            {
                if (!float.IsNaN(Multipliers[i]))
                {
                    count++;
                    last = i;
                }
            }

            var width = (allFieldArea.width - EditorGUIUtility.standardVerticalSpacing * (count - 1)) / count;
            var fieldArea = new Rect(allFieldArea.x, allFieldArea.y, width, allFieldArea.height);

            var displayValue = GetDisplayValue(value, DefaultValue);

            // Draw the active fields.
            for (int i = 0; i < Multipliers.Length; i++)
            {
                var multiplier = Multipliers[i];
                if (float.IsNaN(multiplier))
                    continue;

                if (hasLabel)
                {
                    fieldArea.xMin = area.xMin;
                }
                else if (i < last)
                {
                    fieldArea.width = width;
                    fieldArea.xMax = Mathf.Round(fieldArea.xMax);
                }
                else
                {
                    fieldArea.xMax = area.xMax;
                }

                EditorGUI.BeginChangeCheck();

                var fieldValue = displayValue * multiplier;
                fieldValue = DoSpecialFloatField(fieldArea, label, fieldValue, DisplayConverters[i]);
                label = null;
                hasLabel = false;

                if (EditorGUI.EndChangeCheck())
                    value = fieldValue / multiplier;

                fieldArea.x += fieldArea.width + EditorGUIUtility.standardVerticalSpacing;
            }

            DoOptionalAfterGUI(IsOptional, toggleArea, ref value, DefaultValue, guiWasEnabled, previousLabelWidth);

            Validate.ValueRule(ref value, Rule);
        }


        /// <summary>[Editor-Only]
        /// Draws a <see cref="EditorGUI.FloatField(Rect, GUIContent, float)"/> with an alternate string when it is not
        /// selected (for example, "1" might become "1s" to indicate "seconds").
        /// </summary>
        /// <remarks>
        /// This method treats most <see cref="EventType"/>s normally, but for <see cref="EventType.Repaint"/> it
        /// instead draws a text field with the converted string.
        /// </remarks>
        public static float DoSpecialFloatField(Rect area, GUIContent label, float value, CompactUnitConversionCache toString)
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

        private void DoOptionalBeforeGUI(bool isOptional, Rect area, out Rect toggleArea, out bool guiWasEnabled, out float previousLabelWidth)
        {
            toggleArea = area;
            guiWasEnabled = GUI.enabled;
            previousLabelWidth = EditorGUIUtility.labelWidth;
            if (!isOptional)
                return;

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

        private void DoOptionalAfterGUI(bool isOptional, Rect area, ref float value, float defaultValue, bool guiWasEnabled, float previousLabelWidth)
        {
            GUI.enabled = guiWasEnabled;
            EditorGUIUtility.labelWidth = previousLabelWidth;

            if (!isOptional)
                return;

            area.x += UnitGUI.StandardSpacing;

            var wasEnabled = !float.IsNaN(value);

            // Use the EditorGUI method instead to properly handle EditorGUI.showMixedValue.
            //var isEnabled = GUI.Toggle(area, wasEnabled, GUIContent.none);

            var indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var isEnabled = EditorGUI.Toggle(area, wasEnabled);

            EditorGUI.indentLevel = indentLevel;

            if (isEnabled != wasEnabled)
            {
                value = isEnabled ? defaultValue : float.NaN;
                UnitGUI.Deselect();
            }
        }

        /// <summary>[Editor-Only] Returns the value that should be displayed for a given field.</summary>
        public static float GetDisplayValue(float value, float defaultValue)
            => float.IsNaN(value) ? defaultValue : value;
    }
}