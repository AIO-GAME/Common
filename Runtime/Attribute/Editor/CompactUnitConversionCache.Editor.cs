/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-07-28
|||✩ Document: ||| ->
|||✩ - - - - - |*/

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;

namespace AIO
{
    /// <summary>
    /// CompactUnitConversionCache_Editor
    /// </summary>
    public partial class CompactUnitConversionCache
    {
        #region Convert float

        /// <summary>Calculate the index of the cache to use for the given parameters.</summary>
        private int CalculateCacheIndex(float value, float width)
        {
            //if (value > LargeExponentialThreshold ||
            //    value < -LargeExponentialThreshold)
            //    return 0;

            var valueString = value.ToStringCached();

            // It the approximated string wouldn't be shorter than the original, don't approximate.
            if (valueString.Length < 2 + ApproximateSuffix.Length)
                return 0;

            if (_SuffixWidth == 0)
            {
                if (_WidthCache == null)
                {
                    _WidthCache = UnitGUI.CreateWidthCache(EditorStyles.numberField);
                    _FieldPadding = EditorStyles.numberField.padding.horizontal;

                    _ApproximateSymbolWidth = _WidthCache.Convert("~") - _FieldPadding;
                }

                _SuffixWidth = _WidthCache.Convert(Suffix);
            }

            // If the field is wide enough to fit the full value, don't approximate.
            width -= _FieldPadding + _ApproximateSymbolWidth * 0.75f;
            var valueWidth = _WidthCache.Convert(valueString) + _SuffixWidth;
            if (valueWidth <= width)
                return 0;

            // If the number of allowed characters would include the full value, don't approximate.
            var suffixedLength = valueString.Length + Suffix.Length;
            var allowedCharacters = (int)(suffixedLength * width / valueWidth);
            if (allowedCharacters + 2 >= suffixedLength)
                return 0;

            return allowedCharacters;
        }


        /// <summary>
        /// Returns a cached string representing the `value` trimmed to fit within the `width` (if necessary) and with
        /// the <see cref="Suffix"/> added on the end.
        /// </summary>
        public string Convert(float value, float width, bool showApproximations = false)
        {
            if (value == 0) return ConvertedZero;

            if (showApproximations) return GetCacheDouble(0).Convert(value);

            if (value < SmallExponentialThreshold &&
                value > -SmallExponentialThreshold)
                return value > 0 ? ConvertedSmallPositive : ConvertedSmallNegative;

            var index = CalculateCacheIndex(value, width);
            return GetCacheDouble(index).Convert(value);
        }

        /// <summary>Creates and returns a cache for the specified `characterCount`.</summary>
        private ConversionCache<double, string> GetCacheDouble(int characterCount)
        {
            while (CachesDouble.Count <= characterCount)
                CachesDouble.Add(null);

            var cache = CachesDouble[characterCount];
            if (cache == null)
            {
                if (characterCount == 0)
                {
                    cache = new ConversionCache<double, string>(value => value.ToStringCached() + Suffix);
                }
                else
                {
                    cache = new ConversionCache<double, string>(value =>
                    {
                        var valueString = value.ToStringCached();

                        if (value > LargeExponentialThreshold ||
                            value < -LargeExponentialThreshold)
                            goto IsExponential;

                        if (_DecimalSeparator == null)
                            _DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                        var decimalIndex = valueString.IndexOf(_DecimalSeparator, StringComparison.CurrentCulture);
                        if (decimalIndex < 0 || decimalIndex > characterCount)
                            goto IsExponential;

                        // Not exponential.
                        return valueString.Substring(0, characterCount) + ApproximateSuffix;

                        IsExponential:
                        var digits = Math.Max(0, characterCount - ApproximateSuffix.Length - 1);
                        var format = GetExponentialFormat(digits);
                        valueString = value.ToString(format);
                        TrimExponential(ref valueString);
                        return valueString + Suffix;
                    });
                }

                CachesDouble[characterCount] = cache;
            }

            return cache;
        }

        #endregion

        #region Convert double

        /// <summary>
        /// Returns a cached string representing the `value` trimmed to fit within the `width` (if necessary) and with
        /// the <see cref="Suffix"/> added on the end.
        /// </summary>
        private int CalculateCacheIndex(double value, float width)
        {
            //if (value > LargeExponentialThreshold ||
            //    value < -LargeExponentialThreshold)
            //    return 0;

            var valueString = value.ToStringCached();

            // It the approximated string wouldn't be shorter than the original, don't approximate.
            if (valueString.Length < 2 + ApproximateSuffix.Length)
                return 0;

            if (_SuffixWidth == 0)
            {
                if (_WidthCache == null)
                {
                    _WidthCache = UnitGUI.CreateWidthCache(EditorStyles.numberField);
                    _FieldPadding = EditorStyles.numberField.padding.horizontal;

                    _ApproximateSymbolWidth = _WidthCache.Convert("~") - _FieldPadding;
                }

                _SuffixWidth = _WidthCache.Convert(Suffix);
            }

            // If the field is wide enough to fit the full value, don't approximate.
            width -= _FieldPadding + _ApproximateSymbolWidth * 0.75f;
            var valueWidth = _WidthCache.Convert(valueString) + _SuffixWidth;
            if (valueWidth <= width)
                return 0;

            // If the number of allowed characters would include the full value, don't approximate.
            var suffixedLength = valueString.Length + Suffix.Length;
            var allowedCharacters = (int)(suffixedLength * width / valueWidth);
            if (allowedCharacters + 2 >= suffixedLength)
                return 0;

            return allowedCharacters;
        }

