/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-28
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AIO
{
    internal class UnitsFloatAttribute : UnitsAttribute
    {
        private Dictionary<double, float> MultipliersDic = new Dictionary<double, float>();

        /// <summary>[Editor-Only] The unit conversion ratios.</summary>
        /// <remarks><c>valueInUnitX = valueInBaseUnits * Multipliers[x];</c></remarks>
        public IReadOnlyList<double> Multipliers { get; private set; }

        /// <summary>[Editor-Only]
        /// The value to display if the actual value is <see cref="float.NaN"/>.
        /// </summary>
        public float DefaultValue { get; set; }

        /// <summary>Creates a new <see cref="UnitsAttribute"/>.</summary>
        public UnitsFloatAttribute(IReadOnlyList<double> multipliers, IList<CompactUnitConversionCache> suffixes, int unitIndex = 0)
        {
            Multipliers = multipliers;
            UnitIndex = unitIndex;
            DisplayConverters = suffixes;
        }

        /// <summary>[Editor-Only]
        /// Draws this attribute's fields.
        /// </summary>
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
            var index = 0;
            var max = 1d;
            foreach (var multiplier in Multipliers)
            {
                if (!double.IsNaN(multiplier))
                {
                    count++;
                    last = index;
                }

                index++;
                max *= multiplier;
            }

            var width = (allFieldArea.width - EditorGUIUtility.standardVerticalSpacing * (count - 1)) / count;
            var fieldArea = new Rect(allFieldArea.x, allFieldArea.y, width, allFieldArea.height);

            var displayValue = GetDisplayValue(value, DefaultValue);

            // Draw the active fields.
            label.text = string.Concat(label.text, " -> [", displayValue.ToString("0.##"), "]");
            for (var i = 0; i < Multipliers.Count; i++)
            {
                if (double.IsNaN(Multipliers[i])) continue;
                var display = 0f;

                if (Multipliers[i] <= Math.Abs(displayValue))
                {
                    if (Multipliers.Count == 1)
                    {
                        display = displayValue / (float)Multipliers[i];
                    }
                    else display = Mathf.Floor(displayValue / (float)Multipliers[i]);

                    displayValue %= (display * (float)Multipliers[i]);
                }

                if (!MultipliersDic.ContainsKey(Multipliers[i])) MultipliersDic.Add(Multipliers[i], display);
                else MultipliersDic[Multipliers[i]] = display;

                if (hasLabel) fieldArea.xMin = area.xMin;
                else if (i < last)
                {
                    fieldArea.width = width;
                    fieldArea.xMax = Mathf.Round(fieldArea.xMax);
                }
                else fieldArea.xMax = area.xMax;

                EditorGUI.BeginChangeCheck();

                MultipliersDic[Multipliers[i]] = DoSpecialField(fieldArea, label, MultipliersDic[Multipliers[i]], DisplayConverters[i]);

                if (EditorGUI.EndChangeCheck())
                {
                    value = 0;
                    foreach (var multiplier in Multipliers)
                    {
                        value += MultipliersDic[multiplier] * (float)multiplier;
                    }
                }

                label = null;
                hasLabel = false;

                fieldArea.x += fieldArea.width + EditorGUIUtility.standardVerticalSpacing;
            }

            DoOptionalAfterGUI(IsOptional, toggleArea, ref value, DefaultValue, guiWasEnabled, previousLabelWidth);

            Validate.ValueRule(ref value, Rule);
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

        /// <summary>[Editor-Only]
        /// Begins a GUI property block to be ended by EndProperty
        /// </summary>
        protected static void BeginProperty(Rect area, SerializedProperty property, ref GUIContent label, out float value)
        {
            label = EditorGUI.BeginProperty(area, label, property);
            EditorGUI.BeginChangeCheck();
            value = property.floatValue;
        }

        /// <summary>[Editor-Only]
        /// Ends a GUI property block started by BeginProperty
        /// </summary>
        protected static void EndProperty(Rect area, SerializedProperty property, ref float value)
        {
            if (UnitGUI.TryUseClickEvent(area, 2))
                DefaultValueAttribute.SetToDefault(ref value, property);

            if (EditorGUI.EndChangeCheck())
                property.floatValue = value;

            EditorGUI.EndProperty();
        }

        public override void OnGUI(Rect area, SerializedProperty property, GUIContent label)
        {
            BeginProperty(area, property, ref label, out var value);
            DoFieldGUI(area, label, ref value);
            EndProperty(area, property, ref value);
        }
    }
}