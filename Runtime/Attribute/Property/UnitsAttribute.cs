using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AIO
{
    /// <summary>
    /// 单位属性
    /// </summary>
    internal abstract partial class UnitsAttribute
    {
        /// <summary>
        /// Creates a new <see cref="UnitsAttribute"/>.
        /// </summary>
        protected UnitsAttribute()
        {
        }

        /// <summary>
        /// The validation rule applied to the value.
        /// </summary>
        public Validate.Value Rule { get; set; }

        /// <summary>[Editor-Only]
        /// The converters used to generate display strings for each of the fields.
        /// </summary>
        public IList<CompactUnitConversionCache> DisplayConverters { get; protected set; }
        
        /// <summary>[Editor-Only]
        /// The index of the <see cref="DisplayConverters"/> for the attributed serialized value.
        /// </summary>
        public int UnitIndex { get; protected set; }

        /// <summary>[Editor-Only]
        /// Should the field have a toggle to set its value to <see cref="float.NaN"/>?
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>[Editor-Only]
        /// Returns the value that should be displayed for a given field.
        /// </summary>
        public static float GetDisplayValue(float value, float defaultValue)
        {
            return float.IsNaN(value) ? defaultValue : value;
        }

        /// <summary>[Editor-Only]
        /// Returns the value that should be displayed for a given field.
        /// </summary>
        public static double GetDisplayValue(double value, double defaultValue)
        {
            return double.IsNaN(value) ? defaultValue : value;
        }

        /// <summary>[Editor-Only]
        /// Returns the value that should be displayed for a given field.
        /// </summary>
        public static long GetDisplayValue(long value, long defaultValue)
        {
            if (value > long.MaxValue || value < long.MinValue)
                return defaultValue;
            return value;
        }
        
        /// <summary>[Editor-Only]
        /// Returns the value that should be displayed for a given field.
        /// </summary>
        public static int GetDisplayValue(int value, int defaultValue)
        {
            if (value > int.MaxValue || value < int.MinValue)
                return defaultValue;
            return value;
        }
    }
}