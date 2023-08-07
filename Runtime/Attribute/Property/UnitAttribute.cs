/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-28
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AIO
{
    /// <summary>
    /// 单位属性
    /// </summary>
    [Conditional(Strings.UnityEditor)]
    public partial class UnitAttribute : SelfDrawerAttribute
    {
        private UnitsAttribute _unitsAttribute;

        public IReadOnlyList<double> MultipliersDouble { get; private set; }

        public int UnitIndex { get; private set; }

        public IList<CompactUnitConversionCache> DisplayConverters { get; protected set; }

        /// <summary>
        /// Creates a new <see cref="UnitsAttribute"/>.
        /// </summary>
        protected UnitAttribute()
        {
        }

        #region Construction

        /// <summary>
        /// Creates a new <see cref="UnitAttribute"/>.
        /// </summary>
        public UnitAttribute(string suffix)
        {
            SetUnits(new double[] { 1 }, new CompactUnitConversionCache[] { new CompactUnitConversionCache(suffix) }, 0);
        }

        /// <summary>Creates a new <see cref="UnitAttribute"/>.</summary>
        public UnitAttribute(IDictionary<double, string> data, int unitIndex = 0)
        {
            var multipliers = new double[data.Count];
            var suffixes = new string[data.Count];
            var i = 0;
            foreach (var item in data)
            {
                multipliers[i] = item.Key;
                suffixes[i] = item.Value;
                i++;
            }

            SetUnits(multipliers, new CompactUnitConversionCache[suffixes.Length], unitIndex);
            for (i = 0; i < suffixes.Length; i++)
                DisplayConverters[i] = new CompactUnitConversionCache(suffixes[i]);
        }

        /// <summary>Creates a new <see cref="UnitAttribute"/>.</summary>
        public UnitAttribute(IReadOnlyList<double> multipliers, IReadOnlyList<string> suffixes, int unitIndex = 0)
        {
            SetUnits(multipliers, new CompactUnitConversionCache[suffixes.Count], unitIndex);
            for (var i = 0; i < suffixes.Count; i++) DisplayConverters[i] = new CompactUnitConversionCache(suffixes[i]);
        }

        /// <summary>[Editor-Only] Sets the unit details.</summary>
        protected void SetUnits(IReadOnlyList<double> multipliers, IList<CompactUnitConversionCache> displayConverters, int unitIndex = 0)
        {
            if (multipliers.Count != displayConverters.Count)
                throw new ArgumentException(
                    $"[Units] {nameof(MultipliersDouble)} and {nameof(DisplayConverters)} must have the same Length.");

            if (unitIndex < 0 || unitIndex >= multipliers.Count)
                throw new ArgumentOutOfRangeException(
                    $"[Units] {nameof(UnitIndex)} must be an index in the {nameof(MultipliersDouble)} array.");

            MultipliersDouble = multipliers;
            DisplayConverters = displayConverters;
            UnitIndex = unitIndex;
        }

        #endregion

        private void Check(string type)
        {
            if (_unitsAttribute == null)
            {
                switch (type)
                {
                    case "double":
                        _unitsAttribute = new UnitsDoubleAttribute(MultipliersDouble, DisplayConverters, UnitIndex);
                        break;
                    case "float":
                        _unitsAttribute = new UnitsFloatAttribute(MultipliersDouble, DisplayConverters, UnitIndex);
                        break;
                    case "long":
                        _unitsAttribute = new UnitsLongAttribute(MultipliersDouble, DisplayConverters, UnitIndex);
                        break;
                    case "int":
                        _unitsAttribute = new UnitsInt32Attribute(MultipliersDouble, DisplayConverters, UnitIndex);
                        break;
                    default: return;
                }
            }
        }

#if UNITY_EDITOR
        public override void OnGUI(Rect area, SerializedProperty property, GUIContent label)
        {
            Check(property.type);
            if (_unitsAttribute is null)
            {
                EditorGUI.LabelField(area, label.text, $"[Error] Unit {property.type} is not supported", EditorStyles.helpBox);
            }
            else _unitsAttribute.OnGUI(area, property, label);
        }

        /// <summary> [Editor-Only]
        /// Determines how many lines tall the `property` should be.
        /// </summary>
        private int GetLineCount(SerializedProperty property, GUIContent label) => EditorGUIUtility.wideMode ? 1 : 2;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Check(property.type);
            if (_unitsAttribute is null)
            {
                var lineCount = GetLineCount(property, label);
                return EditorGUIUtility.singleLineHeight * lineCount + EditorGUIUtility.standardVerticalSpacing * (lineCount - 1);
            }

            return _unitsAttribute.GetPropertyHeight(property, label);
        }
#endif
    }
}