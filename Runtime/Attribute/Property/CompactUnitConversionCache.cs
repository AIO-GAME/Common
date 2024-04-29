#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>[Editor-Only]
    /// A system for formatting floats as strings that fit into a limited area and storing the results so they can be
    /// reused to minimise the need for garbage collection, particularly for string construction.
    /// </summary>
    /// <example>
    /// With <c>"x"</c> as the suffix:
    /// <list type="bullet">
    /// <item><c>1.111111</c> could instead show <c>1.111~x</c>.</item>
    /// <item><c>0.00001234567</c> would normally show <c>1.234567e-05</c>, but with this it instead shows <c>0~x</c>
    /// because very small values generally aren't useful.</item>
    /// <item><c>99999999</c> shows <c>1e+08x</c> because very large values are already approximations and trying to
    /// format them correctly would be very difficult.</item>
    /// </list>
    /// This system only affects the display value. Once you select a field, it shows its actual value.
    /// </example>
    /// https://kybernetik.com.au/animancer/api/Animancer.Editor/CompactUnitConversionCache
    /// 
    public sealed partial class CompactUnitConversionCache
    {
        /// <summary>Values smaller than this become <c>0~</c> or <c>-0~</c>.</summary>
        public const float SmallExponentialThreshold = 0.0001f;

        /// <summary>Values larger than this can't be approximated.</summary>
        public const float LargeExponentialThreshold = 9999999f;

        /************************************************************************************************************************/

        /// <summary>The character(s) used to separate decimal values in the current OS language.</summary>
        public static string _DecimalSeparator;

#if UNITY_EDITOR
        /// <summary>Strings mapped to the width they would require for a <see cref="UnityEditor.EditorStyles.numberField"/>.</summary>
#endif
        private static ConversionCache<string, float> _WidthCache;

#if UNITY_EDITOR
        /// <summary>Padding around the text in a <see cref="UnityEditor.EditorStyles.numberField"/>.</summary>
#endif
        public static float _FieldPadding;

#if UNITY_EDITOR
        /// <summary>The pixel width of the <c>~</c> character when drawn by <see cref="UnityEditor.EditorStyles.numberField"/>.</summary>
#endif
        public static float _ApproximateSymbolWidth;

        /// <summary>The <see cref="Suffix"/> with a <c>~</c> before it to indicate an approximation.</summary>
        public readonly string ApproximateSuffix;

        /// <summary>The value <c>-0</c> with the <see cref="ApproximateSuffix"/>.</summary>
        public readonly string ConvertedSmallNegative;

        /// <summary>The value <c>0</c> with the <see cref="ApproximateSuffix"/>.</summary>
        public readonly string ConvertedSmallPositive;

        /// <summary>The value <c>0</c> with the <see cref="Suffix"/>.</summary>
        public readonly string ConvertedZero;
        /************************************************************************************************************************/

        /// <summary>The suffix added to the end of each value.</summary>
        public readonly string Suffix;

#if UNITY_EDITOR
        /// <summary>The pixel width of the <see cref="Suffix"/> when drawn by <see cref="UnityEditor.EditorStyles.numberField"/>.</summary>
#endif
        public float _SuffixWidth;

        /// <summary>The caches for each character count.</summary>
        /// <remarks><c>this[x]</c> is a cache that outputs strings with <c>x</c> characters.</remarks>
        private List<ConversionCache<double, string>> CachesDouble = new List<ConversionCache<double, string>>();

        /// <summary>The caches for each character count.</summary>
        /// <remarks><c>this[x]</c> is a cache that outputs strings with <c>x</c> characters.</remarks>
        private List<ConversionCache<int, string>> CachesInt = new List<ConversionCache<int, string>>();


        /************************************************************************************************************************/
        /// <summary>Creates a new <see cref="CompactUnitConversionCache"/>.</summary>
        public CompactUnitConversionCache(string suffix)
        {
            Suffix                 = suffix;
            ApproximateSuffix      = "~" + suffix;
            ConvertedZero          = "0" + Suffix;
            ConvertedSmallPositive = "0" + ApproximateSuffix;
            ConvertedSmallNegative = "-0" + ApproximateSuffix;
        }
    }
}