#region

using System;
using System.Diagnostics;
using System.Globalization;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// 下拉框检视器（支持 string、int、float 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class DropdownAttribute : InspectorAttribute
    {
        /// <summary>
        /// 下拉框检视器（string、int、float）
        /// </summary>
        /// <param name="values">下拉框可选值</param>
        public DropdownAttribute(params string[] values)
        {
#if UNITY_EDITOR
            ValueType      = typeof(string);
            Values         = new object[values.Length];
            DisplayOptions = values;
            for (var i = 0; i < values.Length; i++)
            {
                Values[i]         = values[i];
                DisplayOptions[i] = values[i].ToString(CultureInfo.CurrentCulture);
            }
#endif
        }

        /// <summary>
        /// 下拉框检视器（string、int、float）
        /// </summary>
        /// <param name="values">下拉框可选值</param>
        public DropdownAttribute(params int[] values)
        {
#if UNITY_EDITOR
            ValueType      = typeof(int);
            Values         = new object[values.Length];
            DisplayOptions = new string[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                Values[i]         = values[i];
                DisplayOptions[i] = values[i].ToString();
            }
#endif
        }

        /// <summary>
        /// 下拉框检视器（string、int、float）
        /// </summary>
        /// <param name="values">下拉框可选值</param>
        public DropdownAttribute(params float[] values)
        {
#if UNITY_EDITOR
            ValueType      = typeof(float);
            Values         = new object[values.Length];
            DisplayOptions = new string[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                Values[i]         = values[i];
                DisplayOptions[i] = values[i].ToString(CultureInfo.CurrentCulture);
            }
#endif
        }
#if UNITY_EDITOR
        public Type     ValueType      { get; private set; }
        public object[] Values         { get; private set; }
        public string[] DisplayOptions { get; private set; }
#endif
    }
}