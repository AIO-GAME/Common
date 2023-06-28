using System.ComponentModel;
using System.Diagnostics;

namespace AIO
{
    /// <summary>
    /// 单位
    /// </summary>
    public static class UNIT
    {
        /// <summary>
        /// [单位:度][Unit:º]
        /// </summary>
        [Conditional(Strings.UnityEditor)]
        [Description("度")]
        public sealed class DegreesAttribute : UnitsAttribute
        {
            /// <inheritdoc />
            public DegreesAttribute() : base(" º")
            {
            }
        }

        /// <summary>
        /// [单位:度/秒][Unit:º/s]
        /// </summary>
        [Conditional(Strings.UnityEditor)]
        [Description("度/秒")]
        public sealed class DegreesPerSecondAttribute : UnitsAttribute
        {
            /// <inheritdoc />
            public DegreesPerSecondAttribute() : base(" º/s")
            {
            }
        }

        /// <summary>
        /// [单位:米][Unit:m]
        /// </summary>
        [Conditional(Strings.UnityEditor)]
        [Description("米")]
        public sealed class MetersAttribute : UnitsAttribute
        {
            /// <inheritdoc />
            public MetersAttribute() : base(" m")
            {
            }
        }

        /// <summary>
        /// [单位:米/秒][Unit:m/s]
        /// </summary>
        [Conditional(Strings.UnityEditor)]
        [Description("米/秒")]
        public sealed class MetersPerSecondAttribute : UnitsAttribute
        {
            /// <inheritdoc />
            public MetersPerSecondAttribute() : base(" m/s")
            {
            }
        }

        /// <summary>
        /// [单位:米/毫秒][Unit:m/s²]
        /// </summary>
        [Conditional(Strings.UnityEditor)]
        [Description("米/毫秒")]
        public sealed class MetersPerSecondPerSecondAttribute : UnitsAttribute
        {
            /// <inheritdoc />
            public MetersPerSecondPerSecondAttribute() : base(" m/s²")
            {
            }
        }

        /// <summary>
        /// [单位:乘数][Unit:x]
        /// </summary>
        [Conditional(Strings.UnityEditor)]
        [Description("乘数")]
        public sealed class MultiplierAttribute : UnitsAttribute
        {
            /// <inheritdoc />
            public MultiplierAttribute() : base(" x")
            {
            }
        }

        /// <summary>
        /// [单位:秒][Unit:s]
        /// </summary>
        [Conditional(Strings.UnityEditor)]
        [Description("秒")]
        public sealed class SecondsAttribute : UnitsAttribute
        {
            /// <inheritdoc />
            public SecondsAttribute() : base(" s")
            {
            }
        }

        /// <summary>
        /// [单位:千米][Unit:km]
        /// </summary>
        [Conditional(Strings.UnityEditor)]
        [Description("千米")]
        public sealed class KilometersAttribute : UnitsAttribute
        {
            /// <inheritdoc />
            public KilometersAttribute() : base(" km")
            {
            }
        }


        /// <summary>
        /// [单位:米 厘米换算][Unit:m cm]
        /// </summary>
        [Conditional(Strings.UnityEditor)]
        [Description("米 厘米 换算")]
        public sealed class MetersAndCentimetersAttribute : UnitsAttribute
        {
            /// <inheritdoc />
            public MetersAndCentimetersAttribute() : base(new float[] { 1, 100 }, new string[] { " m", " cm" })
            {
            }
        }
    }
}