#region

using System;
using UnityEngine;

#endregion

namespace AIO
{
    /// <summary>
    /// [Editor-Only]
    /// Various GUI utilities used throughout Animancer.
    /// </summary>
    internal static partial class UnitGUI
    {
        // The "g" format gives a lower case 'e' for exponentials instead of upper case 'E'.
        private static readonly ConversionCache<float, string>
            FloatToString = new ConversionCache<float, string>(value => $"{value:g}");

        private static readonly ConversionCache<double, string>
            DoubleToString = new ConversionCache<double, string>(value => $"{value:g}");

        private static readonly ConversionCache<int, string>
            IntToString = new ConversionCache<int, string>(value => $"{value:g}");

        /// <summary>[Animancer Extension]
        /// Calls <see cref="float.ToString(string)"/> using <c>"g"</c> as the format and caches the result.
        /// </summary>
        public static string ToStringCached(this float value) { return FloatToString.Convert(value); }

        public static string ToStringCached(this double value) { return DoubleToString.Convert(value); }

        public static string ToStringCached(this int value) { return IntToString.Convert(value); }

        /// <summary>
        /// Returns true and uses the current event if it is <see cref="EventType.MouseUp"/> inside the specified
        /// `area`.
        /// </summary>
        public static bool TryUseClickEvent(Rect area, int button = -1)
        {
            var currentEvent = Event.current;
            if (currentEvent.type == EventType.MouseUp &&
                (button < 0 || currentEvent.button == button) &&
                area.Contains(currentEvent.mousePosition))
            {
                GUI.changed = true;
                currentEvent.Use();

                if (currentEvent.button == 2)
                    Deselect();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true and uses the current event if it is <see cref="EventType.MouseUp"/> inside the last GUI Layout
        /// <see cref="Rect"/> that was drawn.
        /// </summary>
        public static bool TryUseClickEventInLastRect(int button = -1) { return TryUseClickEvent(GUILayoutUtility.GetLastRect(), button); }

        /// <summary>Deselects any selected IMGUI control.</summary>
        public static void Deselect() { GUIUtility.keyboardControl = 0; }

        #region Standard Values

        /// <summary>
        /// The highlight color used for fields showing a warning.
        /// </summary>
        public static readonly Color WarningFieldColor = new Color(1, 0.9f, 0.6f);

        /// <summary>
        /// The highlight color used for fields showing an error.
        /// </summary>
        public static readonly Color ErrorFieldColor = new Color(1, 0.6f, 0.6f);

        /// <summary>
        /// <see cref="GUILayout.ExpandWidth"/> set to false.
        /// </summary>
        public static readonly GUILayoutOption[] DontExpandWidth = { GUILayout.ExpandWidth(false) };

        private static float _IndentSize = -1;

        private static float _ToggleWidth = -1;

        /// <summary>The width of a standard <see cref="GUISkin.toggle"/> with no label.</summary>
        public static float ToggleWidth
        {
            get
            {
                if (Math.Abs(_ToggleWidth - -1) < 0)
                {
                    GUI.skin.toggle.CalcMinMaxWidth(GUIContent.none, out _, out var maxWidth);
                    _ToggleWidth = Mathf.Ceil(maxWidth);
                }

                return _ToggleWidth;
            }
        }

        /// <summary>The color of the standard label text.</summary>
        public static Color TextColor => GUI.skin.label.normal.textColor;

        private static GUIStyle _MiniButton;

        #endregion

        #region Layout

        /// <summary>Indicates where <see cref="LayoutSingleLineRect"/> should add the <see cref="StandardSpacing"/>.</summary>
        public enum SpacingMode
        {
            /// <summary>No extra space.</summary>
            None,

            /// <summary>Add extra space before the new area.</summary>
            Before,

            /// <summary>Add extra space after the new area.</summary>
            After,

            /// <summary>Add extra space before and after the new area.</summary>
            BeforeAndAfter
        }

        /// <summary>
        /// Subtracts the `width` from the left side of the `area` and returns a new <see cref="Rect"/> occupying the
        /// removed section.
        /// </summary>
        public static Rect StealFromLeft(ref Rect area, float width, float padding = 0)
        {
            var newRect = new Rect(area.x, area.y, width, area.height);
            area.xMin += width + padding;
            return newRect;
        }

        /// <summary>
        /// Subtracts the `width` from the right side of the `area` and returns a new <see cref="Rect"/> occupying the
        /// removed section.
        /// </summary>
        public static Rect StealFromRight(ref Rect area, float width, float padding = 0)
        {
            area.width -= width + padding;
            return new Rect(area.xMax + padding, area.y, width, area.height);
        }

        /// <summary>
        /// Divides the given `area` such that the fields associated with both labels will have equal space
        /// remaining after the labels themselves.
        /// </summary>
        public static void SplitHorizontally(Rect      area,
                                             string    label0,
                                             string    label1,
                                             out float width0,
                                             out float width1,
                                             out Rect  rect0,
                                             out Rect  rect1)
        {
            width0 = CalculateLabelWidth(label0);
            width1 = CalculateLabelWidth(label1);

            const float Padding = 1;

            rect0 = rect1 = area;

            var remainingWidth = area.width - width0 - width1 - Padding;
            rect0.width = width0 + remainingWidth * 0.5f;
            rect1.xMin  = rect0.xMax + Padding;
        }

        /// <summary>
        /// Creates a <see cref="ConversionCache{TKey, TValue}"/> for calculating the GUI width occupied by text using the
        /// specified `style`.
        /// </summary>
        public static ConversionCache<string, float> CreateWidthCache(GUIStyle style)
        {
            return new ConversionCache<string, float>(text =>
            {
                var content = new GUIContent(text);
                style.CalcMinMaxWidth(content, out _, out var maxWidth);
                var width = Mathf.Ceil(maxWidth);
                content.text = null;
                return width;
            });
        }

        private static ConversionCache<string, float> _LabelWidthCache;

        /// <summary>
        /// Calls <see cref="GUIStyle.CalcMinMaxWidth"/> using <see cref="GUISkin.label"/> and returns the max
        /// width. The result is cached for efficient reuse.
        /// </summary>
        public static float CalculateLabelWidth(string text)
        {
            if (_LabelWidthCache == null)
                _LabelWidthCache = CreateWidthCache(GUI.skin.label);

            return _LabelWidthCache.Convert(text);
        }

        #endregion

        #region Labels

        private static GUIStyle _WeightLabelStyle;
        private static float    _WeightLabelWidth = -1;

        /// <summary>
        /// Draws a label showing the `weight` aligned to the right side of the `area` and reduces its
        /// <see cref="Rect.width"/> to remove that label from its area.
        /// </summary>
        public static void DoWeightLabel(ref Rect area, float weight)
        {
            var label = WeightToShortString(weight, out var isExact);

            if (_WeightLabelStyle == null)
                _WeightLabelStyle = new GUIStyle(GUI.skin.label);

            if (_WeightLabelWidth < 0)
            {
                _WeightLabelStyle.fontStyle = FontStyle.Italic;

                var content = new GUIContent("0.0");
                _WeightLabelStyle.CalcMinMaxWidth(content, out _, out var maxWidth);
                _WeightLabelWidth = Mathf.Ceil(maxWidth);
                content.text      = null;
            }

            _WeightLabelStyle.normal.textColor = Color.Lerp(Color.grey, TextColor, weight);
            _WeightLabelStyle.fontStyle        = isExact ? FontStyle.Normal : FontStyle.Italic;

            var weightArea = StealFromRight(ref area, _WeightLabelWidth);

            GUI.Label(weightArea, label, _WeightLabelStyle);
        }

        private static ConversionCache<float, string> _ShortWeightCache;

        /// <summary>Returns a string which approximates the `weight` into no more than 3 digits.</summary>
        private static string WeightToShortString(float weight, out bool isExact)
        {
            isExact = true;

            switch (weight)
            {
                case 0:
                    return "0.0";
                case 1:
                    return "1.0";
            }

            isExact = false;

            if (weight >= -0.5f && weight < 0.05f)
                return "~0.";
            if (weight >= 0.95f && weight < 1.05f)
                return "~1.";

            if (weight <= -99.5f)
                return "-??";
            if (weight >= 999.5f)
                return "???";

            if (_ShortWeightCache == null)
                _ShortWeightCache = new ConversionCache<float, string>(value =>
                {
                    if (value < -9.5f) return $"{value:F0}";
                    if (value < -0.5f) return $"{value:F0}.";
                    if (value < 9.5f) return $"{value:F1}";
                    if (value < 99.5f) return $"{value:F0}.";
                    return $"{value:F0}";
                });

            var rounded = weight > 0 ? Mathf.Floor(weight * 10) : Mathf.Ceil(weight * 10);
            isExact = Mathf.Approximately(weight * 10, rounded);

            return _ShortWeightCache.Convert(weight);
        }

        private static ConversionCache<string, string> _NarrowTextCache;

        #endregion
    }
}