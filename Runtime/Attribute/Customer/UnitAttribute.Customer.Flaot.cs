#region

using System.ComponentModel;
using System.Diagnostics;

#endregion

namespace AIO
{
    /// <summary>
    /// 单位
    /// </summary>
    public static class UNIT
    {
        #region Nested type: DegreesAttribute

        /// <summary>
        /// [单位:度][Unit:º]
        /// </summary>
        [Conditional(Strings.UnityEditor), Description("度")]
        public sealed class DegreesAttribute : UnitAttribute
        {
            /// <inheritdoc />
            public DegreesAttribute() : base(" º") { }
        }

        #endregion

        #region Nested type: DegreesPerSecondAttribute

        /// <summary>
        /// [单位:度/秒][Unit:º/s]
        /// </summary>
        [Conditional(Strings.UnityEditor), Description("度/秒")]
        public sealed class DegreesPerSecondAttribute : UnitAttribute
        {
            /// <inheritdoc />
            public DegreesPerSecondAttribute() : base(" º/s") { }
        }

        #endregion

        #region Nested type: MetersPerSecondAttribute

        /// <summary>
        /// [单位:米/秒][Unit:m/s]
        /// </summary>
        [Conditional(Strings.UnityEditor), Description("米/秒")]
        public sealed class MetersPerSecondAttribute : UnitAttribute
        {
            /// <inheritdoc />
            public MetersPerSecondAttribute() : base(" m/s") { }
        }

        #endregion

        #region Nested type: MetersPerSecondPerSecondAttribute

        /// <summary>
        /// [单位:米/毫秒][Unit:m/s²]
        /// </summary>
        [Conditional(Strings.UnityEditor), Description("米/毫秒")]
        public sealed class MetersPerSecondPerSecondAttribute : UnitAttribute
        {
            /// <inheritdoc />
            public MetersPerSecondPerSecondAttribute() : base(" m/s²") { }
        }

        #endregion

        #region Nested type: MultiplierAttribute

        /// <summary>
        /// [单位:乘数][Unit:x]
        /// </summary>
        [Conditional(Strings.UnityEditor), Description("乘数")]
        public sealed class MultiplierAttribute : UnitAttribute
        {
            /// <inheritdoc />
            public MultiplierAttribute() : base(" x") { }
        }

        #endregion
    }
}