        /// <summary>
        /// Returns a cached string representing the `value` trimmed to fit within the `width` (if necessary) and with
        /// the <see cref="Suffix"/> added on the end.
        /// </summary>
        public string Convert(double value, float width, bool showApproximations = false)
        {
            if (value == 0) return ConvertedZero;

            if (showApproximations) return GetCacheDouble(0).Convert(value);

            if (value < SmallExponentialThreshold &&
                value > -SmallExponentialThreshold)
                return value > 0 ? ConvertedSmallPositive : ConvertedSmallNegative;

            var index = CalculateCacheIndex(value, width);
            return GetCacheDouble(index).Convert(value);
        }

        #endregion

        #region Convert int

        /// <summary>Creates and returns a cache for the specified `characterCount`.</summary>
        private ConversionCache<int, string> GetCacheInt(int characterCount)
        {
            while (CachesInt.Count <= characterCount)
                CachesInt.Add(null);

            var cache = CachesInt[characterCount];
            if (cache == null)
            {
                if (characterCount == 0)
                {
                    cache = new ConversionCache<int, string>(value => value.ToStringCached() + Suffix);
                }
                else
                {
                    cache = new ConversionCache<int, string>(value =>
                    {
                        var valueString = value.ToStringCached();

                        if (value > LargeExponentialThreshold ||
                            value < -LargeExponentialThreshold)
                            goto IsExponential;

                        if (_DecimalSeparator == null)
                            _DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                        var decimalIndex = valueString.IndexOf(_DecimalSeparator, StringComparison.CurrentCulture);
                        if (decimalIndex < 0 || decimalIndex > characterCount)
                            goto IsExponential;

                        // Not exponential.
                        return valueString.Substring(0, characterCount) + ApproximateSuffix;

                        IsExponential:
                        var digits = Math.Max(0, characterCount - ApproximateSuffix.Length - 1);
                        var format = GetExponentialFormat(digits);
                        valueString = value.ToString(format);
                        TrimExponential(ref valueString);
                        return valueString + Suffix;
                    });
                }

                CachesInt[characterCount] = cache;
            }

            return cache;
        }

        /// <summary>
        /// Returns a cached string representing the `value` trimmed to fit within the `width` (if necessary) and with
        /// the <see cref="Suffix"/> added on the end.
        /// </summary>
        private int CalculateCacheIndex(int value, float width)
        {
            //if (value > LargeExponentialThreshold ||
            //    value < -LargeExponentialThreshold)
            //    return 0;

            var valueString = value.ToStringCached();

            // It the approximated string wouldn't be shorter than the original, don't approximate.
            if (valueString.Length < 2 + ApproximateSuffix.Length)
                return 0;

            if (_SuffixWidth == 0)
            {
                if (_WidthCache == null)
                {
                    _WidthCache = UnitGUI.CreateWidthCache(EditorStyles.numberField);
                    _FieldPadding = EditorStyles.numberField.padding.horizontal;

                    _ApproximateSymbolWidth = _WidthCache.Convert("~") - _FieldPadding;
                }

                _SuffixWidth = _WidthCache.Convert(Suffix);
            }

            // If the field is wide enough to fit the full value, don't approximate.
            width -= _FieldPadding + _ApproximateSymbolWidth * 0.75f;
            var valueWidth = _WidthCache.Convert(valueString) + _SuffixWidth;
            if (valueWidth <= width)
                return 0;

            // If the number of allowed characters would include the full value, don't approximate.
            var suffixedLength = valueString.Length + Suffix.Length;
            var allowedCharacters = (int)(suffixedLength * width / valueWidth);
            if (allowedCharacters + 2 >= suffixedLength)
                return 0;

            return allowedCharacters;
        }

        /// <summary>
        /// Returns a cached string representing the `value` trimmed to fit within the `width` (if necessary) and with
        /// the <see cref="Suffix"/> added on the end.
        /// </summary>
        public string Convert(int value, float width, bool showApproximations = false)
        {
            if (value == 0) return ConvertedZero;

            if (showApproximations) return GetCacheInt(0).Convert(value);

            if (value < SmallExponentialThreshold &&
                value > -SmallExponentialThreshold)
                return value > 0 ? ConvertedSmallPositive : ConvertedSmallNegative;

            var index = CalculateCacheIndex(value, width);
            return GetCacheInt(index).Convert(value);
        }

        #endregion

        /************************************************************************************************************************/

        /************************************************************************************************************************/

        /************************************************************************************************************************/

        private static List<string> _ExponentialFormats;

        /// <summary>Returns a format string to include the specified number of `digits` in an exponential number.</summary>
        public static string GetExponentialFormat(int digits)
        {
            if (_ExponentialFormats == null)
                _ExponentialFormats = new List<string>();

            while (_ExponentialFormats.Count <= digits)
                _ExponentialFormats.Add("g" + _ExponentialFormats.Count);

            return _ExponentialFormats[digits];
        }

        /************************************************************************************************************************/

        private static void TrimExponential(ref string valueString)
        {
            var length = valueString.Length;
            if (length <= 4 ||
                valueString[length - 4] != 'e' ||
                valueString[length - 2] != '0')
                return;

            valueString =
                valueString.Substring(0, length - 2) +
                valueString[length - 1];
        }
    }
}
#endif
